using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Mod
{
    [ImplementPropertyChanged]
    public class OutPutValue
    {
        public OutPutValue()
        {
            ischeck = false;
            material = new MaterialGrid();
            productList = new List<ProductGrid>();
            result =new ResultGrid();
            SumSun = 0;
            Percentage = 0;
        }
        public bool ischeck { get; set; }
        /// <summary>
        /// 原材料
        /// </summary>
        public MaterialGrid material { get; set; }
        /// <summary>
        /// 下料集合
        /// </summary>
        public List<ProductGrid> productList { get; set; }
        ///<summary>
        ///外部导入套料结果
        /// </summary>
        public ResultGrid result { get; set; }
        /// <summary>
        /// 下料材料的总长度+损耗
        /// </summary>
        public int SumSun { get; set; }
        /// <summary>
        /// 组对工位
        /// </summary>
        //public int AssemblyStation { get; set; }//cf
        public float Percentage { get; set; }
        public int SumProduct()
        {
            int revalue = 0;
            foreach (ProductGrid pro in productList)
            {
                revalue = revalue + pro.length;
            }
            return revalue;
        }
        public void SumPercentage()
        {
            var revalue = SumProduct();
            Percentage = revalue / material.length;
        }
    }
}
