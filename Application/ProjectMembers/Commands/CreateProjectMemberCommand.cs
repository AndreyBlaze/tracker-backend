using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.ProjectMembers.Commands;

public record CreateProjectMemberCommand(Guid ProjectId, Guid UserId) : ICommand;

public class CreateProjectMemberCommandHandler : ICommandHandler<CreateProjectMemberCommand>
{
    private readonly IProjectMembersRepository _projectMembersRepository;

    public CreateProjectMemberCommandHandler(IProjectMembersRepository projectMembersRepository)
    {
        _projectMembersRepository = projectMembersRepository;
    }

    public async Task<Result> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var sameMember = await _projectMembersRepository.GetByUserIdInProject(request.UserId, request.ProjectId, cancellationToken);

        if (sameMember is not null) return Result.Failure(ProjectMembersResult.Exists(request.UserId));

        ProjectMember newMember = new()
        {
            Id = Guid.NewGuid(),
            DateAdd = DateTimeOffset.UtcNow,
            DateUpdate = DateTimeOffset.UtcNow,
            ProjectId = request.ProjectId,
            UserId = request.UserId
        };

        try
        {
            var res = await _projectMembersRepository.AddAsync(newMember, cancellationToken);

            if (res is null) return Result.Failure<Guid>(new("ProjectMembers.ServerError", $"Error - Database Add error"));

            return Result.Success(res);
        }
        catch(Exception ex)
        {
            return Result.Failure<Guid>(new("ProjectMembers.ServerError", $"Error - {ex.ToString()}"));
        }

    }
}
