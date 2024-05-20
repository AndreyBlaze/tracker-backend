using Shared;

namespace Application.TaskColumns;

public static class TaskColumnsResult
{
    public static Error NotFound(Guid id) => new Error(Code: "TaskColumns.NotFound", Description: $"Task column with ID = '{id}' is not found");
}
