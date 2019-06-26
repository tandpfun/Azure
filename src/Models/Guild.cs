﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Models
{
    public class Guild
    {
        public readonly ulong ID;
        public string Prefix;
        public int ModlogCount;
        public List<int> Tickets;
        public List<GuildUser> GuildUsers;
        public int FilterLevel; // Filter strength of Vision API. 0 = none, 1 = allow lewd stuff, 2 = allow mildy lewd stuff, 3 = keep it clean
        public int WarnsForAction; // How many warns until the bot takes action; Default is 0 (doesn't take any action)
        public bool BanForWarns; // Defaults to false; False kicks the user, True bans the user

        public Guild(SocketGuild guild)
        {
            ID = guild.Id;
            Prefix = "//";
            ModlogCount = 0;
            Tickets = new List<int>();
            foreach (var user in guild.Users) {
                GuildUser guildUser = new GuildUser(user);
                GuildUsers.Add(guildUser);
            }
            FilterLevel = 0;
            WarnsForAction = 0;
            BanForWarns = false;
        }

        public Guild ChangePrefix(string Prefix)
        {
            this.Prefix = Prefix;
            return this;
        }

        public Guild IncreaseModlogCount()
        {
            ModlogCount++;
            return this;
        }

        public Guild AddTicket(int TicketID)
        {
            Tickets.Add(TicketID);
            return this;
        }

        public Guild SetGuildUser(GuildUser guildUser)
        {
            var user = GuildUsers.Where(x => x.ID == guildUser.ID).First();
            GuildUsers.Remove(user);
            GuildUsers.Add(guildUser);
            return this;
        }

        public Guild AddGuildUser(SocketUser user)
        {
            GuildUsers.Add(new GuildUser(user));
            return this;
        }

        public Guild RemoveGuildUser(GuildUser user)
        {
            if (GuildUsers.Where(x => x.ID == user.ID).Count() != 1)
                throw new Exception("User not found or multiple users with the same ID");
            GuildUsers.Remove(user);
            return this;
        }

        public Guild SetFilterLevel(int level)
        {
            FilterLevel = level;
            return this;
        }

        public Guild SetWarnLimit(int limit)
        {
            WarnsForAction = limit;
            return this;
        }

        public Guild SetActionForWarns(bool action)
        {
            BanForWarns = action;
            return this;
        }
    }
}
