using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FillLatinSquare
{
    public partial class Form1 : Form
    {
        private LatinSquareSolver _latinSquareSolver;
        private Graphics _initCanvas;
        private Graphics _resultCanvas;
        private bool _initCanvasPainted = false;
        private int _resultCanvasPainted = 0; // 0 nothing, 1 GRC result, 2 GRASP result
        private Font drawFont = new Font("Arial", 12);

        public Form1()
        {
            InitializeComponent();
            initializeOther();
        }

        private void initializeOther()
        {
            _latinSquareSolver = new LatinSquareSolver();
            _latinSquareSolver.TaskFinishedCallBack += OnInitSquareFinished;
            _latinSquareSolver._GRASPSolver.GRCFinishedCallBack += OnGRCFinished;
            _latinSquareSolver._GRASPSolver.GRASPFinishedCallBack += OnGRASPFinished;
        }

        private void OnInitSquareFinished()
        {
            this.Invoke((EventHandler)delegate
            {
                paintInitSquare();
                _initCanvasPainted = true;
                _resultCanvasPainted = 0;
                this.Cursor = Cursors.Default;
            });
        }
        private void OnGRCFinished()
        {
            this.Invoke((EventHandler)delegate
            {
                paintResultSquare(_latinSquareSolver._GRASPSolver.Solution);
                conflictLabel.Text = _latinSquareSolver.CalTotalConfilit(_latinSquareSolver._GRASPSolver.Solution).ToString();
                _resultCanvasPainted = 1;
                this.Cursor = Cursors.Default;
            });
        }
        private void OnGRASPFinished()
        {
            this.Invoke((EventHandler)delegate
            {
                paintResultSquare(_latinSquareSolver._GRASPSolver.BestSolution);
                conflictLabel.Text = _latinSquareSolver.CalTotalConfilit(_latinSquareSolver._GRASPSolver.BestSolution).ToString();
                conflictLabel.Text = string.Format("Conflict number: {0}\nGRASP Iterations: {1}\nElapsed time: {2}s\nPR evolution Iterations: {3}", 
                    _latinSquareSolver.CalTotalConfilit(_latinSquareSolver._GRASPSolver.BestSolution), _latinSquareSolver._GRASPSolver.GRASPIter, 
                    _latinSquareSolver._GRASPSolver.ElapsedT.ToString("0.000"), _latinSquareSolver._GRASPSolver.PREvIter);
                _resultCanvasPainted = 2;
                this.Cursor = Cursors.Default;
            });
        }

        private void OnInitClick(object sender, EventArgs e)
        {
            int length = 0;
            int.TryParse(lengthTxtBox.Text, out length);
            float fillRate = -1f;
            if (fillRateTxtBox.Text.Contains("%"))
            {
                float.TryParse(fillRateTxtBox.Text.Replace("%", ""), out fillRate);
                fillRate /= 100f;
            }
            else
            {
                float.TryParse(fillRateTxtBox.Text, out fillRate);
            }
            if (length > 0 && fillRate >= 0 && fillRate < 1)
            {
                _latinSquareSolver.Length = length;
                _latinSquareSolver.FillRate = fillRate;
                this.Cursor = Cursors.WaitCursor;
                // running as background
                Thread thread = new Thread(new ThreadStart(_latinSquareSolver.InitLatinSquare));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("Illegal data!");
            }
        }

        private void OnGRASPClick(object sender, EventArgs e)
        {
            if (_latinSquareSolver.Length > 0)
            {
                var gRASPSolver = _latinSquareSolver._GRASPSolver;
                var pRSolver = gRASPSolver.pRSolver;
                int maxIterGRASP = gRASPSolver.MaxIterGRASP;
                int maxIterLS = gRASPSolver.MaxIterLS;
                int maxIterStagnation = gRASPSolver.MaxIterNoImprovement;
                float fixedAlpha = gRASPSolver.FixedAlpha;
                int reactiveAlphaBlockIter = gRASPSolver.BlockIterReactiveAlpha;
                int sizeOfES = pRSolver.SizeOfES;
                float pRAlpha = pRSolver.FixedAlpha;
                float truncatedLengthRate = pRSolver.TruncatedLengthRate;
                int evNum = pRSolver.EvNum;
                float distanceRateThreshold = pRSolver.DistanceRateThreshold;

                int.TryParse(maxIterTextBox.Text, out maxIterGRASP);
                int.TryParse(lSMaxIterTextBox.Text, out maxIterLS);
                int.TryParse(lSStagnationTextBox.Text, out maxIterStagnation);
                float.TryParse(fixedAlphaTextBox.Text, out fixedAlpha);
                int.TryParse(blockIterTextBox.Text, out reactiveAlphaBlockIter);
                string[] reactiveAlphaStrArray = realphaTextBox.Text.Split(',');
                int.TryParse(sizeOfESTextBox.Text, out sizeOfES);
                float.TryParse(pRAlphaTextBox.Text, out pRAlpha);
                float.TryParse(truncatedLengthTextBox.Text, out truncatedLengthRate);
                int.TryParse(evTextBox.Text, out evNum);
                float.TryParse(dThresholdTextBox.Text, out distanceRateThreshold);

                
                gRASPSolver.PRFlag = pRCheckBox.Checked;
                gRASPSolver.ReactiveAlphaFlag = reactiveAlphaCheckBox.Checked;
                gRASPSolver.EvPRFlag = pREvCheckBox.Checked;
                gRASPSolver.MaxIterGRASP = maxIterGRASP;
                gRASPSolver.MaxIterLS = maxIterLS;
                gRASPSolver.MaxIterNoImprovement = maxIterStagnation;
                gRASPSolver.FixedAlpha = fixedAlpha;
                gRASPSolver.BlockIterReactiveAlpha = reactiveAlphaBlockIter;
                if (reactiveAlphaStrArray.Length > 0)
                {
                    gRASPSolver.ReactiveAlphaList.Clear();
                    foreach (var str in reactiveAlphaStrArray)
                    {
                        gRASPSolver.ReactiveAlphaList.Add(float.Parse(str));
                    }
                }
                pRSolver.SizeOfES = sizeOfES;
                pRSolver.FixedAlpha = pRAlpha;
                pRSolver.TruncatedLengthRate = truncatedLengthRate;
                pRSolver.EvNum = evNum;
                pRSolver.DistanceRateThreshold = distanceRateThreshold;

                this.Cursor = Cursors.WaitCursor;
                // running as background
                Thread thread = new Thread(new ThreadStart(_latinSquareSolver.RunGRASP));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("Init first!");
            }
        }

        private void OnGRCClick(object sender, EventArgs e)
        {
            if (_latinSquareSolver.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                // running as background
                Thread thread = new Thread(new ThreadStart(_latinSquareSolver.RunGRC));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("Init first!");
            }
        }

        private void paintInitSquare()
        {
            if (_initCanvas == null)
            {
                _initCanvas = initPanel.CreateGraphics();
            }
            paintSquare(_latinSquareSolver.InitSquare, _initCanvas);
        }
        private void paintResultSquare(List<int> square)
        {
            if (_resultCanvas == null)
            {
                _resultCanvas = resultPanel.CreateGraphics();
            }
            paintSquare(square, _resultCanvas);
        }

        private void paintSquare(List<int> square, Graphics canvas)
        {
            canvas.Clear(Color.White);
            var paintLength = initPanel.Width / _latinSquareSolver.Length;
            var color = Color.Black;
            Pen pen = new Pen(color);
            var solidBrush = new SolidBrush(color);

            for (int i = 0; i < square.Count; i++)
            {
                if (square[i] > 0)
                {
                    int row = i / _latinSquareSolver.Length;
                    int col = i % _latinSquareSolver.Length;
                    var rectangle = new Rectangle(row * paintLength, col * paintLength, paintLength, paintLength);
                    solidBrush.Color = colorByNum(square[i]);
                    canvas.FillRectangle(solidBrush, rectangle);
                    solidBrush.Color = Color.White;
                    if (_latinSquareSolver.Length <= 26)
                    {
                        canvas.DrawString(((char)(square[i] + 'A' - 1)).ToString(), drawFont, solidBrush, row * paintLength, col * paintLength);
                    }
                }
            }

            for (int row = 1; row < _latinSquareSolver.Length; row++)
            {
                canvas.DrawLine(pen, 0, row * paintLength, paintLength * _latinSquareSolver.Length, row * paintLength);
            }

            for (int col = 1; col < _latinSquareSolver.Length; col++)
            {
                canvas.DrawLine(pen, col * paintLength, 0, col * paintLength, paintLength * _latinSquareSolver.Length);
            }

            pen.Dispose();
            solidBrush.Dispose();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (_initCanvasPainted)
            {
                paintInitSquare();
            }
            if (_resultCanvasPainted == 1)
            {
                paintResultSquare(_latinSquareSolver._GRASPSolver.Solution);
            }
            else if (_resultCanvasPainted == 2)
            {
                paintResultSquare(_latinSquareSolver._GRASPSolver.BestSolution);
            }
        }

        private Color colorByNum(int num)
        {
            float i = num;
            if (i < 10) i = i * 302.3f;
            if (i < 100) i = i * 31.2f;
            for (; i > 255; i *= 0.98f) ;
            var temp = i.ToString().Substring(i.ToString().Length - 3);
            temp = temp.Replace('.', ' ');
            i += int.Parse(temp);
            for (; i > 255; i -= 255) ;

            int j = Convert.ToInt32(i);
            if (j < 10) j += 10;

            var R = j * (j / 100);
            for (; R > 255; R -= 255) ;
            if (R < 50) R += 60;

            var G = j * (j % 100);
            for (; G > 255; G -= 255) ;
            if (G < 50) G += 60;
            var B = j * (j % 10);
            for (; B > 255; B -= 255) ;
            if (B < 50) B += 60;
            return Color.FromArgb(R, G, B);
        }

    }
}
