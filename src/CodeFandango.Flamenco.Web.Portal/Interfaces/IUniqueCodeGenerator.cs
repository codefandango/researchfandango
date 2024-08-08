using CodeFandango.Flamenco.Models;

namespace CodeFandango.Flamenco.Web.Portal.Interfaces
{
    public interface IUniqueCodeGenerator
    {
        Task<Success<string>> GenerateUniqueCode(string scope, string input);
    }
}
