using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using swnbot.Classes;
using System.Text.RegularExpressions;

namespace swnbot.Roll {

    public class roller : ModuleBase<SocketCommandContext> {
        public static int singleRoll (String roll) {
            int num_dice = 1;
            int result = 0;
            int di = roll.IndexOf ('d');
            if (di == -1) return Int32.Parse (roll);
            int diceSize = Int32.Parse (roll.Substring (di + 1)); //value of string after 'd'
            if (di != 0) num_dice = Int32.Parse (roll.Substring (0, di));

            for (int i = 0; i < num_dice; i++) {
                result += swnbot.Program.rand.Next (0, diceSize) + 1;
            }
            return result;
        }

        [Command ("roll")]
        private async Task RollAsync (params string[] args) {
            int rtn = 0;
            string roll = string.Join ("", args).Replace (" ", "");
            char[] splits = new char[] { '+', '-' };
            string[] parts = roll.Split (splits);
            foreach (String partOfRoll in parts) { //roll each dice specified
                rtn += singleRoll (partOfRoll);
            }

            SocketGuildUser usr = Context.Guild.GetUser (Context.Message.Author.Id) as SocketGuildUser;

            string rtn_name = usr.Nickname == null ? usr.Username : usr.Nickname;

            await ReplyAsync (rtn_name + " rolled a " + rtn.ToString ());
        }

        private static int PrivateRollAsync (params string[] args) {
            int rtn = 0;
            string roll = string.Join ("", args).Replace (" ", "");
            char[] splits = new char[] { '+', '-' };
            string[] parts = roll.Split (splits);
            foreach (String partOfRoll in parts) { //roll each dice specified
                rtn += singleRoll (partOfRoll);
            }

            return rtn;
        }
    }

}