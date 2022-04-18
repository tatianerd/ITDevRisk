using ITDevRisk.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IDevRisk.Application.Tests
{
    public class TradeCategorizationServiceUnitTests
    {
        private readonly ITradeCategorizationService _tradeCategorizationService;
        private readonly Mock<ITradeCategorizationService> _tradeCategorizationServiceMock;

        public TradeCategorizationServiceUnitTests()
        {
            var mock = new MockRepository(MockBehavior.Default);

            _tradeCategorizationServiceMock = mock.Create<ITradeCategorizationService>();

            var serviceProvider = new ServiceCollection()
            .AddSingleton<ITradeCategorizationService, TradeCategorizationService>()
            .BuildServiceProvider();

            _tradeCategorizationService = serviceProvider.GetService<ITradeCategorizationService>();
        }

        [Fact]
        public async Task CategorizeShouldReturnNull()
        {
            List<string> expected = null;

            _tradeCategorizationServiceMock.Setup(x => x.Categorize(null)).Returns(Task.FromResult(expected));

            var result = await _tradeCategorizationService.Categorize(null).ConfigureAwait(false);

            Assert.Null(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CategorizeShouldReturnSuccess()
        {
            List<string> expected = new List<string>()
            {
                "HIGHRISK",
                "EXPIRED",
                "MEDIUMRISK",
                "MEDIUMRISK"
            };

            _tradeCategorizationServiceMock.Setup(x => x.Categorize(It.IsAny<List<string>>())).Returns(Task.FromResult(expected));

            var result = await _tradeCategorizationService.Categorize(
                new List<string>()
                {
                    "12/11/2020",
                        "4",
                        "2000000 Private 12/29/2025",
                        "400000 Public 07/01/2020",
                        "5000000 Public 01/02/2024",
                        "3000000 Public 10/26/2023"
                }).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CategorizeShouldReturnOnlyExpired()
        {
            List<string> expected = new List<string>()
            {
                "EXPIRED",
                "EXPIRED",
                "EXPIRED",
                "EXPIRED"
            };

            _tradeCategorizationServiceMock.Setup(x => x.Categorize(It.IsAny<List<string>>())).Returns(Task.FromResult(expected));

            var result = await _tradeCategorizationService.Categorize(
                new List<string>()
                {
                    "12/11/2020",
                        "4",
                        "2000000 Private 12/29/2019",
                        "400000 Public 07/01/2020",
                        "5000000 Public 01/02/2018",
                        "3000000 Public 10/12/2020"
                }).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CategorizeShouldReturnOnlyHighRisk()
        {
            List<string> expected = new List<string>()
            {
                "HIGHRISK",
                "HIGHRISK",
                "HIGHRISK"
            };

            _tradeCategorizationServiceMock.Setup(x => x.Categorize(It.IsAny<List<string>>())).Returns(Task.FromResult(expected));

            var result = await _tradeCategorizationService.Categorize(
                new List<string>()
                {
                    "12/11/2020",
                        "3",
                        "2000000 Private 12/29/2022",
                        "4000000 Private 01/02/2030",
                        "5000000 Private 10/14/2020"
                }).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CategorizeShouldReturnOnlyMediumRisk()
        {
            List<string> expected = new List<string>()
            {
                "MEDIUMRISK",
                "MEDIUMRISK"
            };

            _tradeCategorizationServiceMock.Setup(x => x.Categorize(It.IsAny<List<string>>())).Returns(Task.FromResult(expected));

            var result = await _tradeCategorizationService.Categorize(
                new List<string>()
                {
                    "12/11/2020",
                        "2",
                        "5000000 Public 12/29/2025",
                        "3000000 Public 01/02/2024"
                }).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CategorizeShouldValidateNumberOfTrades()
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => _tradeCategorizationService.Categorize(new List<string>()
                {
                    "12/11/2020",
                        "5",
                        "2000000 Public 12/29/2025",
                        "5000000 Public 01/02/2024"
                }));

            Assert.Equal("Number of trades has to match", exception.Message);
        }
    }
}
