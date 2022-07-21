using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class Content
    {
        //public static Map MakeEmpty()
        //{
        //    Game.Map map = Map.MakeEmpty();

        //    //Process process = new Process();
        //    //process.SetMap(map);
        //    //Location myCharectorLocatin = new Location(new Ordinate(69), new Abscissa(23));
        //    //process.AddHero(Game.Hero.MakeDefault(new Location(new Ordinate(5), new Abscissa(5))));
        //    //process.AddHero(Game.Hero.MakeDefault());

        //    return (map);
        //}

        public static Map Creeper()
        {
            return (Map.MakeCreeper());
            //Game.Map map = Map.MakeSmile();

            //Process process = new Process();
            //process.SetMap(map);
            ////Location myCharectorLocatin = new Location(new Ordinate(69), new Abscissa(23));
            ////process.AddHero(Game.Hero.MakeDefault(new Location(new Ordinate(5), new Abscissa(5))));
            //process.AddHero(Game.Hero.MakeDefault());

            //return (process);
        }
    }
}
