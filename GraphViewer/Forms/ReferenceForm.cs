using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GraphViewer.Forms
{
    public partial class ReferenceForm : Form
    {
        private const string IMAGE_FOLDER = "assets";
        private const string IMAGE_NAME = "2";
        private readonly string[] IMAGE_EXTENSIONS = [".jpg", ".jpeg", ".png"];
        private readonly List<System.Windows.Forms.Label> _labels = [];
        private int _index = 0;
        private readonly Form _helloForm;
        private record TextView(string Title, string Text);

        private readonly List<TextView> _pages = [
            new ("Информация о программе",
                "Программа позволяет продемонстрировать работу алгоритма Дейкстры\nи алгоритма адаптивной маршрутизации с учетом ограничений\nпо пропускной способности и задержки в каналах связи."),
            new("Принципы работы",
                "1.Задать значения пропускной способности и задержки для каждого канала связи.\n" +
                "2.Задать требования по пропускной способности и задержки.\n" +
                "3. Выбрать узел истока.\n" +
                "4. Выбрать узел стока.\n" +
                "5. Нажать на узел стока.\n" +
                "6. Нажать на узел стока.\n" +
                "7. Нажать на узел истока.\n" +
                "8. Выбрать следующую пару узлов."),
            new ("Область применения",
                 "Данная программа может быть применена\nна рабочем месте администратора сети.")];

        public ReferenceForm(Form helloForm)
        {
            InitializeComponent();
            _labels.AddRange(Controls.OfType<System.Windows.Forms.Label>());
            _helloForm = helloForm;
            bNext.Click += BNext_Click;
            bPrev.Click += BPrev_Click;
            this.FormClosing += ReferenceForm_FormClosing;
            this.Load += ReferenceForm_Load;
            this.Resize += ReferenceForm_Resize;
        }

        private void ReferenceForm_Resize(object? sender, EventArgs e)
        {
            foreach (var label in _labels)
            {
                label.Font = new Font(label.Font.FontFamily, MathF.Sqrt(this.Width * this.Height) / 40f, label.Font.Style);
            }
            const int topMargin = 50;
            lTop2.Location = new(Width - lTop2.Width - topMargin, lTop2.Location.Y);
            lCenter.Location = new(this.Width / 2 - lCenter.Width / 2, this.Height * 1 / 3 - lCenter.Height / 2);

            lBottom.Location = new(this.Width / 2 - lBottom.Width / 2, lCenter.Location.Y + (int)MathF.Sqrt(this.Height) * 3);


        }

        private void SetText(TextView text)
        {
            lCenter.Text = text.Title;
            lCenter.Location = new Point(this.Size.Width / 2 - lCenter.Width / 2, lCenter.Location.Y);

            lBottom.Text = text.Text;
            lBottom.Location = new Point(this.Size.Width / 2 - lBottom.Width / 2, lBottom.Location.Y);
        }

        private void ReferenceForm_Load(object? sender, EventArgs e)
        {
            SetText(_pages.First());

            Controls.OfType<System.Windows.Forms.Label>().ToList().ForEach(l =>
            {
                l.Parent = pbImage;
                l.BackColor = Color.Transparent;
                l.ForeColor = Color.Black;

            });

            ReferenceForm_Resize(default, default!);

            string? imagePath = null;

            foreach (var extension in IMAGE_EXTENSIONS)
            {
                string potentialPath = Path.Combine(IMAGE_FOLDER, IMAGE_NAME + extension);
                if (File.Exists(potentialPath))
                {
                    imagePath = potentialPath;
                    break;
                }
            }

            pbImage.Image = imagePath != null ? Image.FromFile(imagePath) : Properties.Resources.image;
        }

        private void ReferenceForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _helloForm.Show();
        }

        private void BPrev_Click(object? sender, EventArgs e)
        {
            _index--;
            if (_index < 0)
            {
                this.Close();
                _helloForm.Show();
            }
            else
            {
                SetText(_pages[_index]);
                bNext.Visible = _index != _pages.Count - 1;
                lTop1.Visible = _index == 0;
                lTop2.Visible = _index == 0;
            }
        }

        private void BNext_Click(object? sender, EventArgs e)
        {
            _index = Math.Min(_index + 1, _pages.Count - 1);
            SetText(_pages[_index]);
            bNext.Visible = _index != _pages.Count - 1;
            lTop1.Visible = _index == 0;
            lTop2.Visible = _index == 0;
        }
    }
}
