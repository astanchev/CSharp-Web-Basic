using System;
using System.Collections.Generic;
using System.Text;

namespace SulsApp.Services
{
    using ViewModels.Home;
    using ViewModels.Problems;
    using ViewModels.Submissions;

    public interface IProblemsService
    {
        void CreateProblem(string name, int points);

        IEnumerable<IndexProblemViewModel> GetAllProblems();

        ProblemDetailsViewModel GetProblemDetails(string problemId);

        CreateFormViewModel GetProblemForSubmissionForm(string problemId);
    }
}
