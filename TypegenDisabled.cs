namespace XState
{
    public class TypegenDisabled : TypegenFlag
    {
        bool TypegenFlag.Typegen => false; // This is the only property that is defined in TypegenDisabled and should be false
    }
}
