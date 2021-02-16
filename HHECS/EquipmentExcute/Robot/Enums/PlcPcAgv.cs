using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot.Enums
{
    public enum PlcPcAgv
    {
        #region 料框类型A
        /// <summary>
        /// 料框信息给定确认
        /// </summary>
        A_Incoming_OK,
        /// <summary>
        /// AGV上料到达确认
        /// </summary>
        A_Blank_Reach_OK,
        /// <summary>
        /// AGV上料离开确认
        /// </summary>
        A_Blank_Leave_OK,
        /// <summary>
        /// AGV下料到达确认
        /// </summary>
        A_Load_Reach_OK,
        /// <summary>
        /// AGV下料离开确认
        /// </summary>
        A_Load_Leave_OK,
        /// <summary>
        /// AGV允许到达
        /// </summary>
        A_Ready_Reach,
        /// <summary>
        /// AGV允许离开
        /// </summary>
        A_Ready_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        A_Type_Feedback,
        /// <summary>
        /// 缓存区域
        /// </summary>
        A_Cache_Area_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier1_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row1_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line1_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num1_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier2_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row2_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line2_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num2_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier3_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row3_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line3_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num3_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier4_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row4_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line4_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num4_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier5_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row5_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line5_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num5_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier6_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row6_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line6_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num6_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier7_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row7_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line7_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num7_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier8_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row8_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line8_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num8_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier9_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row9_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line9_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num9_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier10_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row10_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line10_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num10_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier11_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row11_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line11_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num11_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier12_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row12_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line12_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num12_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier13_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row13_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line13_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num13_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier14_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row14_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line14_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num14_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier15_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row15_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line15_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num15_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        A_Tier16_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        A_Row16_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        A_Line16_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        A_Num16_Feedback,

        #endregion
 
        #region 料框类型B
        /// <summary>
        /// 料框信息给定确认
        /// </summary>
        B_Incoming_OK,
        /// <summary>
        /// AGV上料到达确认
        /// </summary>
        B_Blank_Reach_OK,
        /// <summary>
        /// AGV上料离开确认
        /// </summary>
        B_Blank_Leave_OK,
        /// <summary>
        /// AGV下料到达确认
        /// </summary>
        B_Load_Reach_OK,
        /// <summary>
        /// AGV下料离开确认
        /// </summary>
        B_Load_Leave_OK,
        /// <summary>
        /// AGV允许到达
        /// </summary>
        B_Ready_Reach,
        /// <summary>
        /// AGV允许离开
        /// </summary>
        B_Ready_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        B_Type_Feedback,
        /// <summary>
        /// 缓存区域
        /// </summary>
        B_Cache_Area_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier1_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row1_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line1_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num1_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier2_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row2_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line2_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num2_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier3_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row3_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line3_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num3_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier4_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row4_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line4_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num4_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier5_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row5_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line5_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num5_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier6_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row6_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line6_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num6_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier7_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row7_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line7_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num7_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier8_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row8_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line8_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num8_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier9_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row9_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line9_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num9_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier10_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row10_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line10_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num10_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier11_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row11_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line11_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num11_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier12_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row12_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line12_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num12_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier13_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row13_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line13_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num13_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier14_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row14_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line14_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num14_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier15_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row15_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line15_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num15_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        B_Tier16_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        B_Row16_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        B_Line16_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        B_Num16_Feedback,

        #endregion

        #region 料框类型C
        /// <summary>
        /// 料框信息给定确认
        /// </summary>
        C_Incoming_OK,
        /// <summary>
        /// AGV上料到达确认
        /// </summary>
        C_Blank_Reach_OK,
        /// <summary>
        /// AGV上料离开确认
        /// </summary>
        C_Blank_Leave_OK,
        /// <summary>
        /// AGV下料到达确认
        /// </summary>
        C_Load_Reach_OK,
        /// <summary>
        /// AGV下料离开确认
        /// </summary>
        C_Load_Leave_OK,
        /// <summary>
        /// AGV允许到达
        /// </summary>
        C_Ready_Reach,
        /// <summary>
        /// AGV允许离开
        /// </summary>
        C_Ready_Leave,
        /// <summary>
        /// 工件型号
        /// </sum'mary>
        C_Type_Feedback,
        /// <summary>
        /// 缓存区域
        /// </summary>
        C_Cache_Area_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier1_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row1_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line1_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num1_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier2_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row2_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line2_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num2_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier3_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row3_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line3_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num3_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier4_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row4_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line4_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num4_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier5_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row5_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line5_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num5_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier6_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row6_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line6_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num6_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier7_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row7_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line7_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num7_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier8_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row8_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line8_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num8_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier9_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row9_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line9_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num9_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier10_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row10_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line10_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num10_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier11_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row11_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line11_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num11_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier12_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row12_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line12_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num12_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier13_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row13_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line13_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num13_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier14_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row14_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line14_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num14_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier15_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row15_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line15_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num15_Feedback,
        /// <summary>
        /// 层
        /// </summary>
        C_Tier16_Feedback,
        /// <summary>
        /// 列
        /// </summary>
        C_Row16_Feedback,
        /// <summary>
        /// 行
        /// </summary>
        C_Line16_Feedback,
        /// <summary>
        /// 数量
        /// </summary>
        C_Num16_Feedback,

        #endregion

    }
}
