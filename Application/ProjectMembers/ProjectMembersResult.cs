using Shared;

namespace Application.ProjectMembers;

public static class ProjectMembersResult
{
    public static Error Exists(Guid userId) => new Error("ProjectMembers.Exists", $"Error - user with id = \"{userId}\" is already in the project");
}
