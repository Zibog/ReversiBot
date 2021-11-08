using System.ComponentModel;
using System.Windows.Forms;

namespace ReversiUI
{
    partial class ReversiUi
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GamePanel = new System.Windows.Forms.Panel();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.whitePly = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.blackPly = new System.Windows.Forms.NumericUpDown();
            this.whiteHeuristic = new System.Windows.Forms.GroupBox();
            this.humanWhite = new System.Windows.Forms.RadioButton();
            this.weightedWhite = new System.Windows.Forms.RadioButton();
            this.cornersWhite = new System.Windows.Forms.RadioButton();
            this.mobilityWhite = new System.Windows.Forms.RadioButton();
            this.tileWhite = new System.Windows.Forms.RadioButton();
            this.blackHeuristic = new System.Windows.Forms.GroupBox();
            this.humanBlack = new System.Windows.Forms.RadioButton();
            this.weightedBlack = new System.Windows.Forms.RadioButton();
            this.cornersBlack = new System.Windows.Forms.RadioButton();
            this.mobilityBlack = new System.Windows.Forms.RadioButton();
            this.tileBlack = new System.Windows.Forms.RadioButton();
            this.NextMoveBtn = new System.Windows.Forms.Button();
            this.StopOrClear = new System.Windows.Forms.Button();
            this.OptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whitePly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPly)).BeginInit();
            this.whiteHeuristic.SuspendLayout();
            this.blackHeuristic.SuspendLayout();
            this.SuspendLayout();
            // 
            // GamePanel
            // 
            this.GamePanel.Location = new System.Drawing.Point(190, 0);
            this.GamePanel.Margin = new System.Windows.Forms.Padding(0);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(675, 675);
            this.GamePanel.TabIndex = 5;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.label4);
            this.OptionsPanel.Controls.Add(this.whitePly);
            this.OptionsPanel.Controls.Add(this.label1);
            this.OptionsPanel.Controls.Add(this.blackPly);
            this.OptionsPanel.Controls.Add(this.whiteHeuristic);
            this.OptionsPanel.Controls.Add(this.blackHeuristic);
            this.OptionsPanel.Controls.Add(this.NextMoveBtn);
            this.OptionsPanel.Controls.Add(this.StopOrClear);
            this.OptionsPanel.Location = new System.Drawing.Point(0, 0);
            this.OptionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(190, 650);
            this.OptionsPanel.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 353);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "White Plies";
            // 
            // whitePly
            // 
            this.whitePly.Location = new System.Drawing.Point(125, 350);
            this.whitePly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.whitePly.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.whitePly.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.whitePly.Name = "whitePly";
            this.whitePly.Size = new System.Drawing.Size(37, 23);
            this.whitePly.TabIndex = 13;
            this.whitePly.Tag = "white";
            this.whitePly.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.whitePly.ValueChanged += new System.EventHandler(this.SetPly);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 169);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Black Plies";
            // 
            // blackPly
            // 
            this.blackPly.Location = new System.Drawing.Point(125, 167);
            this.blackPly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.blackPly.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.blackPly.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blackPly.Name = "blackPly";
            this.blackPly.Size = new System.Drawing.Size(37, 23);
            this.blackPly.TabIndex = 11;
            this.blackPly.Tag = "black";
            this.blackPly.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.blackPly.ValueChanged += new System.EventHandler(this.SetPly);
            // 
            // whiteHeuristic
            // 
            this.whiteHeuristic.Controls.Add(this.humanWhite);
            this.whiteHeuristic.Controls.Add(this.weightedWhite);
            this.whiteHeuristic.Controls.Add(this.cornersWhite);
            this.whiteHeuristic.Controls.Add(this.mobilityWhite);
            this.whiteHeuristic.Controls.Add(this.tileWhite);
            this.whiteHeuristic.Location = new System.Drawing.Point(13, 196);
            this.whiteHeuristic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.whiteHeuristic.Name = "whiteHeuristic";
            this.whiteHeuristic.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.whiteHeuristic.Size = new System.Drawing.Size(164, 148);
            this.whiteHeuristic.TabIndex = 10;
            this.whiteHeuristic.TabStop = false;
            this.whiteHeuristic.Text = "White Heuristic";
            // 
            // humanWhite
            // 
            this.humanWhite.AutoSize = true;
            this.humanWhite.Checked = true;
            this.humanWhite.Location = new System.Drawing.Point(8, 121);
            this.humanWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.humanWhite.Name = "humanWhite";
            this.humanWhite.Size = new System.Drawing.Size(100, 19);
            this.humanWhite.TabIndex = 4;
            this.humanWhite.TabStop = true;
            this.humanWhite.Tag = "humanWhite";
            this.humanWhite.Text = "Human Player";
            this.humanWhite.UseVisualStyleBackColor = true;
            this.humanWhite.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // weightedWhite
            // 
            this.weightedWhite.AutoSize = true;
            this.weightedWhite.Location = new System.Drawing.Point(8, 96);
            this.weightedWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weightedWhite.Name = "weightedWhite";
            this.weightedWhite.Size = new System.Drawing.Size(102, 19);
            this.weightedWhite.TabIndex = 3;
            this.weightedWhite.Tag = "weightedWhite";
            this.weightedWhite.Text = "Weighted Tiles";
            this.weightedWhite.UseVisualStyleBackColor = true;
            this.weightedWhite.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // cornersWhite
            // 
            this.cornersWhite.AutoSize = true;
            this.cornersWhite.Location = new System.Drawing.Point(8, 73);
            this.cornersWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cornersWhite.Name = "cornersWhite";
            this.cornersWhite.Size = new System.Drawing.Size(66, 19);
            this.cornersWhite.TabIndex = 2;
            this.cornersWhite.Tag = "cornersWhite";
            this.cornersWhite.Text = "Corners";
            this.cornersWhite.UseVisualStyleBackColor = true;
            this.cornersWhite.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // mobilityWhite
            // 
            this.mobilityWhite.AutoSize = true;
            this.mobilityWhite.Location = new System.Drawing.Point(7, 48);
            this.mobilityWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mobilityWhite.Name = "mobilityWhite";
            this.mobilityWhite.Size = new System.Drawing.Size(106, 19);
            this.mobilityWhite.TabIndex = 1;
            this.mobilityWhite.Tag = "mobilityWhite";
            this.mobilityWhite.Text = "Actual Mobility";
            this.mobilityWhite.UseVisualStyleBackColor = true;
            this.mobilityWhite.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // tileWhite
            // 
            this.tileWhite.AutoSize = true;
            this.tileWhite.Location = new System.Drawing.Point(7, 22);
            this.tileWhite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tileWhite.Name = "tileWhite";
            this.tileWhite.Size = new System.Drawing.Size(79, 19);
            this.tileWhite.TabIndex = 0;
            this.tileWhite.Tag = "tileWhite";
            this.tileWhite.Text = "Tile Count";
            this.tileWhite.UseVisualStyleBackColor = true;
            this.tileWhite.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // blackHeuristic
            // 
            this.blackHeuristic.Controls.Add(this.humanBlack);
            this.blackHeuristic.Controls.Add(this.weightedBlack);
            this.blackHeuristic.Controls.Add(this.cornersBlack);
            this.blackHeuristic.Controls.Add(this.mobilityBlack);
            this.blackHeuristic.Controls.Add(this.tileBlack);
            this.blackHeuristic.Location = new System.Drawing.Point(13, 12);
            this.blackHeuristic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.blackHeuristic.Name = "blackHeuristic";
            this.blackHeuristic.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.blackHeuristic.Size = new System.Drawing.Size(164, 148);
            this.blackHeuristic.TabIndex = 9;
            this.blackHeuristic.TabStop = false;
            this.blackHeuristic.Text = "Black Heuristic";
            // 
            // humanBlack
            // 
            this.humanBlack.AutoSize = true;
            this.humanBlack.Checked = true;
            this.humanBlack.Location = new System.Drawing.Point(8, 121);
            this.humanBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.humanBlack.Name = "humanBlack";
            this.humanBlack.Size = new System.Drawing.Size(100, 19);
            this.humanBlack.TabIndex = 4;
            this.humanBlack.TabStop = true;
            this.humanBlack.Tag = "humanBlack";
            this.humanBlack.Text = "Human Player";
            this.humanBlack.UseVisualStyleBackColor = true;
            this.humanBlack.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // weightedBlack
            // 
            this.weightedBlack.AutoSize = true;
            this.weightedBlack.Location = new System.Drawing.Point(8, 96);
            this.weightedBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weightedBlack.Name = "weightedBlack";
            this.weightedBlack.Size = new System.Drawing.Size(102, 19);
            this.weightedBlack.TabIndex = 3;
            this.weightedBlack.Tag = "weightedBlack";
            this.weightedBlack.Text = "Weighted Tiles";
            this.weightedBlack.UseVisualStyleBackColor = true;
            this.weightedBlack.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // cornersBlack
            // 
            this.cornersBlack.AutoSize = true;
            this.cornersBlack.Location = new System.Drawing.Point(8, 73);
            this.cornersBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cornersBlack.Name = "cornersBlack";
            this.cornersBlack.Size = new System.Drawing.Size(66, 19);
            this.cornersBlack.TabIndex = 2;
            this.cornersBlack.Tag = "cornersBlack";
            this.cornersBlack.Text = "Corners";
            this.cornersBlack.UseVisualStyleBackColor = true;
            this.cornersBlack.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // mobilityBlack
            // 
            this.mobilityBlack.AutoSize = true;
            this.mobilityBlack.Location = new System.Drawing.Point(7, 48);
            this.mobilityBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mobilityBlack.Name = "mobilityBlack";
            this.mobilityBlack.Size = new System.Drawing.Size(106, 19);
            this.mobilityBlack.TabIndex = 1;
            this.mobilityBlack.Tag = "mobilityBlack";
            this.mobilityBlack.Text = "Actual Mobility";
            this.mobilityBlack.UseVisualStyleBackColor = true;
            this.mobilityBlack.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // tileBlack
            // 
            this.tileBlack.AutoSize = true;
            this.tileBlack.Location = new System.Drawing.Point(7, 22);
            this.tileBlack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tileBlack.Name = "tileBlack";
            this.tileBlack.Size = new System.Drawing.Size(79, 19);
            this.tileBlack.TabIndex = 0;
            this.tileBlack.Tag = "tileBlack";
            this.tileBlack.Text = "Tile Count";
            this.tileBlack.UseVisualStyleBackColor = true;
            this.tileBlack.CheckedChanged += new System.EventHandler(this.ChangeGameMode);
            // 
            // NextMoveBtn
            // 
            this.NextMoveBtn.Location = new System.Drawing.Point(13, 379);
            this.NextMoveBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NextMoveBtn.Name = "NextMoveBtn";
            this.NextMoveBtn.Size = new System.Drawing.Size(164, 31);
            this.NextMoveBtn.TabIndex = 8;
            this.NextMoveBtn.Text = "Next";
            this.NextMoveBtn.UseVisualStyleBackColor = true;
            this.NextMoveBtn.Click += new System.EventHandler(this.NextMove);
            // 
            // StopOrClear
            // 
            this.StopOrClear.Location = new System.Drawing.Point(13, 427);
            this.StopOrClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.StopOrClear.Name = "StopOrClear";
            this.StopOrClear.Size = new System.Drawing.Size(164, 32);
            this.StopOrClear.TabIndex = 5;
            this.StopOrClear.Text = "Stop / Clear";
            this.StopOrClear.UseVisualStyleBackColor = true;
            this.StopOrClear.Click += new System.EventHandler(this.Reset);
            // 
            // BoardVisual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 636);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.GamePanel);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ReversiUi";
            this.Text = "Reversi";
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whitePly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPly)).EndInit();
            this.whiteHeuristic.ResumeLayout(false);
            this.whiteHeuristic.PerformLayout();
            this.blackHeuristic.ResumeLayout(false);
            this.blackHeuristic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel GamePanel;
        private Panel OptionsPanel;
        private Button StopOrClear;
        private Button NextMoveBtn;
        private GroupBox blackHeuristic;
        private RadioButton cornersBlack;
        private RadioButton mobilityBlack;
        private RadioButton tileBlack;
        private RadioButton humanBlack;
        private RadioButton weightedBlack;
        private GroupBox whiteHeuristic;
        private RadioButton humanWhite;
        private RadioButton weightedWhite;
        private RadioButton cornersWhite;
        private RadioButton mobilityWhite;
        private RadioButton tileWhite;
        private Label label4;
        private NumericUpDown whitePly;
        private Label label1;
        private NumericUpDown blackPly;
    }
}

