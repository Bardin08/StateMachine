using System;

namespace StateMachine.Extensions
{
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the base type of the given type or null if it's equal to TCutOff.
        /// </summary>
        public static Type GetBaseType<TCutOff>(this Type type)
        {
            return type != typeof(TCutOff) ? type.BaseType : null;
        }
    }
}