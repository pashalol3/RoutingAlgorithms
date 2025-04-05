using RoutingAlgorithms.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphViewer.Forms
{
    public class MetricEditForm : Form
    {
        private NumericUpDown txtDelay;
        private NumericUpDown txtBandWidth;

        public MetricF Metric { get; private set; }

        public MetricEditForm(MetricF initialMetric)
        {
            this.Size = new Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lDelay = new Label
            {
                Text = "Задержка:",
                Left = 20,
                Top = 20
            };
            txtDelay = new NumericUpDown
            {
                Left = 120,
                Top = 50,
                Value = (decimal)initialMetric.Delay,
                DecimalPlaces = 1, 
                Increment = 0.1m,  
                Minimum = 0,       
                Maximum = 1000     
            };

            var lBandWidth = new Label
            {
                Text = "Пропусная способность:",
                Left = 20,
                Top = 50
            };
            txtBandWidth = new NumericUpDown
            {
                Left = 120,
                Top = 20,
                Value = (decimal)initialMetric.BandWidth,
                DecimalPlaces = 1, 
                Increment = 0.1m,  
                Minimum = 0,       
                Maximum = 1000     
            };

            var btnOk = new Button
            {
                Text = "OK",
                Left = 120,
                Top = 80,
                DialogResult = DialogResult.OK
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                Left = 200,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lDelay, txtDelay, lBandWidth, txtBandWidth, btnOk, btnCancel });

            btnOk.Click += BtnOk_Click;

            btnCancel.Click += (s, e) => this.Close();

            this.KeyDown += MetricEditForm_KeyDown;
            this.KeyPreview = true;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {

            Metric = new MetricF((float)txtDelay.Value, (float)txtBandWidth.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void MetricEditForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
