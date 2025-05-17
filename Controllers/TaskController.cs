using Demo.DTOs;
using Demo.Interface;
using Demo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskservice;

        public TaskController(ITaskService taskservice)
        {
            _taskservice = taskservice;
        }

        [HttpGet("GetAllTaskDetails")]
        [Authorize]
        public async Task<ActionResult<List<TaskItems>>> GetAllTaskDetails()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _taskservice.GetAllTasks(role, userId);
            return Ok(result);
        }

        [HttpGet("GetAllTaskDetailsById/{id}")]
        [Authorize]
        public async Task<ActionResult<TaskItems>> GetAllTaskDetailById(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _taskservice.GetTaskById(id, role, userId);
            if (result == null)
                return Forbid("Not authorized to view this task.");

            return Ok(result);
        }

        [HttpPost("AddNewTaskDetails")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewTask([FromBody] TaskCreateDto taskDto)
        {
            if (taskDto == null)
                return BadRequest("Please provide task details.");

            var task = new TaskItems
            {
                TaskName = taskDto.Title,
                Description = taskDto.Description,
                UserId = taskDto.UserId
            };
            await _taskservice.CreateNewTask(task);
            return Ok("Task added successfully.");
        }
    }
}
