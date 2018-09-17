using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;

namespace swnbot.Classes
{
    public class helpers

    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object SetPropValue(object src, string propName, object value)
        {
            src.GetType().GetProperty(propName).SetValue(src,value);

            return src;
        }

        public static Embed ObjToEmbed(object obj, string title_property_name = "")
        {
            string[] properties = obj.GetType().GetProperties().Select(e=>e.Name).ToArray();
            var embed = new EmbedBuilder();

            foreach (var property in properties)
            {                
                embed.WithTitle(helpers.GetPropValue(obj,title_property_name).ToString());
                embed.AddInlineField(property,helpers.GetPropValue(obj,property));
            }

            return embed.Build();
        }
    }

}