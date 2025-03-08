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
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemRepository toDoItemRepository;

        public ToDoItemsController(IToDoItemRepository toDoItemRepository)
        {
            this.toDoItemRepository = toDoItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoItem([FromBody] CreateToDoItemRequestDto request)
        {
            var ToDoItem = new ToDoItem
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                Detail = request.Detail,
                UserId = request.UserId,
                PriorityId = request.PriorityId,
                CreateDate = DateTime.Now,
            };

            await toDoItemRepository.CreateAsync(ToDoItem);

            var response = new ToDoItemDto
            {
                Id = ToDoItem.Id,
                Title = ToDoItem.Title,
                CreateDate = ToDoItem.CreateDate,
                IsCompleted = ToDoItem.IsCompleted,
                Detail = ToDoItem.Detail,
                UserId = ToDoItem.UserId,
                PriorityId = ToDoItem.PriorityId,
            };

            return Ok(response);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingToDoItems()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId)) 
                return Unauthorized();

            var pendingToDoItems = await toDoItemRepository.GetPendingToDoItemsByUserIdAsync(Guid.Parse(userId));

            var response = pendingToDoItems.Select(item => new ToDoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                CreateDate = item.CreateDate,
                IsCompleted = item.IsCompleted,
                Detail = item.Detail,
                UserId = item.UserId,
                PriorityId = item.PriorityId,
            }).ToList();

            return Ok(response);

        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedToDoItems()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId)) 
                return Unauthorized();

            var completedToDoItems = await toDoItemRepository.GetCompletedToDoItemsByUserIdAsync(Guid.Parse(userId));

            var response = completedToDoItems.Select(item => new ToDoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                CreateDate = item.CreateDate,
                IsCompleted = item.IsCompleted,
                Detail = item.Detail,
                UserId = item.UserId,
                PriorityId = item.PriorityId,
            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetToDoItemById([FromRoute] Guid id)
        {
            var toDoItem = await toDoItemRepository.GetById(id);

            if (toDoItem == null)
                return NotFound();

            var response = new ToDoItemDto
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                CreateDate = toDoItem.CreateDate,
                IsCompleted = toDoItem.IsCompleted,
                Detail = toDoItem.Detail,
                UserId = toDoItem.UserId,
                PriorityId = toDoItem.PriorityId,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditToDoItem([FromRoute] Guid id, UpdateToDoItemRequestDto request)
        {
            var toDoItem = new ToDoItem
            {
                Id = id,
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                Detail = request.Detail,
                UserId = request.UserId,
                PriorityId = request.PriorityId,
            };

            var toDoItemUpdated = await toDoItemRepository.UpdateAsync(toDoItem);

            if (toDoItemUpdated == null)
                return NotFound();

            var response = new ToDoItemDto
            {
                Id = toDoItemUpdated.Id,
                Title = toDoItemUpdated.Title,
                CreateDate = toDoItemUpdated.CreateDate,
                IsCompleted = toDoItemUpdated.IsCompleted,
                Detail = toDoItemUpdated.Detail,
                UserId = toDoItemUpdated.UserId,
                PriorityId = toDoItemUpdated.PriorityId
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteToDoItem([FromRoute] Guid id)
        {
            var item = await toDoItemRepository.DeleteAsync(id);

            if (item == null) 
                return NotFound();

            var response = new ToDoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                CreateDate = item.CreateDate,
                IsCompleted = item.IsCompleted,
                Detail = item.Detail,
                UserId = item.UserId,
                PriorityId = item.PriorityId
            };

            return Ok(response);
        }
    }
}
