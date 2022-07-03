using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
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
        public Size(Utility.Height height, Utility.Weight weight)
        {
            HeightValue = height;
            WeightValue = weight;
        }

        public Utility.Height HeightValue { get; set; }
        public Utility.Weight WeightValue { get; set; }
    }
}
