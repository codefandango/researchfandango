using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Studies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Web.Portal.Interfaces
{
    public interface IStudyService
    {
        Task<Success<List<StudyModel>>> GetStudies();
        Task<Success<EditableFieldCollection>> GetDefinition();
        Task<Success<StudyModel>> GetStudy(int id);
    }
}
