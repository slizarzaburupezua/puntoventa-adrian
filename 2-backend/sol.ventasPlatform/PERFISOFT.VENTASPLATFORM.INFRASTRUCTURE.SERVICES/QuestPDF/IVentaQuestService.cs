using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.QuestPDFLibrary
{
    public interface IVentaQuestService
    {
        Task<(string FileName, string Base64File)> GenerarBoletaFacturaAsync(FiltroGenerarBoletaFactura filtro);
    }
}
