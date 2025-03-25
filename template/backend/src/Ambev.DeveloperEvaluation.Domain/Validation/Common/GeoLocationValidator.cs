using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Common;

/// <summary>
/// Validator for the GeoLocation class.
/// </summary>
public class GeoLocationValidator : AbstractValidator<GeoLocation>
{
    public GeoLocationValidator()
    {
        RuleFor(geo => geo.Latitude)
            .InclusiveBetween(-90, 90)  
            .WithMessage("The latitude of the location. May range from -90.0 to 90.0.");

        RuleFor(geo => geo.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("The longitude of the location. May range from -180.0 to 180.0.");
    }
}
