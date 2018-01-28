using System;
using System.Linq;
using NUnit.Framework;
using Security.V2.Contracts;
using Security.V2.Core.Ioc;

namespace Security.Tests
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

    public static class IocService
    {
        public static void InTestRequestScope(this IDependency dependency, ServiceLocatorTest serviceLocatorTest)
        {
            dependency.Scope = new TestRequestScope(dependency.Registry, serviceLocatorTest);
        }
    }

    public class TestRequestScope : IScope
    {
        private readonly IRegistry _registry;
        private readonly ServiceLocatorTest _serviceLocatorTest;

        public TestRequestScope(IRegistry registry, ServiceLocatorTest serviceLocatorTest)
        {
            _registry = registry;
            _serviceLocatorTest = serviceLocatorTest;
            _serviceLocatorTest.BeginScope += ServiceLocatorTest_BeginScope            ; ;
            _serviceLocatorTest.EndScope += ServiceLocatorTest_EndScope;
        }

        private void ServiceLocatorTest_EndScope(object sender, EventArgs e)
        {
            var dependencies = _registry.DependencyCollection.Where(_ => _.Scope is TestRequestScope);
            foreach (var dependency in dependencies)
            {
                _registry.ServiceInstanceDismiss(dependency.ServiceType);
            }
        }

        private void ServiceLocatorTest_BeginScope(object sender, EventArgs e)
        {
            
        }

        public object GetObject(IRequest request, Type serviceType)
        {
            var instance = request.GetService(serviceType);
            if (instance != null)
                return instance;

            var dependency = _registry.GetFromRegistry(serviceType);
            if (dependency == null)
                throw new ObjectDisposedException($"Object {dependency.ServiceType} disposed");

            if (dependency.MethodImplement != null)
            {
                instance = dependency.MethodImplement();
                if (instance != null)
                    return instance;
            }

            instance = dependency.ResolveByType(request);
            request.SetService(serviceType, instance);
            return instance;
        }
    }

    public interface ISample1
    {
        string Name { get; set; }
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
        public string Name { get; set; }
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
