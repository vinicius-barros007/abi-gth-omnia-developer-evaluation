using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, GetUserCommand>()
            .ConstructUsing(id => new GetUserCommand(id));

        CreateMap<GetUserResult, GetUserResponse>()
            .ForMember(destination => destination.Name, option => option.MapFrom(source => source));

        CreateMap<GetUserResult, PersonName>()
            .ForMember(destination => destination.FirstName, option => option.MapFrom(source => source.FirstName))
            .ForMember(destination => destination.LastName, option => option.MapFrom(source => source.LastName));
    }
}
