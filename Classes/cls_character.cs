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
using JsonFlatFileDataStore;
using Newtonsoft.Json;
using swnbot.Classes;

namespace swnbot.Classes
{

    public class RollToHit
    {
        public string Roll { get; set; }
        public int AttackBonus { get; set; }
        public int StatModifier { get; set; }
        public int SkillModifier { get; set; }
        public string DiceResults { get; set; }
        public int Result { get; set; }

    }

    public class character
    {
        public int ID { get; set; }
        public ulong player_discord_id { get; set; }
        public string name { get; set; }
        public CharacterClass[] Class { get; set; }
        public Backgrounds Background { get; set; }
        public Gender Gender { get; set; }
        public List<Classes.skills> skills { get; set; } = Skill.InitSkills(System.IO.File.ReadAllText("Data/skills.json")).ToList();
        public List<Classes.Foci> foci { get; set; } = Foci.FromJson(System.IO.File.ReadAllText("Data/foci.json")).ToList();
        public string Faction { get; set; }
        public string Homeworld { get; set; }
        public int cur_hp { get; set; }
        public int max_hp { get; set; }
        public int cur_sys_strain { get; set; }
        public int max_sys_strain { get; set; }
        public int permanent_strain { get; set; }
        public int cur_xp { get; set; }
        public int xp_til_next { get; set; }
        public int ac { get; set; }
        public int atk_bonus { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
        public int creds { get; set; }
        public int armor { get; set; } = -1;
        public Weapon[] weapons { get; set; } = new Weapon[0];

        public static List<character> get_character()
        {
            var store = new DataStore("character.json");

            // Get employee collection
            return store.GetCollection<character>().AsQueryable().ToList();
        }

        public static character get_character(int id)
        {
            var store = new DataStore("character.json");

            // Get employee collection
            return store.GetCollection<character>().AsQueryable().FirstOrDefault(e => e.ID == id);
        }

        public static character get_character(string name)
        {
            var store = new DataStore("character.json");

            // Get employee collection
            return store.GetCollection<character>().AsQueryable().FirstOrDefault(e => e.name == name);
        }

        public static character get_character(ulong player_id)
        {
            var store = new DataStore("character.json");

            // Get employee collection
            return store.GetCollection<character>().AsQueryable().FirstOrDefault(e => e.player_discord_id == player_id);
        }

        public static void insert_character(character character)
        {
            var store = new DataStore("character.json");

            // Get employee collection
            store.GetCollection<character>().InsertOneAsync(character);

            store.Dispose();
        }

        public static void update_character(character character)
        {
            var store = new DataStore("character.json");

            store.GetCollection<character>().UpdateOne(e => e.ID == character.ID, character as object);
            store.Dispose();
        }

        public static void update_weapons(character character, Weapon[] weaps)
        {
            var store = new DataStore("character.json");

            // Get employee collection
            var collection = store.GetCollection<character>();

            dynamic source = new ExpandoObject();
            source.Weapons = weaps;
            collection.UpdateOne(e => e.ID == character.ID, source as object);

        }

        public static void delete_character(character character)
        {
            var store = new DataStore("character.json");

            store.GetCollection<character>().DeleteOne(e => e.ID == character.ID);
            store.Dispose();
        }

        private object RollToHit(string stat, string weapon_name, int optional_mod = 0)
        {
            int modifier = 0;
            modifier += optional_mod;
            Classes.RollToHit rh = new Classes.RollToHit();

            Classes.Weapon weap = this.weapons.FirstOrDefault(e => e.Name == weapon_name);
            if (weap == null)
            {
                return null;
            }

            string title = this.name + "rolls to hit";
            if (this == null)
            {
                return null;
            }

            modifier += this.atk_bonus;
            rh.AttackBonus = this.atk_bonus;
            modifier += stat_mod.mod_from_stat_val((int)helpers.GetPropValue(this, weap.Attribute.ToString().ToLower()));
            rh.StatModifier = stat_mod.mod_from_stat_val((int)helpers.GetPropValue(this, weap.Attribute.ToString().ToLower()));
            modifier += (int)this.skills.First(e => e.Name == "Shoot").Level;
            rh.SkillModifier = (int)this.skills.First(e => e.Name == "Shoot").Level;

            rh.Roll = "1d20";
            List<int> rolls = new List<int>();
            rolls = Commands.roller.Roll("1d20");
            rh.DiceResults = "(" + string.Join(", ", rolls) + ")";
            modifier += rolls.Sum();
            rh.Result = modifier;

            return helpers.ObjToEmbed(rh, title);
        }

        private static readonly Dictionary<string, string> short_to_long = new Dictionary<string, string> {
            { "str", "strength" },
            { "dex", "dexerity" },
            { "con", "constitution"},
            { "int", "inteligence"},
            { "wis", "wisdom"},
            { "cha", "charism"}
        };

    }

}