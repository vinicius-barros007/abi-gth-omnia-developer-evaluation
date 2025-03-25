using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>()
            .ForMember(destination => destination.FirstName, option => option.MapFrom(source => source.Person.FirstName))
            .ForMember(destination => destination.LastName, option => option.MapFrom(source => source.Person.LastName))
            .ForMember(destination => destination.Address, option => option.MapFrom(source => source.Person.Address));

        CreateMap<Person, GetUserResult>()
            .ForMember(destination => destination.Username, option => option.Ignore())
            .ForMember(destination => destination.Email, option => option.Ignore())
            .ForMember(destination => destination.Phone, option => option.Ignore())
            .ForMember(destination => destination.Role, option => option.Ignore())
            .ForMember(destination => destination.Status, option => option.Ignore());
    }
}
