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

namespace swnbot.Classes {

    public class character {
        private string name{get;set;}
        private string xp{get;set;}
        private Classes.Class Class {get;set;}
        private Classes.Background Background{get;set;}
        private Classes.Gender Gender{get;set;}
        private string Faction{get;set;}
        private string Homeworld{get;set;}
        private int cur_hp{get;set;}
        private int max_hp{get;set;}
        private int cur_sys_strain{get;set;}
        private int max_sys_strain{get;set;}
        private int permanent_strain{get;set;}
        private int cur_xp{get;set;}
        private int xp_til_next{get;set;}
        private int ac {get;set;}
        private int atk_bonus{get;set;}
        private int strength{get;set;}
        private int dexterity{get;set;}
        private int constitution{get;set;}
        private int intelligence{get;set;}
        private int wisdom{get;set;}
        private int charisma{get;set;}
        private int creds{get;set;}
        
    }

}