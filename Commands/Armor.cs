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
        public async Task ChangearmorAsync(params string[] args)
        {
            string armor_name = string.Join(" ",args);
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            character character = character.get_character(Context.Message.Author.Id);

            if (character == null)
            {
                await ReplyAsync("Character not found. Please create one before proceeding.");
                return;
            }
            Classes.Armor selection = Classes.Armor.InitArmor(System.IO.File.ReadAllText("Data/armor.json")).ToList().FirstOrDefault(e => e.Name == armor_name);

            if (selection == null)
            {
                await ReplyAsync("Armor selection invalid.");
                return;

            }

            character.armor = selection;
            if (selection.Ac > character.ac) character.ac = selection.Ac;

            await ReplyAsync("Armor changed");
        }

        [Command("displayarmor")]
        public async Task DisplayarmorAsync()
        {
            await Context.Channel.SendFileAsync("Data/armor.json",null,false,null);
        }

        [Command("displayarmor")]
        public async Task DisplayarmorAsync(int ID)
        {
            Classes.Armor armor = Classes.Armor.InitArmor(System.IO.File.ReadAllText("Data/armor.json")).ToList().FirstOrDefault(e=>e.Id == ID);

            if(armor == null)
            {
                await ReplyAsync("Selection Invalid.");
                return;
            }

            await Context.Channel.SendMessageAsync("",false,helpers.ObjToEmbed(armor,"Name"),null);
        }

        
    }
}