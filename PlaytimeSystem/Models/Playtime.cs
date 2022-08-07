// -----------------------------------------------------------------------
// <copyright file="Playtime.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Models
{
    using System;
    using LiteDB;

    /// <summary>
    /// Represents the playtime a player has accumulated.
    /// </summary>
    public class Playtime
    {
        private double time;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playtime"/> class.
        /// </summary>
        /// <param name="userId"><inheritdoc cref="UserId"/></param>
        /// <param name="time"><inheritdoc cref="Time"/></param>
        public Playtime(string userId, double time = 0)
        {
            UserId = userId;
            Time = time;
            FirstConnect = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the id of the playtime.
        /// </summary>
        [BsonId]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the last known nickname for the user.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the amount of time, in seconds, that has been played.
        /// </summary>
        public double Time
        {
            get => time;
            set
            {
                if (value < 0)
                    value = 0;

                time = value;
            }
        }

        /// <summary>
        /// Gets or sets the date and time the player first connected to the server.
        /// </summary>
        public DateTime FirstConnect { get; set; }

        /// <summary>
        /// Returns the amount of time that has been played.
        /// </summary>
        /// <returns>A <see cref="TimeSpan"/> representing how much time has been played.</returns>
        public TimeSpan ToTimespan() => TimeSpan.FromSeconds(Time);
    }
}