using System;
using System.Collections.Generic;
using System.Text;

namespace BucketOOP
{
    class ContainerException : Exception
    {
        public enum ContainerErrorCodes
        {
            TypeMisMatch,
            InvalidSize,
            InvalidVolume,
        }

        public ContainerErrorCodes Error { get; set; }

        public ContainerException(string message, ContainerErrorCodes error) : base(message)
        {
            Error = error;
        }
    }
}
