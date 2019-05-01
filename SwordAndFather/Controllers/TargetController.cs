using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Data;
using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddTarget(CreateTargetRequest createRequest)
        {
            var repository = new TargetRepository();

            var newTarget = repository.AddTarget(
                createRequest.Name,
                createRequest.Location,
                createRequest.FitnessLevel,
                createRequest.UserId);

            return Created($"/api/target/{newTarget.Id}", newTarget);
        }
    }
}