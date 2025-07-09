using iPlanner.Application.DTO;

namespace iPlanner.Application.Interfaces.Repository
{
    public interface IReportsExporterAbstractFactory
    {
        IExporter<ReportsDTO> GetBasicExcelExporter();
        IExporter<ReportsDTO> GetOvertimeExcelExporter();
    }
}
