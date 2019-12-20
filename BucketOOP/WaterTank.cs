using System;
using System.Collections.Generic;
using System.Text;

namespace BucketOOP
{
    class WaterTank : Container
    {
        public enum Sizes
        {
            Small = 80,
            Medium = 120,
            Large = 160,
        }
        public WaterTank() : base((uint)Sizes.Small) { }
        public WaterTank(Sizes size) : base((uint)size) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">The size of the watertank</param>
        /// <param name="volume">The content of the watertank</param>
        public WaterTank(Sizes size, uint volume) : base((uint)size, volume) { }

        public override string ToString()
        {
            return $"Type:\tWaterTank\n" +
                $"Size:\t{(Sizes)(int)Size}\n" +
                $"Volume:\t{Volume}";
        }
    }


}
