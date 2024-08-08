using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    public class UniqueCodeGenerator : IUniqueCodeGenerator
    {
        private readonly IDataAccess data;

        public UniqueCodeGenerator(IDataAccess data)
        {
            this.data = data;
        }

        public async Task<Success<string>> GenerateUniqueCode(string scope, string input)
        {
            var pascalCase = ToPascalCase(input);

            var index = 1;
            while (await data.UniqueCodeExists(scope, pascalCase))
            {
                pascalCase = ToPascalCase(input) + index;
                index++;

                if (index > 100)
                    return new Success<string>(false, "Failed to generate unique code");
            }

            return new Success<string>(model: pascalCase);
        }

        public string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            StringBuilder pascalCase = new StringBuilder();
            bool newWord = true;

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (newWord)
                    {
                        pascalCase.Append(char.ToUpper(c, CultureInfo.InvariantCulture));
                        newWord = false;
                    }
                    else
                    {
                        pascalCase.Append(char.ToLower(c, CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    newWord = true;
                }
            }

            return pascalCase.ToString();
        }
    }
}
