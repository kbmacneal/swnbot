using System.Collections.Generic;
using System.Collections;

namespace swnbot.Classes

{
    public class stat_mod
    {
        public int value {get;set;}
        public int mod {get;set;}

        public static List<stat_mod> gen_mods()
        {
            List<stat_mod> rtn = new List<stat_mod>();

            for (int i = 1; i < 19; i++)
            {
                if(i<=3)
                {
                    stat_mod mod = new stat_mod(){
                        value=i,
                        mod = -2
                    };
                    rtn.Add(mod);
                }

                if(i>=4&&i<=7)
                {
                    stat_mod mod = new stat_mod(){
                        value=i,
                        mod = -1
                    };
                    
                    rtn.Add(mod);
                }

                if(i>=8&&i<=13)
                {
                    stat_mod mod = new stat_mod(){
                        value=i,
                        mod = 0
                    };
                    rtn.Add(mod);
                }
                
                if(i>=14&&i<=17)
                {
                    stat_mod mod = new stat_mod(){
                        value=i,
                        mod = 1
                    };
                    rtn.Add(mod);
                }
                
                if(i==18)
                {
                    stat_mod mod = new stat_mod(){
                        value=i,
                        mod = 2
                    };
                    rtn.Add(mod);
                }
            }


            return rtn;
        }
    }

}