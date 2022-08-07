// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem
{
    using Exiled.API.Interfaces;
    using PlaytimeSystem.Commands.Client;
    using PlaytimeSystem.Commands.RemoteAdmin;
    using PlaytimeSystem.Configs;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="PlaytimeParentConfig"/> class.
        /// </summary>
        public PlaytimeParentConfig PlaytimeParent { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="AddPlaytimeCommand"/> command.
        /// </summary>
        public AddPlaytimeCommand AddPlaytime { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="GetPlaytimeCommand"/> command.
        /// </summary>
        public GetPlaytimeCommand GetPlaytime { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="RemovePlaytimeCommand"/> command.
        /// </summary>
        public RemovePlaytimeCommand RemovePlaytime { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="ResetPlaytimeCommand"/> command.
        /// </summary>
        public ResetPlaytimeCommand ResetPlaytime { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="SetPlaytimeCommand"/> command.
        /// </summary>
        public SetPlaytimeCommand SetPlaytime { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="PlaytimeCommand"/> command.
        /// </summary>
        public PlaytimeCommand PlaytimeClient { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="LeaderboardCommand"/> command.
        /// </summary>
        public LeaderboardCommand Leaderboard { get; set; } = new();
    }
}