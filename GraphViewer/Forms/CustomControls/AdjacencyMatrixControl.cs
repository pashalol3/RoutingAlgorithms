using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewer.Forms.CustomControls
{
    public class AdjacencyMatrixControl : UserControl
    {
        private NumericUpDown nudSize;
        private DataGridView dgvMatrix;

        public AdjacencyMatrixControl()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Настройка NumericUpDown
            nudSize = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 10,
                Value = 1,
                Location = new Point(10, 10),
                Width = 60
            };
            nudSize.ValueChanged += (s, e) => GenerateMatrix();

            // Настройка DataGridView
            dgvMatrix = new DataGridView
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
                BackgroundColor = SystemColors.Window
            };
            dgvMatrix.CellClick += DgvMatrix_CellClick;

            Controls.Add(nudSize);
            Controls.Add(dgvMatrix);
            GenerateMatrix();
        }

        private void GenerateMatrix()
        {
            this.SuspendLayout();

            int size = (int)nudSize.Value;
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();

            // Создание столбцов
            for (int i = 0; i < size; i++)
            {
                dgvMatrix.Columns.Add(new DataGridViewTextBoxColumn
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

            // Создание строк
            for (int i = 0; i < size; i++)
            {
                dgvMatrix.Rows.Add();
                dgvMatrix.Rows[i].HeaderCell.Value = ((char)('A' + i)).ToString();
                dgvMatrix.Rows[i].Height = 30;
            }

            // Расчет размеров
            int headerWidth = dgvMatrix.RowHeadersWidth + 2;
            int headerHeight = dgvMatrix.ColumnHeadersHeight + 2;

            dgvMatrix.Width = size * 30 + headerWidth;
            dgvMatrix.Height = size * 30 + headerHeight;

            // Обновление размеров контрола
            this.Size = new Size(
                Math.Max(dgvMatrix.Right + 10, nudSize.Right + 10),
                dgvMatrix.Bottom + 10
            );

            this.ResumeLayout();
        }

        private void DgvMatrix_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.RowIndex == e.ColumnIndex) return;

            var cell = dgvMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.Value = cell.Value?.ToString() == "1" ? null : "1"; 

            dgvMatrix.Rows[e.ColumnIndex].Cells[e.RowIndex].Value = cell.Value;
        }

        public int[,] GetMatrix()
        {
            int size = (int)nudSize.Value;
            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var value = dgvMatrix.Rows[i].Cells[j].Value;
                    matrix[i, j] = value != null && value.ToString() == "1" ? 1 : 0;
                }
            }

            return matrix;
        }
    }
}
