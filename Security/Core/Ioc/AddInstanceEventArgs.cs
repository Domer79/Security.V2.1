using System;

namespace Security.Core.Ioc
{
    public class AddInstanceEventArgs: EventArgs
    {
        public Type ServiceType { get; set; }
    }
}