﻿#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Generated by ScaffoldApp.
namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
	public class BrandsController : Controller
    {
        // Add service injections here
        private readonly IBrandService _brandService;
        

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [AllowAnonymous]
        // GET: Brands
        public IActionResult Index()
        {
            List<BrandModel> brandList = _brandService.Query().ToList();
            return View(brandList);
        }

        [AllowAnonymous]
        // GET: Brands/Details/5
        public IActionResult Details(int id)
        {
            BrandModel brand = _brandService.Query().SingleOrDefault(b => b.Id == id);  
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                var result = _brandService.Add(brand);
                if(result.IsSuccessful)
                return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(brand);
        }

        // GET: Brands/Edit/5
        public IActionResult Edit(int id)
        {
            BrandModel brand = _brandService.Query().SingleOrDefault(b => b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(brand);
        }

        // POST: Brands/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                var result = _brandService.Update(brand);
                if (result.IsSuccessful)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(brand);
        }

        // GET: Brands/Delete/5
        public IActionResult Delete(int id)
        {
            BrandModel brand = _brandService.Query().SingleOrDefault(b => b.Id==id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _brandService.Delete(id);
            return RedirectToAction(nameof(Index), new { message = result.Message });
        }
	}
}
