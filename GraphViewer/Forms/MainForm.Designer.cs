namespace GraphViewer
{
    partial class MainForm
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
            _nudDelay = new NumericUpDown();
            _nudBandwidth = new NumericUpDown();
            _label1 = new Label();
            _label2 = new Label();
            _bNext = new Button();
            _bInputMatrix = new Button();
            ((System.ComponentModel.ISupportInitialize)_nudDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_nudBandwidth).BeginInit();
            SuspendLayout();
            // 
            // _nudDelay
            // 
            _nudDelay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _nudDelay.BackColor = Color.White;
            _nudDelay.Font = new Font("Times New Roman", 14.25F);
            _nudDelay.Location = new Point(322, 410);
            _nudDelay.Name = "_nudDelay";
            _nudDelay.Size = new Size(39, 29);
            _nudDelay.TabIndex = 1;
            _nudDelay.ValueChanged += nudDelay_ValueChanged;
            // 
            // _nudBandwidth
            // 
            _nudBandwidth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _nudBandwidth.BackColor = Color.White;
            _nudBandwidth.Font = new Font("Times New Roman", 14.25F);
            _nudBandwidth.Location = new Point(322, 381);
            _nudBandwidth.Name = "_nudBandwidth";
            _nudBandwidth.Size = new Size(39, 29);
            _nudBandwidth.TabIndex = 0;
            _nudBandwidth.ValueChanged += nudBandwidth_ValueChanged;
            // 
            // _label1
            // 
            _label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _label1.AutoSize = true;
            _label1.BackColor = Color.White;
            _label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _label1.Location = new Point(12, 386);
            _label1.Name = "_label1";
            _label1.Size = new Size(304, 19);
            _label1.TabIndex = 2;
            _label1.Text = "Ограничение по пропусконой способности";
            // 
            // _label2
            // 
            _label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _label2.AutoSize = true;
            _label2.BackColor = Color.White;
            _label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _label2.Location = new Point(12, 415);
            _label2.Name = "_label2";
            _label2.Size = new Size(186, 19);
            _label2.TabIndex = 3;
            _label2.Text = "Ограничение по задержке";
            // 
            // _bNext
            // 
            _bNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _bNext.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _bNext.Location = new Point(688, 410);
            _bNext.Name = "_bNext";
            _bNext.Size = new Size(100, 30);
            _bNext.TabIndex = 4;
            _bNext.Text = "Далее";
            _bNext.UseVisualStyleBackColor = true;
            // 
            // bInputMatrix
            // 
            _bInputMatrix.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _bInputMatrix.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _bInputMatrix.Location = new Point(582, 410);
            _bInputMatrix.Name = "bInputMatrix";
            _bInputMatrix.Size = new Size(100, 30);
            _bInputMatrix.TabIndex = 5;
            _bInputMatrix.Text = "Ввести";
            _bInputMatrix.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_bInputMatrix);
            Controls.Add(_bNext);
            Controls.Add(_label2);
            Controls.Add(_nudDelay);
            Controls.Add(_nudBandwidth);
            Controls.Add(_label1);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)_nudDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)_nudBandwidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label _label2;
        private Label _label1;
        private NumericUpDown _nudDelay;
        private NumericUpDown _nudBandwidth;
        private Button _bNext;
        private Button _bInputMatrix;
    }
}
