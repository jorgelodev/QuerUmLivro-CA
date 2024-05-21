using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using QuerUmLivro.Infra.Services.DTOs.Interesses;
using QuerUmLivro.Infra.Services.Interfaces;
using QuerUmLivro.Infra.Services.ViewModels.Interesses;
using System.Text;
namespace QuerUmLivro.Test.Infra.Services
{
    public class InteresseControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Mock<IInteresseService> _interesseServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public InteresseControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _interesseServiceMock = new Mock<IInteresseService>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task ManifestarInteresse_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var manifestarInteresseViewModel = new ManifestarInteresseViewModel
            {
                LivroId = 1,
                InteressadoId = 1,
                Justificativa = "Interessado no livro."
            };

            var manifestarInteresseDto = new ManifestarInteresseDto
            {
                LivroId = 1,
                InteressadoId = 1,
                Justificativa = "Interessado no livro."
            };

            var interesseDto = new InteresseDto
            {
                Id = 1,
                LivroId = 1,
                InteressadoId = 1,
                Justificativa = "Interessado no livro."
            };

            _mapperMock.Setup(m => m.Map<ManifestarInteresseDto>(It.IsAny<ManifestarInteresseViewModel>()))
                .Returns(manifestarInteresseDto);

            _interesseServiceMock.Setup(s => s.ManifestarInteresse(It.IsAny<ManifestarInteresseDto>()))
                .Returns(interesseDto);

            var content = new StringContent(JsonConvert.SerializeObject(manifestarInteresseViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/manifestar-interesse", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<InteresseDto>(responseString);
            responseObj.Should().BeEquivalentTo(interesseDto);
        }

        [Fact]
        public async Task AprovarInteresse_Should_ReturnOk_When_ValidRequest()
        {
            // Arrange
            var aprovarInteresseViewModel = new AprovarInteresseViewModel
            {
                Id = 1,
                DoadorId = 1
            };

            var aprovarInteresseDto = new AprovarInteresseDto
            {
                Id = 1,
                DoadorId = 1
            };

            var interesseDto = new InteresseDto
            {
                Id = 1,
                LivroId = 1,
                InteressadoId = 1,
                Justificativa = "Interessado no livro."
            };

            _mapperMock.Setup(m => m.Map<AprovarInteresseDto>(It.IsAny<AprovarInteresseViewModel>()))
                .Returns(aprovarInteresseDto);

            _interesseServiceMock.Setup(s => s.AprovarInteresse(It.IsAny<AprovarInteresseDto>()))
                .Returns(aprovarInteresseDto);

            var content = new StringContent(JsonConvert.SerializeObject(aprovarInteresseViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/aprovar-interesse", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<InteresseDto>(responseString);
            responseObj.Should().BeEquivalentTo(interesseDto);
        }
    }
}