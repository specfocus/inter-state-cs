namespace XState
{
    public class TypegenEnabled : TypegenFlag
    {
        bool TypegenFlag.Typegen => true; // This is the only property that is defined in TypegenEnabled and should be true
    }
}
