using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HUAHENG.Project.Ads
{
    public partial class FrmFitup_Welding_PC_station : Form
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class FitupWeldingPCstation
        {
            public float OD;
            public float WT;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
            public string MaterialType;
        }

        private string NetId = "127.255.255.1.1.1";//Control Address
        private TwinCAT.Ads.TcAdsClient adsClient = null;
        private int HandleVar_FitupWeldingPCstation = 0;

        public FrmFitup_Welding_PC_station()
        {
            InitializeComponent();
        }

        private void FrmFitup_Welding_PC_station_Load(object sender, EventArgs e)
        {
            AdsInit();
        }
        
        private void AdsInit()
        {
            //Connect 
            adsClient = new TwinCAT.Ads.TcAdsClient();
            adsClient.Connect(NetId, 801);

            //add Variables
            HandleVar_FitupWeldingPCstation = adsClient.CreateVariableHandle(".FitupWeldingPCstation");
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            FitupWeldingPCstation cps = new FitupWeldingPCstation();
            cps.MaterialType = tBox_MaterialType.Text;
            cps.OD = (float)Math.Round(decimal.Parse(tBox_OD.Text), 2);
            cps.WT = (float)Math.Round(decimal.Parse(tBox_WT.Text), 2);

            //send to control

            try
            {
                //if  Write to Control Error please try again again again...
                adsClient.WriteAny(HandleVar_FitupWeldingPCstation, cps);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }    
}
