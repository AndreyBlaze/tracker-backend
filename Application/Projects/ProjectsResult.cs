using Shared;

namespace Application.Projects;

public static class ProjectsResult
{
    public static Error Exists(string name) => new Error("Projects.Exists", $"Error - project with name = \"{name}\" already exists");

}
