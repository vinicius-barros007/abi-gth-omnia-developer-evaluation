using Ambev.DeveloperEvaluation.Integration.TestData;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class UserRepositoryTests : RepositoryBaseTests
    {
        private readonly UserRepository _repository;

        public UserRepositoryTests() : base()
        {
            _repository = new UserRepository(_context);
        }

        [Fact(DisplayName = "Creation of user should succeed")]
        public async Task Create_ValidUser_Then_ShouldSucceed()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact(DisplayName = "Creation of user should fail")]
        public async Task Create_InvalidUser_Then_ShouldThrowError()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            user.Email = default!;

            // Act
            var action = async () =>
            {
                await _repository.CreateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            };

            // Assert
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact(DisplayName = "Deletion of user should succeed")]
        public async Task Delete_ValidUser_Then_ShouldSucceed()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            user = await _repository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Act
            bool userDeleted = await _repository.DeleteAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();

            // Assert
            userDeleted.Should().BeTrue();
        }

        [Fact(DisplayName = "Deletion of user should fail")]
        public async Task Delete_InvalidUser_Then_ShouldThrowError()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            bool userDeleted = await _repository.DeleteAsync(userId);
            await _unitOfWork.SaveChangesAsync();

            // Assert
            userDeleted.Should().BeFalse();
        }
    }
}
