using System;
using System.Collections.Generic;
using WeatherTest.Data;
using Moq;
using Xunit;
using WeatherTest.Data.Model;
using WeatherTest.SampleApp.Process;
using WeatherTest.Data.Web;

namespace WeatherTest.Sample.Tests.Process
{

    public class WeatherAggregate_Test_Data
    {
        private IWeatherData DataAccess { get; set; }

        public WeatherAggregate_Test_Data()
        {
            var mock = new Mock<IWeatherData>();
            mock.Setup(x => x.GetForecastsAsync(It.IsAny<String>())).ReturnsAsync(new List<WeatherDataQuery>()
            {
                new WeatherDataQuery
                {
                    Temperature = 68,
                    WindSpeed = 10,
                    TempMetric = 2,
                    SpeedMetric = 1
                    
                },
                new WeatherDataQuery
                {
                    Temperature = 10,
                    WindSpeed = 8,
                    TempMetric = 1,
                    SpeedMetric = 2
                }
            });
            DataAccess = mock.Object;
        }

        [Theory]
        [InlineData(1, 1, 15)]
        public void Test_Aggregate_Temp_Cel(int speedUnit, int tempUnit, int expectedResult)
        {
            //arrange
            var weatherAggregate = new WeatherAggregate(speedUnit, tempUnit, DataAccess);

            //act
            var response = weatherAggregate.GetAggregateAsync(It.IsAny<String>()).Result;

            //assert
            Assert.Equal(response.Temperature, expectedResult);
        }

        [Theory]
        [InlineData(1, 1, 7.5)]
        public void Test_Aggregate_Speed_Mph(int speedUnit, int tempUnit, double expectedResult)
        {
            //arrange
            var weatherAggregate = new WeatherAggregate(speedUnit, tempUnit, DataAccess);

            //act
            var response = weatherAggregate.GetAggregateAsync(It.IsAny<String>()).Result;

            //assert
            Assert.Equal(response.WindSpeed, expectedResult);
        }

        [Theory]
        [InlineData(2, 2, 59)]
        public void Test_Aggregate_Temp_Faren(int speedUnit, int tempUnit, int expectedResult)
        {
            //arrange
            var weatherAggregate = new WeatherAggregate(speedUnit, tempUnit, DataAccess);

            //act
            var response = weatherAggregate.GetAggregateAsync(It.IsAny<String>()).Result;

            //assert
            Assert.Equal(response.Temperature, expectedResult);
        }

        [Theory]
        [InlineData(2, 2, 12)]
        public void Test_Aggregate_Speed_Kph(int speedUnit, int tempUnit, double expectedResult)
        {
            //arrange
            var weatherAggregate = new WeatherAggregate(speedUnit, tempUnit, DataAccess);

            //act
            var response = weatherAggregate.GetAggregateAsync(It.IsAny<String>()).Result;

            //assert
            Assert.Equal(response.WindSpeed, expectedResult);
        }
    }
}
