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

    public class roller : ModuleBase<SocketCommandContext>
    {
        public static int singleRoll(String roll)
        {
            int num_dice = 1;
            int result = 0;
            int di = roll.IndexOf('d');
            if (di == -1) return Int32.Parse(roll);
            int diceSize = Int32.Parse(roll.Substring(di + 1)); //value of string after 'd'
            if (di != 0) num_dice = Int32.Parse(roll.Substring(0, di));

            for (int i = 0; i < num_dice; i++)
            {
                result += swnbot.Program.rand.Next(0, diceSize) + 1;
            }
            return result;
        }

        [Command("roll")]
        public async Task RollAsync(params string[] args)
        {
            string roll = string.Join("", args).Replace(" ", "");
            List<int> dice_results = new List<int>();

            DiceBag db = new DiceBag();

            if(roll.Contains('-') || roll.Contains('+'))
            {
                char[] splits = new char[] { '+', '-' };
                string[] two_parts = roll.Split(splits);
                int mod = 0;
                if(roll.Contains('-'))
                {
                    mod = -1* Convert.ToInt32(two_parts[1]);
                }
                else{
                    mod = Convert.ToInt32(two_parts[1]);
                }
                string[] parameters = two_parts[0].Split('d');
                uint num_dice = 1;
                int dice_sides = 2;

                if(parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else{
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity( dice , num_dice );

                dice_results.Add(mod);
            }
            else{
                string[] parameters = roll.Split('d');
                uint num_dice = 1;
                int dice_sides = 2;

                if(parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else{
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity( dice , num_dice );
                
            }
            

            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id) as SocketGuildUser;

            string rtn_name = usr.Nickname == null ? usr.Username : usr.Nickname;

            await ReplyAsync(rtn_name + " rolled a " + dice_results.Sum() + " (" + string.Join(", ",dice_results) + ")");
        }
    }

}