using Security.Tests.DI.Interfaces;

namespace Security.Tests.DI.Concrete
{
    public class Sample2 : ISample2
    {
        public ISample1 Sample1 { get; }

        public Sample2(ISample1 sample1)
        {
            Sample1 = sample1;
        }
    }
}