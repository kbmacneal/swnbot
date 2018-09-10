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
using swnbot.Classes;

namespace swnbot.Commands {
    public class charactercreation : ModuleBase<SocketCommandContext> {
        [Command ("newcharacter")]
        public async Task NewcharacterAsync (string name) {
            Classes.character character = new character ();

            character.name = name;

            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject (character);

            await System.IO.File.WriteAllTextAsync (name + ".json", serialized);

            RequestOptions opt = new RequestOptions ();
            opt.RetryMode = RetryMode.RetryRatelimit;

            await Context.Channel.SendFileAsync (name + ".json", "Here is your character sheet in json format. You will need to use the sb!uploadcharacter command to perform bulk updates.", false, opt);

        }
    }
}