namespace GraphViewer.Forms
{
    public partial class CustomDialog: Form
    {
        public CustomDialog(string msg)
        {
            InitializeComponent();
            lText.Text = msg;
            this.Height = lText.Height + 80;
            this.Width = lText.Width + 80;
            this.KeyDown += (s, e) => 
            {
                if(e.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

   
    }
}
