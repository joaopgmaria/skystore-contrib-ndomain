﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.CQRS.Projections
{
    /// <summary>
    /// Represents a key/value store where query objects can be stored and retrieved.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryStore<T>
    {
        /// <summary>
        /// Gets the query object with the given id
        /// </summary>
        /// <param name="id">id of the query object</param>
        /// <returns>Query</returns>
        Task<Query<T>> Get(string id);

        /// <summary>
        /// Saves a query object with the given id.
        /// If the query already existed, it is overriden
        /// </summary>
        /// <param name="id">id of the query object</param>
        /// <param name="query">query object</param>
        /// <returns>Task</returns>
        Task Set(string id, Query<T> query, int expectedVersion);
    }
}
