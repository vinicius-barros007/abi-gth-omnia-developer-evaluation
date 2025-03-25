using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>()
            .ForMember(destination => destination.FirstName, option => option.MapFrom(source => source.Name.FirstName))
            .ForMember(destination => destination.LastName, option => option.MapFrom(source => source.Name.LastName))
            .ForMember(destination => destination.City, option => option.MapFrom(source => source.Address.City))
            .ForMember(destination => destination.Street, option => option.MapFrom(source => source.Address.Street))
            .ForMember(destination => destination.Number, option => option.MapFrom(source => source.Address.Number))
            .ForMember(destination => destination.ZipCode, option => option.MapFrom(source => source.Address.ZipCode))
            .ForMember(destination => destination.Latitude, option => option.MapFrom(source => source.Address.GeoLocation.Latitude))
            .ForMember(destination => destination.Longitude, option => option.MapFrom(source => source.Address.GeoLocation.Longitude));

        CreateMap<CreateUserResult, CreateUserResponse>()
            .ForMember(destination => destination.Name, option => option.MapFrom(source => source));

        CreateMap<CreateUserResult, PersonName>()
            .ForMember(destination => destination.FirstName, option => option.MapFrom(source => source.FirstName))
            .ForMember(destination => destination.LastName, option => option.MapFrom(source => source.LastName));
    }
}
