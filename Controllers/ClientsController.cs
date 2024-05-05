using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestesAPI.Models;
using TestesAPI.Repositories;

namespace TestesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public ClientsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var clients = await _unitOfWork.Clients.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id:int:min(1)}")]
    public IActionResult Get(int id)
    {
        var client = _unitOfWork.Clients.GetAsync(c => c.Id == id).Result;  
        
        if (client == null)
            return NotFound();
        
        return Ok(client);
    }

    [HttpGet("{id:int:min(1)}/vehicles")]
    public async Task<IActionResult> GetVehiclesByClient(int id)
    {
        var vehicles = await _unitOfWork.Clients.GetVehiclesByClientAsync(id);

        if (vehicles == null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Client client)
    {
        if (client is null)
            return BadRequest();

        var existsClient = await _unitOfWork.Clients.GetClientByEmailAsync(client.Email!);

        if (existsClient is not null)
            return BadRequest(new { message = "Client already exists" });

        var newClient = _unitOfWork.Clients.Create(client);

        if (newClient == null)
            return BadRequest();

        await _unitOfWork.CommitAsync();

        return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> Put(int id, Client client)
    {
        if (client == null || id != client.Id || id <= 0)
            return BadRequest();

        var existsClient = await _unitOfWork.Clients.GetAsync(c => c.Id == id);

        if (existsClient == null)
            return NotFound();
        
        var clientByEmail = await _unitOfWork.Clients.GetClientByEmailAsync(client.Email!);

        if (clientByEmail != null && clientByEmail.Id != id)
            return BadRequest(new { message = "Client already exists" });

        existsClient.Nome = client.Nome;
        existsClient.Email = client.Email;
        existsClient.Vehicles = client.Vehicles;

        _unitOfWork.Clients.Update(existsClient);
        await _unitOfWork.CommitAsync();

        return Ok(new {
            message = "Client updated successfully",
            client
        });
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _unitOfWork.Clients.GetAsync(c => c.Id == id).Result;

        if (client == null)
            return NotFound();

        _unitOfWork.Clients.Delete(client);
        await _unitOfWork.CommitAsync();

        return Ok(client);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(SignInManager<IdentityUser> signInManager, [FromBody] object empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "User logged out" });
        }

        return Unauthorized();
    }
}