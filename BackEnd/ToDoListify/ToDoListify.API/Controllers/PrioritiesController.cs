using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListify.API.Models.Domain;
using ToDoListify.API.Models.DTO;
using ToDoListify.API.Repositories.Interface;

namespace ToDoListify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository priorityRepository;

        public PrioritiesController(IPriorityRepository priorityRepository)
        {
            this.priorityRepository = priorityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = await priorityRepository.GetAllAsync();

            var priorityDtos = priorities.Select(p => new PriorityDto
            {
                Id = p.Id,
                PriorityLevel = p.PriorityLevel
            }).ToList();

            // Return the list of PriorityDto objects
            return Ok(priorityDtos);
        }
    }
}
