using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using RestSharp;
using swnbot.Classes;

namespace swnbot.Commands
{
    public class Armor : ModuleBase<SocketCommandContext>
    {
        [Command("changearmor")]
        public async Task ChangearmorAsync(int armor_id)
        {
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            character character = character.get_character(usr.Nickname);

            if (character == null)
            {
                await ReplyAsync("Character not found. Please create one before proceeding.");
                return;
            }
            Classes.Armor selection = Classes.Armor.InitArmor().ToList().FirstOrDefault(e => e.Id == armor_id);

            if (selection == null)
            {
                await ReplyAsync("Armor selection invalid.");
                return;

            }

            character.armor = armor_id;
            if (selection.Ac > character.ac) character.ac = selection.Ac;

            await ReplyAsync("Armor changed");
        }

        [Command("displayarmor")]
        public async Task DisplayarmorAsync()
        {
            await Context.Channel.SendFileAsync("armor.json",null,false,null);
        }

        [Command("displayarmor")]
        public async Task DisplayarmorAsync(int ID)
        {
            Classes.Armor armor = Classes.Armor.InitArmor().ToList().FirstOrDefault(e=>e.Id == ID);

            if(armor == null)
            {
                await ReplyAsync("Selection Invalid.");
                return;
            }

            await Context.Channel.SendMessageAsync("",false,ObjToEmbed(armor,"Name"),null);
        }

        public static Embed ObjToEmbed(object obj, string title_property_name = "")
        {
            string[] properties = obj.GetType().GetProperties().Select(e=>e.Name).ToArray();
            var embed = new EmbedBuilder();

            foreach (var property in properties)
            {                
                embed.WithTitle(GetPropValue(obj,title_property_name).ToString());
                embed.AddInlineField(property,GetPropValue(obj,property));
            }

            return embed.Build();
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}