using Domain.Entities;

namespace DTO.Mapping;

public static class TaskColumnMapper
{
    public static TaskColumn MapTaskColumn(TaskColumnDTO model)
    {
        return new()
        {
            Id = model.Id ?? Guid.NewGuid(),
            Name = model.Name,
            DashboardId = model.DashboardId,
        };
    }
}
