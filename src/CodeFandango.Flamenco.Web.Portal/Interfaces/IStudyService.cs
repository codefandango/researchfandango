using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Web.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Web.Portal.Interfaces
{
    public interface IStudyService : IObjectEditorService<Study, StudyModel>
    {
        Task<Success<StudyModel>> CreateOrUpdateStudy(StudyModel study);
    }
}
