namespace MultiTenantManagement.Infrastructure.Features.Authentication.Dtos
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}
