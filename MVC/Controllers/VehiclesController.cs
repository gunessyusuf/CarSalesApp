﻿#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;

//Generated by ScaffoldApp.
namespace MVC.Controllers
{
    public class VehiclesController : Controller
    {
        // Add service injections here
        private readonly IVehicleService _vehicleService;
        private readonly IBrandService _brandService;
        private ICustomerService _customerService;
        private IColorService _colorService;
        private IModelService _modelService;
		public VehiclesController(IVehicleService vehicleService, IBrandService brandService, ICustomerService customerService, IColorService colorService, IModelService modelService)
		{
			_vehicleService = vehicleService;
			_brandService = brandService;
			_customerService = customerService;
			_colorService = colorService;
			_modelService = modelService;
		}

		// GET: Vehicles
		public IActionResult Index()
        {
            List<VehicleModel> vehicleList = _vehicleService.Query().ToList(); // TODO: Add get list service logic here
            return View(vehicleList);
        }

        // GET: Vehicles/Details/5
        public IActionResult Details(int id)
        {
            VehicleModel vehicle = _vehicleService.Query().SingleOrDefault(v => v.Id == id); 
            if (vehicle == null)
            {
                return View("Error", "Vehicle not found!");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["BrandId"] = new SelectList(null, "Id", "Name");
            ViewData["ColorId"] = new SelectList(null, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(null, "Id", "Name");
            ViewData["ModelId"] = new SelectList(null, "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleModel vehicle)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                return RedirectToAction(nameof(Index));
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["BrandId"] = new SelectList(null, "Id", "Name", vehicle.BrandId);
            ViewData["ColorId"] = new SelectList(null, "Id", "Name", vehicle.ColorId);
            ViewData["CustomerId"] = new SelectList(null, "Id", "Name", vehicle.CustomerId);
            ViewData["ModelId"] = new SelectList(null, "Id", "Name", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public IActionResult Edit(int id)
        {
            VehicleModel vehicle = _vehicleService.Query().SingleOrDefault(v => v.Id == id);
			//var customers = _customerService.Query().ToList();
			var customerModel = _customerService.Query().Select(c => new CustomerModel()
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname,
				CarsDisplay = c.CarsDisplay,
				CustomerDisplay = c.CustomerDisplay,
			}).ToList();

			if (vehicle == null)
            {
                return View("Error", "Vehicle not found!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["BrandId"] = new SelectList(_brandService.Query().ToList(), "Id", "Name", vehicle.BrandId);
            ViewData["ColorId"] = new SelectList(_colorService.Query().ToList(), "Id", "Name", vehicle.ColorId);
            ViewData["CustomerId"] = new SelectList(_customerService.Query().ToList(), "Id", "CustomerDisplay", vehicle.CustomerId);
            
            ViewData["ModelId"] = new SelectList(_modelService.Query().ToList(), "Id", "Name", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleModel vehicle)
        {
			//var customers = _customerService.Query().ToList();
			var customerModel = _customerService.Query().Select(c => new CustomerModel()
			{
				Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                CarsDisplay = c.CarsDisplay,
                CustomerDisplay = c.CustomerDisplay,
			}).ToList();


			if (ModelState.IsValid)
            {
                var result = _vehicleService.Update(vehicle);
				
				if (true)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["BrandId"] = new SelectList(_brandService.Query().ToList(), "Id", "Name", vehicle.BrandId);
            ViewData["ColorId"] = new SelectList(_colorService.Query().ToList(), "Id", "Name", vehicle.ColorId);
            ViewData["CustomerId"] = new SelectList(_customerService.Query().ToList(), "Id", "CustomerDisplay", vehicle.CustomerId);

            ViewData["ModelId"] = new SelectList(_modelService.Query().ToList(), "Id", "Name", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public IActionResult Delete(int id)
        {
            VehicleModel vehicle = null; // TODO: Add get item service logic here
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}