using CliTddApi.Service.Entity;
using CliTddApi.Service.Process;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using Moq.Protected;

namespace CliTddApi.Test
{
    public class TestWeatherVisualCrossingService
    {
        private Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private Mock<HttpClient> _httpClientMock = new();
        private Service.Process.WeatherVisualCrossingService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestWeatherVisualCrossingService()
        {
            // Setting up the HttpClientFactory to return a mocked HttpClient
            _httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(_httpClientMock.Object);
            _service = new WeatherVisualCrossingService(_httpClientFactoryMock.Object);
        }


        [Theory]
        [InlineData("London", 51.5064, -0.12721)]
        [InlineData("Sydney", -33.8696, 151.207)]
        public async Task Should_Return_Async_Correct_Lat_Long_Valid_InLineSample(string city, float lat, float longtude)
        {
            //Arrange
            // TestWeatherVisualCrossingService() constructor

            //Act
            var actualResponse = await _service.GetWeatherForCityAsync(city);

            //Asset
            Assert.NotNull(actualResponse);
            actualResponse.latitude.Should().Be(lat);
            actualResponse.longitude.Should().Be(longtude);
        }


        [Theory]
        [MemberData(nameof(GetTestLatLongData))]
        public async Task Should_Return_Correct_Async_Lat_Long_Valid_MemberDataSample(string city, params float[] latLongList)
        {
            //Arrange
            // TestWeatherVisualCrossingService() constructor

            //Act
            var actualResponse = await _service.GetWeatherForCityAsync(city);

            //Asset
            Assert.NotNull(actualResponse);
            actualResponse.latitude.Should().Be(latLongList[0]);
            actualResponse.longitude.Should().Be(latLongList[1]);
        }


        [Theory]
        [InlineData("London", 51.5064, -0.12721)]
        [InlineData("Sydney", -33.8696, 151.207)]
        public void Should_Return_Correct_Sequential_Lat_Long_Valid_InLineSample(string city, float lat, float longtude)
        {
            //Arrange
            // TestWeatherVisualCrossingService() constructor

            //Act
            var actualResponse = _service.GetWeatherForCity(city);

            //Asset
            Assert.NotNull(actualResponse);
            actualResponse.latitude.Should().Be(lat);
            actualResponse.longitude.Should().Be(longtude);
        }


        [Theory]
        [MemberData(nameof(GetTestLatLongData))]
        public void Should_Return_Correct_Sequential_Lat_Long_Valid_MemberDataSample(string city, params float[] latLongList)
        {
            //Arrange
            // TestWeatherVisualCrossingService() constructor

            //Act
            var actualResponse = _service.GetWeatherForCity(city);

            //Asset
            Assert.NotNull(actualResponse);
            actualResponse.latitude.Should().Be(latLongList[0]);
            actualResponse.longitude.Should().Be(latLongList[1]);
        }


        /// <summary>
        ///  Sample for Should_Return_Correct_Lat_Long_Valid_MemberDataSample()
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetTestLatLongData()
        {
            yield return new object[] { "London", new float[] { (float)51.5064, (float)-0.12721 } };
            yield return new object[] { "Sydney", new float[] { (float)-33.8696, (float)151.207 } };
        }
    }
}