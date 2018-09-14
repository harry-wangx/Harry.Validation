using Harry.Validation.CodeProvider;
using Harry.Validation.ImageProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<ICodeFactory, CodeFactory>();
            services.TryAddSingleton<IImageFactory, ImageFactory>();

            return services;
        }

        public static IServiceCollection AddGeneralCodeProvider(this IServiceCollection services,Action<GeneralCodeOptions> optionsAction=null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<ICodeProvider, GeneralCodeProvider>();

            if (optionsAction != null)
            {
                GeneralCodeOptions options = new GeneralCodeOptions();
                optionsAction.Invoke(options);
                services.AddSingleton(options);
            }

            return services;
        }

        public static IServiceCollection AddGeneralImageProvider(this IServiceCollection services, Action<GeneralImageOptions> optionsAction = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IImageProvider, GeneralImageProvider>();

            if (optionsAction != null)
            {
                GeneralImageOptions options = new GeneralImageOptions();
                optionsAction.Invoke(options);
                services.AddSingleton(options);
            }

            return services;
        }
    }
}
