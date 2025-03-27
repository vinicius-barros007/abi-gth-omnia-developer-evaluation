using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.IoC.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void RegisterCommandValidators(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Application.ApplicationLayer))
                ?? throw new InvalidOperationException("Cannot find assembly");

            var validatorTypes = assembly.GetTypes().Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)
            );

            foreach (var validatorType in validatorTypes)
            {
                var validatorInterface = validatorType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

                services.AddScoped(validatorInterface, validatorType);
            }
        }
    }
}
