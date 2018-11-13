using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.ServerMods.WorldEdit;

namespace Vintagestory.HeightmapImporter
{
    public static class RegisterUtil
    {
        public static void RegisterTool(ModSystem mod)
        {
            ((WorldEdit)mod).RegisterTool("HeightmapImporter", typeof(HeightmapImporterTool));
        }
    }

    public class HeightmapImporterTool : ToolBase
    {
        int[,] heights;

        public string Filename
        {
            get { return workspace.StringValues["std.heightmapImportToolFilename"]; }
            set { workspace.StringValues["std.heightmapImportToolFilename"] = value; }
        }

        public HeightmapImporterTool(WorldEditWorkspace workspace, IBlockAccessorRevertable blockAccess) : base(workspace, blockAccess)
        {
            if (!workspace.StringValues.ContainsKey("std.heightmapImportToolFilename")) Filename = null;
        }


        public override bool OnWorldEditCommand(WorldEdit worldEdit, CmdArgs args)
        {
            switch (args[0])
            {

                case "ims":
                    if (args.Length <= 1)
                    {
                        worldEdit.Bad("Please specify a filename");
                        return true;
                    }

                    TryLoadHeightMap(worldEdit, args[1]);
                    return true;

            }

            return false;
        }


        public override bool ApplyToolBuild(WorldEdit worldEdit, Block block/*, BlockPos pos*/, ushort oldBlockId,/* BlockFacing onBlockFace, Vec3f hitPos, bool didOffset*/ BlockSelection blockSel, BlockPos targetPos, ItemStack withItemStack)
        {
            if (heights == null) return false;

            worldEdit.Good("Ok, placing blocks, this may take a while");

            int width = heights.GetLength(0);
            int length = heights.GetLength(1);
            BlockPos tmpPos = new BlockPos();

            IBlockAccessor blockAccessorBulk = worldEdit.sapi.World.BulkBlockAccessor;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    int height = heights[x, z];

                    tmpPos.Set(targetPos.X + x - width / 2, targetPos.Y, targetPos.Z + z - length / 2);
                    
                    for (int y = 0; y < height; y++)
                    {
                        blockAccessorBulk.SetBlock(block.BlockId, tmpPos);
                        tmpPos.Up();
                    }
                }
            }

            blockAccessorBulk.Commit();

            return false;
        }


        public void TryLoadHeightMap(WorldEdit worldEdit, string filename)
        {
            string folderPath = worldEdit.sapi.GetOrCreateDataPath("Heightmaps");
            string filePath = Path.Combine(folderPath, filename);

            if (!File.Exists(filePath))
            {
                worldEdit.Bad("No such file found.");
                return;
            }

            Bitmap bmp = null;

            try
            {
                bmp = new Bitmap(filePath);
            } catch (Exception e)
            {
                worldEdit.Bad("Unable to load {0}: {1}", filePath, e);
                return;
            }

            heights = new int[bmp.Width, bmp.Height];

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    heights[x, y] = bmp.GetPixel(x, y).R;
                }
            }

            worldEdit.Good("Ok, loaded {0}x{1} image", bmp.Width, bmp.Height);

            bmp.Dispose();
        }

        public override Vec3i Size
        {
            get { return new Vec3i(1, 1, 1); }
        }

    }
}
