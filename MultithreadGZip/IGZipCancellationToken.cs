namespace MultithreadGZip
{
    public interface IGZipCancellationToken
    {
        bool IsCancelled { get; set; }
    }
}