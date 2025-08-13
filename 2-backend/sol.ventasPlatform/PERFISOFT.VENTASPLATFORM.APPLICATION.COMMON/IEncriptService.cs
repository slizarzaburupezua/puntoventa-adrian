namespace PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON
{
    public interface IEncriptService
    {
        string CreateHashPassword(string password, string salt);
        string GenerateSaltPassword();
        string OpenSSLEncrypt(string plainText);
        string OpenSSLDecrypt(string encrypted);
    }
}
