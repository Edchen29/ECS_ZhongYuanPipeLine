using System.Collections.Generic;

namespace HHECS.Model.Entities
{
    public class CarStation
    {
        /// <summary>
        /// 出入口库位1
        /// </summary>
        /// <returns></returns>
        public static Location getStation(Equipment car)
        {
            if (car.Code == "car1")
            {
                return station1();
            }
            else
            {
                return station2();
            }
        }
        /// <summary>
        /// 1号布料机位置
        /// </summary>
        /// <returns></returns>
        public static Location station1()
        {
            Location location = new Location();
            location.Code = "L10-24-01";
            location.Row = 10;
            location.Line = 24;
            location.Layer = 1;
            return location;
        }
        /// <summary>
        /// 2号布料机位置
        /// </summary>
        /// <returns></returns>
        public static Location station2()
        {
            Location location = new Location();
            location.Code = "L10-27-01";
            location.Row = 10;
            location.Line = 27;
            location.Layer = 1;
            return location;
        }
    }

    /// <summary>
    /// 小车的位置信息
    /// </summary>
    public class CarLocation
    {
        /// <summary>
        ///  行：1代表 {1-9}行、 3 代表{10-18}行
        /// </summary>
        public string row;
        /// <summary>
        /// 列
        /// </summary>
        public string line;
        /// <summary>
        /// 层
        /// </summary>
        public string layer;
        /// <summary>
        /// 位置： 0=初始化 1=在巷道中 2=在母车上 3=在1#充电桩 4=在2#充电桩 5=等待接空盘上料位
        /// </summary>
        public string location;
        /// <summary>
        /// 小车编码
        /// </summary>
        public string carNo;
        /// <summary>
        /// 控制模式
        /// </summary>
        public string controlMode;
    }


    //public class CarPosition
    //{
    //    static Dictionary<int, string> position;
    //    public static Dictionary<int, string> getPosition()
    //    {
    //        if (position == null)
    //        {
    //            position = new Dictionary<int, string>();
    //            position.Add(0, "初始化");
    //            position.Add(1, "在巷道中");
    //            position.Add(2, "在母车上");
    //            position.Add(3, "在1#充电桩");
    //            position.Add(4, "在2#充电桩");
    //            position.Add(5, "等待接空盘");
    //            position.Add(6, "装料点");
    //            position.Add(7, "左端头");
    //            position.Add(8, "右端头");
    //        }
    //        return position;
    //    }
    //}

}
