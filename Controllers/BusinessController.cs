﻿using homecoming.api.Abstraction;
using homecoming.api.Model;
using homecoming.api.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BusinessController : Controller
    {
        IRepository<Business> repo;
        IWebHostEnvironment web;
        HomecomingDbContext db;
        public BusinessController(IWebHostEnvironment host, HomecomingDbContext cx)
        {
            web = host;
            db = cx;
            repo = new BusinessRepo(web,db);
        }
        
        [HttpGet]
        public IActionResult GetAllBusinesses()
        {
           return Ok(repo.FindAll());
        }

        [HttpPost]
        public IActionResult RegisterBusiness([FromForm]Business business)
        {
            if(business != null)
            {
                repo.Add(business);
               return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + business.BusinessId,business);
            }
            else
            {
                return BadRequest("not created");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBusiness(int id)
        {
          Business business=  repo.GetById(id);
            if(business != null)
            {
                return Ok(business);
            }
            else
            {
                return BadRequest("Business with id: "+ id + " Not found");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Business buss)
        {
            if(buss != null)
            {
                repo.Update(id, buss);
                return Ok("Update Successful");
            }
            else
            {
                return BadRequest("Updated failed");
            }
        }
    }
}