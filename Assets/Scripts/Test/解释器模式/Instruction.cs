namespace ZiercCode.Test.解释器模式
{
    /// <summary>
    /// 指令集，储存所有指令字节码
    /// </summary>
    public enum Instruction
    {
        Z_SET_HEALTH = 1,
        Z_GET_HEALTH = 2,
        Z_LITERAL = 3
    }
}