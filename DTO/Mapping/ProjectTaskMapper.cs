using Domain.Entities;

namespace DTO.Mapping;

public static class ProjectTaskMapper
{
    public static ProjectTask MapProjectTask(ProjectTaskDTO task)
    {
        return new()
        {
            Id = task.Id ?? Guid.NewGuid(),
            ProjectId = task.ProjectId,
            ColumnId = task.ColumnId,
            Text = task.Text,
            UserId = (Guid)task.UserId!,
            DeadLine = task.DeadLine,
        };
    }
}
