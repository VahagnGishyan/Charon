using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public static class Content
    {
        public static Process MakeEmpty()
        {
            Game.Map map = Map.MakeEmpty();

            Process process = new Process();
            process.SetMap(map);
            //Location myCharectorLocatin = new Location(new Ordinate(69), new Abscissa(23));
            //process.AddHero(Game.Hero.MakeDefault(new Location(new Ordinate(5), new Abscissa(5))));
            process.AddHero(Game.Hero.MakeDefault());

            return (process);
        }

        public static Process Smile()
        {
            Game.Map map = Map.MakeSmile();

            Process process = new Process();
            process.SetMap(map);
            //Location myCharectorLocatin = new Location(new Ordinate(69), new Abscissa(23));
            //process.AddHero(Game.Hero.MakeDefault(new Location(new Ordinate(5), new Abscissa(5))));
            process.AddHero(Game.Hero.MakeDefault());

            return (process);
        }
    }
}
