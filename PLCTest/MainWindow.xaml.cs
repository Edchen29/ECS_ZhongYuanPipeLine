using HHECS.Bll;
using HHECS.Model.Entities;
using HHECS.Model.PLCHelper.Implement;
using HHECS.Model.PLCHelper.Interfaces;
using HHECS.Model.PLCHelper.PLCComponent;
using HHECS.Model.PLCHelper.PLCComponent.Sharp7Component;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLCTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Equipment> Equipments { get; set; }
        IPLC plc = null;
        public MainWindow()
        {
            InitializeComponent();
            //Init();
        }

        private void Init()
        {
            var result1 = AppSession.Dal.GetCommonModelByCondition<Equipment>($"");
            var result2 = AppSession.Dal.GetCommonModelByCondition<EquipmentProp>("");
            var result3 = AppSession.Dal.GetCommonModelByCondition<EquipmentType>("");
            var result4 = AppSession.Dal.GetCommonModelByCondition<EquipmentTypeTemplate>("");
            if (!result1.Success || !result2.Success || !result3.Success || !result4.Success)
            {
                MessageBox.Show("初始化设备信息异常");
                return;
            }

            Equipments = result1.Data;
            var EquipmentProps = result2.Data.Where(t => Equipments.Count(a => a.Id == t.EquipmentId) > 0).ToList();
            var EquipmentTypes = result3.Data.Where(t => Equipments.Count(a => a.EquipmentTypeId == t.Id) > 0).ToList();
            var EquipmentTypeTemplates = result4.Data.Where(t => EquipmentTypes.Count(a => a.Id == t.EquipmentTypeId) > 0).ToList();
            EquipmentProps.ForEach(t => { t.Equipment = Equipments.Find(a => a.Id == t.EquipmentId); t.EquipmentTypeTemplate = EquipmentTypeTemplates.Find(a => a.Id == t.EquipmentTypeTemplateId); });

            //组合逻辑外键
            Equipments.ForEach(t =>
            {
                t.EquipmentType = EquipmentTypes.FirstOrDefault(i => i.Id == t.EquipmentTypeId);
                t.EquipmentProps.AddRange(EquipmentProps.Where(i => i.EquipmentId == t.Id).ToList());
            });

            Equipments = Equipments.Where(t => t.Code.StartsWith("PLC")).ToList();

        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Read();
            });
            //Task.Run(() =>
            //{
            //    Read();
            //});
            //Task.Run(() =>
            //{

            //    Read();
            //}
            //);
        }

        private void Read()
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 1000000; i++)
            {
                stopwatch.Restart();
                var a = plc.Reads(Equipments.SelectMany(t => t.EquipmentProps).ToList());
                stopwatch.Stop();
                if (a.Success)
                {
                    Console.WriteLine($"线程：{Thread.CurrentThread.ManagedThreadId}，读取次数：{i}，耗时：{stopwatch.ElapsedMilliseconds.ToString()}，{string.Join("||", Equipments.Select(t => t.EquipmentProps).Select(t => $"设备：{t[0].Equipment.Code}，读取值：{string.Join(",", t.Select(n => n.Value))}").ToList())}");
                }
                else
                {
                    MessageBox.Show(a.Msg);
                    return;
                }
            }
        }

        private void Write()
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 1000000; i++)
            {
                stopwatch.Restart();
                var prop7 = Equipments.SelectMany(t => t.EquipmentProps).Where(t => t.EquipmentTypeTemplateCode == "Prop7").ToList();
                var prop8 = Equipments.SelectMany(t => t.EquipmentProps).Where(t => t.EquipmentTypeTemplateCode == "Prop8").ToList();
                var prop9 = Equipments.SelectMany(t => t.EquipmentProps).Where(t => t.EquipmentTypeTemplateCode == "Prop9").ToList();
                prop7.ForEach(t => t.Value = i.ToString());
                prop8.ForEach(t =>
                {
                    if (t.Value == "True")
                    {
                        t.Value = "False";
                    }
                    else
                    {
                        t.Value = "True";
                    }
                });
                prop9.ForEach(t => t.Value = "HelloWorld");
                var a = plc.Writes(Equipments.SelectMany(t => t.EquipmentProps).ToList());
                stopwatch.Stop();
                if (a.Success)
                {
                    Console.WriteLine($"线程：{Thread.CurrentThread.ManagedThreadId}，写入次数：{i}，耗时：{stopwatch.ElapsedMilliseconds.ToString()}，{string.Join("||", Equipments.Select(t => t.EquipmentProps).Select(t => $"设备：{t[0].Equipment.Code}，写入值：{string.Join(",", t.Select(n => n.Value))}").ToList())}");
                }
                else
                {
                    MessageBox.Show(a.Msg);
                    return;
                }
            }
        }

        private void BtnWrite_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Write();
            });
            //Task.Run(() =>
            //{
            //    Write();
            //});
            //Task.Run(() =>
            //{
            //    Write();
            //});
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            List<S7PLCHelper> list = new List<S7PLCHelper>();
            foreach (var item in Equipments)
            {
                list.Add(new S7PLCHelper()
                {
                    PLCIP = item.IP,
                    PLCType = PLCType.S7_1500.ToString()
                });
            }
            plc?.DisConnect();
            plc = new S7Implement()
            {
                S7PLCHelpers = list
            };
            var temp = plc.Connect();
            if (temp.Success)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show($"连接失败：{temp.Msg}");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            plc?.DisConnect();
        }

        private void BtnOPCConnect_Click(object sender, RoutedEventArgs e)
        {
            plc = new OPCImplement("192.168.10.3")
            {
                Equipments = Equipments
            };
            plc?.DisConnect();
            var temp = plc.Connect();
            if (temp.Success)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show($"连接失败：{temp.Msg}");
            }
        }

        private void BtnPrinter_Click(object sender, RoutedEventArgs e)
        {
            string ip = "127.0.0.1";
            var data = AppSession.PrinterService.ConversionData("01", "0001", "0011", "", "");
            var res = AppSession.PrinterService.SendPrinterData(ip, data.Data.DataSplitting);
            if (res.Success)
            {
                MessageBox.Show($"成功：{res.Msg}");
            }
            else
            {
                MessageBox.Show($"失败:{res.Msg}");
            }
        }
    }
}
