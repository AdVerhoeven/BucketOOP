using System;
using System.Collections.Generic;
using System.Text;

namespace BucketOOP
{
    class Bucket : Container
    {
        const uint defaultSize = 12;
        const uint minSize = 10;
        #region Constructors
        /// <summary>
        /// Creates a new default bucket.
        /// </summary>
        public Bucket() : base(defaultSize) { }

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="size">The capacity or size of the bucket.</param>
        public Bucket(uint size) 
        {
            /* 
             * Possible replacement, not very user-friendly since you get a bucket of size 10 
             * when you try to create one of size < 10. 
             * : base(Math.Max(minsSize,size)) { }
             */
            if (size >= minSize)
            {
                Size = size;
            }
            else
            {
                throw new ContainerException($"Size cannot be smaller than {minSize}." +
                    $"\n{size} is to small.", ContainerException.ContainerErrorCodes.InvalidSize);
            }
        }
        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="size">The capacity or size of the bucket.</param>
        /// <param name="volume">The content or volume inside the bucket.</param>
        public Bucket(uint size, uint volume) : this(size) => Volume = volume;
        #endregion
        #region Methods
        /// <summary>
        /// Attempts to fill a bucket with the contents of c.
        /// </summary>
        /// <param name="c">The container that you want to pour into the bucket.</param>
        public override void FillWith(Container c)
        {
            if(c is Bucket)
            {
                base.FillWith(c);
            }
            else
            {
                throw new ContainerException($"Can't add contents of {c.GetType()} to a {GetType()}", ContainerException.ContainerErrorCodes.TypeMisMatch);
            }
        }
        #endregion
    }
}
