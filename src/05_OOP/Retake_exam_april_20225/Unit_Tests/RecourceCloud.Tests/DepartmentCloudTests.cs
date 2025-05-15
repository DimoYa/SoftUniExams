using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecourceCloud.Tests
{
    public class DepartmentCloudTests
    {
        private DepartmentCloud departmentCloud;

        [SetUp]
        public void Setup()
        {
            this.departmentCloud = new DepartmentCloud();
        }

        [Test]
        public void VerifyConstructorInit()
        {
            Assert.Multiple(() =>
            {
                Assert.That(departmentCloud.Tasks, Is.Not.Null);
                Assert.That(departmentCloud.Tasks, Is.Empty);
                Assert.That(departmentCloud.Resources, Is.Not.Null);
                Assert.That(departmentCloud.Resources, Is.Empty);
            });
        }

        [Test]
        public void LogArgumentsDiffThanLenThreeThrowsEx()
        {
            var taks = new string[] { "1", "Label", "Resource", "4" };

            Assert.That(() => departmentCloud.LogTask(taks), Throws
                .TypeOf<ArgumentException>()
                .With.Message.EqualTo("All arguments are required."));
        }

        [Test]
        public void LogArgumentsNullThrowsEx()
        {
            var taks = new string[] { "1", "Label", null };

            Assert.Multiple(() =>
            {
                Assert.That(() => departmentCloud.LogTask(taks), Throws
                    .TypeOf<ArgumentException>()
                    .With.Message.EqualTo("Arguments values cannot be null."));
                Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void LogArgumentsExistingTaskReturnMsg()
        {
            var taks = new string[] { "1", "Label", "Resource" };
            departmentCloud.LogTask(taks);

            var expected = departmentCloud.LogTask(taks);

            Assert.Multiple(() =>
            {
                Assert.That(expected, Is.EqualTo("Resource is already logged."));
                Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void LogArgumentsSuccessReturnMsg()
        {
            var taks = new string[] { "1", "Label", "Resource" };

            var expected = departmentCloud.LogTask(taks);

            Assert.Multiple(() =>
            {
                Assert.That(expected, Is.EqualTo("Task logged successfully."));
                Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(1));
            });
        }


        [Test]
        public void CreateResourceEmptyTaskReturnFalse()
        {
            var expected = departmentCloud.CreateResource();

            Assert.Multiple(() =>
            {
                Assert.That(expected, Is.False);
                Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(0));
                Assert.That(departmentCloud.Resources.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void CreateResourceSuccessReturnTrue()
        {
            var taks = new string[] { "1", "Label", "Resource" };
            departmentCloud.LogTask(taks);
            var expected = departmentCloud.CreateResource();

            Assert.Multiple(() =>
            {
                Assert.That(expected, Is.True);
                Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(0));
                Assert.That(departmentCloud.Resources.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestResourceNullReturnNull()
        {
            var expected = departmentCloud.TestResource("Resource");
            Assert.That(expected, Is.Null);
        }

        [Test]
        public void TestResourceReturnTestedResource()
        {
            var taks = new string[] { "1", "Label", "Resource" };
            departmentCloud.LogTask(taks);
            departmentCloud.CreateResource();

            var expected = departmentCloud.TestResource("Resource");

            Assert.Multiple(() =>
            {
                Assert.That(expected?.IsTested, Is.True);
                Assert.That(expected?.Name, Is.EqualTo("Resource"));
            });
        }
    }
}