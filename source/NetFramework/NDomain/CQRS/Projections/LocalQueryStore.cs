﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.CQRS.Projections
{
    /// <summary>
    /// In-memory and InProc implementation of a QueryStore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalQueryStore<T> : IQueryStore<T>
    {
        private readonly ConcurrentDictionary<string, Query<T>> data;

        public LocalQueryStore()
        {
            this.data = new ConcurrentDictionary<string, Query<T>>();
        }

        public Task<Query<T>> Get(string id)
        {
            var query = this.data.GetOrAdd(id, i =>
                new Query<T>
                {
                    Id = id,
                    Version = 0,
                    DateUtc = DateTime.UtcNow,
                    Data = default(T)
                });

            return Task.FromResult(query);
        }

        public Task Set(string id, Query<T> query, int expectedVersion)
        {
            Query<T> prevQuery = null;

            do
            {
                prevQuery = this.data[id];
                if (prevQuery.Version >= query.Version)
                {
                    // do nothing, the current version is more recent that the one
                    // we are trying to set
                    return Task.FromResult(true);
                }
            }
            while (this.data.TryUpdate(id, query, prevQuery) == false);
                
            return Task.FromResult(true);
        }
    }
}
