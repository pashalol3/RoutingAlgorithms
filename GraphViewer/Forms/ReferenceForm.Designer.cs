namespace GraphViewer.Forms
{
    partial class ReferenceForm
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
            bNext = new Button();
            bPrev = new Button();
            lTop1 = new Label();
            lTop2 = new Label();
            lCenter = new Label();
            lBottom = new Label();
            pbImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            SuspendLayout();
            // 
            // bNext
            // 
            bNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bNext.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bNext.Location = new Point(542, 379);
            bNext.Name = "bNext";
            bNext.Size = new Size(100, 30);
            bNext.TabIndex = 1;
            bNext.Text = "Далее";
            bNext.UseVisualStyleBackColor = true;
            // 
            // bPrev
            // 
            bPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bPrev.Font = new Font("Times New Roman", 14.25F);
            bPrev.Location = new Point(12, 379);
            bPrev.Name = "bPrev";
            bPrev.Size = new Size(100, 30);
            bPrev.TabIndex = 2;
            bPrev.Text = "Назад";
            bPrev.UseVisualStyleBackColor = true;
            // 
            // lTop1
            // 
            lTop1.AutoSize = true;
            lTop1.BackColor = Color.Transparent;
            lTop1.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lTop1.Location = new Point(12, 9);
            lTop1.Name = "lTop1";
            lTop1.Size = new Size(325, 63);
            lTop1.TabIndex = 3;
            lTop1.Text = "Разработчики программы:\r\nСтарший преподаватель кафедры №13 \r\nКурсант 102 учебной группы ";
            lTop1.TextAlign = ContentAlignment.TopCenter;
            // 
            // lTop2
            // 
            lTop2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lTop2.AutoSize = true;
            lTop2.BackColor = Color.Transparent;
            lTop2.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lTop2.Location = new Point(516, 30);
            lTop2.Name = "lTop2";
            lTop2.Size = new Size(126, 42);
            lTop2.TabIndex = 4;
            lTop2.Text = "Андреев С.Ю.\r\nГрицюк З.И.\r\n";
            // 
            // lCenter
            // 
            lCenter.Anchor = AnchorStyles.None;
            lCenter.AutoSize = true;
            lCenter.BackColor = Color.Transparent;
            lCenter.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lCenter.Location = new Point(295, 150);
            lCenter.Name = "lCenter";
            lCenter.Size = new Size(74, 22);
            lCenter.TabIndex = 5;
            lCenter.Text = "[TEXT]";
            lCenter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lBottom
            // 
            lBottom.Anchor = AnchorStyles.None;
            lBottom.AutoSize = true;
            lBottom.BackColor = Color.Transparent;
            lBottom.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lBottom.Location = new Point(295, 260);
            lBottom.Name = "lBottom";
            lBottom.Size = new Size(70, 21);
            lBottom.TabIndex = 6;
            lBottom.Text = "[TEXT]";
            lBottom.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbImage
            // 
            pbImage.Dock = DockStyle.Fill;
            pbImage.Location = new Point(0, 0);
            pbImage.Name = "pbImage";
            pbImage.Size = new Size(654, 421);
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImage.TabIndex = 7;
            pbImage.TabStop = false;
            // 
            // ReferenceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(654, 421);
            Controls.Add(lBottom);
            Controls.Add(lCenter);
            Controls.Add(lTop2);
            Controls.Add(lTop1);
            Controls.Add(bPrev);
            Controls.Add(bNext);
            Controls.Add(pbImage);
            Name = "ReferenceForm";
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button bNext;
        private Button bPrev;
        private Label lTop1;
        private Label lTop2;
        private Label lCenter;
        private Label lBottom;
        private PictureBox pbImage;
    }
}