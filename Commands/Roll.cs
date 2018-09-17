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
        [Command("roll")]
        private async Task RollAsync(params string[] args)
        {
            string roll = string.Join("", args).Replace(" ", "");
            List<int> dice_results = Roll(roll);

            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id) as SocketGuildUser;

            string rtn_name = usr.Nickname == null ? usr.Username : usr.Nickname;

            await ReplyAsync(rtn_name + " rolled a " + dice_results.Sum() + " (" + string.Join(", ", dice_results) + ")");

        }

        [Command("skillroll")]
        private async Task SkillRollAsync(string skill_name, string stat_name, int mod = 0)
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
            if(short_to_long.TryGetValue(stat_name.ToLower(), out stat)) 
            {
            } else {
                await ReplyAsync("Stat invalid");
                return;
            }

            List<int> dice_results = new List<int>();

            if(character.skills.First(e=>e.Name==skill_name).Specialist > 0)
            {
                int num_die = 2+character.skills.First(e=>e.Name==skill_name).Specialist;
                string diceroll = num_die.ToString() + "d6";
                int modifier = stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,stat)) + mod;
                
                dice_results = RollKeeps(diceroll + "+" + modifier.ToString(),2);
            }
            else
            {
                string diceroll = "2d6+" + (stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,stat)) + mod).ToString();

                dice_results = Roll(diceroll);
            }
            
            
            await ReplyAsync(rtn_name + " rolled a " + dice_results.Sum() + " (" + string.Join(", ", dice_results) + ")");
        }

        [Command("attackranged")]
        private async Task AttackrangedAsync(string weapon_name, int mod = 0) 
        {
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            string rtn_name = usr.Nickname == null ? usr.Username : usr.Nickname;

            character character = character.get_character(Context.Message.Author.Id);
            if(character == null)
            {
                await ReplyAsync("Create a character first.");
                return;
            }

            Weapon weap = character.weapons.FirstOrDefault(e=>e.Name == weapon_name);

            if(weap == null)
            {
                await ReplyAsync("Weapon selection invalid.");
            }

            List<int> attack_results = new List<int>();
            List<int> damage_results = new List<int>();
        
            Embed hit = (Embed)character.RollToHit(weap,mod);

            Embed dmg = (Embed)weap.RollRangedDamage(character,mod);        

            await Context.Channel.SendMessageAsync("",false,hit,null);
            await Context.Channel.SendMessageAsync("",false,dmg,null);
        }

        private static readonly Dictionary<string, string> short_to_long = new Dictionary<string, string> 
        {
            { "str", "strength" },
            { "dex", "dexerity" },
            { "con", "constitution"},
            { "int", "inteligence"},
            { "wis", "wisdom"},
            { "cha", "charism"}
        };

        public static List<int> RollKeeps(string base_roll, int keep_int)
        {            
            return Roll(base_roll).OrderBy(x=>x).Take(keep_int).ToList();
        }
        
        public static List<int> Roll(string roll)
        {
            List<int> dice_results = new List<int>();

            DiceBag db = new DiceBag();
            char[] dice_splits = {'d','D'};
            if (roll.Contains('-') || roll.Contains('+'))
            {
                char[] splits = new char[] { '+', '-' };
                string[] two_parts = roll.Split(splits);
                int mod = 0;
                if (roll.Contains('-'))
                {
                    mod = -1 * Convert.ToInt32(two_parts[1]);
                }
                else
                {
                    mod = Convert.ToInt32(two_parts[1]);
                }
                string[] parameters = two_parts[0].Split(dice_splits);
                uint num_dice = 1;
                int dice_sides = 2;

                if (parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else
                {
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity(dice, num_dice);

                dice_results.Add(mod);
            }
            else
            {
                string[] parameters = roll.Split(dice_splits);
                uint num_dice = 1;
                int dice_sides = 2;

                if (parameters[0] != "")
                {
                    num_dice = Convert.ToUInt32(parameters[0]);
                    dice_sides = Convert.ToInt32(parameters[1]);
                }
                else
                {
                    dice_sides = Convert.ToInt32(parameters[1]);
                }

                DiceBag.Dice dice = (DiceBag.Dice)System.Enum.Parse(typeof(DiceBag.Dice), dice_sides.ToString());

                dice_results = db.RollQuantity(dice, num_dice);

            }

            return dice_results;

        }
    }

}