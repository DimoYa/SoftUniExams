namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Tests
    {
        [Test]
        public void Constructor_ValidName_SetsName()
        {
            // Arrange
            string stationName = "TestStation";

            // Act
            RailwayStation station = new RailwayStation(stationName);

            // Assert
            Assert.AreEqual(stationName, station.Name);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Constructor_NullEmptyName_ThrowsArgumentException(string name)
        {
            // Arrange
            string stationName = name;

            // Act and Assert
            Assert.That(() => new RailwayStation(stationName), Throws
                .TypeOf<ArgumentException>()
                .With.Message.EqualTo("Name cannot be null or empty!"));
        }

        [Test]
        public void NewArrivalOnBoard_AddTrainToArrivalQueue()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            // Act
            station.NewArrivalOnBoard(trainInfo);

            // Assert
            Assert.AreEqual(trainInfo, station.ArrivalTrains.Peek());
        }

        [Test]
        public void DepartureTrain_Queue()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string firstTrainInfo = "Train123";
            string secondTrainInfo = "Train124";

            // Act
            station.NewArrivalOnBoard(firstTrainInfo);
            station.TrainHasArrived(firstTrainInfo);
            station.NewArrivalOnBoard(secondTrainInfo);
            station.TrainHasArrived(secondTrainInfo);

            // Assert
            Assert.AreEqual(2, station.DepartureTrains.Count);
            Assert.AreEqual(firstTrainInfo, station.DepartureTrains.First());

            station.TrainHasLeft(firstTrainInfo);

            Assert.AreEqual(1, station.DepartureTrains.Count);
            Assert.AreEqual(secondTrainInfo, station.DepartureTrains.First());
        }

        [Test]
        public void TrainHasArrived_ValidTrainInfo_ReturnsExpectedMessage()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);

            // Act
            string result = station.TrainHasArrived(trainInfo);

            // Assert
            Assert.AreEqual($"{trainInfo} is on the platform and will leave in 5 minutes.", result);
        }

        [Test]
        public void TrainHasArrived_WrongTrainInfo_ReturnsErrorMessage()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);

            // Act
            string result = station.TrainHasArrived("WrongTrain");

            // Assert
            Assert.AreEqual($"There are other trains to arrive before WrongTrain.", result);
        }

        [Test]
        public void TrainHasLeft_TrainInfoInDepartureQueue_ReturnsTrue()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);

            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);
            station.TrainHasArrived(trainInfo);

            // Act
            bool result = station.TrainHasLeft(trainInfo);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TrainHasLeft_TrainInfoNotInDepartureQueue_ReturnsFalse()
        {
            // Arrange
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);

            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);
            station.TrainHasArrived(trainInfo);

            // Act
            bool result = station.TrainHasLeft("invalid");

            // Assert
            Assert.IsFalse(result);
        }
    }
}