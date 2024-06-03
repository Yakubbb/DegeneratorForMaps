namespace DegeneratorForMaps.ResourcePacks
{
    internal class StoneWallsPack : IResourcePack
    {
        public HashSet<char> CanGoThrough { get; init; } = new();
        public HashSet<char> CantGoThrough { get; init; } = new() { '░', '▒', '▓' };

        public char GetRandomCanGoThroughBlock() => CantGoThrough.ElementAt(Random.Shared.Next(CanGoThrough.Count));

        public char GetRandomCantGoThroughBlock() => CantGoThrough.ElementAt(Random.Shared.Next(CantGoThrough.Count));
    }
}

