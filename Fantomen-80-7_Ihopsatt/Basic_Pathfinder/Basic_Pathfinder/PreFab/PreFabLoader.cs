using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Basic_Pathfinder.PreFab
{
    public partial class PreFabLoader : Form
    {
        string[] protoFiles;

        public PreFabLoader()
        {
            InitializeComponent();
        }

        private void PreFabLoader_Load(object sender, EventArgs e)
        {
            protoFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dat");
            if (protoFiles.Length < 1 && MessageBox.Show("There are no prefabricated files!\nPlease create at least one before trying to load!", "Warning", MessageBoxButtons.OK) == DialogResult.OK)
                Dispose();

            var viablePreFabs = PreFabUser.ReadAll(protoFiles);
            for (int i = 0; i < viablePreFabs.Length; i++) {
                if (viablePreFabs[i].NodeSize == WorldGeneration.NodeSize) {
                    string fileName = protoFiles[i].Remove(protoFiles[i].IndexOf('.'));
                    ViablePreFabsCmbBox.Items.Add(fileName.Remove(0, fileName.LastIndexOf('\\')+1));
                }
            }
            AcceptButton = LoadBtn;
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            WorldGeneration.RepeatPreFab = repeatCheck.GetItemChecked(0);
            PreFabUser.Load(PreFabUser.Read(protoFiles[ViablePreFabsCmbBox.SelectedIndex]));
            Dispose();
        }
    }
}
