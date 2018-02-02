using System;

namespace Security.V2.Core.Ioc
{
    public class AddInstanceEventArgs: EventArgs
    {
        public Type ServiceType { get; set; }
    }
}