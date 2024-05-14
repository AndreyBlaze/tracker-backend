using Domain.Entities;

namespace DTO.Mapping;

public static class ProjectMapper
{
    public static Project MapProject(ProjectDTO project)
    {
        return new()
        {
            Id = project.Id ?? Guid.NewGuid(),
            Name = project.Name,
            FileId = project.FileId,
        };
    }
}
