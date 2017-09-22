﻿using NDomain.Bus.Subscriptions;
using NDomain.Bus.Subscriptions.Redis;
using NDomain.Tests.Specs;
using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.Redis.Tests.Bus.Subscriptions
{
    [TestFixture(Category = "Redis")]
    [Ignore("No Redis support in CI for the time being.")]
    public class RedisSubscriptionBrokerTests : SubscriptionBrokerSpecs
    {
        ConnectionMultiplexer connection;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new ConfigurationOptions();
            options.AllowAdmin = true;
            options.EndPoints.Add("localhost:6379");

            try
            {
                this.connection = ConnectionMultiplexer.Connect(options);
                this.connection.PreserveAsyncOrder = false;
            }
            catch (Exception ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }

        protected override ISubscriptionBroker CreateSubscriptionBroker()
        {
            return new RedisSubscriptionBroker(connection, "ndomain.tests");
        }
    
        protected override void OnSetUp()
        {
            connection.GetServer("localhost:6379").FlushAllDatabases();
        }

        protected override void OnTearDown()
        {
            connection.GetServer("localhost:6379").FlushAllDatabases();
        }
    }
}
