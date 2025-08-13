using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private VentasPlatformContext _context;
        public UsuarioRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> SelectByIdAsync(int id)
        {
            return await All<Usuario>()
                        .AsNoTracking()
                        .Where(q => q.ID == id && q.ESTADO == Flags.Habilitado)
                        .FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> SelectAllAsync()
        {
            return await All<Usuario>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado && p.ACTIVO == Flags.Activado
                )
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO.Date)
                .Select(p => new Usuario
                {
                    ID = p.ID,
                    NOMBRES = p.NOMBRES,
                    CORREO_ELECTRONICO = p.CORREO_ELECTRONICO
                }).ToListAsync();
        }

        public async Task<List<Usuario>> SelectAllByFilterAsync(FiltroConsultaUsuario filtro, int idUsuario)
        {
            return await All<Usuario>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado
                )
                .Where(p => string.IsNullOrEmpty(filtro.NOMBRES) || p.NOMBRES.Contains(filtro.NOMBRES))
                .Where(p => string.IsNullOrEmpty(filtro.APELLIDOS) || p.APELLIDOS.Contains(filtro.APELLIDOS))
                .Where(p => string.IsNullOrEmpty(filtro.CORREO_ELECTRONICO) || p.CORREO_ELECTRONICO.Contains(filtro.CORREO_ELECTRONICO))
                .Where(p => !filtro.FECHA_REGISTRO_INICIO.HasValue || p.FECHA_REGISTRO.Date >= filtro.FECHA_REGISTRO_INICIO)
                .Where(p => !filtro.FECHA_REGISTRO_FIN.HasValue || p.FECHA_REGISTRO.Date <= filtro.FECHA_REGISTRO_FIN)
                .Where(p => filtro.LST_GENERO == null || filtro.LST_GENERO.Length == Numeracion.Cero || filtro.LST_GENERO.Contains(p.GENERO.ID))
                .Where(p => p.ID != idUsuario)
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .Include(p => p.GENERO)
                .Include(p => p.ROL)
                .Include(p => p.TIPODOCUMENTO)
                .Include(p => p.USUARIOID)
                .ToListAsync();
        }

        public async Task<List<ObtenerColaboradoresActivos>> SelectAllActivesAsync()
        {
            return await All<Usuario>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado && p.ACTIVO == Flags.Activado)
                .OrderBy(p => p.FECHA_REGISTRO)
                .Include(p => p.ROL)
                .Select(p => new ObtenerColaboradoresActivos
                {
                    Nombres = p.NOMBRES,
                    Apellidos = p.APELLIDOS,
                    CorreoElectronico = p.CORREO_ELECTRONICO,
                    Celular = p.CELULAR,
                    UrlFoto = p.URLFOTO,
                    NombreRol = p.ROL.NOMBRE
                }).ToListAsync();
        }

        public async Task<int> SelectIdByCorreoAsync(string correo)
        {
            return await All<Usuario>()
                        .Where(usuario => usuario.CORREO_ELECTRONICO == correo)
                        .Select(x => x.ID).FirstOrDefaultAsync();
        }

        public async Task<string> SelectNombreUsuarioByCorreoAsync(string correo)
        {
            return await All<Usuario>()
                        .Where(usuario => usuario.CORREO_ELECTRONICO == correo)
                        .Select(x => x.NOMBRES).FirstOrDefaultAsync();
        }

        public async Task<string> SelectNombreUsuarioByIdAsync(int idUsuario)
        {
            return await All<Usuario>()
                        .Where(usuario => usuario.ID == idUsuario)
                        .Select(x => x.NOMBRES).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> SelectDetailByIdAsync(int idUsuario)
        {
            return await All<Usuario>()
                        .AsNoTracking()
                        .Where(q => q.ID == idUsuario && q.ESTADO == Flags.Habilitado)
                        .Include(u => u.GENERO)
                        .Include(u => u.TIPODOCUMENTO)
                        .Include(u => u.ROL)
                        .Include(p => p.USUARIOID)
                        .FirstOrDefaultAsync();
        }

        public async Task<UsuarioContrasena?> SelectUsuarioContraseniaByIdAsync(int idUsuario)
        {
            return await All<UsuarioContrasena>()
                        .AsNoTracking()
                        .Where(q => q.ID == idUsuario && q.ESTADO == Flags.Habilitado)
                        .FirstOrDefaultAsync();
        }

        public async Task<UsuarioContrasena?> SelectUsuarioContraseniaByCorreoAsync(int idUsuario)
        {
            return await All<UsuarioContrasena>()
                        .AsNoTracking()
                        .Where(q => q.ID == idUsuario && q.ESTADO == Flags.Habilitado)
                        .FirstOrDefaultAsync();
        }

        public async Task<Usuario> InsertAsync(Usuario usuario)
        {
            await base.InsertAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task AddUsuarioIdGuidAsync(UsuarioId usuarioIdHash)
        {
            await base.InsertAsync(usuarioIdHash);
        }

        public async Task AddUsuarioClaveAsync(UsuarioContrasena usuarioContrasena)
        {
            await base.InsertAsync(usuarioContrasena);
        }

        public async Task UpdateUsuarioContraseniaAsync(UsuarioContrasena usuarioContrasena)
        {
            await base.UpdateAsync(usuarioContrasena);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            await base.UpdateAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateNoEsPrimerLogueoAsync(int idUsuario)
        {
            await _context.Usuario.Where(x => x.ID == idUsuario).ExecuteUpdateAsync(u => u.SetProperty(e => e.FLG_PRIMERA_VEZ_LOGUEO, Flags.False));
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            await base.DeleteAsync(usuario);
        }

        public async Task<bool> ExistCorreoAsync(string correo)
        {
            return await All<Usuario>()
                         .AsNoTracking()
                         .AnyAsync(u => u.CORREO_ELECTRONICO == correo);
        }

        public async Task<bool> ExistNumeroDocumentoAsync(string numeroDocumento)
        {
            return await All<Usuario>()
                         .AsNoTracking()
                         .AnyAsync(u => u.NUMERO_DOCUMENTO == numeroDocumento);
        }

        public async Task<Usuario?> GetLoginAsync(string correo)
        {
            return await All<Usuario>()
                        .AsNoTracking()
                        .Where(q => q.CORREO_ELECTRONICO == correo && q.ESTADO == Flags.Habilitado)
                        .Include(q => q.GENERO)
                        .Include(q => q.ROL)
                        .FirstOrDefaultAsync();
        }

        public async Task<string> GetSaltAsync(int idUsuario)
        {
            return await All<UsuarioContrasena>()
                        .Where(usuario => usuario.ID_USUARIO == idUsuario)
                        .Select(x => x.SALT).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidaClaveHashAsync(string claveHash, int idUsuario)
        {
            return await All<UsuarioContrasena>()
                .Where(q => q.ID_USUARIO == idUsuario
                && q.CLAVE_HASH == claveHash)
                .AnyAsync();
        }

        public async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await All<UsuarioId>()
                        .Where(q => q.ID_USUARIO_GUID == idUsuario && q.ESTADO == Flags.Habilitado)
                        .Select(x => x.ID_USUARIO).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetIdGuidUserById(int IdUsuario)
        {
            return await All<UsuarioId>()
                        .Where(q => q.ID_USUARIO == IdUsuario && q.ESTADO == Flags.Habilitado)
                        .Select(x => x.ID_USUARIO_GUID)
                        .FirstOrDefaultAsync();
        }

        #region TOKEN OLVIDECONTRASENIA

        public async Task<string> GetTokenRestableceContraseniaAsync(string correoElectronico)
        {
            return await All<UsuarioRestableceContrasenia>()
                         .Where(q => q.CORREO_ELECTRONICO == correoElectronico && q.ESTADO == Flags.Habilitado)
                         .Select(x => x.TOKEN).FirstOrDefaultAsync();
        }

        public async Task<DateTime> GetFechaRegistroTokenRestableceContraseniaAsync(string correoElectronico)
        {
            return await All<UsuarioRestableceContrasenia>()
                         .Where(q => q.CORREO_ELECTRONICO == correoElectronico && q.ESTADO == Flags.Habilitado)
                         .Select(x => x.FECHA_REGISTRO).FirstOrDefaultAsync();
        }

        public async Task<UsuarioRestableceContrasenia?> SelectUsuarioRestableceContraseniaByCorreoAsync(string correoElectronico)
        {
            return await All<UsuarioRestableceContrasenia>()
                        .AsNoTracking()
                        .Where(q => q.CORREO_ELECTRONICO == correoElectronico && q.ESTADO == Flags.Habilitado && q.VERIFICADO == Flags.TokenNoVerificado)
                        .FirstOrDefaultAsync();
        }

        public async Task InsertUsuarioTokenRestableceContraseniaAsync(UsuarioRestableceContrasenia usuarioOTP)
        {
            await base.InsertAsync(usuarioOTP);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuarioTokenRestableceContraseniaAsync(UsuarioRestableceContrasenia usuarioOTP)
        {
            await base.UpdateAsync(usuarioOTP);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioTokenRestableceContraseniaAsync(UsuarioRestableceContrasenia usuarioOTP)
        {
            await base.DeleteAsync(usuarioOTP);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActivoAsync(int idUsuario, bool flgActivo)
        {
            await _context.Usuario.Where(x => x.ID == idUsuario).ExecuteUpdateAsync(u => u.SetProperty(e => e.ACTIVO, flgActivo));
        }

        #endregion
    }
}
