using Shared;

namespace Application.Dashboards;

public static class DashboardsResult
{
    public static Error NotFound(Guid id) => new Error(Code: "Dashboards.NotFound", Description: $"Dashboard with ID = '{id}' is not found");
}
