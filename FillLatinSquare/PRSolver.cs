using System;
using System.Collections.Generic;
using System.Text;

namespace FillLatinSquare
{
    class PRSolver
    {
        private int sizeOfES = 10;
        private List<SolutionAndObjectVal> eSList = new List<SolutionAndObjectVal>();
        private List<GRASPSolver.Move> candidateSwapList = new List<GRASPSolver.Move>();
        private List<GRASPSolver.Move> rCSwapList = new List<GRASPSolver.Move>();
        private Random rnd = new Random();

        private List<int> tempSolution = new List<int>();
        private List<int> bestFoundFowardSolution = new List<int>();
        private List<int> bestFoundBackSolution = new List<int>();
        private List<int> bestFoundSolution; //this is just a reference, not instance
        private int bestFoundConflictVal;

        private GRASPSolver gRASPSolver;

        //params about evolution
        private List<int> bestEvSolution = new List<int>();
        private int bestEvConflictVal;
        private float bestEvDistance;

        public List<SolutionAndObjectVal> ESList
        { 
            get
            {
                return eSList;
            } 
        }

        public SolutionAndObjectVal FirstES
        {
            get
            {
                if (eSList.Count > 0)
                {
                    return eSList[0];
                }
                return null;
            }
        }

        public int SizeOfES {
            get
            {
                return sizeOfES;
            }
            set
            {
                sizeOfES = value;
            }
        }

        public List<int> BestFoundSolution
        {
            get
            {
                return bestFoundSolution;
            }
        }

        public List<int> BestEvSolution
        {
            get
            {
                return bestEvSolution;
            }
        }

        public float TruncatedLengthRate { get; set; } = 0.2f;
        public float FixedAlpha { get; set; } = 0.3f;
        public float DistanceRateThreshold { get; set; } = 0.3f;
        public int EvNum { get; set; } = 8;

        public PRSolver(GRASPSolver gRASPSolver)
        {
            this.gRASPSolver = gRASPSolver;
        }

        public int NewSIndexInESList { get; set; } = -1;

        public void InitPRParams()
        {
            eSList.Clear();
        }

        public bool PRBackForwardWithRndES(List<int> initS, int iniSCVal)
        {
            var rndIndex = rnd.Next(sizeOfES);
            return PRBackForward(initS, iniSCVal, eSList[rndIndex].Solution, eSList[rndIndex].ObjectVal);
        }

        public bool PRBackForward(List<int> initS, int iniSCVal, List<int> guidingS, int guidingSCVal)
        {
            bestFoundFowardSolution.Clear();
            bestFoundBackSolution.Clear();
            int bestFoundCValForward = iniSCVal;
            var pRCanBeExecuted =  PR(initS, guidingS, tempSolution, bestFoundFowardSolution, ref bestFoundCValForward);
            if (pRCanBeExecuted)
            {
                int bestFoundCValBack = guidingSCVal;
                PR(guidingS, initS, tempSolution, bestFoundBackSolution, ref bestFoundCValBack);
                bestFoundSolution = bestFoundCValForward < bestFoundCValBack ? bestFoundFowardSolution : bestFoundBackSolution;
                bestFoundConflictVal = bestFoundCValForward < bestFoundCValBack ? bestFoundCValForward : bestFoundCValBack;
            }
            return pRCanBeExecuted;
        }

        /// <summary>
        /// greedy randomized PR
        /// </summary>
        /// <param name="fromS"></param>
        /// <param name="toS"></param>
        /// <param name="tempS"></param>
        /// <param name="bestSFound"></param>
        /// <param name="bestFoundCVal"></param>
        /// <returns>successful?</returns>
        public bool PR(List<int> fromS, List<int> toS, List<int> tempS, List<int> bestSFound, ref int bestFoundCVal)
        {
            tempS.Clear();
            fromS.ForEach(e => {
                tempS.Add(e);
                bestSFound.Add(e);
            });
            int steps = 0;
            int maxSwapCost = 0, minSwapCost = int.MaxValue;
            int tempCVal = int.MaxValue;
            initCandiateMoveList(fromS, toS, ref maxSwapCost, ref minSwapCost);
            if (candidateSwapList.Count == 0) //no swap can be performed
            {
                return false;
            }
            int pathLength = candidateSwapList.Count;
            int truncatedLength = (int) (TruncatedLengthRate * pathLength);
            while (candidateSwapList.Count > 0 || steps < truncatedLength)
            {
                rCSwapList.Clear();
                candidateSwapList.ForEach(swap =>
                {
                    if (swap.iCost <= minSwapCost + FixedAlpha * (maxSwapCost - minSwapCost))
                    {
                        rCSwapList.Add(swap);
                    }
                });
                int rndSelectedSwapIndex = rnd.Next(rCSwapList.Count);
                var swap = rCSwapList[rndSelectedSwapIndex];
                tempS[swap.index] = swap.val;
                gRASPSolver.UpdateBestSolution(bestSFound, ref bestFoundCVal, tempS, ref tempCVal);
                updateCandidateList(tempS, rndSelectedSwapIndex, fromS[swap.index], ref maxSwapCost, ref minSwapCost);
                steps++;
            }
            return true;
        }

        public bool IfDistanceFarEnoughFromESList(List<int> s)
        {
            var initialFillRate = gRASPSolver.latinSquareSolver.FillRate;
            float averageD = distanceToESList(s);
            float thresholdD = (1 - initialFillRate) * s.Count * DistanceRateThreshold;
            return averageD > thresholdD;
        }

        private float distanceToESList(List<int> s)
        {
            int totalD = 0;
            var initS = gRASPSolver.latinSquareSolver.InitSquare;
            foreach (var eS in eSList)
            {
                for (int i = 0; i < eS.Solution.Count; i++)
                {
                    if (initS[i] == 0 && eS.Solution[i] != s[i])
                    {
                        totalD++;
                    }
                }
            }
            return (float)totalD / sizeOfES;
        }

        private int distance(List<int> s1, List<int> s2)
        {
            int d = 0;
            var initS = gRASPSolver.latinSquareSolver.InitSquare;
            var initialFillRate = gRASPSolver.latinSquareSolver.FillRate;
            for (int i = 0; i < initS.Count; i++)
            {
                if (initS[i] == 0 && s1[i] != s2[i])
                {
                    d++;
                }
            }
            return d;
        }

        public int UpdateES(List<int> s)
        {
            var conflictVal = gRASPSolver.latinSquareSolver.CalTotalConfilit(s);
            if (conflictVal < eSList[0].ObjectVal ||
                (conflictVal < eSList[sizeOfES - 1].ObjectVal && IfDistanceFarEnoughFromESList(s)))
            {
                int minD = int.MaxValue;
                int minDIndex = sizeOfES - 1;
                for (int i = sizeOfES - 1; i >= 0; i--)
                {
                    if (conflictVal >= eSList[i].ObjectVal)
                    {
                        break;
                    }
                    var d = distance(s, eSList[i].Solution);
                    if (d < minD)
                    {
                        minD = d;
                        minDIndex = i;
                    }
                }
                var tempSolutionAndVal = eSList[minDIndex];
                eSList.RemoveAt(minDIndex);
                tempSolutionAndVal.UpdateSolutioinAndVal(s, conflictVal);
                return addSToESList(tempSolutionAndVal);
            }
            else
            {
                return -1;
            }
        }

        public void EvolutionEveryPair()
        {
            bestEvConflictVal = int.MaxValue;
            bestEvDistance = 0;
            bestEvSolution.Clear();
            for (int i = 0; i < eSList.Count; i++)
            {
                for (int j = i + 1; j < eSList.Count; j ++)
                {
                    bool pRSuccess = false;
                    pRSuccess = PRBackForward(eSList[i].Solution, eSList[i].ObjectVal, eSList[j].Solution, eSList[j].ObjectVal);
                    if (pRSuccess)
                    {
                        if (bestFoundConflictVal < bestEvConflictVal ||
                            (bestFoundConflictVal == bestEvConflictVal && distanceToESList(BestFoundSolution) > bestEvDistance))
                        {
                            bestEvConflictVal = bestFoundConflictVal;
                            bestFoundSolution.CopyTo(bestEvSolution);
                            bestEvDistance = distanceToESList(bestEvSolution);
                        }
                    }
                }
            }
        }

        public void EvolutionWithOthersInESList()
        {
            bestEvConflictVal = int.MaxValue;
            bestEvDistance = 0;
            bestEvSolution.Clear();
            for (int i = 0; i < eSList.Count; i++)
            {
                if (i != NewSIndexInESList)
                {
                    bool pRSuccess = false;
                    pRSuccess = PRBackForward(eSList[NewSIndexInESList].Solution, eSList[NewSIndexInESList].ObjectVal, eSList[i].Solution, eSList[i].ObjectVal);
                    if (pRSuccess)
                    {
                        if (bestFoundConflictVal < bestEvConflictVal ||
                            (bestFoundConflictVal == bestEvConflictVal && distanceToESList(BestFoundSolution) > bestEvDistance))
                        {
                            bestEvConflictVal = bestFoundConflictVal;
                            bestFoundSolution.CopyTo(bestEvSolution);
                            bestEvDistance = distanceToESList(bestEvSolution);
                        }
                    }
                }
            }
        }

        private int addSToESList(SolutionAndObjectVal sAndVal)
        {
            if (eSList.Count >= sizeOfES)
            {
                return -1;
            }
            int i = 0;
            for (; i < eSList.Count; i++)
            {
                if (sAndVal.ObjectVal < eSList[i].ObjectVal)
                {
                    break;
                }
            }
            eSList.Insert(i, sAndVal);
            return i;
        }

        public void AddSToESList(List<int> s, int conflictVal)
        {
            addSToESList(new SolutionAndObjectVal(s, conflictVal));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempS">temp solution relinking</param>
        /// <param name="selectedSwapIndex">selected index of candidate swap list</param>
        /// <param name="originVal">original val of fromSolution</param>
        /// <param name="maxSwapCost"></param>
        /// <param name="minSwapCost"></param>
        private void updateCandidateList(List<int> tempS, int selectedSwapIndex, int originVal, ref int maxSwapCost, ref int minSwapCost)
        {
            var swap = candidateSwapList[selectedSwapIndex];
            candidateSwapList.RemoveAt(selectedSwapIndex);
            int length = gRASPSolver.latinSquareSolver.Length;
            for (int i = 0; i < candidateSwapList.Count; i++)
            {
                var cSwap = candidateSwapList[i];
                if (cSwap.val == swap.val || cSwap.val == originVal) // update related swap in candidate list
                {
                    if (cSwap.index / length == swap.index / length ||
                        cSwap.index % length == swap.index % length)
                    {
                        candidateSwapList[i] = new GRASPSolver.Move(cSwap.index, cSwap.val, gRASPSolver.latinSquareSolver.CountSwapConflict(tempS, cSwap.index, cSwap.val));
                    }
                }
            }
            //update maxICost and minICost
            maxSwapCost = 0;
            minSwapCost = int.MaxValue;
            for (int i = 0; i < candidateSwapList.Count; i++)
            {
                var iCost = candidateSwapList[i].iCost;
                if (iCost > maxSwapCost)
                {
                    maxSwapCost = iCost;
                }
                if (iCost < minSwapCost)
                {
                    minSwapCost = iCost;
                }
            }
        }

        private void initCandiateMoveList(List<int> fromS, List<int> toS, ref int maxSwapCost, ref int minSwapCost)
        {
            candidateSwapList.Clear();
            maxSwapCost = 0;
            minSwapCost = int.MaxValue;
            var initS = gRASPSolver.latinSquareSolver.InitSquare;
            for (int i = 0; i < initS.Count; i++)
            {
                if (initS[i] == 0 && fromS[i] != toS[i])
                {
                    var swapCost = gRASPSolver.latinSquareSolver.CountSwapConflict(fromS, i, toS[i]);
                    if (swapCost > maxSwapCost)
                    {
                        maxSwapCost = swapCost;
                    }
                    if (swapCost < minSwapCost)
                    {
                        minSwapCost = swapCost;
                    }
                    candidateSwapList.Add(new GRASPSolver.Move(i, toS[i], swapCost));
                }
            }
        }

        public class SolutionAndObjectVal
        {
            public List<int> Solution { get; set; }
            public int ObjectVal { get; set; }

            public SolutionAndObjectVal()
            {
                Solution = new List<int>();
                ObjectVal = -1;
            }

            public SolutionAndObjectVal(List<int> solution, int objectVal)
            {
                Solution = new List<int>();
                solution.CopyTo(Solution);
                ObjectVal = objectVal;
            }

            public void UpdateSolutioinAndVal(List<int> s, int objectVal)
            {
                for (int i = 0; i < s.Count; i++)
                {
                    Solution[i] = s[i];
                }
                ObjectVal = objectVal;
            }
        }
    }
}
