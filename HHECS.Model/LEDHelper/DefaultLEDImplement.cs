using HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.LEDHelper
{
    /// <summary>
    /// LED的默认实现
    /// 注意将led5KSDK.dll拷贝到输出目录
    /// </summary>
    public class DefaultLEDImplement : ILED
    {
        public Dictionary<string, Queue<string>> MQs { get; set; } = new Dictionary<string, Queue<string>>();

        public List<LED> LEDs { get; set; } = new List<LED>();

        public List<LEDCreateOption> Options { get; set; }

        public DefaultLEDImplement(List<LEDCreateOption> options)
        {
            Options = options;
            foreach (var item in options)
            {
                LEDs.Add(new LED(item.IP, item.Port, item.Timesec));
            }
        }

        public void BeginSendInfo()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    for (int i = 0; i < LEDs.Count(); i++)
                    {
                        await Task.Delay(500);
                        if (LEDs[i] != null)
                        {
                            if (MQs.TryGetValue(LEDs[i].LEDIP, out Queue<string> mq))
                            {
                                if (mq.Count > 0)
                                {
                                    string msg = mq.Dequeue();
                                    int n = LEDs[i].SendLedInfo(msg);
                                    if (i != 0)
                                    {
                                        var option = Options.FirstOrDefault(t => t.IP == LEDs[i].LEDIP);
                                        if (option != null)
                                        {
                                            LEDs[i] = new LED(option.IP, option.Port, option.Timesec);
                                            LEDs[i].SendLedInfo(msg);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        public void Push<T>(string key, string msg, T t)
        {
            throw new NotImplementedException();
        }

        public void Push(string key, string msg)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            if (MQs.TryGetValue(key, out Queue<string> mq))
            {
                mq.Enqueue(msg);
            }
            else
            {
                Queue<string> q = new Queue<string>();
                q.Enqueue(msg);
                MQs.Add(key, q);
            }

        }
    }
}
