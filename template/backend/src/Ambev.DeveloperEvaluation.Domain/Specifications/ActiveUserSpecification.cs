using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Domain.Enums.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
