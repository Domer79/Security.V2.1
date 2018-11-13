using System;
using NUnit.Framework;
using Security.Core.Ioc;
using Security.Core.Ioc.Exceptions;
using Security.Tests.DI.Concrete;
using Security.Tests.DI.Interfaces;

namespace Security.Tests.DI
{
    [TestFixture]
    public class ServiceLocatorTest
    {
        [SetUp]
        public void TestSetup()
        {

        }

        [TearDown]
        public void TestsDown()
        {

        }

        [Test]
        public void TestMethod1()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>();
            serviceLocator.RegisterType<ISample2, Sample2>();
            serviceLocator.RegisterType<ISampleManager, SampleManager>();

            var sample1 = serviceLocator.Resolve<ISample1>();

            Assert.That(sample1, Is.InstanceOf<ISample1>());
            serviceLocator.Dispose();
        }

        [Test]
        public void TestMethod2()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>();
            serviceLocator.RegisterType<ISample2, Sample2>();
            serviceLocator.RegisterType<ISampleManager, SampleManager>();

            var sample2 = serviceLocator.Resolve<ISample2>();

            Assert.That(sample2.Sample1, Is.InstanceOf<ISample1>());
            serviceLocator.Dispose();
        }

        [Test]
        public void TestMethod3()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>();
            serviceLocator.RegisterType<ISample2, Sample2>();
            serviceLocator.RegisterType<ISampleManager, SampleManager>();

            var sampleManager = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager.Sample1, Is.InstanceOf<ISample1>());
            Assert.That(sampleManager.Sample2, Is.InstanceOf<ISample2>());
            serviceLocator.Dispose();
        }

        [Test]
        public void TestRequestScope_Sample1_Name_Always_ExpectedValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISample2, Sample2>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InTestRequestScope(this);

            var sampleManager1 = serviceLocator.Resolve<ISampleManager>();
            sampleManager1.Sample1.Name = "Damir Garipov";
            
            var sampleManager2 = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager2.Sample1.Name, Is.EqualTo(sampleManager1.Sample1.Name));
            serviceLocator.Dispose();
        }

        [TestCase("Damir Garipov")]
        public void TestRequestScope_Sample1_Name_Always_Not_ExpectedValue(string expectedName)
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISample2, Sample2>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InTestRequestScope(this);

            var sampleManager1 = serviceLocator.Resolve<ISampleManager>();
            sampleManager1.Sample1.Name = expectedName;

            OnEndScope();
            var sampleManager2 = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager2.Sample1.Name, Is.Null);
            Assert.That(sampleManager1.Sample1.Name, Is.EqualTo(expectedName));
            serviceLocator.Dispose();
        }

        [Test]
        public void TestFactoryDependency_ReturnAlways_ExpectedValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISample2, Sample2>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InTestRequestScope(this);
            serviceLocator.RegisterFactory<ISampleFactory, SampleFactory>();

            var sampleFactory = serviceLocator.Resolve<ISampleFactory>();

            Assert.That(sampleFactory, Is.InstanceOf<ISampleFactory>());
            Assert.That(sampleFactory.Sample1, Is.Not.Null);
            Assert.That(sampleFactory.Sample2, Is.Not.Null);
            Assert.That(sampleFactory.SampleManager, Is.Not.Null);
            Assert.That(sampleFactory.SampleManager.Sample1, Is.Not.Null);
            Assert.That(sampleFactory.SampleManager.Sample2, Is.Not.Null);
            serviceLocator.Dispose();
        }

        [Test]
        public void TestFactoryDependency_Expected_DependencyResolveException()
        {
            var serviceLocator = new ServiceLocator();
//            serviceLocator.RegisterType<ISample1, Sample1>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISample2, Sample2>().InTestRequestScope(this);
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InTestRequestScope(this);
            serviceLocator.RegisterFactory<ISampleFactory, SampleFactory>();

            Assert.Catch<DependencyResolveException>(() =>
            {
                serviceLocator.Resolve<ISampleFactory>();
            });
            serviceLocator.Dispose();
        }

        [TestCase()]
        public void TestSingletoneScope_ReturnAlways_ExpectedValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>().InSingletonScope();
            serviceLocator.RegisterType<ISample2, Sample2>().InSingletonScope();
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InSingletonScope();

            var sampleManager1 = serviceLocator.Resolve<ISampleManager>();
            sampleManager1.Sample1.Name = "Damir Garipov";

            var sampleManager2 = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager2.Sample1.Name, Is.EqualTo(sampleManager1.Sample1.Name));
            serviceLocator.Dispose();
        }

        [TestCase()]
        public void TestTransientScope_ReturnAlways_NullValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterType<ISample1, Sample1>();
            serviceLocator.RegisterType<ISample2, Sample2>().InSingletonScope();
            serviceLocator.RegisterType<ISampleManager, SampleManager>().InSingletonScope();

            var sampleManager1 = serviceLocator.Resolve<ISampleManager>();
            sampleManager1.Sample1.Name = "Damir Garipov";

            var sampleManager2 = serviceLocator.Resolve<ISampleManager>();

            Assert.That(sampleManager2.Sample1.Name, Is.Null);
            serviceLocator.Dispose();
        }

        [Test]
        public void TestRegisterByMethod_AndReturnAlways_ExpectedValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterByMethod(typeof(ISample1), () => new Sample1());

            var sample1 = serviceLocator.Resolve<ISample1>();
            sample1.Name = "Sample1";

            Assert.That(sample1, Is.InstanceOf<Sample1>());
            sample1 = serviceLocator.Resolve<ISample1>();
            Assert.That(sample1.Name, Is.Null);
        }

        [Test]
        public void TestRegisterByMethod_InSingletoneScope_AndReturnAlways_ExpectedValue()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterByMethod(typeof(ISample1), () => new Sample1()).InSingletonScope();

            var sample1 = serviceLocator.Resolve<ISample1>();
            sample1.Name = "Sample1";

            Assert.That(sample1, Is.InstanceOf<Sample1>());
            sample1 = serviceLocator.Resolve<ISample1>();
            Assert.That(sample1.Name, Is.EqualTo("Sample1"));
        }

        public event EventHandler BeginScope;
        public event EventHandler EndScope;

        protected virtual void OnBeginScope()
        {
            BeginScope?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEndScope()
        {
            EndScope?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface ISampleFactory: IDisposable
    {
        ISample1 Sample1 { get; set; }
        ISample2 Sample2 { get; set; }
        ISampleManager SampleManager { get; set; }
    }

    public class SampleFactory : ISampleFactory
    {
        public ISample1 Sample1 { get; set; }
        public ISample2 Sample2 { get; set; }
        public ISampleManager SampleManager { get; set; }

        public void Dispose()
        {
            
        }
    }
}
