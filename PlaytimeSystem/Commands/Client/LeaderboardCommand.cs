// -----------------------------------------------------------------------
// <copyright file="LeaderboardCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PlaytimeSystem.Commands.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using CommandSystem;
    using NorthwoodLib.Pools;
    using PlaytimeSystem.Models;

    /// <inheritdoc />
    public class LeaderboardCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "leaderboard";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "top", "tpt" };

        /// <inheritdoc />
        public string Description { get; set; } = "Displays the players that have the most playtime";

        /// <summary>
        /// Gets or sets the amount of players to display on the leaderboard.
        /// </summary>
        [Description("The amount of players to display on the leaderboard.")]
        public int DisplayAmount { get; set; } = 10;

        /// <summary>
        /// Gets or sets how to format the playtimes on the leaderboard.
        /// </summary>
        [Description("How to format the playtimes on the leaderboard. Placeholders: {0}:Rank, {1}:Name, {2}:Playtime")]
        public string LeaderboardFormat { get; set; } = "Leaderboard:\n{0}. {1} - {2}";

        /// <summary>
        /// Gets or sets the way that the timespan should be formatted.
        /// </summary>
        [Description("The way that the timespan should be formatted.")]
        public string TimeFormat { get; set; } = "d'd 'h'h 'm'm 's's'";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            IEnumerable<Playtime> playtimes = Plugin.Instance.PlaytimeCollection.GetAll();
            IOrderedEnumerable<Playtime> orderedPlaytimes = playtimes.OrderBy(playtime => playtime.Time);
            IEnumerable<Playtime> topPlaytimes = orderedPlaytimes.Take(DisplayAmount);
            response = FormatLeaderboard(topPlaytimes);
            return true;
        }

        private string FormatLeaderboard(IEnumerable<Playtime> playtimes)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            int i = 1;
            foreach (Playtime playtime in playtimes)
            {
                stringBuilder.AppendLine(string.Format(LeaderboardFormat, i, playtime.Nickname, playtime.ToTimespan().ToString(TimeFormat)));
                i++;
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder).TrimEnd('\n');
        }
    }
}