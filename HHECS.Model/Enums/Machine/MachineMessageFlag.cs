namespace HHECS.Model.Enums.Machine
{
    /// <summary>
    /// 机器请求
    /// </summary>
    public enum MachineMessageFlag
    {
        默认 = 0,
        PLC自动请求下料 = 1,
        PLC自动请求上料 = 2,
        PLC人工请求下料 = 3,
        WCS回复允许下料 = 6,
        WCS回复允许上料 = 8
    }

    /// <summary>
    /// 机器到达状态
    /// </summary>
    public enum MachineResultFlag
    {
        已到达 = 1,
        未到达 = 2,
    }

    public enum FlipFlag
    {
        默认 = 0,
        PLC自动请求翻转 = 1,
        WCS回复允许翻转 = 6,
    }

    public enum CutFlag
    {
        默认 = 0,
        PLC自动请求切割 = 1,

        WCS回复允许切割 = 6,
        WCS回复结束切割 = 7,
        WCS回复没有套料方案 = 8,
    }


    public enum PrintFlag
    {
        默认 = 0,
        PLC请求打印 = 1,
        WCS回复打印 = 6,
    }
}
