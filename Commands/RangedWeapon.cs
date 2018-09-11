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

    public class RangedWeapons : ModuleBase<SocketCommandContext>
    {
        [Command("rolldamage")]
        private async Task RolldamageAsync(string weapon, int optional_mod = 0)
        {
            int modifier = 0;
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            character character = Classes.character.get_character(usr.Nickname);
            if (character == null)
            {
                await ReplyAsync("User does not have a character, please create one first.");
                return;
            }

            modifier += character.dexterity;

            List<int> rolls = new List<int>();

            Classes.RangedWeapons weap = Classes.RangedWeapons.FromJson("Data/ranged_weapons.json").FirstOrDefault(e => e.Name == weapon);

            if (weap == null)
            {
                await ReplyAsync("Weapon selection invalid, please try again.");
            }

            rolls = roller.Roll(weap.Damage);
            modifier += rolls.Sum();
            modifier += optional_mod;

            await ReplyAsync(usr.Nickname + " rolled a " + modifier + " (" + string.Join(", ", rolls) + ")");
        }

        [Command("rolltohit")]
        private async Task RolltohitAsync()
        {
            int modifier = 0;
            SocketGuildUser usr = Context.Guild.GetUser(Context.Message.Author.Id);
            character character = Classes.character.get_character(usr.Nickname);
            if (character == null)
            {
                await ReplyAsync("User does not have a character, please create one first.");
                return;
            }

            modifier += character.atk_bonus;
            modifier += character.dexterity;
            modifier += (int)character.skills.First(e => e.Name == "Shoot").Level;

            List<int> rolls = new List<int>();
            rolls = roller.Roll("1d20");
            modifier += rolls.Sum();

            await ReplyAsync(usr.Nickname + " rolled a " + modifier + " (" + string.Join(", ", rolls) + ")");
        }
    }

}

// To make an attack roll, the assailant rolls 1d20 and
// adds their attack bonus, their applicable skill level, and
// the attribute modifier most relevant to the weapon.
// If the total is equal or higher than the target’s Armor
// Class, the attack is successful.
// A PC’s attack bonus is usually equal to half their
// character level, rounded down. Characters with the
// Warrior class or an Adventurer with the Partial War-
// rior class option have higher base attack bonuses. NPCs
// have their own attack bonus listed with their statistics,
// which includes any modifiers they might have.