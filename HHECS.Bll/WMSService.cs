using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HHECS.DAL;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;

namespace HHECS.Bll
{
    public class WMSService : BaseService
    {

        public BllResult HeartBeat(string userCode, string password, int warehouseId, string warehouseCode, HttpClient client, List<DictDetail> urls)
        {
            var result = AppSession.CommonService.FormPost(new List<KeyValuePair<string, string>>(), WMSUrls.Heartbeat, client, urls);
            if (!result.Success)
            {
                var temp = AppSession.BllService.GetUserWithRoles(userCode, password,  warehouseCode, client, urls);
                if (temp.Success)
                {
                    return BllResultFactory.Sucess();
                }
                else
                {
                    return BllResultFactory.Error(temp.Msg);
                }
            }
            else
            {
                return BllResultFactory.Sucess();
            }
        }
    }
}
