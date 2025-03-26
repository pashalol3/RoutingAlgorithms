using RoutingAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphViewer
{
    public partial class InputWeight: Form
    {
        public float Delay{ get; private set; }
        public float BandWidth { get; private set; }
        public string EdgeName { get; init; }
        public (Node start, Node end) HoveredEdge { get; private set; }
        public Action RerenderCallback{ get; init; }
        public InputWeight((Node start, Node end) hoveredEdge , Action callback)
        {
            InitializeComponent();

            RerenderCallback = callback;
            Delay = hoveredEdge.start.Neighbors[hoveredEdge.end].Delay;
            BandWidth = hoveredEdge.start.Neighbors[hoveredEdge.end].BandWidth;
            EdgeName = hoveredEdge.ToString();
            HoveredEdge = hoveredEdge;

            this.Text = EdgeName;
            this.tbDelay.TextChanged += TbDelay_TextChanged;
            this.tbBandwidth.TextChanged += TbBandwidth_TextChanged;
            this.tbDelay.KeyPress += TbDelay_KeyPress;
            this.tbBandwidth.KeyPress += TbBandwidth_KeyPress;
            this.bApply.Click += BApply_Click;

            tbBandwidth.Text = BandWidth.ToString();
            tbDelay.Text = Delay.ToString();

        }


        private void BApply_Click(object? sender, EventArgs e)
        {
            HoveredEdge.end.Neighbors[HoveredEdge.start] = new MetricF(Delay, BandWidth);
            HoveredEdge.start.Neighbors[HoveredEdge.end] = new MetricF(Delay, BandWidth);
            RerenderCallback?.Invoke();
            this.Close();
        }

        private void TbBandwidth_TextChanged(object? sender, EventArgs e)
        {
            var culture = CultureInfo.CurrentCulture;
            var input = tbBandwidth.Text.Replace('.',',');

            if (float.TryParse(input, NumberStyles.Float, culture, out float bandwidth))
            {
                BandWidth = bandwidth;
            }
            else
            {
                // Обработка некорректного значения
                // Например, можно очистить текстовое поле
                // TbBandwidth.Text = string.Empty;
            }
        }

        private void TbDelay_TextChanged(object? sender, EventArgs e)
        {
            var culture = CultureInfo.CurrentCulture;
            var input = tbDelay.Text.Replace('.', ','); ;

            if (float.TryParse(input, NumberStyles.Float, culture, out float delay))
            {
                Delay = delay;
            }
            else
            {
                // Обработка некорректного значения
                // Например, можно очистить текстовое поле
                // TbDelay.Text = string.Empty;
            }
        }

        private void TbBandwidth_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void TbDelay_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
    }
}
