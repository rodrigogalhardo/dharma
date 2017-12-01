using System;

namespace Dharma.Application.Services.Attributes
{
    /// <summary>
    /// Atributo para configuração de injeção de dependência.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionTypeAttribute : Attribute
    {
        /// <summary>
        /// Tipo de injeção de dependência.
        /// </summary>
        public readonly InjectionType InjectionType;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="injectionType">Tipo de injeção de dependência.</param>
        public InjectionTypeAttribute(InjectionType injectionType)
        {
            InjectionType = injectionType;
        }
    }

    /// <summary>
    /// Tipo de injeção de dependência.
    /// </summary>
    public enum InjectionType
    {
        /// <summary>
        /// Singleton
        /// </summary>
        Singleton,

        /// <summary>
        /// Scoped
        /// </summary>
        Scoped
    }
}
