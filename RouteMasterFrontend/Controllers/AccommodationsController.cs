﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RouteMasterFrontend.EFModels;
using RouteMasterFrontend.Models.ViewModels.Accommodation;

namespace RouteMasterFrontend.Controllers
{
    public class AccommodationsController : Controller
    {
        private readonly RouteMasterContext _context;

        public AccommodationsController(RouteMasterContext context)
        {
            _context = context;
        }

        // GET: Accommodations
        public async Task<IActionResult> Index()
        {
            
                //IEnumerable<AccommodationIndexVM> accommodations;

                   // accommodations = GetAccommodations(identity.Id)
                   //.OrderBy(a => a.Name.Contains("(已下架)"))
                   //.ThenByDescending(a => a.Id);

                   // return View(accommodations);//.ToList());


                   // accommodations = GetAccommodations(null)
                   //.OrderBy(a => a.Name.Contains("(已下架)"))
                   //.ThenByDescending(a => a.Id);

                   // return View(accommodations);

            var routeMasterContext = _context.Accommodations.Include(a => a.AcommodationCategory).Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);
            return View(await routeMasterContext.ToListAsync());
        }

        // GET: Accommodations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accommodations == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.AcommodationCategory)
                .Include(a => a.Partner)
                .Include(a => a.Region)
                .Include(a => a.Town)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // GET: Accommodations/Create
        public IActionResult Create()
        {
            ViewData["AcommodationCategoryId"] = new SelectList(_context.AcommodationCategories, "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Email");
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name");
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name");
            return View();
        }

        // POST: Accommodations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AcommodationCategoryId,PartnerId,Name,Description,Grade,RegionId,TownId,Address,PositionX,PositionY,Website,IndustryEmail,PhoneNumber,ParkingSpace,CheckIn,CheckOut,Status,Image,CreateDate")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accommodation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcommodationCategoryId"] = new SelectList(_context.AcommodationCategories, "Id", "Name", accommodation.AcommodationCategoryId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Email", accommodation.PartnerId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", accommodation.RegionId);
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accommodations == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations.FindAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            ViewData["AcommodationCategoryId"] = new SelectList(_context.AcommodationCategories, "Id", "Name", accommodation.AcommodationCategoryId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Email", accommodation.PartnerId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", accommodation.RegionId);
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcommodationCategoryId,PartnerId,Name,Description,Grade,RegionId,TownId,Address,PositionX,PositionY,Website,IndustryEmail,PhoneNumber,ParkingSpace,CheckIn,CheckOut,Status,Image,CreateDate")] Accommodation accommodation)
        {
            if (id != accommodation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accommodation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccommodationExists(accommodation.Id))
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
            ViewData["AcommodationCategoryId"] = new SelectList(_context.AcommodationCategories, "Id", "Name", accommodation.AcommodationCategoryId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "Id", "Email", accommodation.PartnerId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", accommodation.RegionId);
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // GET: Accommodations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accommodations == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.AcommodationCategory)
                .Include(a => a.Partner)
                .Include(a => a.Region)
                .Include(a => a.Town)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accommodations == null)
            {
                return Problem("Entity set 'RouteMasterContext.Accommodations'  is null.");
            }
            var accommodation = await _context.Accommodations.FindAsync(id);
            if (accommodation != null)
            {
                _context.Accommodations.Remove(accommodation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccommodationExists(int id)
        {
          return (_context.Accommodations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}