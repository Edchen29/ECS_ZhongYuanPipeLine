using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HUAHENG.Project.Ads
{
    public partial class FrmCutting_PC_station : Form
    {
        public FrmCutting_PC_station()
        {
            InitializeComponent();
        }

        private void FrmCutting_PC_station_Load(object sender, EventArgs e)
        {
            try
            {
                AdsInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string NetId = "127.255.255.1.1.1";//Control Address
        //private string NetId = "192.168.255.105.1.1";
        private TwinCAT.Ads.TcAdsClient adsClient = null;
        private int HandleVar_CuttingPCstation = 0;
        private int HandleVar_Finished1 = 0;
        private int HandleVar_Finished2 = 0;
        private int HandleVar_Finished3 = 0;
        private int HandleVar_Finished4 = 0;

        private void AdsInit()
        {
            //Connect 
            adsClient = new TwinCAT.Ads.TcAdsClient();
            adsClient.Connect(NetId, 801);

            //add Variables
            HandleVar_CuttingPCstation = adsClient.CreateVariableHandle(".CuttingPCstation");
            HandleVar_Finished1 = adsClient.CreateVariableHandle(".Finished1");
            HandleVar_Finished2 = adsClient.CreateVariableHandle(".Finished2");
            HandleVar_Finished3 = adsClient.CreateVariableHandle(".Finished3");
            HandleVar_Finished4 = adsClient.CreateVariableHandle(".Finished4");
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            int Result = 0;
            try
            {
                //must Read it to sure pipe OK
                //this Variable!=0 then Send to Control
                object value=adsClient.ReadAny(HandleVar_Finished4, typeof(Int16));
                Result = int.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (Result != 0)
            {
                CuttingPCstation cps = new CuttingPCstation();
                cps.MaterialType = tBox_MaterialType.Text;
                cps.OD = (float)Math.Round(decimal.Parse(tBox_OD.Text),2);
                cps.Pipe1 = Int16.Parse(tBox_Pipe1.Text);
                cps.Pipe2 = Int16.Parse(tBox_Pipe2.Text);
                cps.Pipe3 = Int16.Parse(tBox_Pipe3.Text);
                cps.Pipe4 = Int16.Parse(tBox_Pipe4.Text);
                cps.PipeCount = Int16.Parse(tBox_PipeCount.Text);
                cps.RawPipeLength = Int16.Parse(tBox_RawPipeLength.Text);
                cps.WT = (float)Math.Round(decimal.Parse(tBox_WT.Text), 2);
                //send to control
                
                try
                {
                    //if  Write to Control Error please try again again again...
                    adsClient.WriteAny(HandleVar_CuttingPCstation, cps);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Btn_Read_Click(object sender, EventArgs e)
        {
            tBox_Finished1.Text = adsClient.ReadAny(HandleVar_Finished1, typeof(Int16)).ToString();
            tBox_Finished2.Text = adsClient.ReadAny(HandleVar_Finished2, typeof(Int16)).ToString();
            tBox_Finished3.Text = adsClient.ReadAny(HandleVar_Finished3, typeof(Int16)).ToString();
            tBox_Finished4.Text = adsClient.ReadAny(HandleVar_Finished4, typeof(Int16)).ToString();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CuttingPCstation
    {
        public Int16 RawPipeLength;
        public float OD;
        public float WT;
        public Int16 PipeCount;
        public Int16 Pipe1;
        public Int16 Pipe2;
        public Int16 Pipe3;
        public Int16 Pipe4;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string MaterialType;
    }
}
