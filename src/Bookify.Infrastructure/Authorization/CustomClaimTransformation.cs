using System.Security.Claims;
using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Bookify.Infrastructure.Authorization;

public class CustomClaimTransformation : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvider;

    public CustomClaimTransformation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(claim => claim.Type == ClaimTypes.Role) && 
            principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }

        var authorizationService = _serviceProvider.GetRequiredService<AuthorizationService>();

        var identityId = principal.GetIdentityId();

        var userRoles = await authorizationService.GetRolesForUserAsync(identityId);

        var claimsIdentity = new ClaimsIdentity();
        
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userRoles.Id.ToString()));

        foreach (var role in userRoles.Roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, role.Name));
        }
        
        principal.AddIdentity(claimsIdentity);
        return principal;
    }
}