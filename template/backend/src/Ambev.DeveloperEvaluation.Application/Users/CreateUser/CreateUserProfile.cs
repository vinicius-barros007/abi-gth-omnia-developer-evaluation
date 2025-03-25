using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(destination => destination.Person, option => option.MapFrom(source => source))
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<CreateUserCommand, Person>()
            .ForMember(destination => destination.Address, option => option.MapFrom(source => source))
            .ForMember(destination => destination.Id, option => option.Ignore())
            .ForMember(destination => destination.UserId, option => option.Ignore())
            .ForMember(destination => destination.User, option => option.Ignore())
            .ForMember(destination => destination.CreatedAt, option => option.Ignore())
            .ForMember(destination => destination.UpdatedAt, option => option.Ignore());

        CreateMap<CreateUserCommand, Address>()
            .ForMember(destination => destination.GeoLocation, option => option.MapFrom(source => source));

        CreateMap<CreateUserCommand, GeoLocation>();

        CreateMap<User, CreateUserResult>()
            .ForMember(destination => destination.FirstName, option => option.MapFrom(source => source.Person.FirstName))
            .ForMember(destination => destination.LastName, option => option.MapFrom(source => source.Person.LastName))
            .ForMember(destination => destination.Address, option => option.MapFrom(source => source.Person.Address));

        CreateMap<Person, CreateUserResult>()
            .ForMember(destination => destination.Username, option => option.Ignore())
            .ForMember(destination => destination.Email, option => option.Ignore())
            .ForMember(destination => destination.Phone, option => option.Ignore())
            .ForMember(destination => destination.Role, option => option.Ignore())
            .ForMember(destination => destination.Status, option => option.Ignore());
    }
}
