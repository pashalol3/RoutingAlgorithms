using GraphViewer.Forms.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewer.Forms
{
    public class MatrixInputDialog : Form
    {
        private AdjacencyMatrixControl? _matrixControl;
        private Button? _btnOk;
        private Button? _btnCancel;

        public int[,] Matrix => _matrixControl!.GetMatrix();

        public MatrixInputDialog()
        {
            InitializeComponents();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void InitializeComponents()
        {
            _matrixControl = new AdjacencyMatrixControl();
            _btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Size = new Size(80, 30) };
            _btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Size = new Size(80, 30) };

            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var buttonPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };

            buttonPanel.Controls.Add(_btnCancel);
            buttonPanel.Controls.Add(_btnOk);

            tableLayout.Controls.Add(_matrixControl, 0, 0);
            tableLayout.Controls.Add(buttonPanel, 0, 1);

            this.Controls.Add(tableLayout);
            this.Padding = new Padding(10);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}
