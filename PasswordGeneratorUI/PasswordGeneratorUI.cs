using PasswordGenerator;
using System;
using System.Windows.Forms;

namespace PasswordGeneratorUI
{
    public partial class PasswordGeneratorUI : Form
    {
        public PasswordGeneratorUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = openFileDialog.FileName;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            PwdGenerator pwdGenerator = new PwdGenerator();
            try
            {
                this.txtPassword.Text = pwdGenerator.GeneratePassword(this.txtFileName.Text, this.txtUtf8.Checked);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.txtPassword.Text);
        }
    }
}
