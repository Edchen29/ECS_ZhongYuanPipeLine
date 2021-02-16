using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Mod
{
    [ImplementPropertyChanged]
    public class ResultGrid
    {
        public bool ischeck { get; set; }

        public int id { get; set; }
        public string GWNo { get; set; }
        public string OD { get; set; }
        public string WT { get; set; }
        public string MT { get; set; }
        public string YLLth { get; set; }
        //public int[] length = new int[15] { get; set; }
        //public List<int> length = new List<int>();
        public int length1 { get; set; }
        public int length2 { get; set; }
        public int length3 { get; set; }
        public int length4 { get; set; }
        public int length5 { get; set; }
        public int length6 { get; set; }
        public int length7 { get; set; }
        public int length8 { get; set; }
        public int length9 { get; set; }
        public int length10 { get; set; }
        public int length11 { get; set; }
        public int length12 { get; set; }
        public int length13 { get; set; }
        public int length14 { get; set; }
        public int length15 { get; set; }
        public string Busy { get; set; }


        public void clear()
        {
            GWNo = null;
            OD = null;
            WT = null;
            MT = null;
            YLLth = null;
            length1 = 0;
            length2 = 0;
            length3 = 0;
            length4 = 0;
            length5 = 0;
            length6 = 0;
            length7 = 0;
            length8 = 0;
            length9 = 0;
            length10 = 0;
            length11 = 0;
            length12 = 0;
            length13 = 0;
            length14 = 0;
            length15 = 0;
            Busy = null;
        }
    }
    
}
