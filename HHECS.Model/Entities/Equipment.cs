using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备列表
	/// </summary>
    [Table("equipment")]
    public partial class Equipment : SysEntity
    {
        public Equipment()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 设备名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 车间标识
	    /// </summary>
        [Column("workshopId")]
        public int WorkshopId { get; set; }
        /// <summary>
	    /// 工厂标识
	    /// </summary>
        [Column("factoryId")]
        public int FactoryId { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 线体ID
	    /// </summary>
        [Column("lineId")]
        public int LineId { get; set; }
        /// <summary>
	    /// 工位
	    /// </summary>
        [Column("stationCode")]
        public string StationCode { get; set; }
        /// <summary>
	    /// 工位ID
	    /// </summary>
        [Column("stationId")]
        public int StationId { get; set; }
        /// <summary>
        /// 巷道
        /// </summary>
        [Column("roadWay")]
        public int RoadWay { get; set; }
        /// <summary>
	    /// 设备IP地址
	    /// </summary>
        [Column("IP")]
        public string IP { get; set; }
        /// <summary>
        /// OPC连接名
        /// </summary>
        [Column("connectName")]
        public string ConnectName { get; set; }        
        /// <summary>
	    /// 设备类型
	    /// </summary>
        [Column("equipmentTypeId")]
        public int EquipmentTypeId { get; set; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        [Column("ledIP")]
        public string LedIp { get; set; }
        /// <summary>
	    /// 是否启用
	    /// </summary>
        [Column("enable")]
        public bool Enable { get; set; }
        /// <summary>
        /// 目标区域
        /// </summary>
        [Column("destinationArea")]
        public string DestinationArea { get; set; }
        /// <summary>
        /// 目的地址
        /// </summary>
        [Column("goAddress")]
        public string GoAddress { get; set; }
        /// <summary>
        /// 自身地址
        /// </summary>
        [Column("selfAddress")]
        public string SelfAddress { get; set; }
        /// <summary>
        ///回退地址
        /// </summary>
        [Column("backAddress")]
        public string BackAddress { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("warehouseCode")]
        public string WarehouseCode { get; set; }
        /// <summary>
        /// 站台编码
        /// </summary>
        [Column("stationIndex")]
        public int? StationIndex { get; set; }
        /// <summary>
        /// 排索引（堆垛机），任务最近距离（桁车）
        /// </summary>
        [Column("rowIndex1")]
        public long RowIndex1 { get; set; }
        /// <summary>
        /// 排索引（堆垛机），任务最远距离（桁车）
        /// </summary>
        [Column("rowIndex2")]
        public long RowIndex2 { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        [Column("columnIndex")]
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        [Column("layerIndex")]
        public int? LayerIndex { get; set; }
        /// <summary>
        /// PLC的DB地址
        /// </summary>
        [Column("basePlcDB")]
        public string BasePlcDB { get; set; }


        [NotMapped]
        /// <summary>
        /// 逻辑外键实体-设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        //[NotMapped]
        ///// <summary>
        ///// 逻辑外键实体-设备属性
        ///// </summary>
        //public List<EquipmentProp> EquipmentProps { get; set; } = new List<EquipmentProp>();

        [NotMapped]
        /// <summary>
        /// 逻辑外键实体-设备对应的站台属性
        /// </summary>
        public Station Station { get; set; }

        //[NotMapped]
        ///// <summary>
        ///// 逻辑外键实体-站台类别属性列表
        ///// </summary>
        //public List<StepStation> StepStationList { get; set; }

        [NotMapped]
        /// <summary>
        /// 逻辑外键实体-站台属性列表
        /// </summary>
        public List<Station> StationList { get; set; } = new List<Station>();
        [Column("Disable")]
        public bool Disable { get; set; }


        #region 添加数据字典 键值只需要执行一次初始化 后续不需要再Find 
        private IDictionary<string, EquipmentProp> m_propMaps = new Dictionary<string, EquipmentProp>();
        private List<EquipmentProp> m_equipmentProps = new List<EquipmentProp>();
        /// <summary>
        /// 逻辑外键实体-设备属性
        /// </summary>
        [NotMapped]
        public List<EquipmentProp> EquipmentProps
        {
            get { return m_equipmentProps; }
            set
            {
                m_equipmentProps = value;
                if (value != null)
                {
                    var maps = new Dictionary<string, EquipmentProp>(value.Count);
                    foreach (var item in value)
                    {
                        maps.Add(item.EquipmentTypeTemplateCode, item);
                    }
                    m_propMaps = maps;
                }
            }
        }

        /// <summary>
        /// 获取设备属性
        /// </summary>
        /// <param name="key">设备属性code </param>
        /// <returns></returns>
        [NotMapped]
        public EquipmentProp this[string key]
        {
            get
            {
                m_propMaps.TryGetValue(key, out EquipmentProp prop);
                return prop;
            }
        }
        #endregion
    }
}