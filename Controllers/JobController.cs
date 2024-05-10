using BLL;
using DAL;
using Entities.Entities;
using JobNest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobNest.Controllers
{
    [Authorize(Roles ="Employer")]
    public class JobController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public JobController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser CurrentUser { get; set; }

        JobService jobService = new JobService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return RedirectToAction("Index");
            }

            var userId = await _userManager.GetUserIdAsync(CurrentUser);
            TempData["UserId"] = userId;
            TempData.Keep();

            var jobs = jobService.GetJobs();
            var jobsPostedByCurrentUser = jobs.Where(x => x.UserId == userId).ToList();
            return Json(jobsPostedByCurrentUser);
        }

        [HttpPost]
        public IActionResult AddNewJob([FromBody] Job jobFormData)
        {
            var response = jobService.AddJobService(jobFormData);
            return Json(response);
        }

        [HttpGet]
        public IActionResult GetJobById(int id)
        {
            var job = jobService.GetJobById(id);
            if (job != null)
            {
                return Json(job);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public JsonResult UpdateJob([FromBody] Job job)
        {
            var jobUpdated = jobService.UpdateJob(job);
            return Json(jobUpdated);
        }

        [HttpGet]
        public IActionResult DeleteJob(int id)
        {

            if (jobService.DeleteJob(id))
            {
                return Json("Success");
            }
            return NotFound();
        }
    }
}
