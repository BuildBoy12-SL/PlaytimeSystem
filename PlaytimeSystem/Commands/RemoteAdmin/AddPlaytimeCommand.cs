// -----------------------------------------------------------------------
// <copyright file="AddPlaytimeCommand.cs" company="Build">
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
    public class AddPlaytimeCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "add";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "a" };

        /// <inheritdoc />
        public string Description { get; set; } = "Adds playtime to a player";

        /// <summary>
        /// Gets or sets the permission required to use this command.
        /// </summary>
        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "pt.set";

        /// <summary>
        /// Gets or sets the response to send to the user when they lack the <see cref="RequiredPermission"/>.
        /// </summary>
        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = "You do not have permission to use this command.";

        /// <summary>
        /// Gets or sets the response to send an insufficient number of arguments is specified.
        /// </summary>
        [Description("The response to send an insufficient number of arguments is specified.")]
        public string ProvideArgumentsResponse { get; set; } = "Usage: playtime set <userId> <time in seconds>";

        /// <summary>
        /// Gets or sets the response to send when no matches are found.
        /// </summary>
        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } = "No matches found.";

        /// <summary>
        /// Gets or sets the response to send when an invalid amount of time is specified.
        /// </summary>
        [Description("The response to send when an invalid amount of time is specified.")]
        public string InvalidTime { get; set; } = "Specify a valid number of seconds.";

        /// <summary>
        /// Gets or sets the response to send when a player's playtime is added to.
        /// </summary>
        [Description("The response to send when a player's playtime is added to.")]
        public string SuccessResponse { get; set; } = "Playtime of {0} has been added to.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count < 2)
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

            if (!float.TryParse(arguments.At(1), out float time))
            {
                response = InvalidTime;
                return false;
            }

            playtime.Time += time;
            Plugin.Instance.PlaytimeCollection.Update(playtime);
            response = string.Format(SuccessResponse, playtime.Nickname);
            return true;
        }
    }
}