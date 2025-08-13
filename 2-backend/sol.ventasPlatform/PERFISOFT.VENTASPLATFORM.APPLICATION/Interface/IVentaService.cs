using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IVentaService
    {
        Task<IList<VentaDTO>> GetAllByFilterAsync(ObtenerVentaRequest request);

        Task<IList<UsuarioDTO>> GetAllUsuariosAsync();

        Task<ResponseDTO> InsertAsync(RegistrarVentaRequest request);

        Task<ResponseDTO> AnulaAsync(AnularVentaRequest request);
    }
}

