using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Xml.Linq;

namespace SecureOpsSystem.Tests
{
    [TestFixture]
    public class SecureHubTests
    {
        private SecureHub secureHub;

        [SetUp]
        public void SetUp()
        {
            this.secureHub = new SecureHub(10);
        }

        [Test]
        public void TestConstructorShouldReturnData()
        {
            Assert.Multiple(() =>
            {
                Assert.That(this.secureHub.Capacity, Is.EqualTo(10));
                Assert.That(this.secureHub.Tools, Is.Not.Null);
            });
            Assert.That(this.secureHub.Tools.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestCapacityLessZeroShouldThrowEx()
        {
            Assert.That(() => new SecureHub(0), Throws
                .TypeOf<ArgumentException>()
                .With.Message.EqualTo("Capacity must be greater than 0."));
        }

        [Test]
        public void TestAddFullCapacityHub()
        {
            var sut = new SecureHub(1);
            sut.AddTool(new SecurityTool("Test", "Test", 2.0));

            var actualMsg = sut.AddTool(new SecurityTool("Test2", "Test2", 2.0));

            Assert.Multiple(() =>
            {
                Assert.That(actualMsg, Is.EqualTo("Secure Hub is at full capacity."));
                Assert.That(sut.Tools.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestAddExistingHub()
        {
            var sut = new SecureHub(1);
            sut.AddTool(new SecurityTool("Test", "Test", 2.0));

            var actualMsg = sut.AddTool(new SecurityTool("Test", "Test2", 2.0));

            Assert.Multiple(() =>
            {
                Assert.That(actualMsg, Is.EqualTo("Security Tool Test already exists in the hub."));
                Assert.That(sut.Tools.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestAddToolSuccess()
        {
            var sut = new SecureHub(1);

            var actualMsg = sut.AddTool(new SecurityTool("Test", "Test2", 2.0));

            Assert.Multiple(() =>
            {
                Assert.That(actualMsg, Is.EqualTo("Security Tool Test added successfully."));
                Assert.That(sut.Tools.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestRemovebSuccess()
        {
            var sut = new SecureHub(2);
            var tool1 = new SecurityTool("Test", "Test", 2.0);
            var tool2 = new SecurityTool("Test2", "Test2", 2.0);

            sut.AddTool(tool1);
            sut.AddTool(tool2);

            var actual = sut.RemoveTool(tool1);

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.True);
                Assert.That(sut.Tools.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestDeployExistingTool()
        {
            var sut = new SecureHub(2);
            var tool = new SecurityTool("Test", "Test", 2.0);
            sut.AddTool(tool);

            var actual = sut.DeployTool("Test");

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.EqualTo(tool));
                Assert.That(sut.Tools.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void TestDeployInExistentToolReturnsNull()
        {
            var sut = new SecureHub(2);

            var actual = sut.DeployTool("Test");

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.Null);
                Assert.That(sut.Tools.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void TestReport()
        {
            var sut = new SecureHub(2);
            var tool1 = new SecurityTool("Test", "Test", 2.0);
            var tool2 = new SecurityTool("Test2", "Test2", 5.5);

            sut.AddTool(tool1);
            sut.AddTool(tool2);

            var actual = sut.SystemReport();
            var expected = "Secure Hub Report:\r\nAvailable Tools: 2\r\nName: " +
                "Test2, Category: Test2, Effectiveness: 5,50\r\nName: Test, Category: " +
                "Test, Effectiveness: 2,00";

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
