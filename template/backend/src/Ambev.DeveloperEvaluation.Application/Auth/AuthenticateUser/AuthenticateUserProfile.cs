using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

/// <summary>
/// AutoMapper profile for authentication-related mappings
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
    /// </summary>
    public AuthenticateUserProfile()
    {
        CreateMap<User, AuthenticateUserResult>()
            .ForMember(destination => destination.Token, opt => opt.Ignore())
            .ForMember(destination => destination.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(destination => destination.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
            .ForMember(destination => destination.LastName, opt => opt.MapFrom(src => src.Person.LastName));
    }
}
