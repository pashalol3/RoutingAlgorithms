using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphViewer.Forms
{
    public partial class HelloForm: Form
    {
        public HelloForm()
        {
            InitializeComponent();

            this.bStart.Click += BStart_Click;
            this.bExit.Click += BExit_Click;
        }

        private void BExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BStart_Click(object? sender, EventArgs e)
        {
            var mainForm = new MainForm(this);
            mainForm.Show();
            this.Hide();
        }
    }
}
