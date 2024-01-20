using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ServiceLibrary;
    using ServiceLibrary.Models;
    using System;
    using static global::CarSimulator.CarSimulator;

    namespace CarSimulator.Test
    {
        [TestClass]
        public class CarSimulatorServiceTest
        {
            [TestMethod]
            public void TestTurnLeft()
            {
                // Arrange
                var randomDriverApiServiceMock = new Mock<IRandomDriverApiService>();
                var carSimulatorService = new CarSimulatorService(randomDriverApiServiceMock.Object);

                // Act
                carSimulatorService.TurnLeft();

                // Assert
                Assert.AreEqual(9, carSimulatorService.GetFuelStatus()); // Adjust expected values based on your logic
                Assert.AreEqual(1, carSimulatorService.GetTiredness());
            }

            [TestMethod]
            public void TestTakeRest()
            {
                // Arrange
                var randomDriverApiServiceMock = new Mock<IRandomDriverApiService>();
                var carSimulatorService = new CarSimulatorService(randomDriverApiServiceMock.Object);

                // Act
                carSimulatorService.TakeRest();

                // Assert
                Assert.AreEqual(0, carSimulatorService.GetTiredness());
            }

            [TestMethod]
            public void TestRefillGas()
            {
                // Arrange
                var randomDriverApiServiceMock = new Mock<IRandomDriverApiService>();
                var carSimulatorService = new CarSimulatorService(randomDriverApiServiceMock.Object);
                carSimulatorService.TurnLeft(); // Reduce fuel to simulate consumption

                // Act
                carSimulatorService.RefillGas();

                // Assert
                Assert.AreEqual(10, carSimulatorService.GetFuelStatus());
            }

            // Repeat similar patterns for other methods...

            [TestMethod]
            public void TestResetGame()
            {
                // Arrange
                var randomDriverApiServiceMock = new Mock<IRandomDriverApiService>();
                var carSimulatorService = new CarSimulatorService(randomDriverApiServiceMock.Object);
                carSimulatorService.TurnLeft(); // Simulate some actions

                // Act
                carSimulatorService.ResetGame();

                // Assert
                Assert.AreEqual(10, carSimulatorService.GetFuelStatus());
                Assert.AreEqual(0, carSimulatorService.GetTiredness());
                Assert.IsNull(carSimulatorService.GetCurrentRandomDriver());
            }
        }
    }


}
