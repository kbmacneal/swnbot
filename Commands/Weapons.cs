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
    public class Weapons : ModuleBase<SocketCommandContext>
    {
        private class RollDamage
        {
            public string Roll { get; set; }
            public int OptionalMod { get; set; }
            public int DexMod { get; set; }
            public string DiceResults { get; set; }
            public int Result { get; set; }

        }

        private class RollToHit
        {
            public string Roll { get; set; }
            public int AttackBonus { get; set; }
            public int StatModifier { get; set; }
            public int SkillModifier { get; set; }
            public string DiceResults { get; set; }
            public int Result { get; set; }

        }

        [Command("Attack")]
        private async Task AttackAsync(string weapon, string skill ="Shoot", int optional_mod = 0)
        {
            int modifier = 0;
            RollToHit rh = new RollToHit();
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            character character = Classes.character.get_character(Context.Message.Author.Id);
            Classes.Weapon weap = Classes.Weapon.FromJson("Data/weapons.json").FirstOrDefault(e => e.Name == weapon);

            string title = character.name + "rolls to hit";
            if (character == null)
            {
                await ReplyAsync("User does not have a character, please create one first.");
                return;
            }
            
            //Attack bonus
            modifier += character.atk_bonus;
            rh.AttackBonus = character.atk_bonus;

            //stat modifier
            modifier += stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,weap.Attribute.ToString().ToLower()));
            rh.StatModifier = stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,weap.Attribute.ToString().ToLower()));

            //skill modifier
            modifier += (int)character.skills.First(e => e.Name == skill).Level;
            rh.SkillModifier = (int)character.skills.First(e => e.Name == skill).Level;


            rh.Roll = "1d20";
            List<int> rolls = new List<int>();
            rolls = roller.Roll("1d20");
            rh.DiceResults = "(" + string.Join(", ", rolls) + ")";
            modifier += rolls.Sum();
            rh.Result = modifier;
            await Context.Channel.SendMessageAsync("", false, helpers.ObjToEmbed(rh, title), null);

            //roll damage
            RollDamage rd = new RollDamage();

            modifier += stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,weap.Attribute.ToString().ToLower()));
            rd.DexMod = stat_mod.mod_from_stat_val((int)helpers.GetPropValue(character,weap.Attribute.ToString().ToLower()));

            rolls = new List<int>();

            if (weap == null)
            {
                await ReplyAsync("Weapon selection invalid, please try again.");
            }

            rolls = roller.Roll(weap.Damage);
            rd.Roll = weap.Damage;
            modifier += rolls.Sum();
            modifier += optional_mod;
            rd.OptionalMod = optional_mod;
            rd.Result = modifier;
            rd.DiceResults = "(" + string.Join(", ", rolls) + ")";

            await Context.Channel.SendMessageAsync("", false, helpers.ObjToEmbed(rd, title), null);
        }

        // [Command("rolltohit")]
        // private async Task RolltohitAsync()
        // {
        //     int modifier = 0;
        //     RollToHit rh = new RollToHit();
        //     SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
        //     character character = Classes.character.get_character(Context.Message.Author.Id);

        //     string title = character.name + "rolls to hit";
        //     if (character == null)
        //     {
        //         await ReplyAsync("User does not have a character, please create one first.");
        //         return;
        //     }

        //     modifier += character.atk_bonus;
        //     rh.AttackBonus = character.atk_bonus;
        //     modifier += stat_mod.mod_from_stat_val(character.dexterity);
        //     rh.StatModifier = stat_mod.mod_from_stat_val(character.dexterity);
        //     modifier += (int)character.skills.First(e => e.Name == "Shoot").Level;
        //     rh.SkillModifier = (int)character.skills.First(e => e.Name == "Shoot").Level;

        //     rh.Roll = "1d20";
        //     List<int> rolls = new List<int>();
        //     rolls = roller.Roll("1d20");
        //     rh.DiceResults = "(" + string.Join(", ", rolls) + ")";
        //     modifier += rolls.Sum();
        //     rh.Result = modifier;
        //     await Context.Channel.SendMessageAsync("", false, helpers.ObjToEmbed(rh, title), null);
        // }
    }

}