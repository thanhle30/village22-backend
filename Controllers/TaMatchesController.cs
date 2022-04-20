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
    public class TaMatchesController : Controller
    {
        private readonly VillageContext _context;
        private Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;

        public TaMatchesController(VillageContext context, UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _context = context;
            userManager = userMngr;
            roleManager = roleMngr;
        }

        // GET: TaMatches
        public async Task<IActionResult> Index()
        {
            IQueryable<TaMatch> villageContext =
                _context.TaMatches
                .Include(t => t.Status)
                .Include(t => t.TaRequest)
                    .ThenInclude(t => t.TeachingAssignment)
                        .ThenInclude(t => t.Course)
                .Include(t => t.TaRequest)
                    .ThenInclude(t => t.TeachingAssignment)
                        .ThenInclude(t => t.Teacher)
                .Include(t => t.TaRequest)
                    .ThenInclude(t => t.Status)              
                .Include(t => t.Ta);

            if (this.User.IsInRole("Teacher"))
            {
                villageContext = villageContext.Where(t => t.TaRequest.TeachingAssignment.TeacherId == userManager.GetUserId(this.User));
            }

            if (this.User.IsInRole("Ta"))
            {
                villageContext = villageContext.Where(t => t.Ta.Id == userManager.GetUserId(this.User));
            }

            ViewData["IsAdmin"] = this.User.IsInRole("Admin");
                //await userManager.GetRolesAsync(await userManager.GetUserAsync(this.User).;

            return View(await villageContext.ToListAsync());
        }

        // GET: TaMatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taMatch = await _context.TaMatches
                .Include(t => t.Status)
                .Include(t => t.TaRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taMatch == null)
            {
                return NotFound();
            }

            return View(taMatch);
        }

        [Authorize(Roles = "Admin")]
        // GET: TaMatches/Create
        public async Task<IActionResult> Create()
        {
            ///Choose a TA Request (show teacher's name, and course's name to identify)
            ///Need to a list of TAs to select from 
            ///No need to deal with status here

            ViewBag.TaRequests = _context.TaRequests
                .Include(t => t.Status)
                .Include(t => t.TeachingAssignment)
                    .ThenInclude(t => t.Course)
                .Include(t => t.TeachingAssignment)
                    .ThenInclude(t => t.Teacher)
                .ToList();

            var ta = await userManager.GetUsersInRoleAsync("Ta");

            ViewData["TAs"] = new SelectList(ta, "Id", "Name"); 

            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: TaMatches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaRequestId,TaId,DateCreated,MessageFromTa, MessageFromTeacher,StatusId")] TaMatch taMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TaRequests = _context.TaRequests
                .Include(t => t.Status)
                .Include(t => t.TeachingAssignment)
                    .ThenInclude(t => t.Course)
                .Include(t => t.TeachingAssignment)
                    .ThenInclude(t => t.Teacher)
                .ToList();

            var ta = await userManager.GetUsersInRoleAsync("Ta");

            ViewData["TAs"] = new SelectList(ta, "Id", "Name");


            return View(taMatch);
        }

        [Authorize(Roles = "Teacher,Ta")]
        //GET: TaMatches/AcceptMatch/{id}
        public ActionResult AcceptMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Accept";

            var decide = new DecideTaMatchViewModel {
                Id = (int) id
            };
            return View("Decide", decide);
        }

        [Authorize(Roles = "Teacher, Ta")]
        [HttpPost]
        public async Task<IActionResult> AcceptMatch(DecideTaMatchViewModel model)
        {
            /// Change Status accordingly, depending on role (Teacher or TA)
            /// Admin won't have access to this button 
            /// After hit the button: show a pop-up that allows entering message and confirming/cancelling the action 
            /// 

            if (ModelState.IsValid)
            {
                var taMatch = await _context.TaMatches.FirstOrDefaultAsync(t => t.Id == model.Id);
                if (taMatch == null)
                {
                    return NotFound();
                }
                if (this.User.IsInRole("Teacher"))
                {
                    taMatch.MessageFromTeacher = model.Message;
                    switch (taMatch.StatusId)
                    {
                        case 1:
                            taMatch.StatusId = 2;
                            break;
                        case 3:
                            taMatch.StatusId = 4;
                            break;
                        case 6:
                            taMatch.StatusId = 2;
                            break;
                        default:
                            break;
                    }
                }
                if (this.User.IsInRole("Ta"))
                {
                    taMatch.MessageFromTa = model.Message;
                    switch (taMatch.StatusId)
                    {
                        case 1:
                            taMatch.StatusId = 3;
                            break;
                        case 2:
                            taMatch.StatusId = 4;
                            break;
                        case 5:
                            taMatch.StatusId = 3;
                            break;
                        default:
                            break;
                    }
                }

                _context.Update(taMatch);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Teacher,Ta")]
        //GET: TaMatches/RejectMatch/{id}
        public ActionResult RejectMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Reject";

            var decide = new DecideTaMatchViewModel
            {
                Id = (int)id
            };
            return View("Decide", decide);
        }

        [Authorize(Roles = "Teacher, Ta")]
        [HttpPost]
        public async Task<IActionResult> RejectMatch(DecideTaMatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taMatch = await _context.TaMatches.FirstOrDefaultAsync(t => t.Id == model.Id);
                if (taMatch == null)
                {
                    return NotFound();
                }
                if (this.User.IsInRole("Teacher"))
                {
                    taMatch.MessageFromTeacher = model.Message;
                    if (taMatch.StatusId == 5)
                    {
                        //do nothing
                    }
                    else taMatch.StatusId = 6;
                }
                if (this.User.IsInRole("Ta"))
                {
                    taMatch.MessageFromTa = model.Message;
                    if (taMatch.StatusId == 6)
                    {
                        //do nothing
                    }
                    else taMatch.StatusId = 5;
                }

                _context.Update(taMatch);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: TaMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taMatch = await _context.TaMatches.FindAsync(id);
            if (taMatch == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.TaMatchStatuses, "Id", "Id", taMatch.StatusId);
            ViewData["TaRequestId"] = new SelectList(_context.TaRequests, "Id", "Id", taMatch.TaRequestId);
            return View(taMatch);
        }

        // POST: TaMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaRequestId,TaId,DateCreated,MessageFromTa,MessageFromTeacher,StatusId")] TaMatch taMatch)
        {
            if (id != taMatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaMatchExists(taMatch.Id))
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
            ViewData["StatusId"] = new SelectList(_context.TaMatchStatuses, "Id", "Id", taMatch.StatusId);
            ViewData["TaRequestId"] = new SelectList(_context.TaRequests, "Id", "Id", taMatch.TaRequestId);
            return View(taMatch);
        }

        // GET: TaMatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taMatch = await _context.TaMatches
                .Include(t => t.Status)
                .Include(t => t.TaRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taMatch == null)
            {
                return NotFound();
            }

            return View(taMatch);
        }

        // POST: TaMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taMatch = await _context.TaMatches.FindAsync(id);
            _context.TaMatches.Remove(taMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaMatchExists(int id)
        {
            return _context.TaMatches.Any(e => e.Id == id);
        }
    }
}
