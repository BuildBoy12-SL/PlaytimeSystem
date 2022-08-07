// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using PlaytimeSystem.Models;
    using UnityEngine;

    /// <summary>
    /// General event handlers.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;
        private readonly Dictionary<string, float> startTimes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">The <see cref="Plugin{TConfig}"/> class reference.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            Exiled.Events.Handlers.Player.Verified += OnVerified;
            Exiled.Events.Handlers.Player.Destroying += OnDestroying;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.Verified -= OnVerified;
            Exiled.Events.Handlers.Player.Destroying -= OnDestroying;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
        }

        private void OnVerified(VerifiedEventArgs ev)
        {
            if (Round.IsStarted)
                startTimes[ev.Player.UserId] = Time.time;

            Playtime playtime = plugin.PlaytimeCollection.Get(ev.Player.UserId);
            if (playtime != null)
            {
                playtime.Nickname = ev.Player.Nickname;
                plugin.PlaytimeCollection.Update(playtime);
                return;
            }

            playtime = new Playtime(ev.Player.UserId) { Nickname = ev.Player.Nickname };
            plugin.PlaytimeCollection.Insert(playtime);
        }

        private void OnDestroying(DestroyingEventArgs ev)
        {
            if (startTimes.TryGetValue(ev.Player.UserId, out float startTime))
            {
                plugin.PlaytimeCollection.Upsert(ev.Player.UserId, Time.time - startTime);
                startTimes.Remove(ev.Player.UserId);
            }
        }

        private void OnRoundStarted()
        {
            foreach (Player player in Player.List)
                startTimes[player.UserId] = Time.time;
        }

        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            foreach (Player player in Player.List)
            {
                if (startTimes.TryGetValue(player.UserId, out float startTime))
                {
                    plugin.PlaytimeCollection.Upsert(player.UserId, Time.time - startTime);
                    startTimes.Remove(player.UserId);
                }
            }
        }
    }
}