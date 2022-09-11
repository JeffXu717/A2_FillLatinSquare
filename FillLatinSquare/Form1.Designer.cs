
namespace FillLatinSquare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InitBtn = new System.Windows.Forms.Button();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.lengthTxtBox = new System.Windows.Forms.TextBox();
            this.fillRateLabel = new System.Windows.Forms.Label();
            this.fillRateTxtBox = new System.Windows.Forms.TextBox();
            this.initPanel = new System.Windows.Forms.Panel();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.GRASPBtn = new System.Windows.Forms.Button();
            this.GRCBtn = new System.Windows.Forms.Button();
            this.conflictLabel = new System.Windows.Forms.Label();
            this.reactiveAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.pRCheckBox = new System.Windows.Forms.CheckBox();
            this.gRASPGroupBox = new System.Windows.Forms.GroupBox();
            this.lSStagnationTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lSMaxIterTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maxIterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pREvCheckBox = new System.Windows.Forms.CheckBox();
            this.reactiveGroupBox = new System.Windows.Forms.GroupBox();
            this.blockIterTextBox = new System.Windows.Forms.TextBox();
            this.realphaTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fixedAlphaTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pRGroupBox = new System.Windows.Forms.GroupBox();
            this.dThresholdTextBox = new System.Windows.Forms.TextBox();
            this.evTextBox = new System.Windows.Forms.TextBox();
            this.truncatedLengthTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pRAlphaTextBox = new System.Windows.Forms.TextBox();
            this.sizeOfESTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gRASPGroupBox.SuspendLayout();
            this.reactiveGroupBox.SuspendLayout();
            this.pRGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // InitBtn
            // 
            this.InitBtn.Location = new System.Drawing.Point(12, 648);
            this.InitBtn.Name = "InitBtn";
            this.InitBtn.Size = new System.Drawing.Size(75, 23);
            this.InitBtn.TabIndex = 0;
            this.InitBtn.Text = "Init";
            this.InitBtn.UseVisualStyleBackColor = true;
            this.InitBtn.Click += new System.EventHandler(this.OnInitClick);
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.Location = new System.Drawing.Point(11, 15);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(47, 17);
            this.LengthLabel.TabIndex = 1;
            this.LengthLabel.Text = "Length";
            // 
            // lengthTxtBox
            // 
            this.lengthTxtBox.Location = new System.Drawing.Point(64, 12);
            this.lengthTxtBox.Name = "lengthTxtBox";
            this.lengthTxtBox.Size = new System.Drawing.Size(47, 23);
            this.lengthTxtBox.TabIndex = 2;
            this.lengthTxtBox.Text = "8";
            // 
            // fillRateLabel
            // 
            this.fillRateLabel.AutoSize = true;
            this.fillRateLabel.Location = new System.Drawing.Point(11, 44);
            this.fillRateLabel.Name = "fillRateLabel";
            this.fillRateLabel.Size = new System.Drawing.Size(49, 17);
            this.fillRateLabel.TabIndex = 1;
            this.fillRateLabel.Text = "FillRate";
            // 
            // fillRateTxtBox
            // 
            this.fillRateTxtBox.Location = new System.Drawing.Point(64, 41);
            this.fillRateTxtBox.Name = "fillRateTxtBox";
            this.fillRateTxtBox.Size = new System.Drawing.Size(47, 23);
            this.fillRateTxtBox.TabIndex = 2;
            this.fillRateTxtBox.Text = "50%";
            // 
            // initPanel
            // 
            this.initPanel.Location = new System.Drawing.Point(1, 130);
            this.initPanel.Name = "initPanel";
            this.initPanel.Size = new System.Drawing.Size(500, 500);
            this.initPanel.TabIndex = 3;
            // 
            // resultPanel
            // 
            this.resultPanel.Location = new System.Drawing.Point(507, 130);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(500, 500);
            this.resultPanel.TabIndex = 3;
            // 
            // GRASPBtn
            // 
            this.GRASPBtn.Location = new System.Drawing.Point(11, 677);
            this.GRASPBtn.Name = "GRASPBtn";
            this.GRASPBtn.Size = new System.Drawing.Size(75, 23);
            this.GRASPBtn.TabIndex = 0;
            this.GRASPBtn.Text = "GRASP";
            this.GRASPBtn.UseVisualStyleBackColor = true;
            this.GRASPBtn.Click += new System.EventHandler(this.OnGRASPClick);
            // 
            // GRCBtn
            // 
            this.GRCBtn.Location = new System.Drawing.Point(103, 677);
            this.GRCBtn.Name = "GRCBtn";
            this.GRCBtn.Size = new System.Drawing.Size(75, 23);
            this.GRCBtn.TabIndex = 0;
            this.GRCBtn.Text = "GRC";
            this.GRCBtn.UseVisualStyleBackColor = true;
            this.GRCBtn.Click += new System.EventHandler(this.OnGRCClick);
            // 
            // conflictLabel
            // 
            this.conflictLabel.AutoSize = true;
            this.conflictLabel.Location = new System.Drawing.Point(639, 633);
            this.conflictLabel.Name = "conflictLabel";
            this.conflictLabel.Size = new System.Drawing.Size(0, 17);
            this.conflictLabel.TabIndex = 4;
            // 
            // reactiveAlphaCheckBox
            // 
            this.reactiveAlphaCheckBox.AutoSize = true;
            this.reactiveAlphaCheckBox.Checked = true;
            this.reactiveAlphaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reactiveAlphaCheckBox.Location = new System.Drawing.Point(126, 12);
            this.reactiveAlphaCheckBox.Name = "reactiveAlphaCheckBox";
            this.reactiveAlphaCheckBox.Size = new System.Drawing.Size(111, 21);
            this.reactiveAlphaCheckBox.TabIndex = 5;
            this.reactiveAlphaCheckBox.Text = "Reactive alpha";
            this.reactiveAlphaCheckBox.UseVisualStyleBackColor = true;
            // 
            // pRCheckBox
            // 
            this.pRCheckBox.AutoSize = true;
            this.pRCheckBox.Checked = true;
            this.pRCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pRCheckBox.Location = new System.Drawing.Point(126, 41);
            this.pRCheckBox.Name = "pRCheckBox";
            this.pRCheckBox.Size = new System.Drawing.Size(106, 21);
            this.pRCheckBox.TabIndex = 5;
            this.pRCheckBox.Text = "Path relinking";
            this.pRCheckBox.UseVisualStyleBackColor = true;
            // 
            // gRASPGroupBox
            // 
            this.gRASPGroupBox.Controls.Add(this.lSStagnationTextBox);
            this.gRASPGroupBox.Controls.Add(this.label3);
            this.gRASPGroupBox.Controls.Add(this.lSMaxIterTextBox);
            this.gRASPGroupBox.Controls.Add(this.label2);
            this.gRASPGroupBox.Controls.Add(this.maxIterTextBox);
            this.gRASPGroupBox.Controls.Add(this.label1);
            this.gRASPGroupBox.Location = new System.Drawing.Point(249, 12);
            this.gRASPGroupBox.Name = "gRASPGroupBox";
            this.gRASPGroupBox.Size = new System.Drawing.Size(178, 112);
            this.gRASPGroupBox.TabIndex = 6;
            this.gRASPGroupBox.TabStop = false;
            this.gRASPGroupBox.Text = "Basic GRASP";
            // 
            // lSStagnationTextBox
            // 
            this.lSStagnationTextBox.Location = new System.Drawing.Point(92, 80);
            this.lSStagnationTextBox.Name = "lSStagnationTextBox";
            this.lSStagnationTextBox.Size = new System.Drawing.Size(74, 23);
            this.lSStagnationTextBox.TabIndex = 1;
            this.lSStagnationTextBox.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "LS Stagnation";
            // 
            // lSMaxIterTextBox
            // 
            this.lSMaxIterTextBox.Location = new System.Drawing.Point(92, 51);
            this.lSMaxIterTextBox.Name = "lSMaxIterTextBox";
            this.lSMaxIterTextBox.Size = new System.Drawing.Size(74, 23);
            this.lSMaxIterTextBox.TabIndex = 1;
            this.lSMaxIterTextBox.Text = "2000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "LS Max Iter";
            // 
            // maxIterTextBox
            // 
            this.maxIterTextBox.Location = new System.Drawing.Point(92, 22);
            this.maxIterTextBox.Name = "maxIterTextBox";
            this.maxIterTextBox.Size = new System.Drawing.Size(74, 23);
            this.maxIterTextBox.TabIndex = 1;
            this.maxIterTextBox.Text = "2000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Iter";
            // 
            // pREvCheckBox
            // 
            this.pREvCheckBox.AutoSize = true;
            this.pREvCheckBox.Checked = true;
            this.pREvCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pREvCheckBox.Location = new System.Drawing.Point(126, 68);
            this.pREvCheckBox.Name = "pREvCheckBox";
            this.pREvCheckBox.Size = new System.Drawing.Size(117, 21);
            this.pREvCheckBox.TabIndex = 5;
            this.pREvCheckBox.Text = "Evolutionary PR";
            this.pREvCheckBox.UseVisualStyleBackColor = true;
            // 
            // reactiveGroupBox
            // 
            this.reactiveGroupBox.Controls.Add(this.blockIterTextBox);
            this.reactiveGroupBox.Controls.Add(this.realphaTextBox);
            this.reactiveGroupBox.Controls.Add(this.label4);
            this.reactiveGroupBox.Controls.Add(this.label5);
            this.reactiveGroupBox.Controls.Add(this.fixedAlphaTextBox);
            this.reactiveGroupBox.Controls.Add(this.label6);
            this.reactiveGroupBox.Location = new System.Drawing.Point(433, 12);
            this.reactiveGroupBox.Name = "reactiveGroupBox";
            this.reactiveGroupBox.Size = new System.Drawing.Size(180, 112);
            this.reactiveGroupBox.TabIndex = 6;
            this.reactiveGroupBox.TabStop = false;
            this.reactiveGroupBox.Text = "Reactive alpha";
            // 
            // blockIterTextBox
            // 
            this.blockIterTextBox.Location = new System.Drawing.Point(92, 80);
            this.blockIterTextBox.Name = "blockIterTextBox";
            this.blockIterTextBox.Size = new System.Drawing.Size(74, 23);
            this.blockIterTextBox.TabIndex = 1;
            this.blockIterTextBox.Text = "200";
            // 
            // realphaTextBox
            // 
            this.realphaTextBox.Location = new System.Drawing.Point(92, 51);
            this.realphaTextBox.Name = "realphaTextBox";
            this.realphaTextBox.Size = new System.Drawing.Size(74, 23);
            this.realphaTextBox.TabIndex = 1;
            this.realphaTextBox.Text = "0.1,0.5,1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Block Iter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Reactive alphas";
            // 
            // fixedAlphaTextBox
            // 
            this.fixedAlphaTextBox.Location = new System.Drawing.Point(92, 22);
            this.fixedAlphaTextBox.Name = "fixedAlphaTextBox";
            this.fixedAlphaTextBox.Size = new System.Drawing.Size(74, 23);
            this.fixedAlphaTextBox.TabIndex = 1;
            this.fixedAlphaTextBox.Text = "0.4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Fixed alpha";
            // 
            // pRGroupBox
            // 
            this.pRGroupBox.Controls.Add(this.dThresholdTextBox);
            this.pRGroupBox.Controls.Add(this.evTextBox);
            this.pRGroupBox.Controls.Add(this.truncatedLengthTextBox);
            this.pRGroupBox.Controls.Add(this.label7);
            this.pRGroupBox.Controls.Add(this.label11);
            this.pRGroupBox.Controls.Add(this.label8);
            this.pRGroupBox.Controls.Add(this.pRAlphaTextBox);
            this.pRGroupBox.Controls.Add(this.sizeOfESTextBox);
            this.pRGroupBox.Controls.Add(this.label10);
            this.pRGroupBox.Controls.Add(this.label9);
            this.pRGroupBox.Location = new System.Drawing.Point(619, 12);
            this.pRGroupBox.Name = "pRGroupBox";
            this.pRGroupBox.Size = new System.Drawing.Size(377, 112);
            this.pRGroupBox.TabIndex = 6;
            this.pRGroupBox.TabStop = false;
            this.pRGroupBox.Text = "Path Relinking";
            // 
            // dThresholdTextBox
            // 
            this.dThresholdTextBox.Location = new System.Drawing.Point(117, 77);
            this.dThresholdTextBox.Name = "dThresholdTextBox";
            this.dThresholdTextBox.Size = new System.Drawing.Size(74, 23);
            this.dThresholdTextBox.TabIndex = 1;
            this.dThresholdTextBox.Text = "0.3";
            // 
            // evTextBox
            // 
            this.evTextBox.Location = new System.Drawing.Point(277, 51);
            this.evTextBox.Name = "evTextBox";
            this.evTextBox.Size = new System.Drawing.Size(74, 23);
            this.evTextBox.TabIndex = 1;
            this.evTextBox.Text = "10";
            // 
            // truncatedLengthTextBox
            // 
            this.truncatedLengthTextBox.Location = new System.Drawing.Point(117, 48);
            this.truncatedLengthTextBox.Name = "truncatedLengthTextBox";
            this.truncatedLengthTextBox.Size = new System.Drawing.Size(74, 23);
            this.truncatedLengthTextBox.TabIndex = 1;
            this.truncatedLengthTextBox.Text = "0.2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "DistanceThreshold";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(197, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Evolution";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-3, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "TruncatedLength";
            // 
            // pRAlphaTextBox
            // 
            this.pRAlphaTextBox.Location = new System.Drawing.Point(277, 22);
            this.pRAlphaTextBox.Name = "pRAlphaTextBox";
            this.pRAlphaTextBox.Size = new System.Drawing.Size(74, 23);
            this.pRAlphaTextBox.TabIndex = 1;
            this.pRAlphaTextBox.Text = "0.5";
            // 
            // sizeOfESTextBox
            // 
            this.sizeOfESTextBox.Location = new System.Drawing.Point(117, 22);
            this.sizeOfESTextBox.Name = "sizeOfESTextBox";
            this.sizeOfESTextBox.Size = new System.Drawing.Size(74, 23);
            this.sizeOfESTextBox.TabIndex = 1;
            this.sizeOfESTextBox.Text = "20";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(197, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "PR alpha";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "SizeOfES";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pRGroupBox);
            this.Controls.Add(this.reactiveGroupBox);
            this.Controls.Add(this.gRASPGroupBox);
            this.Controls.Add(this.pREvCheckBox);
            this.Controls.Add(this.pRCheckBox);
            this.Controls.Add(this.reactiveAlphaCheckBox);
            this.Controls.Add(this.conflictLabel);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.initPanel);
            this.Controls.Add(this.fillRateTxtBox);
            this.Controls.Add(this.fillRateLabel);
            this.Controls.Add(this.lengthTxtBox);
            this.Controls.Add(this.LengthLabel);
            this.Controls.Add(this.GRASPBtn);
            this.Controls.Add(this.GRCBtn);
            this.Controls.Add(this.InitBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.gRASPGroupBox.ResumeLayout(false);
            this.gRASPGroupBox.PerformLayout();
            this.reactiveGroupBox.ResumeLayout(false);
            this.reactiveGroupBox.PerformLayout();
            this.pRGroupBox.ResumeLayout(false);
            this.pRGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InitBtn;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.TextBox lengthTxtBox;
        private System.Windows.Forms.Label fillRateLabel;
        private System.Windows.Forms.TextBox fillRateTxtBox;
        private System.Windows.Forms.Panel initPanel;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Button GRASPBtn;
        private System.Windows.Forms.Button GRCBtn;
        private System.Windows.Forms.Label conflictLabel;
        private System.Windows.Forms.CheckBox reactiveAlphaCheckBox;
        private System.Windows.Forms.CheckBox pRCheckBox;
        private System.Windows.Forms.GroupBox gRASPGroupBox;
        private System.Windows.Forms.TextBox lSStagnationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lSMaxIterTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxIterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox pREvCheckBox;
        private System.Windows.Forms.GroupBox reactiveGroupBox;
        private System.Windows.Forms.TextBox realphaTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fixedAlphaTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox blockIterTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox pRGroupBox;
        private System.Windows.Forms.TextBox dThresholdTextBox;
        private System.Windows.Forms.TextBox truncatedLengthTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sizeOfESTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox evTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox pRAlphaTextBox;
        private System.Windows.Forms.Label label10;
    }
}

