using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface IReportsExporterAbstractFactory
    {
        IExporter<ReportsDTO> GetBasicExcelExporter();
        IExporter<ReportsDTO> GetOvertimeExcelExporter();
    }
}
