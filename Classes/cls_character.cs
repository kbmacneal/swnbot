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
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
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
    }

}