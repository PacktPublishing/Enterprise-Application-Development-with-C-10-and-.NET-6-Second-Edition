using System.Runtime.CompilerServices;

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
