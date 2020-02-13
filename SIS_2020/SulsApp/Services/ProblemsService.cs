using SulsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SulsApp.Services
{
    using System.Linq;
    using ViewModels.Home;
    using ViewModels.Problems;
    using ViewModels.Submissions;

    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
            };
            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public IEnumerable<IndexProblemViewModel> GetAllProblems()
        {
            return db
                .Problems
                .Select(x => new IndexProblemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count(),
                })
                .ToList();
        }

        public ProblemDetailsViewModel GetProblemDetails(string problemId)
        {
            return this.db
                .Problems
                .Where(x => x.Id == problemId)
                .Select(
                x => new ProblemDetailsViewModel
                {
                    Name = x.Name,
                    Problems = x.Submissions.Select(s =>
                        new ProblemDetailsSubmissionViewModel
                        {
                            CreatedOn = s.CreatedOn,
                            AchievedResult = s.AchievedResult,
                            SubmissionId = s.Id,
                            MaxPoints = x.Points,
                            Username = s.User.Username,
                        })
                })
                .FirstOrDefault();
        }

        public CreateFormViewModel GetProblemForSubmissionForm(string problemId)
        {
            return this.db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => new CreateFormViewModel
                {
                    Name = x.Name,
                    ProblemId = x.Id,
                })
                .FirstOrDefault();
        }
    }
}
