using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.TestData.User;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class PersonRepositoryTests : RepositoryBaseTests
    {
        private readonly UserRepository _userRepository;
        private readonly PersonRepository _repository;

        public PersonRepositoryTests() : base()
        {
            _userRepository = new UserRepository(_context);
            _repository = new PersonRepository(_context);
        }

        [Fact(DisplayName = "Creation of person should succeed")]
        public async Task Create_ValidUser_Then_ShouldSucceed()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            user.Person = PersonTestData.GenerateValidPerson();

            user = await _userRepository.CreateAsync(user);
            user.Person.UserId = user.Id;

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(user.Person);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact(DisplayName = "Creation of person should fail")]
        public async Task Create_InvalidUser_Then_ShouldThrowError()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            user.Person = PersonTestData.GenerateValidPerson();

            user = await _userRepository.CreateAsync(user);
            user.Person.FirstName = default!;

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(user.Person);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact(DisplayName = "Deletion of person should succeed")]
        public async Task Delete_ValidUser_Then_ShouldSucceed()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            user = await _userRepository.CreateAsync(user);
            user.Person.UserId = user.Id;

            user.Person = await _repository.CreateAsync(user.Person);
            await _unitOfWork.SaveChangesAsync();

            // Act
            bool personDeleted = await _repository.DeleteAsync(user.Person.Id);
            await _unitOfWork.SaveChangesAsync();

            // Assert
            personDeleted.Should().BeTrue();
        }

        [Fact(DisplayName = "Deletion of person should fail")]
        public async Task Delete_InvalidUser_Then_ShouldThrowError()
        {
            // Arrange
            var personId = Guid.NewGuid();

            // Act
            bool personDeleted = await _repository.DeleteAsync(personId);
            await _unitOfWork.SaveChangesAsync();

            // Assert
            personDeleted.Should().BeFalse();
        }
    }
}
