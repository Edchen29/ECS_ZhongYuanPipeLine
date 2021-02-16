using System;
using System.Windows.Forms;

namespace HUAHENG.Project.Ads
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Btn_CuttingPcStation_Click(object sender, EventArgs e)
        {
            using (FrmCutting_PC_station frmCps = new FrmCutting_PC_station())
            {
                frmCps.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FrmCutting_BevelingPCstation frmCBS = new FrmCutting_BevelingPCstation())
            {
                frmCBS.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FrmEndbeveling_PC_station frmEndS = new FrmEndbeveling_PC_station())
            {
                frmEndS.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FrmFitup_Welding_PC_station frmFWS = new FrmFitup_Welding_PC_station())
            {
                frmFWS.ShowDialog();
            }
        }
    }
}