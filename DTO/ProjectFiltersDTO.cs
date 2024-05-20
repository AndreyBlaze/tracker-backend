using Domain.Types;

namespace DTO;

public class ProjectFiltersDTO
{
    public FilterType Date { get; set; } = FilterType.None;
    public FilterType Name { get; set; } = FilterType.None;
    public string? Search { get; set; }
}
