using System;
using Vintagestory.API.Common;

using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Vintagestory.HeightmapImporter
{
    /// <summary>
    /// Super basic example on how to set blocks in the game
    /// </summary>
    public class HeightmapImporterToolModBase : ModSystem
    {
        ICoreServerAPI api;
        //IModLoader api;

        public override double ExecuteOrder()
        {
            return 1;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            this.api = api;

            if (api.ModLoader.IsModSystemEnabled("Vintagestory.ServerMods.WorldEdit.WorldEdit"))
            {
                RegisterUtil.RegisterTool(api.ModLoader.GetModSystem("Vintagestory.ServerMods.WorldEdit.WorldEdit"));
            }
        }



    }
}