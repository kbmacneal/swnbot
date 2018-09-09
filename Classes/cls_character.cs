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
using System.Dynamic;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using swnbot.Classes;
using JsonFlatFileDataStore;

namespace swnbot.Classes {

    public class skill{
        
    }

    public class character {
        private int ID{get;set;}
        private string name { get; set; }
        private string xp { get; set; }
        private Classes.Class Class { get; set; }
        private Classes.Background Background { get; set; }
        private Classes.Gender Gender { get; set; }
        private List<Classes.skill> skills{get;set;}
        private string Faction { get; set; }
        private string Homeworld { get; set; }
        private int cur_hp { get; set; }
        private int max_hp { get; set; }
        private int cur_sys_strain { get; set; }
        private int max_sys_strain { get; set; }
        private int permanent_strain { get; set; }
        private int cur_xp { get; set; }
        private int xp_til_next { get; set; }
        private int ac { get; set; }
        private int atk_bonus { get; set; }
        private int strength { get; set; }
        private int dexterity { get; set; }
        private int constitution { get; set; }
        private int intelligence { get; set; }
        private int wisdom { get; set; }
        private int charisma { get; set; }
        private int creds { get; set; }

        public character get_character (int id) {
            var store = new DataStore ("character.json");

            // Get employee collection
            return store.GetCollection<character> ().AsQueryable ().FirstOrDefault (e => e.ID == id);
        }

        public void insert_character (character character) {
            var store = new DataStore ("character.json");

            // Get employee collection
            store.GetCollection<character>().InsertOneAsync(character);
        }

        public void update_character (character character) {
            var store = new DataStore ("character.json");

            store.GetCollection<character>().UpdateOne (e => e.ID == character.ID, character);
        }

        public void delete_character (character character) {
            var store = new DataStore ("character.json");

            store.GetCollection<character>().DeleteOne (e => e.ID == character.ID);
        }
    }

}