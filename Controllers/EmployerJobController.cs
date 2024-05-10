using Entities.Context;
using Entities.Entities;
using Entities.ViewModel;
using JobNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace JobNest.Controllers
{
    [Authorize]
    public class EmployerJobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployerJobController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            JobNestDbContext jobNestDbContext = new JobNestDbContext();

            var users = _context.Users.ToList(); // Accessing users using the Users property of ApplicationDbContext
            var jobs = jobNestDbContext.Jobs.ToList();

            var usersAndJobs = users
                                    .Where(user => user.IsEmployer) // Filter users who are employers
                                    .Join(
                                        jobs,
                                        user => user.Id,
                                        job => job.UserId,
                                        (user, job) => new { User = user, Job = job }
                                    )
                                    .GroupBy(
                                        x => new { x.User.Id, x.User.UserName }, // Group by user ID and username
                                        x => x.Job,
                                        (key, group) => new EmployerJobVM
                                        {
                                            UserId = key.Id,
                                            EmployerName = key.UserName,
                                            Jobs = group.ToList()
                                        }).ToList();



            return View(usersAndJobs);
        }
    }
}
