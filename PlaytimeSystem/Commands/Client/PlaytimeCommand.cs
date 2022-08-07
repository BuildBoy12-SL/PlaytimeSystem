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
        /// Gets or sets the response to send when no matches are found.
        /// </summary>
        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } = "No matches found for the specified query.";

        /// <summary>
        /// Gets or sets the response to send to the player.
        /// </summary>
        [Description("The response to send to the player. Available placeholders: {0}:Name, {1}:Id, {2}:FirstJoin, {3}:Time")]
        public string Response { get; set; } = "Showing stats for {0}:\nFirst Join: {2}\nPlaytime: {3}";

        /// <summary>
        /// Gets or sets the way that the timespan should be formatted.
        /// </summary>
        [Description("The way that the timespan should be formatted.")]
        public string TimeFormat { get; set; } = "d'd 'h'h 'm'm 's's'";

        /// <summary>
        /// Gets or sets the format for the first connect date.
        /// </summary>
        [Description("The format for the first connect date.")]
        public string FirstConnectFormat { get; set; } = "MM/dd/yyyy";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string playerIdentifier = Player.Get(sender).UserId;
            if (arguments.Count != 0)
                playerIdentifier = string.Join(" ", arguments);

            Playtime playtime = Plugin.Instance.PlaytimeCollection.Get(playtime => playtime.UserId == playerIdentifier || playtime.Nickname == playerIdentifier);
            if (playtime == null)
            {
                response = NoMatchesResponse;
                return false;
            }

            response = string.Format(Response, playtime.Nickname, playtime.UserId, playtime.FirstConnect.ToString(FirstConnectFormat), playtime.ToTimespan().ToString(TimeFormat));
            return true;
        }
    }
}