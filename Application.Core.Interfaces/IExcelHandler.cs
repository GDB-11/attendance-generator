namespace Application.Core.Interfaces;

public interface IExcelHandler
{
    MemoryStream CreateSpreadSheet(int sessions);
}