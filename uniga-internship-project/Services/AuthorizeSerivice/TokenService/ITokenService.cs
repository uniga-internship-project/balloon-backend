namespace uniga_internship_project.Services.AuthorizeSerivice.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
