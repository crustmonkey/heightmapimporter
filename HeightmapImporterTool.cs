using System;
using System.Collections.Generic;
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
        public static void RegisterTool(ModBase mod)
        {
            ((WorldEdit)mod).RegisterTool("HeightmapImporter", typeof(HeightmapImporterTool));
        }
    }

    public class HeightmapImporterTool : ToolBase
    {
        public HeightmapImporterTool(WorldEditWorkspace workspace, IBlockAccessorRevertable blockAccess) : base(workspace, blockAccess)
        {
        }

        public override Vec3i Size
        {
            get { return new Vec3i(1, 1, 1); }
        }

    }
}
