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

namespace swnbot.Classes {

    public class character {
        public int ID { get; set; }
        public ulong player_discord_id { get; set; }
        public string name { get; set; }
        public string xp { get; set; }
        public Classes.Class Class { get; set; }
        public string Background { get; set; }
        public string Gender { get; set; }
        public List<Classes.skills> skills { get; set; } = Skill.InitSkills(System.IO.File.ReadAllText("Data/skills.json")).ToList();
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
        public int strength { get; set; } //Paramter 'str' when passed in by user?
        public int dexterity { get; set; } //Parameter 'dex' when passed in by user?
        public int constitution { get; set; } //Parameter 'con' when passed in by user?
        public int intelligence { get; set; } //Parameter 'int' when passed in by user?
        public int wisdom { get; set; } //Parameter 'wis' when passed in by user?
        public int charisma { get; set; } //Parameter 'cha' when passed in by user?
        public int creds { get; set; }
        public int armor {get;set;} = -1;

        public static character get_character (int id) {
            var store = new DataStore ("character.json");

            // Get employee collection
            return store.GetCollection<character> ().AsQueryable ().FirstOrDefault (e => e.ID == id);
        }

        public static character get_character (string name) {
            var store = new DataStore ("character.json");

            // Get employee collection
            return store.GetCollection<character> ().AsQueryable ().FirstOrDefault (e => e.name == name);
        }

        public static character get_character (ulong player_id) {
            var store = new DataStore ("character.json");

            // Get employee collection
            return store.GetCollection<character> ().AsQueryable ().FirstOrDefault (e => e.player_discord_id == player_id);
        }

        public static void insert_character (character character) {
            var store = new DataStore ("character.json");

            // Get employee collection
            store.GetCollection<character> ().InsertOneAsync (character);

            store.Dispose();
        }

        public static void update_character (character character) {
            var store = new DataStore ("character.json");

            store.GetCollection<character> ().UpdateOne (e => e.ID == character.ID, character);
            store.Dispose();
        }

        public static void delete_character (character character) {
            var store = new DataStore ("character.json");

            store.GetCollection<character> ().DeleteOne (e => e.ID == character.ID);
            store.Dispose();
        }

        public string skill_roll(string skillname, string stat, int optional_mod = 0) {
            int totalMod = mod; //Helping Modifier
            string dieRoll = "";

            totalMod += 0; stat_mod.mod_from_stat_val(Classes.helpers.GetPropValue(this,short_to_long[stat]));

            skill s = character.skills.First(e=>e.name == skillname);
            totalMod += s.Level;
            dieRoll += (2+s.Specialist).toString() + "d6k2+" + totalMod.toString();

            return dieRoll; //This roll should be what needs to be executed by the system (2-4)d6k2+totalMod
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