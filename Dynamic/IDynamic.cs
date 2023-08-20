namespace XState.Dynamic
{
    public interface IDynamic
    {
        object GetMember(string name);
        bool TryGetMember(string name, out object result);
        bool TrySetMember(string name, object value);
    }
}
