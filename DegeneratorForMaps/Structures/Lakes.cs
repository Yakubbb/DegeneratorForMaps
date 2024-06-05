using DegeneratorForMaps.ResourcePacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegeneratorForMaps.Structures 
{
    internal class Lakes : Structure
    {
        public Lakes(int width, int height, int xOffset, int yOffset) : this(width, height, xOffset, yOffset, 10)
        {
        }
        public Lakes(int width, int height, int depth) : this(width, height, 0, 0, depth)
        {
        }
        public Lakes(int width, int height, int xOffset, int yOffset, int depth) : base(width, height, xOffset, yOffset, depth)
        {
            this.GenerateStructure();
        }
        public override IResourcePack textures { get; init; } = new CleanWaterPack();

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

                            if (Enumerable.Range(0, 2).Contains(Random.Shared.Next(0, DistanceValue(i, j))) || surrounded || semi)
                            {
                                if (IsSurroundedWithWalls(i,j) || IsSurroundedWithFloor(i, j))
                                {
                                    block = textures.GetRandomCantGoThroughBlock();
                                }
                                else
                                {
                                    block = textures.GetRandomCanGoThroughBlock();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

