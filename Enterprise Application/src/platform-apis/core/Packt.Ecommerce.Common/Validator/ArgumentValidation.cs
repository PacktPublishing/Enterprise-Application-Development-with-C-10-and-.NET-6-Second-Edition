// "//-----------------------------------------------------------------------".
// <copyright file="ArgumentValidation.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common.Validator
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Validation extensions.
    /// </summary>
    public static class ArgumentValidation
    {
        /// <summary>
        /// Throw argument null exception if value is null or empty.
        /// </summary>
        /// <param name="input">The value to be validated.</param>
        /// <param name="name">The parameter.</param>
        public static void ThrowIfNullOrEmpty([NotNull] string input, string name)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throw argument null exception if value is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="value">Argument Value.</param>
        /// <param name="expression">Argument expression.</param>
        public static void ThrowIfNull<T>(
            T value,
            [CallerArgumentExpression("value")] string expression = null)
            where T : class
        {
            if (value == null)
            {
                Throw(expression);
            }
        }

        /// <summary>
        /// Helper method to throw ArgumentException.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <exception cref="ArgumentException">Throws ArgumentException with the argument passed in.</exception>
        private static void Throw(string expression)
            => throw new ArgumentException($"Argument {expression} must not be null");
    }
}
