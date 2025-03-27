namespace GraphViewer.Forms
{
    partial class HelloForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            bStart = new Button();
            bExit = new Button();
            bInfo = new Button();
            pImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pImage).BeginInit();
            SuspendLayout();
            // 
            // bStart
            // 
            bStart.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bStart.Location = new Point(244, 493);
            bStart.Name = "bStart";
            bStart.Size = new Size(100, 30);
            bStart.TabIndex = 0;
            bStart.Text = "Начать";
            bStart.UseVisualStyleBackColor = true;
            // 
            // bExit
            // 
            bExit.Font = new Font("Times New Roman", 14.25F);
            bExit.Location = new Point(350, 493);
            bExit.Name = "bExit";
            bExit.Size = new Size(100, 30);
            bExit.TabIndex = 1;
            bExit.Text = "Выход";
            bExit.UseVisualStyleBackColor = true;
            // 
            // bInfo
            // 
            bInfo.Font = new Font("Times New Roman", 14.25F);
            bInfo.Location = new Point(456, 493);
            bInfo.Name = "bInfo";
            bInfo.Size = new Size(100, 30);
            bInfo.TabIndex = 2;
            bInfo.Text = "Справка";
            bInfo.UseVisualStyleBackColor = true;
            // 
            // pImage
            // 
            pImage.Dock = DockStyle.Fill;
            pImage.Location = new Point(0, 0);
            pImage.Name = "pImage";
            pImage.Size = new Size(784, 561);
            pImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pImage.TabIndex = 3;
            pImage.TabStop = false;
            // 
            // HelloForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(bInfo);
            Controls.Add(bExit);
            Controls.Add(bStart);
            Controls.Add(pImage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelloForm";
            ((System.ComponentModel.ISupportInitialize)pImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button bStart;
        private Button bExit;
        private Button bInfo;
        private PictureBox pImage;
    }
}