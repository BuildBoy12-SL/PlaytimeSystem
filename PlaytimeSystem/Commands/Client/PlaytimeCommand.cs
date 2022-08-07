// -----------------------------------------------------------------------
// <copyright file="PlaytimeCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Commands.Client
{
    using System;
    using System.ComponentModel;
    using CommandSystem;
    using Exiled.API.Features;
    using PlaytimeSystem.Models;

    /// <inheritdoc />
    public class PlaytimeCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "playtime";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "pt" };

        /// <inheritdoc />
        public string Description { get; set; } = "Get your playtime";

        /// <summary>
        /// Gets or sets the response to send to the player.
        /// </summary>
        [Description("The response to send to the player.")]
        public string Response { get; set; } = "Playtime: {0}";

        /// <summary>
        /// Gets or sets the way that the timespan should be formatted.
        /// </summary>
        [Description("The way that the timespan should be formatted.")]
        public string TimeFormat { get; set; } = "d'd 'h'h 'm'm 's's'";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            Playtime playtime = Plugin.Instance.PlaytimeCollection.Get(player.UserId);
            response = string.Format(Response, playtime.ToTimespan().ToString(TimeFormat));
            return true;
        }
    }
}