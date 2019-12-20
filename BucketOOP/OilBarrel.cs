using System;
using System.Collections.Generic;
using System.Text;

namespace BucketOOP
{
    class OilBarrel : Container
    {
        const uint defaultSize = 159;
        public OilBarrel() : base(defaultSize) { }
        public OilBarrel(uint volume) : this() => Volume = volume;
    }
}
