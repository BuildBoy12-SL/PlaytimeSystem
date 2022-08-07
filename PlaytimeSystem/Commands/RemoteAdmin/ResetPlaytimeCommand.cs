// -----------------------------------------------------------------------
// <copyright file="ResetPlaytimeCommand.cs" company="Build">
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
    public class ResetPlaytimeCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "reset";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public string Description { get; set; } = "Resets a player's playtime.";

        /// <summary>
        /// Gets or sets the permission required to use this command.
        /// </summary>
        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "pt.reset";

        /// <summary>
        /// Gets or sets the response to send to the user when they lack the <see cref="RequiredPermission"/>.
        /// </summary>
        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = "You do not have permission to use this command.";

        /// <summary>
        /// Gets or sets the response to send an insufficient number of arguments is specified.
        /// </summary>
        [Description("The response to send an insufficient number of arguments is specified.")]
        public string ProvideArgumentsResponse { get; set; } = "Usage: playtime reset <userId>";

        /// <summary>
        /// Gets or sets the response to send when no matches are found.
        /// </summary>
        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } = "No matches found.";

        /// <summary>
        /// Gets or sets the response to send when a player's playtime is reset.
        /// </summary>
        public string SuccessResponse { get; set; } = "Playtime of {0} has been reset.";

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

            Plugin.Instance.PlaytimeCollection.Reset(playtime);
            response = string.Format(SuccessResponse, playtime.Nickname);
            return true;
        }
    }
}