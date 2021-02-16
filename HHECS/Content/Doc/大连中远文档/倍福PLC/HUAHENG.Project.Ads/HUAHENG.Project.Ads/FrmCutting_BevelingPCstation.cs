using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HUAHENG.Project.Ads
{
    public partial class FrmCutting_BevelingPCstation : Form
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CuttingBevelingPCstation
        {
            public Int16 RawPipeLength;
            public float OD;
            public float WT;
            public Int16 PipeCount;
            public Int16 Pipe1;
            public Int16 Pipe2;
            public Int16 Pipe3;
            public Int16 Pipe4;
            public Int16 Pipe5;
            public Int16 Pipe6;
            public Int16 Pipe7;
            public Int16 Pipe8;
            public Int16 Pipe9;
            public Int16 Pipe10;

            [MarshalAs(UnmanagedType.U1)]
            public bool Beveled;
            public Int16 BevelType;
            public float BevelAngle;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
            public string MaterialType;
        }

        private readonly string NetId = "5.18.191.254.1.1"; //Control Address, please adjust
        private TwinCAT.Ads.TcAdsClient adsClient = null;
        private int HandleVar_CuttingBevelingPCstation = 0;
        private int HandleVar_Finished1 = 0;
        private int HandleVar_Finished2 = 0;
        private int HandleVar_Finished3 = 0;
        private int HandleVar_Finished4 = 0;
        private int HandleVar_Finished5 = 0;
        private int HandleVar_Finished6 = 0;
        private int HandleVar_Finished7 = 0;
        private int HandleVar_Finished8 = 0;
        private int HandleVar_Finished9 = 0;
        private int HandleVar_Finished10 = 0;

        public FrmCutting_BevelingPCstation()
        {
            InitializeComponent();
        }

        private void AdsInit()
        {
            try
            {
                // Connect 
                adsClient = new TwinCAT.Ads.TcAdsClient();
                adsClient.Connect(NetId, 801);

                // Add Variables
                HandleVar_CuttingBevelingPCstation = adsClient.CreateVariableHandle(".CuttingBevelingPCstation");
                HandleVar_Finished1 = adsClient.CreateVariableHandle(".Finished1");
                HandleVar_Finished2 = adsClient.CreateVariableHandle(".Finished2");
                HandleVar_Finished3 = adsClient.CreateVariableHandle(".Finished3");
                HandleVar_Finished4 = adsClient.CreateVariableHandle(".Finished4");
                HandleVar_Finished5 = adsClient.CreateVariableHandle(".Finished5");
                HandleVar_Finished6 = adsClient.CreateVariableHandle(".Finished6");
                HandleVar_Finished7 = adsClient.CreateVariableHandle(".Finished7");
                HandleVar_Finished8 = adsClient.CreateVariableHandle(".Finished8");
                HandleVar_Finished9 = adsClient.CreateVariableHandle(".Finished9");
                HandleVar_Finished10 = adsClient.CreateVariableHandle(".Finished10");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }
        }

        private void FrmCutting_BevelingPCstation_Load(object sender, EventArgs e)
        {
            comBox_BevelType.SelectedIndex = 1;
            comBox_Beveled.SelectedIndex = 0;
            AdsInit();            
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                // Must Read it to sure pipe OK
                // This Variable!=0 then Send to Control
                object value = adsClient.ReadAny(HandleVar_Finished10, typeof(Int16));
                var result = int.Parse(value.ToString());

                if (result != 0)
                {
                    CuttingBevelingPCstation cps = new CuttingBevelingPCstation()
                    {
                        MaterialType = tBox_MaterialType.Text,
                        OD = (float)Math.Round(decimal.Parse(tBox_OD.Text), 2),
                        Pipe1 = Int16.Parse(tBox_Pipe1.Text),
                        Pipe2 = Int16.Parse(tBox_Pipe2.Text),
                        Pipe3 = Int16.Parse(tBox_Pipe3.Text),
                        Pipe4 = Int16.Parse(tBox_Pipe4.Text),
                        Pipe5 = Int16.Parse(tBox_Pipe5.Text),
                        Pipe6 = Int16.Parse(tBox_Pipe6.Text),
                        Pipe7 = Int16.Parse(tBox_Pipe7.Text),
                        Pipe8 = Int16.Parse(tBox_Pipe8.Text),
                        Pipe9 = Int16.Parse(tBox_Pipe9.Text),
                        Pipe10 = Int16.Parse(tBox_Pipe10.Text),
                        PipeCount = Int16.Parse(tBox_PipeCount.Text),
                        RawPipeLength = Int16.Parse(tBox_RawPipeLength.Text),
                        WT = (float)Math.Round(decimal.Parse(tBox_WT.Text), 2),
                        Beveled = comBox_Beveled.Text == "Y" ? true : false,
                        BevelType = (Int16)(comBox_BevelType.SelectedIndex + 1),
                        BevelAngle = (float)Math.Round(decimal.Parse(tBox_BevelAngle.Text), 1),
                    };

                    // Send to control
                    // If  write to Control error please try again again again...
                    adsClient.WriteAny(HandleVar_CuttingBevelingPCstation, cps);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Send error: {ex.Message}");
            }
        }

        private void Btn_Read_Click(object sender, EventArgs e)
        {
            try
            {
                tBox_Finished1.Text = adsClient.ReadAny(HandleVar_Finished1, typeof(Int16)).ToString();
                tBox_Finished2.Text = adsClient.ReadAny(HandleVar_Finished2, typeof(Int16)).ToString();
                tBox_Finished3.Text = adsClient.ReadAny(HandleVar_Finished3, typeof(Int16)).ToString();
                tBox_Finished4.Text = adsClient.ReadAny(HandleVar_Finished4, typeof(Int16)).ToString();
                tBox_Finished5.Text = adsClient.ReadAny(HandleVar_Finished5, typeof(Int16)).ToString();
                tBox_Finished6.Text = adsClient.ReadAny(HandleVar_Finished6, typeof(Int16)).ToString();
                tBox_Finished7.Text = adsClient.ReadAny(HandleVar_Finished7, typeof(Int16)).ToString();
                tBox_Finished8.Text = adsClient.ReadAny(HandleVar_Finished8, typeof(Int16)).ToString();
                tBox_Finished9.Text = adsClient.ReadAny(HandleVar_Finished9, typeof(Int16)).ToString();
                tBox_Finished10.Text = adsClient.ReadAny(HandleVar_Finished10, typeof(Int16)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Read error: {ex.Message}");
            }
        }

        private void TBox_RawPipeLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBox_Finished1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}