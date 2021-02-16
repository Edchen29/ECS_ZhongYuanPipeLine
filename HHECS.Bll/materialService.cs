using Dapper;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    public class MaterialService
    {
        /// <summary>
        /// 根据工站的缓存区，找到物料没有耗尽呼叫，找不到就新增呼叫
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public BllResult materialCall(Equipment robot)
        {

            //找到工站对应的缓存位
            var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where srmCode = '{robot.Code}'");
            if (locationResult.Success)
            {
                //找到对应的工站中，材料还未耗尽的呼叫
                var callHeaderResult = AppSession.Dal.GetCommonModelByCondition<MaterialCallHeader>($"needStation = '{robot.Code}' and status <> '{MaterialCallStatus.配送使用完毕.GetIndexString()}'");
                foreach (var item in locationResult.Data)
                {
                    //判断是否呼叫过
                    if (callHeaderResult.Success)
                    {
                        if (callHeaderResult.Data.Exists(t => t.LocationCode == item.Code))
                        {
                            continue;
                        }
                    }
                    //新增一条呼叫
                    MaterialCallHeader materialCallHeader = new MaterialCallHeader();
                        
                    var result = AppSession.Dal.InsertCommonModel<MaterialCallHeader>(materialCallHeader);
                    if (result.Success == false)
                    {
                        return BllResultFactory.Error(result.Msg);
                    }
                }
            }
            return BllResultFactory.Sucess();
        }














        
    }
}
