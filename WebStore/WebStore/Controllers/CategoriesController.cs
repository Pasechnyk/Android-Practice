﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Models.Categories;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyAppContext _appContext;

        public CategoriesController(MyAppContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _appContext.Categories.ToList();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryCreateViewModel model)
        {
            var category = new CategoryEntity
            {
                Name = model.Name,
                Description = model.Description
            };
            _appContext.Categories.Add(category);
            _appContext.SaveChanges();
            return Ok(category);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] CategoryEditViewModel model)
        {
            var category = _appContext.Categories.SingleOrDefault(x => x.Id == model.Id);
            if (category == null)
            {
                return NotFound();
            }
            category.Description = model.Description;
            category.Name = model.Name;
            _appContext.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _appContext.Categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _appContext.Categories.Remove(category);
            _appContext.SaveChanges();
            return Ok();
        }
    }
}