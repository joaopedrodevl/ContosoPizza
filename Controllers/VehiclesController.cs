using Microsoft.AspNetCore.Mvc;
using TestesAPI.Models;
using TestesAPI.Repositories;

namespace TestesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehiclesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpGet("{id:int:min(1)}/client")]
        public async Task<IActionResult> GetClientByVehicle(int id)
        {
            var client = await _unitOfWork.Vehicles.GetClientByVehicleAsync(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Vehicle vehicle)
        {
            if (vehicle is null)
                return BadRequest();

            var existsVehicle = await _unitOfWork.Vehicles.GetVehicleByPlateAsync(vehicle.Plate!);

            if (existsVehicle is not null)
                return BadRequest(new { message = "Vehicle already exists" });

            var newVehicle = _unitOfWork.Vehicles.Create(vehicle);

            if (newVehicle == null)
                return BadRequest();

            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id = newVehicle.Id }, newVehicle);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Put(int id, Vehicle vehicle)
        {
            if (vehicle is null || id != vehicle.Id)
                return BadRequest();
            
            var vehicleExists = await _unitOfWork.Vehicles.GetAsync(v => v.Id == id);

            if (vehicleExists == null)
                return NotFound();

            var vehiclePlate = await _unitOfWork.Vehicles.GetVehicleByPlateAsync(vehicle.Plate!);

            if (vehiclePlate is not null && vehiclePlate.Id != id)
                return BadRequest(new { message = "Vehicle already exists" });

            vehicleExists.Plate = vehicle.Plate;
            vehicleExists.Model = vehicle.Model;
            vehicleExists.Brand = vehicle.Brand;
            vehicleExists.ClientId = vehicle.ClientId;

            _unitOfWork.Vehicles.Update(vehicleExists);

            await _unitOfWork.CommitAsync();

            return Ok(new {
                message = "Vehicle updated successfully",
                vehicle
            });
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            _unitOfWork.Vehicles.Delete(vehicle);

            await _unitOfWork.CommitAsync();

            return Ok(new {
                message = "Vehicle deleted successfully",
                vehicle
            });
        }
    }
}