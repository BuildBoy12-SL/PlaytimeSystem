// -----------------------------------------------------------------------
// <copyright file="PlaytimeParentCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Commands.RemoteAdmin
{
    using System;
    using System.Text;
    using CommandSystem;
    using NorthwoodLib.Pools;

    /// <inheritdoc />
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PlaytimeParentCommand : ParentCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaytimeParentCommand"/> class.
        /// </summary>
        public PlaytimeParentCommand() => LoadGeneratedCommands();

        /// <inheritdoc />
        public override string Command => Plugin.Instance.Translation.PlaytimeParent?.Command ?? "playtime";

        /// <inheritdoc />
        public override string[] Aliases => Plugin.Instance.Translation.PlaytimeParent?.Aliases ?? new[] { "pt" };

        /// <inheritdoc />
        public override string Description => Plugin.Instance.Translation.PlaytimeParent?.Description ?? "Manages the playtime of players";

        /// <inheritdoc />
        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(Plugin.Instance.Translation.AddPlaytime);
            RegisterCommand(Plugin.Instance.Translation.GetPlaytime);
            RegisterCommand(Plugin.Instance.Translation.RemovePlaytime);
            RegisterCommand(Plugin.Instance.Translation.ResetPlaytime);
            RegisterCommand(Plugin.Instance.Translation.SetPlaytime);
        }

        /// <inheritdoc />
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Please specify a valid subcommand! Available:");
            foreach (ICommand command in AllCommands)
            {
                stringBuilder.AppendLine(command.Aliases is { Length: > 0 }
                    ? $"{command.Command} | Aliases: {string.Join(", ", command.Aliases)}"
                    : command.Command);
            }

            response = StringBuilderPool.Shared.ToStringReturn(stringBuilder).TrimEnd();
            return false;
        }
    }
}