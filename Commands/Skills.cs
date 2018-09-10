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

namespace swnbot.Commands
{

    public class skills : ModuleBase<SocketCommandContext>
    {

        [Command("modifyskill")]
        private async Task ModifyskillAsync(string skill_name, int mod)
        {
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);

            character character = character.get_character(usr.Nickname);
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

            foreach (var item in character.skills)
            {
                if(item.Name == skill_name)
                {
                    item.Level = mod;
                }
            }

            Classes.character.update_character(character);

            await ReplyAsync("Skill Updated.");
        }
    }

}