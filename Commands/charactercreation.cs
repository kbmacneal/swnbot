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

namespace swnbot.Commands {
    public class charactercreation : ModuleBase<SocketCommandContext> {
        [Command ("newcharacter")]
        public async Task NewcharacterAsync (string name) {
            Classes.character character = new character ();

            string base_dir = AppDomain.CurrentDomain.BaseDirectory;

            character.name = name;

            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject (character);

            await System.IO.File.WriteAllTextAsync (base_dir + "/Characters/" + name + ".json", serialized);

            RequestOptions opt = new RequestOptions ();
            opt.RetryMode = RetryMode.RetryRatelimit;

            await Context.Channel.SendFileAsync (name + ".json", "Here is your character sheet in json format. You will need to use the sb!uploadcharacter command to perform bulk updates.", false, opt);

        }

        [Command("uploadcharacter")]
        public async Task UploadcharacterAsync()
        {
            if(Context.Message.Attachments.Count == 0)await ReplyAsync("You must attach your json file in order to bulk upload a character");

            Attachment attach = Context.Message.Attachments.ToArray()[0];

            string base_dir = AppDomain.CurrentDomain.BaseDirectory;

            var client = new RestClient(attach.Url);
            RestRequest request = new RestRequest();
            request.Method = Method.GET;
            byte[] response = client.DownloadData(request);
            System.IO.File.WriteAllBytes("temp.json",response);
            
            try
            {
                character character = JsonConvert.DeserializeObject<character>(System.IO.File.ReadAllText("temp.json"));
                if(character == null)
                {
                    System.IO.File.Delete("temp.json");
                    return;
                }
                System.IO.File.Copy("temp.json", base_dir + "/Characters/" + character.name + ".json");
                System.IO.File.Delete("temp.json");
                await ReplyAsync("Character saved");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync("The file submitted was not in the correct specification. Please see an admin");
                System.IO.File.Delete("temp.json");
                throw ex;
            }   
        
            
            
        }
    }
}