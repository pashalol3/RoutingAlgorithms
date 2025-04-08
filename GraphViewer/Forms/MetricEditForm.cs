using RoutingAlgorithms.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphViewer.Forms
{
    public class MetricEditForm : Form
    {
        private readonly NumericUpDown _txtDelay;
        private readonly NumericUpDown _txtBandWidth;

        public MetricF Metric { get; private set; }

        public MetricEditForm(MetricF initialMetric)
        {
            this.Size = new Size(350, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lDelay = new Label
            {
                Text = "Задержка:",
                Left = 20,
                Top = 50
            };
            _txtDelay = new NumericUpDown
            {
                Left = 200,
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
                Top = 20,
                Size = new(170, 20)

            };
            _txtBandWidth = new NumericUpDown
            {
                Left = 200,
                Top = 20,
                Value = (decimal)initialMetric.BandWidth,
                DecimalPlaces = 1, 
                Increment = 0.1m,  
                Minimum = 0,       
                Maximum = 1000     
            };

            var bOk = new Button
            {
                Text = "ОК",
                Left = 120,
                Top = 80,
                DialogResult = DialogResult.OK
            };
            var btnCancel = new Button
            {
                Text = "Отмена",
                Left = 200,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange([lDelay, _txtDelay, lBandWidth, _txtBandWidth, bOk, btnCancel]);

            bOk.Click += BtnOk_Click;

            btnCancel.Click += (s, e) => this.Close();

            this.KeyDown += MetricEditForm_KeyDown;
            this.KeyPreview = true;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {

            Metric = new MetricF((float)_txtDelay.Value, (float)_txtBandWidth.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void MetricEditForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter) 
            {
                BtnOk_Click(default, default);
            }
        }
    }
}
