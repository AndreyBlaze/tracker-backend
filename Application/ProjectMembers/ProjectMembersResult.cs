using Shared;

namespace Application.ProjectMembers;

public static class ProjectMembersResult
{
    public static Error Exists(Guid userId) => new Error("ProjectMembers.Exists", $"Error - user with id = \"{userId}\" is already in the project");
    public static Error NotFound(Guid id) => new Error(Code: "ProjectMembers.NotFound", Description: $"Project member with ID = '{id}' is not found");
    public static Error AccessDenied() => new Error(Code: "ProjectMembers.AccessDenied", Description: $"Error - access denied");
}