using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Filtro;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> SelectByIdAsync(int id);

        Task<List<Usuario>> SelectAllAsync();

        Task<List<Usuario>> SelectAllByFilterAsync(FiltroConsultaUsuario filtro, int idUsuario);

        Task<List<ObtenerColaboradoresActivos>> SelectAllActivesAsync();

        Task<int> SelectIdByCorreoAsync(string correo);

        Task<string> SelectNombreUsuarioByCorreoAsync(string correo);

        Task<string> SelectNombreUsuarioByIdAsync(int idUsuario);

        Task<Usuario?> SelectDetailByIdAsync(int id);

        Task<Usuario> InsertAsync(Usuario usuario);

        Task<Usuario> UpdateAsync(Usuario usuario);

        Task DeleteAsync(Usuario usuario);

        Task AddUsuarioIdGuidAsync(UsuarioId usuarioIdHash);

        Task AddUsuarioClaveAsync(UsuarioContrasena usuarioContrasena);

        Task UpdateUsuarioContraseniaAsync(UsuarioContrasena usuarioContrasena);

        Task<UsuarioContrasena?> SelectUsuarioContraseniaByIdAsync(int idUsuario);

        Task<bool> ExistCorreoAsync(string correoUsuario);

        Task<bool> ExistNumeroDocumentoAsync(string numeroDocumento);

        Task<Usuario?> GetLoginAsync(string correo);

        Task<int> GetIdUsuarioByGuid(Guid idUsuario);

        Task<bool> ValidaClaveHashAsync(string claveHash, int idUsuario);

        Task<Guid> GetIdGuidUserById(int IdUsuario);

        Task<string> GetSaltAsync(int idUsuario);

        Task<string> GetTokenRestableceContraseniaAsync(string correoElectronico);

        Task<DateTime> GetFechaRegistroTokenRestableceContraseniaAsync(string correoElectronico);

        Task<UsuarioRestableceContrasenia?> SelectUsuarioRestableceContraseniaByCorreoAsync(string correoElectronico);

        Task InsertUsuarioTokenRestableceContraseniaAsync(UsuarioRestableceContrasenia usuarioOTP);

        Task UpdateUsuarioTokenRestableceContraseniaAsync(UsuarioRestableceContrasenia usuarioOTP);

        Task UpdateNoEsPrimerLogueoAsync(int idUsuario);

        Task UpdateActivoAsync(int idUsuario, bool flgActivo);

    }
}
