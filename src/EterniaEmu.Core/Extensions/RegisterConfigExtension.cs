using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EterniaEmu.Core.Extensions;

public static class RegisterConfigExtension
{
    public static IHostApplicationBuilder RegisterConfig<TConfig>(
        this IHostApplicationBuilder hostBuilder, string sectionName
    )
        where TConfig : class
    {
        // var config = default(TConfig);
        // configuration.GetSection(sectionName).Bind(config);
        //
        //
        // if (config == null)
        // {
        //     throw new ConfigSectionNotFoundException(sectionName);
        // }


        hostBuilder.Services.AddOptions<TConfig>().BindConfiguration(sectionName);


        return hostBuilder;
    }
}
