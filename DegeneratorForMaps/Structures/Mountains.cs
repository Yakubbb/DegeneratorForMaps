using DegeneratorForMaps.ResourcePacks;

namespace DegeneratorForMaps.Structures
{
    public class Mountains : Structure
    {
        public Mountains(int width, int height, int xOffset, int yOffset) : this(width, height, xOffset, yOffset, 10)
        {
        }
        public Mountains(int width, int height, int depth) : this(width, height, 0, 0, depth)
        {

        }
        public Mountains(int width, int height, int xOffset, int yOffset, int depth) : base(width, height, xOffset, yOffset, depth)
        {
            this.GenerateStructure();
        }
        public override IResourcePack textures { get; init; } = new StoneWallsPack();

        protected override void GenerateStructure()
        {
            for (int depthRun = 0; depthRun < Depth; depthRun++)
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        ref char block = ref StructureField[i, j];
                        block = DefaultBlock;
                        if (IsCharClear(block))
                        {

                            var surrounded = IsSurrounded(i, j);
                            var semi = IsSemiSurrounded(i, j);
                            var blocksNearby = AreBlocksNearby(i, j);

                            if (!blocksNearby && depthRun > 2)
                            {
                                continue;
                            }

                            if (Enumerable.Range(0, 3).Contains(Random.Shared.Next(0, DistanceValue(i, j))) || surrounded || semi)
                            {

                                if (textures.CantGoThrough.Count() < 3)
                                {
                                    block = textures.GetRandomCantGoThroughBlock();
                                }
                                if (surrounded)
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 1);
                                }
                                else if (semi)
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 2);
                                }
                                else
                                {
                                    block = textures.CantGoThrough.ElementAt(textures.CantGoThrough.Count() - 3);
                                }
                                //if (i == randomMountainSpot.y && j == randomMountainSpot.x)
                                //{
                                //    block = 'A';
                                //}

                            }
                        }
                    }
                }
            }
        }
    }
}
