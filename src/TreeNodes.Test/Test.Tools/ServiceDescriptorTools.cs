using Microsoft.Extensions.DependencyInjection;
using System;

namespace TreeNodes.Test
{
    internal static class ServiceDescriptorTools
    {
        public static Type GetImplementationType(this ServiceDescriptor service)
        {
            if (service.ImplementationType != null)
            {
                return service.ImplementationType;
            }

            if (service.ImplementationInstance != null)
            {
                return service.ImplementationInstance!.GetType();
            }

            if (service.ImplementationFactory != null)
            {
                Type[] genericTypeArguments = service.ImplementationFactory!.GetType().GenericTypeArguments;
                return genericTypeArguments[1];
            }

            return null;
        }
    }
}
