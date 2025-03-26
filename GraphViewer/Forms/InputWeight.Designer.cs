namespace GraphViewer
{
    partial class InputWeight
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
            label1 = new Label();
            tbDelay = new TextBox();
            label2 = new Label();
            tbBandwidth = new TextBox();
            bApply = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(26, 15);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 0;
            label1.Text = "Задержка:";
            // 
            // tbDelay
            // 
            tbDelay.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tbDelay.Location = new Point(248, 11);
            tbDelay.Name = "tbDelay";
            tbDelay.Size = new Size(100, 29);
            tbDelay.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(26, 54);
            label2.Name = "label2";
            label2.Size = new Size(220, 21);
            label2.TabIndex = 2;
            label2.Text = "Пропускная способность:";
            // 
            // tbBandwidth
            // 
            tbBandwidth.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tbBandwidth.Location = new Point(248, 50);
            tbBandwidth.Name = "tbBandwidth";
            tbBandwidth.Size = new Size(100, 29);
            tbBandwidth.TabIndex = 3;
            // 
            // bApply
            // 
            bApply.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bApply.Location = new Point(26, 86);
            bApply.Name = "bApply";
            bApply.Size = new Size(322, 30);
            bApply.TabIndex = 4;
            bApply.Text = "Применить";
            bApply.UseVisualStyleBackColor = true;
            // 
            // InputWeight
            // 
            AcceptButton = bApply;
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = bApply;
            ClientSize = new Size(384, 131);
            Controls.Add(bApply);
            Controls.Add(tbBandwidth);
            Controls.Add(label2);
            Controls.Add(tbDelay);
            Controls.Add(label1);
            Font = new Font("Symbol Tiger Expert", 12F, FontStyle.Regular, GraphicsUnit.Point, 2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputWeight";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbDelay;
        private Label label2;
        private TextBox tbBandwidth;
        private Button bApply;
    }
}