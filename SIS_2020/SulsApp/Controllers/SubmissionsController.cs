namespace SulsApp.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using Services;
    public class SubmissionsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var problem = this.problemsService.GetProblemForSubmissionForm(id);

            if (problem == null)
            {
                return this.Error("Problem not found!");
            }

            return this.View(problem);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (code == null || code.Length < 30)
            {
                return this.Error("Please provide code with at least 30 characters.");
            }

            this.submissionsService.Create(this.User, problemId, code);

            return Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.submissionsService.Delete(id);

            return this.Redirect("/");
        }
    }
}
