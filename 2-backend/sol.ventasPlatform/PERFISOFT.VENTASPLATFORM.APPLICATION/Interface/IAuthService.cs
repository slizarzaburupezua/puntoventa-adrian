using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IAuthService
    {
        Task<IniciaSesionDTO> IniciaSesionAsync(IniciaSesionRequest request);

        Task<ResponseDTO> NotifyOlvideContraseniaAsync(NotifyOlvideContraseniaRequest request);

        Task<ResponseDTO> RestablecerContraseniaAsync(RestablecerContraseniaRequest request);

        Task<ResponseDTO> VerifyTokenRestablecerContraseniaAsync(VerifyTokenRestablecerContraseniaRequest request);
    }
}
