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
    public class CharacterCreation : ModuleBase<SocketCommandContext>
    {
        [Command("newcharacter")]
        public async Task NewcharacterAsync(params string[] args)
        {

            string name = string.Join(" ", args);
            Classes.character character = new character
            {
                name = name
            };

            character.player_discord_id = Context.Message.Author.Id;

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
    }
}
