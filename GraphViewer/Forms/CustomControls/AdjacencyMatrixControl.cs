using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewer.Forms.CustomControls
{
    public class AdjacencyMatrixControl : UserControl
    {
        private NumericUpDown? _nudSize;
        private DataGridView? _dgvMatrix;

        public AdjacencyMatrixControl()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _nudSize = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 26,
                Value = 1,
                Location = new Point(10, 10),
                Width = 60
            };
            _nudSize.ValueChanged += (s, e) => GenerateMatrix();

            _dgvMatrix = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                RowHeadersVisible = true,
                ColumnHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Location = new Point(10, 40),
                ReadOnly = true,
                ScrollBars = ScrollBars.None,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = SystemColors.Window,

            };
            _dgvMatrix.DefaultCellStyle.SelectionBackColor = SystemColors.Window; 
            _dgvMatrix.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;

            _dgvMatrix.CellClick += DgvMatrix_CellClick;

            Controls.Add(_nudSize);
            Controls.Add(_dgvMatrix);
            GenerateMatrix();
        }

        private void GenerateMatrix()
        {
            if (_dgvMatrix is null) throw new Exception($"{nameof(_dgvMatrix)} is null");
            if (_nudSize is null) throw new Exception($"{nameof(_nudSize)} is null");

            this.SuspendLayout();
            int size = (int)_nudSize.Value;
            _dgvMatrix.Columns.Clear();
            _dgvMatrix.Rows.Clear();

            for (int i = 0; i < size; i++)
            {
                _dgvMatrix.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = ((char)('A' + i)).ToString(),
                    Width = 30,
                    Resizable = DataGridViewTriState.False,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        BackColor = Color.White
                    }
                });
            }

            _dgvMatrix.RowHeadersWidth = 50;

            for (int i = 0; i < size; i++)
            {
                _dgvMatrix.Rows.Add();
                _dgvMatrix.Rows[i].HeaderCell.Value = ((char)('A' + i)).ToString();
                _dgvMatrix.Rows[i].Height = 30;

                var diagonalCell = _dgvMatrix.Rows[i].Cells[i];
                diagonalCell.Value = "-";
                diagonalCell.ReadOnly = true; 
            }

            int headerWidth = _dgvMatrix.RowHeadersWidth + 2;
            int headerHeight = _dgvMatrix.ColumnHeadersHeight + 2;

            _dgvMatrix.Width = size * 30 + headerWidth;
            _dgvMatrix.Height = size * 30 + headerHeight;

            this.Size = new Size(
                Math.Max(_dgvMatrix.Right + 10, _nudSize.Right + 10),
                _dgvMatrix.Bottom + 10
            );

            this.ResumeLayout();
        }



        private void DgvMatrix_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_dgvMatrix is null) throw new Exception($"{nameof(_dgvMatrix)} is null");
            if (_nudSize is null) throw new Exception($"{nameof(_nudSize)} is null");

            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.RowIndex == e.ColumnIndex) return; 

            var cell = _dgvMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.Value = cell.Value?.ToString() == "1" ? null : "1";

            _dgvMatrix.Rows[e.ColumnIndex].Cells[e.RowIndex].Value = cell.Value;
        }


        public int[,] GetMatrix()
        {
            if (_dgvMatrix is null) throw new Exception($"{nameof(_dgvMatrix)} is null");
            if (_nudSize is null) throw new Exception($"{nameof(_nudSize)} is null");

            int size = (int)_nudSize.Value;
            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var value = _dgvMatrix.Rows[i].Cells[j].Value;
                    matrix[i, j] = value != null && value.ToString() == "1" ? 1 : 0;
                }
            }

            return matrix;
        }
    }
}
