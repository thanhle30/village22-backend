using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Village22.Models;

namespace Village22.Controllers
{
    public class CoursesController : Controller
    {
        private readonly VillageContext _context;
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public CoursesController(VillageContext context, UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _context = context;
            userManager = userMngr;
            roleManager = roleMngr;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();

            //want a list of CourseViewModel (contains TeacherName also)
            List<CourseViewModel> cvModels = new List<CourseViewModel>();

            foreach (Course course in courses)
            {

                var teachingAssignment = await _context.TeachingAssignments.FirstOrDefaultAsync(t => t.CourseId == course.Id);

                var teacherName = "";

                if (teachingAssignment != null)
                {
                    var teacherId = teachingAssignment.TeacherId;
                    teacherName = userManager.Users.FirstOrDefault(u => u.Id == teacherId).Name;
                }
 
                CourseViewModel cvModel = new CourseViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    DateStart = course.DateStart,
                    DateEnd = course.DateEnd,
                    TeacherName = teacherName
                };

                cvModels.Add(cvModel);
            }

            return View(cvModels);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            List<User> users = new List<User>();

            var users_db = userManager.Users.ToList();

            foreach (User user in users_db)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);

                if (user.RoleNames.Contains("Teacher"))
                {
                    users.Add(user);
                }              
            }
            ViewBag.Teachers = users;

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,DateStart,DateEnd")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(course);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(course);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model) 
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Title = model.Title,
                    Description = model.Description,
                    DateStart = model.DateStart,
                    DateEnd = model.DateEnd
                };
                _context.Add(course);
                await _context.SaveChangesAsync();

                var addedCourse = await _context.Courses.FirstAsync(c => c.Title == model.Title & c.Description == model.Description);

                TeachingAssignment taAssm = new TeachingAssignment
                {
                    TeacherId = model.TeacherId,
                    CourseId = addedCourse.Id
                };

                _context.Add(taAssm);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DateStart,DateEnd")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
