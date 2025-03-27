namespace GraphViewer.Forms
{
    public partial class HelloForm : Form
    {
        private const string IMAGE_PATH = "image.jpg";
        public HelloForm()
        {
            InitializeComponent();

            this.bStart.Click += (s, e) =>
            {
                new MainForm(this).Show();
                this.Hide();
            };
            this.Load += (s, e) => pImage.Image = File.Exists(IMAGE_PATH) ? Image.FromFile(IMAGE_PATH) : Properties.Resources.image;
            this.bExit.Click += (s, e) => Application.Exit();
        }
    }
}
