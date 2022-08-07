// -----------------------------------------------------------------------
// <copyright file="PlaytimeCollection.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using LiteDB;

    /// <summary>
    /// Manages an <see cref="ILiteCollection{T}"/> of <see cref="Playtime"/>s.
    /// </summary>
    public class PlaytimeCollection
    {
        private readonly ILiteCollection<Playtime> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaytimeCollection"/> class.
        /// </summary>
        /// <param name="database">The database the collection resides in.</param>
        public PlaytimeCollection(ILiteDatabase database)
        {
            collection = database.GetCollection<Playtime>();
            collection.EnsureIndex(playtime => playtime.UserId);
        }

        /// <inheritdoc cref="ILiteCollection{T}.FindById(BsonValue)"/>
        public Playtime Get(string userId) => collection.FindById(userId);

        /// <inheritdoc cref="ILiteCollection{T}.FindOne(Expression&lt;Func&lt;T, bool&gt;&gt;)"/>
        public Playtime Get(Expression<Func<Playtime, bool>> predicate) => collection.FindOne(predicate);

        /// <inheritdoc cref="ILiteCollection{T}.FindAll"/>
        public IEnumerable<Playtime> GetAll() => collection.FindAll();

        /// <inheritdoc cref="ILiteCollection{T}.Insert(T)"/>
        public BsonValue Insert(Playtime playtime) => collection.Insert(playtime);

        /// <summary>
        /// Resets the playtime of a user.
        /// </summary>
        /// <param name="playtime">The playtime to reset.</param>
        public void Reset(Playtime playtime)
        {
            playtime.Time = 0;
            collection.Update(playtime);
        }

        /// <inheritdoc cref="ILiteCollection{T}.Update(T)"/>
        public void Update(Playtime playtime) => collection.Update(playtime);

        /// <summary>
        /// Updates, or inserts if it does not exist, the playtime of a user.
        /// </summary>
        /// <param name="userId">The user to update the playtime of.</param>
        /// <param name="time">The amount of time to add.</param>
        public void Upsert(string userId, float time)
        {
            Playtime playtime = Get(userId);
            if (playtime == null)
            {
                playtime = new Playtime(userId, time);
                Insert(playtime);
                return;
            }

            playtime.Time += time;
            Update(playtime);
        }
    }
}