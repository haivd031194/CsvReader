using System;

#if UNITY_EDITOR
namespace Zitga.CsvTools
{
    public static class StringUtils
    {
        public static string ConvertSnakeCaseToCamelCase(string snakeCase)
        {
            var strings = snakeCase.Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);
            var result = strings[0];
            for (int i = 1; i < strings.Length; i++)
            {
                var currentString = strings[i];
                result += char.ToUpperInvariant(currentString[0]) +
                          currentString.Substring(1, currentString.Length - 1);
            }

            return result;
        }
    }
}
#endif
