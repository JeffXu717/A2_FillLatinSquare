using System;
using System.Collections.Generic;
using System.Text;

namespace FillLatinSquare
{
    class GRASPSolver
    {
        public LatinSquareSolver latinSquareSolver;
        private List<int> solution = new List<int>();
        private List<int> bestSolution = new List<int>();
        private int bestConflictVal;
        private int curConflictVal;
        private List<Move> candidateList = new List<Move>();
        private List<Move> rCList = new List<Move>();
        private int maxIterGRASP = 2000;
        private int maxIterLS = 2000;
        private int maxIterNoImprovement = 500;
        private Random rnd = new Random();
        private bool bestOrFirst = true;

        //params about reactive alpha
        private bool reactiveAlphaFlag = true;
        private int selectedAlphaIndex = 0;
        private int deltaOfReactiveAlpha = 10;
        private List<int> nList = new List<int>();
        private List<int> objectValSumList = new List<int>();
        private List<float> qList = new List<float>();
        private List<float> pList = new List<float>();

        //module about PR
        private bool pRFlag = true;
        public PRSolver pRSolver;

        //PR evolution
        private int evNum = 8;

        public delegate void TaskFinished();
        public TaskFinished GRCFinishedCallBack;
        public TaskFinished GRASPFinishedCallBack;

        public List<int> Solution
        {
            get
            {
                return solution;
            }
        }

        public List<int> BestSolution
        {
            get
            {
                return bestSolution;
            }
        }

        public bool ReactiveAlphaFlag
        {
            get
            {
                return reactiveAlphaFlag;
            }
            set
            {
                reactiveAlphaFlag = value;
            }
        }

        public bool PRFlag
        {
            get
            {
                return pRFlag;
            }
            set
            {
                pRFlag = value;
            }
        }

        public int GRASPIter { get; set; }
        public int PREvIter { get; set; }
        public double ElapsedT { get; private set; }
        public int MaxIterGRASP
        {
            get
            {
                return maxIterGRASP;
            }
            set
            {
                maxIterGRASP = value;
            }
        }

        public int MaxIterLS
        {
            get
            {
                return maxIterLS;
            }
            set
            {
                maxIterLS = value;
            }
        }

        public int MaxIterNoImprovement
        {
            get
            {
                return maxIterNoImprovement;
            }
            set
            {
                maxIterNoImprovement = value;
            }
        }
        public float FixedAlpha { get; set; } = 0.4f;
        public int BlockIterReactiveAlpha { get; set; } = 200;
        public List<float> ReactiveAlphaList { get; set; } = new List<float>() { 0.2f, 0.5f, 0.8f };
        public bool EvPRFlag { get; set; } = true;

        public GRASPSolver(LatinSquareSolver latinSquareSolver)
        {
            this.latinSquareSolver = latinSquareSolver;
            this.pRSolver = new PRSolver(this);
        }

        public void GRASP()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            if (pRFlag)
            {
                if (EvPRFlag)
                {
                    GRASP_EvPR();
                }
                else
                {
                    GRASP_PR();
                }
            }
            else
            {
                GRASP_Basic();
            }
            stopwatch.Stop();
            ElapsedT = stopwatch.Elapsed.TotalSeconds;
            if (GRASPFinishedCallBack != null) GRASPFinishedCallBack();
        }


        public void GRASP_Basic()
        {
            //reactive option
            if (reactiveAlphaFlag)
            {
                initReactiveAlphaParams();
            }

            GRASPIter = 0;
            PREvIter = 0;
            bestSolution.Clear();
            while (GRASPIter < maxIterGRASP)
            {
                greedyRandomizedConstruction();
                lS(solution);
                UpdateBestSolution(BestSolution, ref bestConflictVal, solution, ref curConflictVal);

                // if best soulition found then quit
                if (bestConflictVal == 0)
                {
                    break;
                }

                // update reactive alphas if needed
                if (reactiveAlphaFlag)
                {
                    objectValSumList[selectedAlphaIndex] += curConflictVal;
                    nList[selectedAlphaIndex] += 1;

                    if (GRASPIter % BlockIterReactiveAlpha == 0)
                    {
                        updateReactiveAlpha();
                    }
                }
                GRASPIter++;
            }
        }

        public void GRASP_PR()
        {
            //reactive option
            if (reactiveAlphaFlag)
            {
                initReactiveAlphaParams();
            }

            pRSolver.InitPRParams();
            GRASPIter = 0;
            PREvIter = 0;
            bestSolution.Clear();
            while (GRASPIter < maxIterGRASP)
            {
                greedyRandomizedConstruction();
                lS(solution);
                UpdateBestSolution(bestSolution, ref bestConflictVal, solution, ref curConflictVal);

                // if best soulition found then quit
                if (bestConflictVal == 0)
                {
                    break;
                }

                // update reactive alphas if needed
                if (reactiveAlphaFlag)
                {
                    objectValSumList[selectedAlphaIndex] += curConflictVal;
                    nList[selectedAlphaIndex] += 1;

                    if (GRASPIter % BlockIterReactiveAlpha == 0)
                    {
                        updateReactiveAlpha();
                    }
                }

                if (pRSolver.ESList.Count < pRSolver.SizeOfES)
                {
                    //copy best solution found by GRC and LS to Elite list
                    pRSolver.AddSToESList(bestSolution, bestConflictVal);
                }
                else
                {
                    bool doesSolutionSame = !pRSolver.PRBackForwardWithRndES(bestSolution, bestConflictVal); // Path relinking in two directions
                    if (!doesSolutionSame)
                    {
                        lS(pRSolver.BestFoundSolution); // LS after PR
                        pRSolver.UpdateES(pRSolver.BestFoundSolution);
                        if (pRSolver.ESList[0].ObjectVal == 0)
                        {
                            break;
                        }
                    }
                }
                GRASPIter++;
            }
            if (pRSolver.FirstES != null)
            {
                if (bestConflictVal > pRSolver.FirstES.ObjectVal)
                {
                    bestSolution = pRSolver.FirstES.Solution;
                    bestConflictVal = pRSolver.FirstES.ObjectVal;
                }
            }
        }

        private void GRASP_EvPR()
        {
            //reactive option
            if (reactiveAlphaFlag)
            {
                initReactiveAlphaParams();
            }

            pRSolver.InitPRParams();
            GRASPIter = 0;
            PREvIter = 0;
            bestSolution.Clear();

            // First fill the ES list
            while (GRASPIter < pRSolver.SizeOfES)
            {
                greedyRandomizedConstruction();
                lS(solution);
                UpdateBestSolution(bestSolution, ref bestConflictVal, solution, ref curConflictVal);

                // update reactive alphas if needed
                if (reactiveAlphaFlag)
                {
                    objectValSumList[selectedAlphaIndex] += curConflictVal;
                    nList[selectedAlphaIndex] += 1;

                    if (GRASPIter % BlockIterReactiveAlpha == 0)
                    {
                        updateReactiveAlpha();
                    }
                }

                //copy best solution found by GRC and LS to Elite list
                pRSolver.AddSToESList(bestSolution, bestConflictVal);
                GRASPIter++;
            }

            var remainIter = maxIterGRASP - pRSolver.SizeOfES;
            var iterEv = remainIter / evNum;
            while (GRASPIter < maxIterGRASP)
            {
                greedyRandomizedConstruction();
                lS(solution);
                UpdateBestSolution(bestSolution, ref bestConflictVal, solution, ref curConflictVal);

                // if best soulition found then quit
                if (bestConflictVal == 0)
                {
                    break;
                }

                // update reactive alphas if needed
                if (reactiveAlphaFlag)
                {
                    objectValSumList[selectedAlphaIndex] += curConflictVal;
                    nList[selectedAlphaIndex] += 1;

                    if (GRASPIter % BlockIterReactiveAlpha == 0)
                    {
                        updateReactiveAlpha();
                    }
                }

                bool doesSolutionSame = !pRSolver.PRBackForwardWithRndES(bestSolution, bestConflictVal); // Path relinking in two directions
                if (!doesSolutionSame)
                {
                    lS(pRSolver.BestFoundSolution); // LS after PR
                    var newSIndexInESList = pRSolver.UpdateES(pRSolver.BestFoundSolution);
                    pRSolver.NewSIndexInESList = newSIndexInESList >= 0 ? newSIndexInESList : pRSolver.NewSIndexInESList; // Always record the last index
                    if (pRSolver.ESList[0].ObjectVal == 0)
                    {
                        break;
                    }
                }

                if (GRASPIter % iterEv == 0)
                {
                    if (pRSolver.NewSIndexInESList >= 0)
                    {
                        //Do evolution
                        pRSolver.EvolutionEveryPair();
                        lS(pRSolver.BestEvSolution);
                        pRSolver.NewSIndexInESList =  pRSolver.UpdateES(pRSolver.BestEvSolution);
                        while (pRSolver.NewSIndexInESList >= 0)
                        {
                            pRSolver.EvolutionWithOthersInESList();
                            lS(pRSolver.BestEvSolution);
                            pRSolver.NewSIndexInESList =  pRSolver.UpdateES(pRSolver.BestEvSolution);
                        }
                        PREvIter++;
                        if (pRSolver.ESList[0].ObjectVal == 0)
                        {
                            break;
                        }
                    }
                }

                GRASPIter++;
            }
            if (pRSolver.FirstES != null)
            {
                if (bestConflictVal > pRSolver.FirstES.ObjectVal)
                {
                    bestSolution = pRSolver.FirstES.Solution;
                    bestConflictVal = pRSolver.FirstES.ObjectVal;
                }
            }
        }
        public void UpdateBestSolution(List<int> bestS, ref int bestCVal, List<int> curS, ref int curCVal)
        {
            curCVal = latinSquareSolver.CalTotalConfilit(curS);
            if (bestS.Count == 0)
            {
                curS.CopyTo(bestS);
                bestCVal = curCVal;
            }
            else
            {
                if (curCVal < bestCVal)
                {
                    for (int i = 0; i < curS.Count; i++)
                    {
                        bestS[i] = curS[i];
                    }
                    bestCVal = curCVal;
                }
            }
        }

        private void lS(List<int> s)
        {
            if (bestOrFirst)
            {
                ls_BestImprovement(s);
            }
            else
            {
                lS_FirstImprovement(s);
            }
        }

        private void  lS_FirstImprovement(List<int> s)
        {
            int iterLS = 0;
            int iterNoImprovement = 0;
            while (iterNoImprovement < maxIterNoImprovement && iterLS < maxIterLS)
            {
                int index = rnd.Next(latinSquareSolver.Length * latinSquareSolver.Length);
                while (latinSquareSolver.InitSquare[index] != 0)
                {
                    index = (index + 1) % s.Count;
                }
                int val = rnd.Next(latinSquareSolver.Length) + 1;
                while (s[index] == val)
                {
                    val = rnd.Next(latinSquareSolver.Length) + 1;
                }
                if (latinSquareSolver.IfAlterBetter(s, index, val))
                {
                    s[index] = val;
                }
                else
                {
                    iterNoImprovement++;
                }
                iterLS++;
            }
        }

        private void ls_BestImprovement(List<int> s)
        {
            int iterLS = 0;
            int iterNoImprovement = 0;
            int maxICost = 0;
            int minICost = int.MaxValue;
            initLSNeighbourhood(s, ref maxICost, ref minICost);
            while (neighbourMoveList.Count > 0 && minICost <= 0 && iterNoImprovement < maxIterNoImprovement && iterLS < maxIterLS)
            {
                improvableMoveList.Clear();
                neighbourMoveList.ForEach(move =>
                {
                    if (move.iCost == minICost)
                    {
                        improvableMoveList.Add(move);
                    }
                });
                var move = improvableMoveList[rnd.Next(improvableMoveList.Count)];
                s[move.index] = move.val;
                updateLSNeighbourhoodList(s, move, ref maxICost, ref minICost);
                if (move.iCost == 0)
                {
                    iterNoImprovement++;
                }
                iterLS++;
            }
        }

        private List<Move> neighbourMoveList = new List<Move>();
        private List<Move> improvableMoveList = new List<Move>();
        private void initLSNeighbourhood(List<int> s, ref int maxICost, ref int minICost)
        {
            var iniS = latinSquareSolver.InitSquare;
            neighbourMoveList.Clear();
            maxICost = 0;
            minICost = int.MaxValue;
            for (int i = 0; i < s.Count; i++)
            {
                if (iniS[i] == 0)
                {
                    for (int j = 1; j <= latinSquareSolver.Length; j++)
                    {
                        var iCost = latinSquareSolver.CountSwapConflict(s, i, j);
                        if (iCost > maxICost)
                        {
                            maxICost = iCost;
                        }
                        if (iCost < minICost)
                        {
                            minICost = iCost;
                        }
                        neighbourMoveList.Add(new Move(i, j, iCost));
                    }
                }
            }
        }
        private void updateLSNeighbourhoodList(List<int> s, Move move, ref int maxICost, ref int minICost)
        {
            neighbourMoveList.RemoveAll(iMove => iMove.index == move.index); //same index move should be removed
            for (int i = 0; i < neighbourMoveList.Count; i++)
            {
                var iMove = neighbourMoveList[i];
                if (iMove.val == move.val)
                {
                    if (iMove.index / latinSquareSolver.Length == move.index / latinSquareSolver.Length ||
                        iMove.index % latinSquareSolver.Length == move.index % latinSquareSolver.Length)
                    {
                        neighbourMoveList[i] = new Move(iMove.index, iMove.val, latinSquareSolver.CountSwapConflict(s, iMove.index, iMove.val));
                    }
                }
            }
            //update maxICost and minICost
            maxICost = 0;
            minICost = int.MaxValue;
            for (int i = 0; i < neighbourMoveList.Count; i++)
            {
                var sCost = neighbourMoveList[i].iCost;
                if (sCost > maxICost)
                {
                    maxICost = sCost;
                }
                if (sCost < minICost)
                {
                    minICost = sCost;
                }
            }
        }


        public void GreedyRandomizedConstruction()
        {
            greedyRandomizedConstruction();
            if (GRCFinishedCallBack != null) GRCFinishedCallBack();
        }

        private void greedyRandomizedConstruction()
        {
            solution.Clear();
            latinSquareSolver.InitSquare.CopyTo(solution);
            int maxICost = 0, minICost = int.MaxValue;
            float alpha = reactiveAlphaFlag ? calReactiveAlpha() : FixedAlpha;
            initCandidateList(ref maxICost, ref minICost);
            while (candidateList.Count > 0)
            {
                rCList.Clear();
                candidateList.ForEach(move =>
                {
                    if (move.iCost <= minICost + alpha * (maxICost - minICost))
                    {
                        rCList.Add(move);
                    }
                });
                var move = rCList[rnd.Next(rCList.Count)];
                solution[move.index] = move.val;
                updateCandidateList(move, ref maxICost, ref minICost);
            }
        }

        private void initCandidateList(ref int maxICost, ref int minICost)
        {
            candidateList.Clear();
            maxICost = 0;
            minICost = int.MaxValue;
            for (int i = 0; i < solution.Count; i++)
            {
                if (solution[i] == 0)
                {
                    for (int j = 1; j <= latinSquareSolver.Length; j++)
                    {
                        var iCost = calIncrementCost(solution, i, j);
                        if (iCost > maxICost)
                        {
                            maxICost = iCost;
                        }
                        if (iCost < minICost)
                        {
                            minICost = iCost;
                        }
                        candidateList.Add(new Move(i, j, iCost));
                    }
                }
            }
        }

        private void updateCandidateList(Move move, ref int maxICost, ref int minICost)
        {
            candidateList.RemoveAll(cMove => cMove.index == move.index); //same index move should be removed
            for (int i = 0; i < candidateList.Count; i++)
            {
                var cMove = candidateList[i];
                if (cMove.val == move.val)               
                {
                    if (cMove.index / latinSquareSolver.Length == move.index / latinSquareSolver.Length ||
                        cMove.index % latinSquareSolver.Length == move.index % latinSquareSolver.Length)
                    {
                        candidateList[i] = new Move(cMove.index, cMove.val, calIncrementCost(solution, cMove.index, cMove.val));
                    }
                }
            }
            //update maxICost and minICost
            maxICost = 0;
            minICost = int.MaxValue;
            for (int i = 0; i < candidateList.Count; i++)
            {
                var iCost = candidateList[i].iCost;
                if (iCost > maxICost)
                {
                    maxICost = iCost;
                }
                if (iCost < minICost)
                {
                    minICost = iCost;
                }
            }
        }

        private int calIncrementCost(List<int> square, int index, int val)
        {
            return latinSquareSolver.CountIncrementConflict(square, index, val);
        }

        private float calReactiveAlpha()
        {
            var randomVal = rnd.NextDouble();
            float p = 0;
            int i = 0;
            for (; i < pList.Count - 1; i++) //in case of calculation error, the last alpha (index = Count - 1) is selected defaultly
            {
                p += pList[i];
                if (randomVal < p)
                {
                    break;
                }
            }
            selectedAlphaIndex = i;
            return ReactiveAlphaList[i];
        }
        private void initReactiveAlphaParams()
        {
            objectValSumList.Clear();
            nList.Clear();
            qList.Clear();
            pList.Clear();
            ReactiveAlphaList.ForEach(e =>
            {
                objectValSumList.Add(0);
                nList.Add(0);
                qList.Add(0);
                pList.Add(1f / ReactiveAlphaList.Count);
            });
        }
        private void updateReactiveAlpha()
        {
            foreach (var n in nList)
            {
                // every alpha should be selected at least once
                if (n == 0)
                {
                    return;
                }
            }

            float qSum = 0;
            for (int i = 0; i < ReactiveAlphaList.Count; i++)
            {
                var Ai = (float) objectValSumList[i] / nList[i];
                qList[i] = (float) Math.Pow(bestConflictVal / Ai, deltaOfReactiveAlpha);
                qSum += qList[i];
            }

            for (int i = 0; i < ReactiveAlphaList.Count; i++)
            {
                pList[i] = qList[i] / qSum;
            }
        }

        public struct Move
        {
            //index of a blank
            public int index;
            //value will be filled
            public int val;
            //increment
            public int iCost;

            public Move(int index, int val, int iCost)
            {
                this.index = index;
                this.val = val;
                this.iCost = iCost;
            }

            public override string ToString()
            {
                return index + " val:" + val + " iCost:" + iCost;
            }
        }
    }
}
