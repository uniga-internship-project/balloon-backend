namespace uniga_internship_project.Services.AuthorizeSerivice.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
