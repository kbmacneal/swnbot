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
using swnbot.Classes.Utilities;

namespace swnbot.Commands
{
    
    public class skill_roller : ModuleBase<SocketCommandContext>
    {
        //The Following Code is also found in roll.cs -- I wasn't sure if it should go in a new file or that one
        /*[Command("skillroll")]
        private async Task SkillRollAsync(string skill_name, string stat_name, int mod)
        {
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            string rtn_name = usr.Nickname == null ? usr.Username : usr.Nickname;

            character character = character.get_character(Context.Message.Author.Id);
            if(character == null)
            {
                await ReplyAsync("Create a character first.");
                return;
            }

            var skill = character.skills.FirstOrDefault(e=>e.Name == skill_name);
            if(skill == null)
            {
                await ReplyAsync("Skill invalid");
                return;
            }

            string stat = "";
            if(short_to_long.TryGetValue(stat_name.ToLower(), out stat)) {

            } else {
                await ReplyAsync("Skill invalid");
                return;
            }
            
            List<int> dice_results = roller.RollKeeps(character.skill_roll(skill, stat, mod),2);
            await ReplyAsync(rtn_name + " rolled a " + dice_results.Sum() + " (" + string.Join(", ", dice_results) + ")");
        }

        private static readonly Dictionary<string, string> short_to_long = new Dictionary<string, string> {
            { "str", "strength" },
            { "dex", "dexerity" },
            { "con", "constitution"},
            { "int", "inteligence"},
            { "wis", "wisdom"},
            { "cha", "charism"}
        };
    
        */
    }

}