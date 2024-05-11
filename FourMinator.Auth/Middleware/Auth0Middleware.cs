using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;


namespace FourMinator.Auth.Middleware
{
    public class Auth0Middleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public Auth0Middleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task Invoke(HttpContext context)
        {

            var error = false;

            var authHeader = context.Request.Headers["Authorization"];
            if (authHeader.Count == 0)
            {
                context.Response.StatusCode = 401;
                error = true;
            }

            if (authHeader[0] == null)
            {
                context.Response.StatusCode = 401;
                error = true;
            }

            if (!authHeader[0].StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                error = true;
            }

            if (error)
            {
                await context.Response.WriteAsync("Invalid token or Auth header!");
                return;
            }

            var token = authHeader[0].Substring("Bearer ".Length);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("https://dev-zs8kctz8n04sgjgm.us.auth0.com/userinfo");
            if (!response.IsSuccessStatusCode)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(context);
        }   

    }
}
