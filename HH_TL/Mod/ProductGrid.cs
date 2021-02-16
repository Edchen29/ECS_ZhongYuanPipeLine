using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Mod
{
    [ImplementPropertyChanged]
    public class ProductGrid
    {
        public int id { get; set; }
        /// <summary>
        /// 外径
        /// </summary>
        public string WaiJing { get; set; }
        /// <summary>
        /// 壁厚
        /// </summary>
        public string BiHou { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string CaiZhi { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Dengji { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string Code { get; set; }//cf
        /// <summary>
        /// 组队工位
        /// </summary>
        public int AssemblyStation { get; set; }

        /// <summary>
        /// 管件编号
        /// </summary>
        public string GuanJianBianHao { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string XiangMuMingCheng { get; set; }
        /// <summary>
        /// 下料编号
        /// </summary>
        public string XiaLiaoBianHao { get; set; }
        /// <summary>
        /// 图纸页码
        /// </summary>
        public string TuZhi { get; set; }


        public string ShouDuanPoKou { get; set; }
        public string MoDuanPoKou { get; set; }
        public string ShouDuanNeiTang { get; set; }
        public string MoDuanNeiTang { get; set; }

        public string WuLiaoBianMa { get; set; }
        public string LiuXiang { get; set; }


        public int length { get; set; }

        internal void clear()
        {
            WaiJing = null;
            BiHou = null;
            CaiZhi = null;
            Dengji = null;

            GuanJianBianHao = null;
            XiangMuMingCheng = null;
            XiaLiaoBianHao = null;
            TuZhi = null;

            ShouDuanPoKou = null;
            MoDuanPoKou = null;
            ShouDuanNeiTang = null;
            MoDuanNeiTang = null;

            WuLiaoBianMa = null; ;
            LiuXiang = null; 

            length = 0;
        }
    }
}
