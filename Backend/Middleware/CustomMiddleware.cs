using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class CustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Check if the attribute is present on the current action
        if (context.GetEndpoint()?.Metadata.GetMetadata<MyCustomAttribute>() != null)
        {
            // // Set custom value in the HttpContext
            // context.Items["URL"] = context.Request.Path;

            // // TODO: Add your middleware logic here.
            // var jwtToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // var jwtToken = context.Request.Query["access_token"].ToString();
            // if (!string.IsNullOrEmpty(jwtToken))
            // {
            //     context.Request.Headers["Authorization"] = $"Bearer {jwtToken}";
            //     var jwtHandler = new JwtSecurityTokenHandler();
            //     var token = jwtHandler.ReadJwtToken(jwtToken.Trim('"'));

            //     if (token != null)
            //     {
            //         context.User = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));
            //         context.Items["User"] = new ClaimsPrincipal(new ClaimsIdentity(token.Claims));
            //     }
            // }
        }
        // Call the next middleware in the pipeline
        await next(context);
    }
}
