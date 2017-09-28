﻿using NDomain.Bus.Transport;
using NDomain.Tests.Specs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.Tests.Bus.Transport
{
    [TestFixture]
    public class LocalTransportTests : TransportSpecs
    {
        public override ITransportFactory CreateFactory()
        {
            return new LocalTransportFactory();
        }
    }
}
