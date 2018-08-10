using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.core;
using Vega.core.Models;
using Vega.Models;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehichlesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IunitofWork unitOfwork;
        public VehichlesController(IMapper mapper, IVehicleRepository repository, IunitofWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task<QueryResultResource<VehicleResource>> GetVehichles(VehichleQueryResource filterResource)
        {
           var filter = mapper.Map<VehichleQueryResource, VehichleQuery>(filterResource);
           var queryResult  = await repository.GetVehicles(filter);
           return mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);         
        } 



        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await repository.FindModelById(vehicleResource.ModelId) == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfwork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")] // api/vehicles/2
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfwork.CompleteAsync();
            
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, false);
            if (vehicle == null)
            {
                return NotFound("No vehicle found with id " + id);
            }

            repository.Remove(vehicle);
            await unitOfwork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("GetById/{id:int}")]       
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound("No vehicle found with id " + id);
            }

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }
    }
}