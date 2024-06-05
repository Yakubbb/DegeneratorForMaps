using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegeneratorForMaps.ResourcePacks
{
    internal class CleanWaterPack : IResourcePack
    {
        public HashSet<char> CanGoThrough { get; set; } = new() { '~' };

        public HashSet<char> CantGoThrough { get; set; } = new() { 'D' };

        public char GetRandomCanGoThroughBlock() => CanGoThrough.ElementAt(Random.Shared.Next(CanGoThrough.Count));

        public char GetRandomCantGoThroughBlock() => CantGoThrough.ElementAt(Random.Shared.Next(CantGoThrough.Count));
    }
}
