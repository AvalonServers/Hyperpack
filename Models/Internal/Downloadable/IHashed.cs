using System;

namespace Hyperpack.Models.Internal.Downloadable
{
    public interface IHashed
    {
        public long Checksum { get; }
    }
}