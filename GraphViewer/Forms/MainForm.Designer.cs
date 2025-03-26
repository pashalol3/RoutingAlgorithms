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
            pbGraphViewer = new PictureBox();
            nudDelay = new NumericUpDown();
            nudBandwidth = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbGraphViewer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBandwidth).BeginInit();
            SuspendLayout();
            // 
            // pbGraphViewer
            // 
            pbGraphViewer.Dock = DockStyle.Fill;
            pbGraphViewer.Location = new Point(0, 0);
            pbGraphViewer.Name = "pbGraphViewer";
            pbGraphViewer.Size = new Size(800, 450);
            pbGraphViewer.TabIndex = 0;
            pbGraphViewer.TabStop = false;
            // 
            // nudDelay
            // 
            nudDelay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            nudDelay.BackColor = Color.White;
            nudDelay.Font = new Font("Times New Roman", 14.25F);
            nudDelay.Location = new Point(322, 410);
            nudDelay.Name = "nudDelay";
            nudDelay.Size = new Size(39, 29);
            nudDelay.TabIndex = 1;
            nudDelay.ValueChanged += nudDelay_ValueChanged;
            // 
            // nudBandwidth
            // 
            nudBandwidth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            nudBandwidth.BackColor = Color.White;
            nudBandwidth.Font = new Font("Times New Roman", 14.25F);
            nudBandwidth.Location = new Point(322, 381);
            nudBandwidth.Name = "nudBandwidth";
            nudBandwidth.Size = new Size(39, 29);
            nudBandwidth.TabIndex = 0;
            nudBandwidth.ValueChanged += nudBandwidth_ValueChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 386);
            label1.Name = "label1";
            label1.Size = new Size(304, 19);
            label1.TabIndex = 2;
            label1.Text = "Ограничение по пропусконой способности";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(12, 415);
            label2.Name = "label2";
            label2.Size = new Size(186, 19);
            label2.TabIndex = 3;
            label2.Text = "Ограничение по задержке";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(nudDelay);
            Controls.Add(nudBandwidth);
            Controls.Add(label1);
            Controls.Add(pbGraphViewer);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)pbGraphViewer).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBandwidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbGraphViewer;
        private Label label2;
        private Label label1;
        private NumericUpDown nudDelay;
        private NumericUpDown nudBandwidth;
    }
}
