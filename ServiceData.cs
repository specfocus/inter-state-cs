namespace XState
{
    public interface IServiceData
    {
        object? Data { get; }
    }

    public class ServiceData : IServiceData
    {
        public object? Data { get; set; }
    }
}
