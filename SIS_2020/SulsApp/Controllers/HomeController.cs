namespace SulsApp.Controllers
{
    using SIS.HTTP;
    using SIS.HTTP.Logging;
    using SIS.MvcFramework;
    using Services;
    using ViewModels.Home;
    using System;
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IProblemsService problemsService;

        public HomeController(ILogger logger, IProblemsService problemsService)
        {
            this.logger = logger;
            this.problemsService = problemsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var problems = this.problemsService.GetAllProblems();

                var loggedInViewModel = new LoggedInViewModel()
                {
                    Problems = problems,
                };

                return this.View(loggedInViewModel, "IndexLoggedIn");
            }

            this.logger.Log("Hello from Index");
            var viewModel = new IndexViewModel
            {
                Message = "Welcome to SULS Platform!",
                Year = DateTime.UtcNow.Year,
            };
            return this.View(viewModel);
        }
    }
}
