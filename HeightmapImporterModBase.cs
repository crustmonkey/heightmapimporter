using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Vintagestory.HeightmapImporter
{
    /// <summary>
    /// Super basic example on how to set blocks in the game
    /// </summary>
    public class HeightmapImporterToolModBase : ModBase
    {
        ICoreServerAPI api;

        public override double ExecuteOrder()
        {
            return 1;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            this.api = api;

            if (api.IsModAvailable("Vintagestory.ServerMods.WorldEdit.WorldEdit"))
            {
                RegisterUtil.RegisterTool(api.GetMod("Vintagestory.ServerMods.WorldEdit.WorldEdit"));
            }
        }



    }
}