using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Pathfinder.PreFab
{
    public partial class PreFabSaver : Form
    {
        public PreFabSaver()
        {
            InitializeComponent();
        }

        private void PreFabSaver_Load(object sender, EventArgs e) => AcceptButton = SaveBtn;

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileNameTbx.Text))
                MessageBox.Show("The file name can not be empty, please write a name.", "Warning!", MessageBoxButtons.OK);
            else {
                PreFabUser.Write(fileNameTbx.Text + ".dat");
                Dispose();
            }
        }
    }
}
