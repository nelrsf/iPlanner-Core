namespace iPlanner.Application.Interfaces
{
    public interface IExporter<T> where T : class
    {
        event EventHandler ExportCompleted;
        string filepath { get; set; }
        string fileLayout { get; set; }
        Task ExportAsync(IEnumerable<T> data);
    }
}
