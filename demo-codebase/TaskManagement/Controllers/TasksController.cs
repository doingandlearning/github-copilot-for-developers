using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>Get all tasks for a user, optionally filtered by priority</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int ownerId, [FromQuery] string? priority = null)
    {
        try
        {
            var tasks = await _taskService.GetByOwnerAsync(ownerId, priority);
            return Ok(tasks);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>Get a single task by ID</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    /// <summary>Create a new task</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        try
        {
            var created = await _taskService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>Update an existing task</summary>
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
    {
        try
        {
            var updated = await _taskService.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // NOTE FOR DEMO: Delete endpoint is missing.
    // Good PRD-driven demo: give delegates a user story and ask them to add it.
}
