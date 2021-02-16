using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Mod
{
    [ImplementPropertyChanged]
    public class MaterialGrid
    {
        public int id { get; set; }
        public string WaiJing { get; set; }
        public string BiHou { get; set; }
        public string CaiZhi { get; set; }
        public string Dengji { get; set; }
        public int length { get; set; }
        public string LuPiHao { get; set; }
        public string WuLiaoHao { get; set; }
    
        public void clear()
        {
            WaiJing = null;
            BiHou = null;
            CaiZhi = null;
            Dengji = null;
            length = 0;
            LuPiHao = null;
            WuLiaoHao = null;
            //ZuDui = "0";//cf
        }
    }
}
