using Ambev.DeveloperEvaluation.Domain.Entities.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
