using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Height
    {
        public Height()
        {

        }

        public Height(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
    public class Weight
    {
        public Weight()
        {

        }

        public Weight(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    public class Size
    {
        public Size()
        {

        }
        public Size(Game.Height height, Game.Weight weight)
        {
            HeightValue = height;
            WeightValue = weight;
        }

        public Game.Height HeightValue { get; set; }
        public Game.Weight WeightValue { get; set; }
    }
}
