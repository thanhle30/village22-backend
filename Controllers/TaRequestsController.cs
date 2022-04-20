using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Village22.Models;

namespace Village22.Controllers
{
    [Authorize]
    public class TaRequestsController : Controller
    {
        private readonly VillageContext _context;
        private Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;

        public TaRequestsController(VillageContext context, UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _context = context;
            userManager = userMngr;
            roleManager = roleMngr;
        }

        [Authorize(Roles = "Teacher,Admin")]
        // GET: TaRequests
        public async Task<IActionResult> Index()
        {
            IQueryable<TaRequest> villageContext = _context.TaRequests.Include(t => t.Status).Include(t => t.TeachingAssignment).ThenInclude(t => t.Course);

            if (this.User.IsInRole("Teacher"))
            {
                villageContext = villageContext.Where(t => t.TeachingAssignment.TeacherId == userManager.GetUserId(this.User));
            }

            return View(await villageContext.ToListAsync());
        }

        // GET: TaRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taRequest = await _context.TaRequests
                .Include(t => t.Status)
                .Include(t => t.TeachingAssignment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taRequest == null)
            {
                return NotFound();
            }

            return View(taRequest);
        }

        [Authorize(Roles = "Teacher")]
        // GET: TaRequests/Create
        public IActionResult Create()
        {
            /// need: Course that associates with this teacher (Name, Id) ---- course is much more approriate. Then, from course and teacherid -> querry teachingAssignment -> save that for TaRequest
            /// No need to choose StatusId, since initially, it's default
            /// What does TaRequest foreign-key with? - TaAssignment, then provide TaAssignment related to this User only.
            /// 
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var teachingAssms = _context.TeachingAssignments.Where(assm => assm.TeacherId == userManager.GetUserId(currentUser));

            List<int> courseIds = new List<int>();

            foreach (TeachingAssignment teachingAssm in teachingAssms)
            {
                courseIds.Add(teachingAssm.CourseId);
            }

            var courses = _context.Courses.Where(c => courseIds.Contains(c.Id));

            ViewData["Courses"] = new SelectList(courses, "Id", "Title");
            return View();
        }

        [Authorize(Roles = "Teacher")]
        // POST: TaRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaRequestViewModel model)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            if (ModelState.IsValid)
            {
                var teachingAssm = _context.TeachingAssignments.First(assm => assm.TeacherId == userManager.GetUserId(currentUser) && assm.CourseId == model.CourseId); //only 1 teaching assignment satifies this
                TaRequest taRequest = new TaRequest
                {
                    Message = model.Message,
                    DateCreated = model.DateCreated,
                    TeachingAssignmentId = teachingAssm.Id,
                    StatusId = model.StatusId
                };
                _context.Add(taRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            } 

            var teachingAssms = _context.TeachingAssignments.Where(assm => assm.TeacherId == userManager.GetUserId(currentUser));
            List<int> courseIds = new List<int>();
            foreach (TeachingAssignment teachingAssm in teachingAssms)
            {
                courseIds.Add(teachingAssm.CourseId);
            }
            var courses = _context.Courses.Where(c => courseIds.Contains(c.Id));
            ViewData["Courses"] = new SelectList(courses, "Id", "Title");

            return View(model);
        }

        // GET: TaRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taRequest = await _context.TaRequests.FindAsync(id);
            if (taRequest == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.TaRequestStatuses, "Id", "Id", taRequest.StatusId);
            ViewData["TeachingAssignmentId"] = new SelectList(_context.TeachingAssignments, "Id", "Id", taRequest.TeachingAssignmentId);
            return View(taRequest);
        }

        // POST: TaRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeachingAssignmentId,Message,DateCreated,StatusId")] TaRequest taRequest)
        {
            if (id != taRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaRequestExists(taRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.TaRequestStatuses, "Id", "Id", taRequest.StatusId);
            ViewData["TeachingAssignmentId"] = new SelectList(_context.TeachingAssignments, "Id", "Id", taRequest.TeachingAssignmentId);
            return View(taRequest);
        }

        // GET: TaRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taRequest = await _context.TaRequests
                .Include(t => t.Status)
                .Include(t => t.TeachingAssignment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taRequest == null)
            {
                return NotFound();
            }

            return View(taRequest);
        }

        // POST: TaRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taRequest = await _context.TaRequests.FindAsync(id);
            _context.TaRequests.Remove(taRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaRequestExists(int id)
        {
            return _context.TaRequests.Any(e => e.Id == id);
        }
    }
}
