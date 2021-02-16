using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot.Enums
{
    public enum PcPlcAgv
    {
        #region 料框类型A
        /// <summary>
        /// 料框信息给定
        /// </summary>
        A_Incoming,
        /// <summary>
        /// AGV上料到达
        /// </summary>
        A_Blank_Reach,
        /// <summary>
        /// AGV上料离开
        /// </summary>
        A_Blank_Leave,
        /// <summary>
        /// AGV下料到达
        /// </summary>
        A_Load_Reach,
        /// <summary>
        /// AGV下料离开
        /// </summary>
        A_Load_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        A_Type,
        /// <summary>
        /// 缓存区域
        /// </summary>
        A_Cache_Area,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier1,
        /// <summary>
        /// 列
        /// </summary>
        A_Row1,
        /// <summary>
        /// 行
        /// </summary>
        A_Line1,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num1,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier2,
        /// <summary>
        /// 列
        /// </summary>
        A_Row2,
        /// <summary>
        /// 行
        /// </summary>
        A_Line2,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num2,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier3,
        /// <summary>
        /// 列
        /// </summary>
        A_Row3,
        /// <summary>
        /// 行
        /// </summary>
        A_Line3,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num3,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier4,
        /// <summary>
        /// 列
        /// </summary>
        A_Row4,
        /// <summary>
        /// 行
        /// </summary>
        A_Line4,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num4,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier5,
        /// <summary>
        /// 列
        /// </summary>
        A_Row5,
        /// <summary>
        /// 行
        /// </summary>
        A_Line5,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num5,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier6,
        /// <summary>
        /// 列
        /// </summary>
        A_Row6,
        /// <summary>
        /// 行
        /// </summary>
        A_Line6,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num6,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier7,
        /// <summary>
        /// 列
        /// </summary>
        A_Row7,
        /// <summary>
        /// 行
        /// </summary>
        A_Line7,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num7,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier8,
        /// <summary>
        /// 列
        /// </summary>
        A_Row8,
        /// <summary>
        /// 行
        /// </summary>
        A_Line8,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num8,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier9,
        /// <summary>
        /// 列
        /// </summary>
        A_Row9,
        /// <summary>
        /// 行
        /// </summary>
        A_Line9,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num9,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier10,
        /// <summary>
        /// 列
        /// </summary>
        A_Row10,
        /// <summary>
        /// 行
        /// </summary>
        A_Line10,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num10,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier11,
        /// <summary>
        /// 列
        /// </summary>
        A_Row11,
        /// <summary>
        /// 行
        /// </summary>
        A_Line11,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num11,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier12,
        /// <summary>
        /// 列
        /// </summary>
        A_Row12,
        /// <summary>
        /// 行
        /// </summary>
        A_Line12,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num12,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier13,
        /// <summary>
        /// 列
        /// </summary>
        A_Row13,
        /// <summary>
        /// 行
        /// </summary>
        A_Line13,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num13,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier14,
        /// <summary>
        /// 列
        /// </summary>
        A_Row14,
        /// <summary>
        /// 行
        /// </summary>
        A_Line14,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num14,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier15,
        /// <summary>
        /// 列
        /// </summary>
        A_Row15,
        /// <summary>
        /// 行
        /// </summary>
        A_Line15,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num15,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier16,
        /// <summary>
        /// 列
        /// </summary>
        A_Row16,
        /// <summary>
        /// 行
        /// </summary>
        A_Line16,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num16,

        #endregion

        #region 料框类型B
        /// <summary>
        /// 料框信息给定
        /// </summary>
        B_Incoming,
        /// <summary>
        /// AGV上料到达
        /// </summary>
        B_Blank_Reach,
        /// <summary>
        /// AGV上料离开
        /// </summary>
        B_Blank_Leave,
        /// <summary>
        /// AGV下料到达
        /// </summary>
        B_Load_Reach,
        /// <summary>
        /// AGV下料离开
        /// </summary>
        B_Load_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        B_Type,
        /// <summary>
        /// 缓存区域
        /// </summary>
        B_Cache_Area,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier1,
        /// <summary>
        /// 列
        /// </summary>
        B_Row1,
        /// <summary>
        /// 行
        /// </summary>
        B_Line1,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num1,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier2,
        /// <summary>
        /// 列
        /// </summary>
        B_Row2,
        /// <summary>
        /// 行
        /// </summary>
        B_Line2,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num2,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier3,
        /// <summary>
        /// 列
        /// </summary>
        B_Row3,
        /// <summary>
        /// 行
        /// </summary>
        B_Line3,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num3,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier4,
        /// <summary>
        /// 列
        /// </summary>
        B_Row4,
        /// <summary>
        /// 行
        /// </summary>
        B_Line4,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num4,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier5,
        /// <summary>
        /// 列
        /// </summary>
        B_Row5,
        /// <summary>
        /// 行
        /// </summary>
        B_Line5,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num5,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier6,
        /// <summary>
        /// 列
        /// </summary>
        B_Row6,
        /// <summary>
        /// 行
        /// </summary>
        B_Line6,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num6,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier7,
        /// <summary>
        /// 列
        /// </summary>
        B_Row7,
        /// <summary>
        /// 行
        /// </summary>
        B_Line7,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num7,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier8,
        /// <summary>
        /// 列
        /// </summary>
        B_Row8,
        /// <summary>
        /// 行
        /// </summary>
        B_Line8,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num8,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier9,
        /// <summary>
        /// 列
        /// </summary>
        B_Row9,
        /// <summary>
        /// 行
        /// </summary>
        B_Line9,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num9,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier10,
        /// <summary>
        /// 列
        /// </summary>
        B_Row10,
        /// <summary>
        /// 行
        /// </summary>
        B_Line10,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num10,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier11,
        /// <summary>
        /// 列
        /// </summary>
        B_Row11,
        /// <summary>
        /// 行
        /// </summary>
        B_Line11,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num11,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier12,
        /// <summary>
        /// 列
        /// </summary>
        B_Row12,
        /// <summary>
        /// 行
        /// </summary>
        B_Line12,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num12,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier13,
        /// <summary>
        /// 列
        /// </summary>
        B_Row13,
        /// <summary>
        /// 行
        /// </summary>
        B_Line13,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num13,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier14,
        /// <summary>
        /// 列
        /// </summary>
        B_Row14,
        /// <summary>
        /// 行
        /// </summary>
        B_Line14,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num14,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier15,
        /// <summary>
        /// 列
        /// </summary>
        B_Row15,
        /// <summary>
        /// 行
        /// </summary>
        B_Line15,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num15,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier16,
        /// <summary>
        /// 列
        /// </summary>
        B_Row16,
        /// <summary>
        /// 行
        /// </summary>
        B_Line16,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num16,

        #endregion

        #region 料框类型C
        /// <summary>
        /// 料框信息给定
        /// </summary>
        C_Incoming,
        /// <summary>
        /// AGV上料到达
        /// </summary>
        C_Blank_Reach,
        /// <summary>
        /// AGV上料离开
        /// </summary>
        C_Blank_Leave,
        /// <summary>
        /// AGV下料到达
        /// </summary>
        C_Load_Reach,
        /// <summary>
        /// AGV下料离开
        /// </summary>
        C_Load_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        C_Type,
        /// <summary>
        /// 缓存区域
        /// </summary>
        C_Cache_Area,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier1,
        /// <summary>
        /// 列
        /// </summary>
        C_Row1,
        /// <summary>
        /// 行
        /// </summary>
        C_Line1,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num1,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier2,
        /// <summary>
        /// 列
        /// </summary>
        C_Row2,
        /// <summary>
        /// 行
        /// </summary>
        C_Line2,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num2,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier3,
        /// <summary>
        /// 列
        /// </summary>
        C_Row3,
        /// <summary>
        /// 行
        /// </summary>
        C_Line3,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num3,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier4,
        /// <summary>
        /// 列
        /// </summary>
        C_Row4,
        /// <summary>
        /// 行
        /// </summary>
        C_Line4,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num4,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier5,
        /// <summary>
        /// 列
        /// </summary>
        C_Row5,
        /// <summary>
        /// 行
        /// </summary>
        C_Line5,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num5,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier6,
        /// <summary>
        /// 列
        /// </summary>
        C_Row6,
        /// <summary>
        /// 行
        /// </summary>
        C_Line6,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num6,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier7,
        /// <summary>
        /// 列
        /// </summary>
        C_Row7,
        /// <summary>
        /// 行
        /// </summary>
        C_Line7,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num7,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier8,
        /// <summary>
        /// 列
        /// </summary>
        C_Row8,
        /// <summary>
        /// 行
        /// </summary>
        C_Line8,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num8,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier9,
        /// <summary>
        /// 列
        /// </summary>
        C_Row9,
        /// <summary>
        /// 行
        /// </summary>
        C_Line9,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num9,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier10,
        /// <summary>
        /// 列
        /// </summary>
        C_Row10,
        /// <summary>
        /// 行
        /// </summary>
        C_Line10,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num10,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier11,
        /// <summary>
        /// 列
        /// </summary>
        C_Row11,
        /// <summary>
        /// 行
        /// </summary>
        C_Line11,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num11,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier12,
        /// <summary>
        /// 列
        /// </summary>
        C_Row12,
        /// <summary>
        /// 行
        /// </summary>
        C_Line12,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num12,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier13,
        /// <summary>
        /// 列
        /// </summary>
        C_Row13,
        /// <summary>
        /// 行
        /// </summary>
        C_Line13,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num13,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier14,
        /// <summary>
        /// 列
        /// </summary>
        C_Row14,
        /// <summary>
        /// 行
        /// </summary>
        C_Line14,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num14,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier15,
        /// <summary>
        /// 列
        /// </summary>
        C_Row15,
        /// <summary>
        /// 行
        /// </summary>
        C_Line15,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num15,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier16,
        /// <summary>
        /// 列
        /// </summary>
        C_Row16,
        /// <summary>
        /// 行
        /// </summary>
        C_Line16,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num16,

        #endregion
    }
}
