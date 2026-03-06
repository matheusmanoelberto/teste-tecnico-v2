using FinancialControl.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialControl.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();

        return services;
    }
}
