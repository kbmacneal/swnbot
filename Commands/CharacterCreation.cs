using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
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
using JsonFlatFileDataStore;

namespace swnbot.Commands
{
    public class CharacterCreation : ModuleBase<SocketCommandContext>
    {
        [Command("newcharacter")]
        public async Task NewcharacterAsync(params string[] args)
        {

            string name = string.Join(" ", args);
            Classes.character character = new character{
                name = name,
                player_discord_id = Context.Message.Author.Id
            };

            character.insert_character(character);

            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(character);

            await System.IO.File.WriteAllTextAsync(name + ".json", serialized);

            RequestOptions opt = new RequestOptions
            {
                RetryMode = RetryMode.RetryRatelimit
            };

            Context.Channel.SendFileAsync(name + ".json", "Here is your character sheet in json format. You will need to use the sb!uploadcharacter command to perform bulk updates.", false, opt).GetAwaiter().GetResult();

        }

        [Command("uploadcharacter")]
        public async Task UploadcharacterAsync()
        {
            if (Context.Message.Attachments.Count == 0) await ReplyAsync("You must attach your json file in order to bulk upload a character.");

            Attachment attach = Context.Message.Attachments.ToArray()[0];

            var client = new RestClient(attach.Url);
            RestRequest request = new RestRequest { Method = Method.GET };
            System.IO.File.WriteAllBytes("temp.json", client.DownloadData(request));

            try
            {
                character character = JsonConvert.DeserializeObject<character>(System.IO.File.ReadAllText("temp.json"));
                if (character == null)
                {
                    System.IO.File.Delete("temp.json");
                    await ReplyAsync("Character file invalid. Contact an Administrator");
                    return;
                }

                character.update_character(character);

                System.IO.File.Delete("temp.json");
                await ReplyAsync("Character saved");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                await ReplyAsync("The file submitted was not in the correct specification. Please see an admin");
                System.IO.File.Delete("temp.json");
            }
        }

        [Command("getcharacter")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetcharacterAsync(params string[] args)
        {
            string name = string.Join(" ",args);
            Classes.character character = character.get_character(name);

            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(character);

            await System.IO.File.WriteAllTextAsync(name + ".json", serialized);

            RequestOptions opt = new RequestOptions
            {
                RetryMode = RetryMode.RetryRatelimit
            };

           Context.Channel.SendFileAsync(name + ".json", "Here is your character sheet in json format. You will need to use the sb!uploadcharacter command to perform bulk updates.", false, opt).GetAwaiter().GetResult();

        }

        [Command("deletecharacter")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DeletecharacterAsync(params string[] args)
        {
            string name = string.Join(" ",args);
            Classes.character character = character.get_character(name);

            Classes.character.delete_character(character);

            await ReplyAsync("Character Deleted.");

        }

        [Command("listcharacters")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ListcharactersAsync()
        {
            List<Classes.character> characters = character.get_character();

            await ReplyAsync(string.Join(System.Environment.NewLine,characters.Select(e=>e.name).ToList()));
        }

        [Command("addweapon")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddweaponAsync(params string[] args)
        {

            string weapon_name = string.Join(" ",args);
            
            Weapon weap = Weapon.FromJson("Data/weapons.json").FirstOrDefault(e=>e.Name == weapon_name);

            if(weap == null)
            {
                await ReplyAsync("Weapon Selection Invalid");
            }

            character character = character.get_character(Context.Message.Author.Id);

            character.weapons.Add(weap);

            Classes.character.update_character(character);

            await ReplyAsync("Weapons Updated");
        }
    }
}
