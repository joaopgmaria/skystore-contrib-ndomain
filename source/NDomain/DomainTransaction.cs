﻿using NDomain.Bus.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NDomain
{
/// <summary>
    /// Provides an message context scope for any processing that happens within a message handler.
    /// The context is available in the same thread and in the same CallContext, so asynchronous programming with the 'await' keyword will preserve the context on the continuations.
    /// </summary>
    /// <remarks>This is not related with <seealso cref="System.Transactions.TransactionScope"/></remarks>
    public class DomainTransactionScope : IDisposable
    {
        // TODO: rename to MessageContextScope

        public DomainTransactionScope(string transactionId, string correlationId, int retryCount)
        {
            CallContext.LogicalSetData("cqrs:transaction", 
                new DomainTransaction(transactionId, correlationId, retryCount));
        }

        public void Dispose()
        {
            if (DomainTransaction.Current != null)
            {
                CallContext.LogicalSetData("cqrs:transaction", null);
            }
        }
    }

    /// <summary>
    /// Contains the contextual properties when processing a message, like its Id and how many times it was retried
    /// </summary>
    public class DomainTransaction
    {
        // TODO: rename to MessageContext

        readonly string id;
        readonly string correlationId;
        readonly int deliveryCount;

        internal DomainTransaction(string id, string correlationId, int deliveryCount)
        {
            this.id = id;
            this.correlationId = correlationId;
            this.deliveryCount = deliveryCount;
        }

        public string Id { get { return this.id; } }
        public string CorrelationId { get { return this.correlationId; } }
        public int DeliveryCount { get { return this.deliveryCount; } }

        /// <summary>
        /// Returns the current, ambient message context. 
        /// The context is available in the same thread and in the same CallContext, so asynchronous programming with the 'await' keyword will preserve the context.
        /// </summary>
        public static DomainTransaction Current
        {
            get
            {
                return CallContext.LogicalGetData("cqrs:transaction") as DomainTransaction;
            }
        }
    }
}
