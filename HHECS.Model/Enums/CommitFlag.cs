using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    public enum CommitFlag
    {
        未提交 = 0,
        已提交 = 1,
        无需提交 = 2,
        提交失败 = 3
    }
}
