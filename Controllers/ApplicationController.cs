using BLL;
using DAL;
using Entities.Entities;
using JobNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using static System.Net.Mime.MediaTypeNames;

namespace JobNest.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser CurrentUser { get; set; }

        ApplicationService service = new ApplicationService();

        // returns the view for list of applications
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            var applications = service.GetApplicationsService();
            var applicationsForCurrentUser = applications.Where(x => x.UserId == CurrentUser.Id).ToList();
            return Json(applicationsForCurrentUser);
        }

        // returns the view for job listings
        public IActionResult Jobs()
        {
            return View();
        }

        // gets the jobs posted by employer and passes it to view
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return RedirectToAction("Index");
            }

            var userEmail = CurrentUser.Email;
            var userId = await _userManager.GetUserIdAsync(CurrentUser);

            TempData["UserId"] = userId;
            //TempData.Keep();

            //*********
            JobRepository jobRepository = new JobRepository();
            List<Job> jobs = jobRepository.GetJobs();
            return Json(jobs);
        }


        // returns the view to allow user apply for a job
        public async Task<IActionResult> CreateApplication(int jobId)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return RedirectToAction("Index");
            }

            var userEmail = CurrentUser.Email;
            var userId = await _userManager.GetUserIdAsync(CurrentUser);

            TempData["JobId"] = jobId;
            TempData["UserEmail"] = userEmail;
            TempData["UserId"] = userId;
            TempData.Keep();
            return View();
        }

        // posts an application information to repository
        [HttpPost]
        public IActionResult CreateApplication([FromBody] Entities.Entities.Application applicationFormData)
        {
            var response = service.AddApplicationService(applicationFormData);
            return Json(response);
        }

        // handles deleting an application
        [HttpPost]
        public JsonResult DeleteApplication(int applicationId)
        {
            if (service.DeleteApplicationService(applicationId))
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
