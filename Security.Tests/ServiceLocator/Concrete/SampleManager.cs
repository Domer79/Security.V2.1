using Security.Tests.ServiceLocator.Interfaces;

namespace Security.Tests.ServiceLocator.Concrete
{
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