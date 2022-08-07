// -----------------------------------------------------------------------
// <copyright file="PlaytimeParentConfig.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Configs
{
    using CommandSystem;
    using PlaytimeSystem.Commands.RemoteAdmin;

    /// <summary>
    /// Handles configurable options for the <see cref="PlaytimeParentCommand"/>.
    /// </summary>
    public class PlaytimeParentConfig
    {
        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Command"/>.
        /// </summary>
        public string Command { get; set; } = "playtime";

        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Aliases"/>.
        /// </summary>
        public string[] Aliases { get; set; } = { "pt" };

        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Description"/>.
        /// </summary>
        public string Description { get; set; } = "Manages the playtime of players";
    }
}