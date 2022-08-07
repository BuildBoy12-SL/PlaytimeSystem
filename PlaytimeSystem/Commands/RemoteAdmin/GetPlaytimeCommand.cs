// -----------------------------------------------------------------------
// <copyright file="GetPlaytimeCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Commands.RemoteAdmin
{
    using System;
    using System.ComponentModel;
    using CommandSystem;
    using Exiled.Permissions.Extensions;
    using PlaytimeSystem.Models;

    /// <inheritdoc />
    public class GetPlaytimeCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "get";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "g" };

        /// <inheritdoc />
        public string Description { get; set; } = "Gets the playtime of a player";

        /// <summary>
        /// Gets or sets the permission required to use this command.
        /// </summary>
        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "pt.get";

        /// <summary>
        /// Gets or sets the response to send to the user when they lack the <see cref="RequiredPermission"/>.
        /// </summary>
        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = "You do not have permission to use this command.";

        /// <summary>
        /// Gets or sets the response to send an insufficient number of arguments is specified.
        /// </summary>
        [Description("The response to send an insufficient number of arguments is specified.")]
        public string ProvideArgumentsResponse { get; set; } = "Usage: playtime get <userId>";

        /// <summary>
        /// Gets or sets the response to send when no matches are found.
        /// </summary>
        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } = "No matches found.";

        /// <summary>
        /// Gets or sets the response to send when the player has no playtime.
        /// </summary>
        [Description("The response to send when the player has no playtime. Available placeholders: {0}:PlayerName")]
        public string NoTime { get; set; } = "{0} has no playtime.";

        /// <summary>
        /// Gets or sets the response to send when a playtime is found.
        /// </summary>
        [Description("The response to send when a playtime is found. Available placeholders: {0}:PlayerName, {1}:FirstConnect, {2}:Time")]
        public string SuccessResponse { get; set; } = "{0}'s Playtime\nFirst Connect: {1}\nTotal: {2}";

        /// <summary>
        /// Gets or sets the format for the first connect date.
        /// </summary>
        [Description("The format for the first connect date.")]
        public string FirstConnectFormat { get; set; } = "MM/dd/yyyy";

        /// <summary>
        /// Gets or sets the way that the timespan should be formatted.
        /// </summary>
        [Description("The way that the timespan should be formatted.")]
        public string TimeFormat { get; set; } = "d'd 'h'h 'm'm 's's'";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count == 0)
            {
                response = ProvideArgumentsResponse;
                return false;
            }

            Playtime playtime = Plugin.Instance.PlaytimeCollection.Get(arguments.At(0));
            if (playtime == null)
            {
                response = NoMatchesResponse;
                return false;
            }

            if (playtime.Time == 0)
            {
                response = string.Format(NoTime, playtime.Nickname);
                return false;
            }

            response = string.Join(SuccessResponse, playtime.Nickname, playtime.FirstConnect.ToString(FirstConnectFormat), playtime.ToTimespan().ToString(TimeFormat));
            return true;
        }
    }
}