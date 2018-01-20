using System;
using NUnit.Framework;
using Security.V2.Core.Ioc;

namespace Security.Tests
{
    [TestFixture]
    public class ServiceLocatorTest
    {

        [Test]
        public void TestMethod1()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1>().AsSingle<Sample1>();
            serviceLocator.RegisterType<ISample2>().AsSingle<Sample2>();
            serviceLocator.RegisterType<ISampleManager>().AsSingle<SampleManager>();

            var sample1 = serviceLocator.Resolve<ISample1>();

            Assert.That(sample1, Is.InstanceOf<ISample1>());
        }

        [Test]
        public void TestMethod2()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1>().AsSingle<Sample1>();
            serviceLocator.RegisterType<ISample2>().AsSingle<Sample2>();
            serviceLocator.RegisterType<ISampleManager>().AsSingle<SampleManager>();

            var sample2 = serviceLocator.Resolve<ISample2>();

            Assert.That(sample2.Sample1, Is.InstanceOf<ISample1>());
        }

        [Test]
        public void TestMethod3()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1>().AsSingle<Sample1>();
            serviceLocator.RegisterType<ISample2>().AsSingle<Sample2>();
            serviceLocator.RegisterType<ISampleManager>().AsSingle<SampleManager>();

            var sampleManager = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager.Sample1, Is.InstanceOf<ISample1>());
            Assert.That(sampleManager.Sample2, Is.InstanceOf<ISample2>());
        }
    }

    public interface ISample1
    {
    }

    public interface ISample2
    {
        ISample1 Sample1 { get; }
    }

    public interface ISampleManager
    {
        ISample1 Sample1 { get; }
        ISample2 Sample2 { get; }
    }

    public class Sample1 : ISample1
    {
    }

    public class Sample2 : ISample2
    {
        public ISample1 Sample1 { get; }

        public Sample2(ISample1 sample1)
        {
            Sample1 = sample1;
        }
    }

    public class SampleManager : ISampleManager
    {
        public ISample1 Sample1 { get; }
        public ISample2 Sample2 { get; }

        public SampleManager(ISample1 sample1, ISample2 sample2)
        {
            Sample1 = sample1;
            Sample2 = sample2;
        }
    }
}
