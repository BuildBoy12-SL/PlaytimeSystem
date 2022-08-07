// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem
{
    using System;
    using System.IO;
    using CommandSystem;
    using Exiled.API.Features;
    using LiteDB;
    using PlaytimeSystem.Commands.Client;
    using PlaytimeSystem.Models;
    using RemoteAdmin;

    /// <inheritdoc />
    public class Plugin : Plugin<Config, Translation>
    {
        private LiteDatabase database;
        private EventHandlers eventHandlers;

        /// <summary>
        /// Gets a static instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Gets the collection of playtime.
        /// </summary>
        public PlaytimeCollection PlaytimeCollection { get; private set; }

        /// <inheritdoc/>
        public override string Author => "Build";

        /// <inheritdoc/>
        public override string Name => "PlaytimeSystem";

        /// <inheritdoc/>
        public override string Prefix => "PlaytimeSystem";

        /// <inheritdoc/>
        public override Version Version { get; } = new(1, 0, 0);

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new(5, 2, 2);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            if (string.IsNullOrEmpty(Config.DatabasePath) || string.IsNullOrEmpty(Config.DatabaseFile))
            {
                Log.Error($"Database path or file is not set. Please set it in the config file. {nameof(Name)} will not load.");
                return;
            }

            Instance = this;
            eventHandlers = new EventHandlers(this);
            eventHandlers.Subscribe();
            database = new LiteDatabase(Path.Combine(Config.DatabasePath, Config.DatabaseFile));
            PlaytimeCollection = new PlaytimeCollection(database);
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            database?.Checkpoint();
            PlaytimeCollection = null;
            database = null;
            eventHandlers?.Unsubscribe();
            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }

        /// <inheritdoc />
        public override void OnRegisteringCommands()
        {
            RegisterClientCommands();
            base.OnRegisteringCommands();
        }

        private void RegisterClientCommands()
        {
            if (Translation.PlaytimeClient != null)
            {
                QueryProcessor.DotCommandHandler.RegisterCommand(Translation.PlaytimeClient);
                Commands[typeof(ClientCommandHandler)][typeof(PlaytimeCommand)] = Translation.PlaytimeClient;
            }
            else
            {
                Log.Error("Playtime client translation is null. Please set it in the translations file. Playtime client command will not load.");
            }

            if (Translation.Leaderboard != null)
            {
                QueryProcessor.DotCommandHandler.RegisterCommand(Translation.Leaderboard);
                Commands[typeof(ClientCommandHandler)][typeof(LeaderboardCommand)] = Translation.Leaderboard;
            }
            else
            {
                Log.Error("Leaderboard client translation is null. Please set it in the translations file. Leaderboard client command will not load.");
            }
        }
    }
}