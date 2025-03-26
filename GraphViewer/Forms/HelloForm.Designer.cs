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
            SuspendLayout();
            // 
            // bStart
            // 
            bStart.Location = new Point(212, 307);
            bStart.Name = "bStart";
            bStart.Size = new Size(75, 23);
            bStart.TabIndex = 0;
            bStart.Text = "Начать";
            bStart.UseVisualStyleBackColor = true;
            // 
            // bExit
            // 
            bExit.Location = new Point(310, 307);
            bExit.Name = "bExit";
            bExit.Size = new Size(75, 23);
            bExit.TabIndex = 1;
            bExit.Text = "Выход";
            bExit.UseVisualStyleBackColor = true;
            // 
            // bInfo
            // 
            bInfo.Location = new Point(407, 307);
            bInfo.Name = "bInfo";
            bInfo.Size = new Size(75, 23);
            bInfo.TabIndex = 2;
            bInfo.Text = "Справка";
            bInfo.UseVisualStyleBackColor = true;
            // 
            // HelloForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(bInfo);
            Controls.Add(bExit);
            Controls.Add(bStart);
            Name = "HelloForm";
            ResumeLayout(false);
        }

        #endregion

        private Button bStart;
        private Button bExit;
        private Button bInfo;
    }
}