namespace PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON
{
    public interface IJwTokenGenerator
    {
        string GenerateTokenSignIn(Guid idUsuarioGuid, string nombres, string apellidos, string correo, int idRol, string nombreRol, int idMoneda, string idFoto, string urlfoto);
    }
}
