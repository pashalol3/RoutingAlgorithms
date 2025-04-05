namespace GraphViewer.Forms
{
    public partial class HelloForm : Form
    {
        private const string IMAGE_FOLDER = "assets";
        private const string IMAGE_NAME = "1";
        private readonly string[] IMAGE_EXTENSIONS = [".jpg", ".jpeg", ".png"];

        public HelloForm()
        {
            InitializeComponent();

            this.bStart.Click += (s, e) =>
            {
                new MainForm(this).Show();
                this.Hide();
            };
            this.bInfo.Click += (s, e) => 
            {
                new ReferenceForm(this).Show();
                this.Hide();
            };
            this.Load += (s, e) =>
            {
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
            };

            this.bExit.Click += (s, e) => Application.Exit();
        }
    }
}
