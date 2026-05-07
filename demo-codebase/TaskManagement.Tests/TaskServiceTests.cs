using Moq;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;
using TaskManagement.Services;
using Xunit;

namespace TaskManagement.Tests;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockRepository;
    private readonly TaskService _sut;

    public TaskServiceTests()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _sut = new TaskService(_mockRepository.Object);
    }

    // -------------------------------------------------------------------------
    // CreateAsync
    // -------------------------------------------------------------------------

    [Fact]
    public async Task CreateAsync_WithValidRequest_ReturnsMappedResponse()
    {
        // Arrange
        var request = new CreateTaskRequest("Fix login bug", "Users can't log in", "High", 42);

        _mockRepository
            .Setup(r => r.SaveAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync((TaskItem t) => { t.Id = 1; return t; });

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Fix login bug", result.Title);
        Assert.Equal("High", result.Priority);
        Assert.Equal(42, result.OwnerId);
    }

    [Fact]
    public async Task CreateAsync_WithEmptyTitle_ThrowsArgumentException()
    {
        var request = new CreateTaskRequest("", null, "Medium", 1);

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateAsync(request));
    }

    [Fact]
    public async Task CreateAsync_WithTitleExceeding100Chars_ThrowsArgumentException()
    {
        var request = new CreateTaskRequest(new string('x', 101), null, "Medium", 1);

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateAsync(request));
    }

    [Fact]
    public async Task CreateAsync_WithInvalidPriority_ThrowsArgumentException()
    {
        var request = new CreateTaskRequest("Valid title", null, "Urgent", 1);

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateAsync(request));
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_CallsRepositorySave()
    {
        var request = new CreateTaskRequest("A task", null, "Low", 1);

        _mockRepository
            .Setup(r => r.SaveAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync((TaskItem t) => { t.Id = 1; return t; });

        await _sut.CreateAsync(request);

        _mockRepository.Verify(r => r.SaveAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    // -------------------------------------------------------------------------
    // UpdateAsync
    // -------------------------------------------------------------------------

    [Fact]
    public async Task UpdateAsync_WithNonExistentId_ReturnsNull()
    {
        _mockRepository
            .Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((TaskItem?)null);

        var result = await _sut.UpdateAsync(99, new UpdateTaskRequest(null, null, null, null));

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenStatusSetToDone_SetsCompletedAt()
    {
        // Arrange
        var existing = new TaskItem { Id = 1, Title = "A task", OwnerId = 1 };

        _mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync((TaskItem t) => t);

        // Act
        var result = await _sut.UpdateAsync(1, new UpdateTaskRequest(null, null, null, "Done"));

        // Assert
        // NOTE FOR DEMO: This test currently FAILS — there's a bug in UpdateAsync.
        // Use /fix to find and correct it.
        Assert.NotNull(result?.CompletedAt);
    }

    // -------------------------------------------------------------------------
    // GetByOwnerAsync
    // -------------------------------------------------------------------------

    [Fact]
    public async Task GetByOwnerAsync_WithPriorityFilter_OnlyReturnsMatchingTasks()
    {
        _mockRepository
            .Setup(r => r.GetByOwnerAsync(1, Priority.High))
            .ReturnsAsync(new List<TaskItem>
            {
                new() { Id = 1, Title = "Urgent fix", Priority = Priority.High, OwnerId = 1 }
            });

        var results = await _sut.GetByOwnerAsync(1, "High");

        Assert.Single(results);
        Assert.Equal("High", results.First().Priority);
    }

    // -------------------------------------------------------------------------
    // DEMO GAPS — good for TDD exercise
    // -------------------------------------------------------------------------

    // TODO: Add tests for:
    // - CreateAsync with whitespace-only title (should fail)
    // - UpdateAsync with empty title (should fail)
    // - GetByOwnerAsync with invalid priority string
    // - DeleteAsync delegates to repository
}
