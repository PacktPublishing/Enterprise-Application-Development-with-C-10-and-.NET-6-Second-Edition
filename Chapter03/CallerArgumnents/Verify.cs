using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CallerArgumnents
{
    internal static class Verify
    {
        public static void IsTrue(this bool value,
        [CallerArgumentExpression("value")] string expression = null)
        {
            if (!value)
                Throw(expression);
        }
        private static void Throw(string expression)
            => throw new ArgumentException($"{expression} must be True");
    }
}
