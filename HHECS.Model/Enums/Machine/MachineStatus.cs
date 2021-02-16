namespace HHECS.Model.Enums.Machine
{
    public enum OperationModelFlag
    {
        维修 = 1, 
        手动 = 2, 
        机载操作 = 3, 
        单机自动 = 4, 
        联机 = 5,
    }


    public enum TotalErrorFlag
    {
        无故障 = 0,
        有故障 = 1,
    }
}
