namespace DISampleWeb.Services
{
    public interface IDisposableSingletonService
    {
        void SampleMethod();
    }
    public class DisposableSingletonService : IDisposableSingletonService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SampleMethod()
        {
            throw new NotImplementedException();
        }
    }
}
