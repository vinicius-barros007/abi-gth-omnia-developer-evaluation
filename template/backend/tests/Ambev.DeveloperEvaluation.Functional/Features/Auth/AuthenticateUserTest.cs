using Ambev.DeveloperEvaluation.Functional.Features.Users.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Functional.Features.Auth
{
    public class AuthenticateUserTest : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthenticateUserTest(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Authentication using valid credentials should return created status code")]
        public async Task Valid_Credentials_ShouldReturn_Ok()
        {
            // Arrange
            var user = CreateUserRequestTestData.GenerateValidRequest();
            var request = new AuthenticateUserRequest()
            { 
                Email = user.Email,
                Password = user.Password
            };

            var response = await _client.PostAsJsonAsync("/api/users", user);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();

            // Act
            response = await _client.PostAsJsonAsync($"/api/auth", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<AuthenticateUserResponse>>();

            content.Should().NotBeNull();
            content.Success.Should().BeTrue();

            content.Message.Should().Be("User authenticated successfully");
            content.Data.Should().NotBeNull();

            var authenticateResponse = content.Data;
            authenticateResponse.Role.Should().Be(user.Role.ToString());
            authenticateResponse.Name.FirstName.Should().Be(user.Name.FirstName);
            authenticateResponse.Name.LastName.Should().Be(user.Name.LastName);
            authenticateResponse.Email.Should().Be(user.Email);
            authenticateResponse.Token.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Authentication using empty credentials should return bad request status code")]
        public async Task Empty_UserId_ShouldReturn_BadRequest()
        {
            // Arrange
            var request = new AuthenticateUserRequest();

            // Act
            var response = await _client.PostAsJsonAsync($"/api/auth", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();

            content.Message.Should().Be("Validation Failed");
            content.Errors.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Authentication using invalid credentials should return bad request status code")]
        public async Task Invalid_UserId_ShouldReturn_NotFound()
        {
            // Arrange
            var user = CreateUserRequestTestData.GenerateValidRequest();
            var request = new AuthenticateUserRequest()
            {
                Email = user.Email,
                Password = user.Password
            };

            // Act
            var response = await _client.PostAsJsonAsync($"/api/auth", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            content.Should().NotBeNull();
            content.Success.Should().BeFalse();
            content.Message.Should().Be("Invalid credentials");
        }
    }
}
