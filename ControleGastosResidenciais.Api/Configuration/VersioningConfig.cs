using Asp.Versioning;

namespace ControleGastosResidenciais.Api.Configuration;

public static class VersioningConfig
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new ApiVersion(1);
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'V";
                o.SubstituteApiVersionInUrl = true;
            });
        return services;
    }
}
