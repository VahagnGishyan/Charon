using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Output;
using Utility;

namespace Game
{
    public static class Process
    {
        static Process() { }

        public static void SetMap(Game.Map map)
        {
            ConsoleMap.SetMap(map);
        }

        public static void SetHero(Game.Hero hero = null)
        {
            if(hero == null)
            {
                Hero = Game.Hero.MakeDefault(ConsoleMap.HeroStartLocation);
            }
            else
            {
                Hero = hero;
            }
            ConsoleMap.SetHero(Hero);
        }

        public static void StartGame() // dont use
        {
        }

        public static void GameOver()
        {
            Hero.Dead = true;
        }

        public static bool IsGameOver()
        {
            return (Hero.Dead);
        }

        public static void MoveHero()
        {
            ConsoleMap.MoveCharector(Hero, Input.Keyboard.LastPassedKey());
        }

        public static void HeroPowerSpace()
        {
            Location loc = Hero.Loc;
        }

        public static void PowerHero()
        {
            Charector charector         = Hero;
            System.ConsoleKeyInfo input = Input.Keyboard.LastPassedKey();

            switch (input.Key)
            {
                case ConsoleKey.B:        Hero.Boom();      break;
                case ConsoleKey.Spacebar: HeroPowerSpace(); break;
                default: break;
            }
        }

        public static bool IsMoveKey()
        {
            var symbol = Input.Keyboard.LastPassedKey().Key;
            return (symbol == ConsoleKey.W || symbol == ConsoleKey.A ||
                    symbol == ConsoleKey.S || symbol == ConsoleKey.D);
        }

        public static bool IsPowerKey()
        {
            var symbol = Input.Keyboard.LastPassedKey().Key;
            return (symbol == ConsoleKey.B || symbol == ConsoleKey.Separator);
        }

        //private static void MTHeroPowerBoom()
        //{
        //    Thread thread = new Thread(Hero.HeroBoom);
        //    thread.Start(Hero);
        //}

        private static void MTHeroPowerFire()
        {
            Thread thread = new Thread(Hero.HeroFire);
            thread.Start(Hero);
        }

        public static void UserPass()
        {
            Input.Keyboard.UserPass();
            if (IsMoveKey())
            {
                if (Input.Keyboard.IsPressedShift())
                {
                    Process.MTHeroPowerFire();
                }
                else
                {
                    Process.MoveHero();
                }
            }
            else if(IsPowerKey())
            {
                Process.PowerHero();
            }
        }



        //////////////////////////////////////////////////////////////////////////////////////
        // Zombies //
        //////////////////////////////////////////////////////////////////////////////////////

        private static List<Zomby> MakeNullZombies(int count)
        {
            List<Zomby> zombies = new List<Zomby>(count);
            for (int index = 0; index < count; ++index)
            {
                zombies.Add(null);
            }
            return zombies;
        }

        //private static List<Zomby> MakeZombies(int count)
        //{
        //    List<Zomby> zombies = new List<Zomby>(count);
        //    for (int index = 0; index < count; ++index)
        //    {
        //        zombies.Add(new Zomby(ConsoleMap.ZombyStartLocation));
        //    }
        //    return zombies;
        //}
        public static void KillZomby(int index)
        {
            Zombies[index].Dead = true;
            ConsoleMap.Write(Zombies[index].Loc, ((char)Output.ConsoleSymbols.Space));
        }

        public static void KillZomby(Location loc)
        {
            for(int index = 0; index < Zombies.Count; ++index)
            {
                if(Location.IsEqual(Zombies[index].Loc, loc))
                {
                    KillZomby(index);
                    break;
                }
            }
        }

        public static void MoveZomby(Zomby zomby, Location loc)
        {
            ConsoleMap.MoveCharector(zomby, loc);
            //if(zomby.Loc == Hero.Loc)
            //if (zomby.Loc.OrdinateValue.Value == Hero.Loc.OrdinateValue.Value &&
                //zomby.Loc.AbscissaValue.Value == Hero.Loc.AbscissaValue.Value)
            if(Location.IsEqual(zomby.Loc, Hero.Loc))
            {
                Process.GameOver();
                return;
            }
            Thread.Sleep(200);
        }
        public static void MoveZomby(Zomby zomby, Utility.Location.ShiftTo direct)
        {
            MoveZomby(zomby, Location.Shift(zomby.Loc, direct));
        }

        //public static void MoveZomby(int index, Utility.Location.ShiftTo direct)
        //{
        //    MoveZomby(Zombies[index], direct);
        //}


        public static Location RandomDirection(Zomby zomby, Random random)
        {
            List<Location> locations = new List<Location>();
            Location.ShiftTo[] durs = { Location.ShiftTo.Right, Location.ShiftTo.Up,
                                        Location.ShiftTo.Left,  Location.ShiftTo.Down};

            for (int index = 0; index < durs.Length; ++index)
            {
                Location loc = Location.Shift(zomby.Loc, durs[index]);
                if(ConsoleMap.IsHeroCurrentLocation(loc))
                {
                    return (loc);
                }
                if(ConsoleMap.IsMovable(loc))
                {
                    locations.Add(loc);
                }
            }

            if (locations.Count == 0)
            {
                return (null);
            }
            else if (locations.Count == 1)
            {
                return (locations[0]);
            }
            else
            {
                int rInt = random.Next(0, locations.Count);
                return (locations[rInt]);
            }
        }

        //private static Location.ShiftTo MoveZombyRandomDirection(Zomby zomby, Random random) // temp
        //{
        //    List<Location> movableLocations = ConsoleMap.MovableLocations(zomby.Loc);
        //    if (movableLocations == null || movableLocations.Count == 0)
        //    {
        //        return (Location.ShiftTo.Invalid);
        //    }
        //    else if (movableLocations.Count == 1)
        //    {
        //        return (Location.SolveDirection(zomby.Loc, movableLocations[0]));
        //    }
        //    else
        //    {
        //        int rInt = random.Next(0, movableLocations.Count);
        //        return (Location.SolveDirection(zomby.Loc, movableLocations[rInt]));
        //    }
        //}

        public static void MoveZombies()
        {
            Random random = new Random();
            bool isAllDead;
            while (!Process.IsGameOver() && Zombies != null)
            {
                isAllDead = true;
                for (int index = 0; !Process.IsGameOver() && index < Zombies.Count; ++index)
                {
                    if (Zombies[index] == null)
                    {
                        if (ConsoleMap.ZombyStartLocationFree())
                        {
                            Zombies[index] = new Zomby(ConsoleMap.ZombyStartLocation);
                            MoveZomby(Zombies[index],  ConsoleMap.ZombyStartLocation);
                            --index;
                            continue;
                        }
                    }
                    else if (Zombies[index].Dead != true)
                    {
                        isAllDead = false;
                        Location direction = RandomDirection(Zombies[index], random);

                        if (direction == null)
                        {
                            //KillZomby(Zombies, index);
                            continue;
                        }

                        MoveZomby(Zombies[index], direction);
                    }
                }
                if (isAllDead)
                {
                    Zombies = null;
                    Loging.Loger.WriteLineMessage("[Game].Process, in function MoveZombies(), all zombies is dead");
                }
            }
        }

        private static void LetOutZombiesImpl()
        {
            // make zombies
            Zombies = MakeNullZombies(ZombiesMaxCount);
            //Zombies = new List<Zomby>(ZombiesMaxCount);

            // move zombies
            MoveZombies();
        }

        public static void LetOutZombies()
        {
            //LetOutZombiesImpl();
            Loging.Loger.WriteLineMessage("[Game].Process, Run LetOutZombies() in multithreading");
            Thread thread = new Thread(LetOutZombiesImpl);
            thread.Start();
        }

        //////////////////////////////////////////////////////////////////////////////////////

        //static List<Charector>  Charectors { get; set; } // didn't use
        static int              ZombiesMaxCount = 10;
        static List<Zomby>      Zombies    { get; set; }

        static Hero             Hero       { get; set; }
    }
}

//TODO
// CleanCode #
// 
//