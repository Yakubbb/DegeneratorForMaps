using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegeneratorForMaps.ResourcePacks
{
    public interface IResourcePack
    {
        public HashSet<char> CanGoThrough { get; }
        public HashSet<char> CantGoThrough { get; }
        public char GetRandomCanGoThroughBlock();
        public char GetRandomCantGoThroughBlock();
    }
}
