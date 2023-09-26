﻿    #nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
    using DataAccess.Contexts;
    using DataAccess.Entities;
using Business.Services;
using Business.Models;
using System.Reflection.Metadata;

//Generated by ScaffoldApp.
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        // Add service injections here
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        // GET: api/Models
        [HttpGet]
        public IActionResult Get()
        {
            List<ModelModel> modelList = _modelService.Query().ToList();
            return Ok(modelList);
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ModelModel model = _modelService.Query().SingleOrDefault(m => m.Id == id);
			if (model == null)
            {
                return NotFound("Model not found!");
            }
			return Ok(model);
        }

		// POST: api/Models
        
        [HttpPost]
        public IActionResult Post(ModelModel model)
        {
            
            if (ModelState.IsValid)
            {
                var result = _modelService.Add(model);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = model.Id }, model);
                }
                ModelState.AddModelError("Message", result.Message);
            }

            return BadRequest(ModelState); // 400 http status code
        }

        // PUT: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(ModelModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _modelService.Update(model);
                if (result.IsSuccessful)
                {
                    //return Ok(blog);
                    return NoContent();
                }
                ModelState.AddModelError("Message", result.Message);
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _modelService.Delete(id);
            return NoContent();
        }
	}
}
