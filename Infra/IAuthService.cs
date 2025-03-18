namespace ApiTesta.Infra
{
    public interface IAuthService 
    {
        string GenerateJwtToken(string email);
        string computedSha256Hash(string password);
    }
}
