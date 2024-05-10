using Entities.Context;
using Entities.ViewModel;
using JobNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobNest.Controllers
{
    [Authorize]
    public class ApplicantJobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantJobController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            JobNestDbContext jobNestDbContext = new JobNestDbContext();

            var users = _context.Users.ToList(); // Accessing users using the Users property of ApplicationDbContext
            var jobs = jobNestDbContext.Jobs.ToList();
            var applications = jobNestDbContext.Applications.ToList();

            var usersAndApplications = users
                                    .Where(user => !user.IsEmployer) // Filter users who are applicants
                                    .Join(
                                        applications,
                                        user => user.Id,
                                        app => app.UserId,
                                        (user, app) => new { User = user, Application = app }
                                    )
                                    .GroupBy(
                                        x => new { x.User.Id, x.User.UserName }, // Group by user ID and username
                                        x => x.Application,
                                        (key, group) => new ApplicantJobVM
                                        {
                                            ApplicantName = key.UserName,
                                            AppliedJobs = group.ToList()
                                        }).ToList();



            return View(usersAndApplications);
        }
    }
}
