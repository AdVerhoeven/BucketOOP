using System;
using System.Collections.Generic;
using System.Text;

namespace BucketOOP
{
    abstract class Container
    {
        #region Properties
        #region Private
        private uint volume = 0;
        #endregion

        #region Public
        public bool IsFull => (volume == Size);
        public bool IsEmpty => (volume == 0);
        public uint Remainder => Size - volume;
        public uint Size { get; protected set; }
        public uint Volume
        {
            get => volume;
            set
            {
                if (value >= Size)
                {
                    //Calculate the overflow.
                    uint overflow = value - Size;                    
                    volume = Size;
                    Full?.Invoke(this, new ContainerEventArgs(overflow));
                }
                else
                {
                    volume = value;
                }
            }
        }
        public bool WarnWhenFull = false;
        public bool AcceptOverflow = true;
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// A default constructor for all derived classes.
        /// </summary>
        public Container() { }

        /// <summary>
        /// Creates a container object.
        /// </summary>
        /// <param name="size">The size or capacity of the container object.</param>
        public Container(uint size) { Size = size; }

        /// <summary>
        /// Create a container object.
        /// </summary>
        /// <param name="size">The size or capacity of the container object.</param>
        /// <param name="volume">The volume or content of the container object.</param>
        public Container(uint size, uint volume) : this(size)
        {
            Volume += volume;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Empties a container
        /// </summary>
        public void Empty()
        {
            volume = 0;
        }

        /// <summary>
        /// Empties a container with a given amount.
        /// </summary>
        /// <param name="x">The amount to remove from the container</param>
        public void Empty(uint x)
        {
            if (volume >= x)
            {
                volume -= x;
            }
            else
            {
                //Best practice??
                Empty();

                //throw new InvalidOperationException($"Cannot subtract {x} from {volume}. " +
                //    $"Would result in a negative volume.");
            }
        }

        /// <summary>
        /// Fills a container with a given amount.
        /// </summary>
        /// <param name="x">The amount to fill the container with.</param>
        public virtual void Fill(uint x)
        {
            Volume += x;
        }

        /// <summary>
        /// Fills one bucket with the contents of the other.
        /// </summary>
        /// <param name="b"></param>
        public virtual void FillWith(Container c)
        {
            //Old code, I made it virtual to allow the override to determine wether you can fill one container
            //with the other if they do not have matching types.
            //if (GetType() != c.GetType())
            //{
            //    throw new ContainerException($"Can't add contents of {c.GetType()} to a {GetType()}",
            //        ContainerException.ContainerErrorCodes.TypeMisMatch);
            //}
            if (!AcceptOverflow)
            {
                //Since we do not accept overflow, we keep some contents in the other bucket.
                uint totalVol = c.Volume + this.Volume;
                //Use tenary operator to make sure we show an overflow of 0
                //This method puts any overflow back into its old container so there is no overflow.
                Volume = (totalVol > this.Size) ? Size : Volume + c.Volume;
                if (totalVol > this.Size)
                {
                    //pour back the overflow into its old bucket.
                    c.Volume = totalVol - this.Size;
                }
            }
            else
            {
                Volume += c.Volume;
                //Empty the bucket we just poured into our bucket.
                c.Volume = 0;
            }
            
        }

        /// <summary>
        /// A string representation of a Container object
        /// </summary>
        /// <returns>Type, Size and Volume</returns>
        public override string ToString()
        {
            return $"Type:\t{GetType().Name}\nSize:\t{Size}\nVolume:\t{Volume}";
        }
        #endregion

        #region Events
        public delegate void ContainerEventHandler(object sender, ContainerEventArgs e);
        public event ContainerEventHandler Full;
        #endregion
    }
    #region ContainerEventArgs Class
    /// <summary>
    /// Makes sure the Full event returns any overflow.
    /// </summary>
    public class ContainerEventArgs : System.EventArgs
    {
        public ContainerEventArgs() { }
        public ContainerEventArgs(uint overflow)
        {
            Overflow = overflow;
        }
        public uint Overflow { get; } = 0;
    }
    #endregion
}
