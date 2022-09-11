using System;
using System.Collections.Generic;
using System.Text;

namespace FillLatinSquare
{
    class LatinSquareSolver
    {
        private Random rnd = new Random();
        public GRASPSolver _GRASPSolver;
        private List<int> conflictList = new List<int>();

        public delegate void TaskFinished();//声明一个在完成任务时通知主线程的委托
        public TaskFinished TaskFinishedCallBack;

        public int Length { get; set; }
        public List<int> InitSquare { get; private set; }
        public float FillRate { get; set; }

        public LatinSquareSolver()
        {
            _GRASPSolver = new GRASPSolver(this);
            InitSquare = new List<int>();
        }

        public void InitLatinSquare()
        {
            initEmptySquare();
            fillLatinSquareRandomly();
            if (TaskFinishedCallBack != null)
            {
                TaskFinishedCallBack();
            }
        }


        private void initEmptySquare()
        {
            InitSquare.Clear();
            for (int i = 0; i < Length * Length; i++)
            {
                InitSquare.Add(0);
            }
        }

        private void fillLatinSquareRandomly()
        {
            int num = (int)(Length * Length * FillRate);
            int i = 0;
            while (i < num)
            {
                int val = rnd.Next(Length) + 1;
                int row = rnd.Next(Length);
                int col = rnd.Next(Length);
                if (!ifConflictInRow(InitSquare, row, val) && !ifConflictInCol(InitSquare, col, val))
                {
                    InitSquare[row * Length + col] = val;
                    i++;
                }
            }
        }

        private bool ifConflictInRow(List<int> square, int row, int val)
        {
            for (int i = row * Length; i < (row + 1) * Length; i++)
            {
                if (square[i] == val)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ifConflictInCol(List<int> square, int col, int val)
        {
            for (int i = col; i < Length * Length; i += Length)
            {
                if (square[i] == val)
                {
                    return true;
                }
            }
            return false;
        }

        public void RunGRC()
        {
            initConflictList();
            _GRASPSolver.GreedyRandomizedConstruction();
        }

        public void RunGRASP()
        {
            initConflictList();
            _GRASPSolver.GRASP();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="square"></param>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns>0, no collision with val in row an col. 1, 1 collision with val in row or col. 2, collisions with val in row and col </returns>
        public int CountIncrementConflict(List<int> square, int index, int val)
        {
            int count = 0;
            var row = index / Length;
            for (int i = row * Length; i < (row + 1) * Length; i++)
            {
                if (square[i] == val)
                {
                    count++;
                    break;
                }
            }
            var col = index % Length;
            for (int i = col; i < Length * Length; i += Length)
            {
                if (square[i] == val)
                {
                    count++;
                    break;
                }
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="square"></param>
        /// <param name="index"></param>
        /// <param name="newVal"></param>
        /// <returns>-2, -1, 0, 1, 2</returns>
        public int CountSwapConflict(List<int> square, int index, int newVal)
        {
            int countOldRow = 0;
            int countNewRow = 0;
            int countOldCol = 0;
            int countNewCol = 0;
            int oldVal = square[index];
            var row = index / Length;
            for (int i = row * Length; i < (row + 1) * Length; i++)
            {
                if (i != index)
                {
                    if (square[i] == oldVal && countOldRow == 0)
                    {
                        countOldRow++;
                    }
                    if (square[i] == newVal && countNewRow == 0)
                    {
                        countNewRow++;
                    }
                    if (countOldRow > 0 && countNewRow > 0)
                    {
                        break;
                    }
                }
            }
            var col = index % Length;
            for (int i = col; i < Length * Length; i += Length)
            {
                if (i != index)
                {
                    if (square[i] == oldVal && countOldCol == 0)
                    {
                        countOldCol++;
                    }
                    if (square[i] == newVal && countNewCol == 0)
                    {
                        countNewCol++;
                    }
                    if (countOldCol > 0 && countNewCol > 0)
                    {
                        break;
                    }
                }
            }
            return countNewRow + countNewCol - countOldRow - countOldCol;
        }

        public bool IfAlterBetter(List<int> square, int index, int val)
        {
            var row = index / Length;
            var col = index % Length;
            int countOrigin = 0;
            int countVal = 0;
            for (int i = row * Length; i < (row + 1) * Length; i++)
            {
                if (index != i)
                {
                    if (square[i] == square[index])
                    {
                        countOrigin++;
                    }
                    else if (square[i] == val)
                    {
                        countVal++;
                    }
                }
            }
            for (int i = col; i < Length * Length; i += Length)
            {
                if (index != i)
                {
                    if (square[i] == square[index])
                    {
                        countOrigin++;
                    }
                    else if (square[i] == val)
                    {
                        countVal++;
                    }
                }
            }
            return countVal < countOrigin;

        }

        private int countConflictInRow(List<int> square, int row)
        {
            resetConflictList();
            for (int i = row * Length; i < (row + 1) * Length; i++)
            {
                if (square[i] > 0)
                {
                    conflictList[square[i] - 1] += 1;   
                }
            }
            return calConflict();
        }

        private int countConflictInCol(List<int> square, int col)
        {
            resetConflictList();
            for (int i = col; i < Length * Length; i += Length)
            {
                if (square[i] > 0)
                {
                    conflictList[square[i] - 1] += 1;   
                }
            }
            return calConflict();
        }
        private void initConflictList()
        {
            conflictList.Clear();
            for (int i = 0; i < Length; i++)
            {
                conflictList.Add(0);
            }
        }

        private void resetConflictList()
        {
            for (int i = 0; i < Length; i++)
            {
                conflictList[i] = 0;
            }
        }

        public int CalTotalConfilit(List<int> square)
        {
            int total = 0;
            for (int row = 0; row < Length; row++)
            {
                total += countConflictInRow(square, row);
            }
            for (int col = 0; col < Length; col++)
            {
                total += countConflictInCol(square, col);
            }
            return total;
        }

        private int calConflict()
        {
            int conflict = 0;
            conflictList.ForEach(e =>
            {
                if (e > 1)
                {
                    conflict += e - 1;
                }
            });
            return conflict;
        }

        public override string ToString()
        {
            string str = String.Empty;
            if (Length > 0 && InitSquare != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("InitSquare =  \n");
                for (int i = 0; i < Length * Length; i++)
                {
                    sb.Append(InitSquare[i].ToString().PadRight(4));
                    if ((i + 1) % Length == 0)
                    {
                        sb.Append("\n");
                    }
                }
                str = sb.ToString();
            }
            return str;
        }
    }
}
