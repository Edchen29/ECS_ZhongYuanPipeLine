using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HUAHENG.Project.Ads
{
    public partial class FrmEndbeveling_PC_station : Form
    {
        public FrmEndbeveling_PC_station()
        {
            InitializeComponent();
        }
        private string NetId = "127.255.255.1.1.1";//Control Address
        private TwinCAT.Ads.TcAdsClient adsClient = null;
        private int HandleVar_EndBevelingPCstation = 0;
        private int HandleVar_Finished = 0;
        

        private void AdsInit()
        {
            //Connect 
            adsClient = new TwinCAT.Ads.TcAdsClient();
            adsClient.Connect(NetId, 801);

            //add Variables
            HandleVar_EndBevelingPCstation = adsClient.CreateVariableHandle(".EndBevelingPCstation");
            HandleVar_Finished = adsClient.CreateVariableHandle(".Finished");
           
        }
        private void Endbeveling_PC_station_Load(object sender, EventArgs e)
        {
            AdsInit();
            comBox_BeleveType.SelectedIndex = 1;
            comBox_Beleve.SelectedIndex = 0;
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            bool Result = false;
            try
            {
                //must Read it to sure pipe OK
                //this Variable!=0 then Send to Control
                object value = adsClient.ReadAny(HandleVar_Finished, typeof(bool));
                Result = bool.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (Result)
            {
                EndBevelingPCstation cps = new EndBevelingPCstation();
                cps.MaterialType = tBox_MaterialType.Text;
                cps.OD = (float)Math.Round(decimal.Parse(tBox_OD.Text), 2);
                cps.WT = (float)Math.Round(decimal.Parse(tBox_WT.Text), 2);
                cps.Beleve = comBox_Beleve.Text == "Y" ? true : false;

                cps.BeleveType = (Int16)(comBox_BeleveType.SelectedIndex + 1);
                cps.BeleveAngle = (float)Math.Round(decimal.Parse(tBox_BeleveAngle.Text), 1);

                //send to control

                try
                {
                    //if  Write to Control Error please try again again again...
                    adsClient.WriteAny(HandleVar_EndBevelingPCstation, cps);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Btn_Read_Click(object sender, EventArgs e)
        {
            tBox_Finished.Text = adsClient.ReadAny(HandleVar_Finished, typeof(bool)).ToString();
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class EndBevelingPCstation
    {      
        public float OD;
        public float WT;
        [MarshalAs(UnmanagedType.U1)]
        public bool Beleve;
        public Int16 BeleveType;
        public float BeleveAngle;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string MaterialType;
    }
}