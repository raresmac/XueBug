using BepInEx;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XueBug.Patches;

namespace XueBug.Plugins
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class XueBugBase : BaseUnityPlugin
    {
        private const string modGUID = "HuaPiaoPiao.XueBug";
        private const string modName = "XueBug";
        private const string modVersion = "0.0.3";

        //HoarderBugAIPatch
        internal static AssetBundle xueHuaBundle, xueHuaOrigBundle;
        internal static AudioClip[] xueHuaSoundFX, xueHuaOrigSoundFX;
        internal static AudioClip[] new_chitterSFX, new_angryScreechSFX;

        //BoomboxItemPatch
        internal static AssetBundle otelulGalatiBundle;
        internal static AudioClip[] otelulGalatiSoundFX;
        internal static AudioClip[] new_musicAudios;


        private readonly Harmony harmony = new Harmony(modGUID);

        void Awake()
        {
            harmony.PatchAll(typeof(XueBugBase));
            harmony.PatchAll(typeof(HoarderBugAIPatch));
            harmony.PatchAll(typeof(BoomboxItemPatch));
            string thisLocation = this.Info.Location;
            thisLocation = thisLocation.TrimEnd("XueBug.dll".ToCharArray());

            // XueHuaPiaoPiao
            xueHuaSoundFX = new AudioClip[1];
            xueHuaOrigSoundFX = new AudioClip[1];
            xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "chitterSFX");
            xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "angryScreechSFX");

            // Otelul Galati
            otelulGalatiSoundFX = new AudioClip[1];
            otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios0");

            if (xueHuaBundle == null || xueHuaOrigBundle == null)
            {
                this.Logger.LogError("Failed to load xueHua asset bundle!");
                return;
            }
            else
            {
                xueHuaSoundFX = xueHuaBundle.LoadAllAssets<AudioClip>();
                xueHuaOrigSoundFX = xueHuaOrigBundle.LoadAllAssets<AudioClip>();

                new_chitterSFX = new AudioClip[xueHuaSoundFX.Length];
                new_chitterSFX[0] = xueHuaSoundFX[0];

                new_angryScreechSFX = new AudioClip[xueHuaOrigSoundFX.Length];
                new_angryScreechSFX[0] = xueHuaOrigSoundFX[0];
            }

            if (otelulGalatiBundle == null)
            {
                this.Logger.LogError("Failed to load otelulGalati asset bundle!");
                return;
            }
            else
            {
                otelulGalatiSoundFX = otelulGalatiBundle.LoadAllAssets<AudioClip>();

                new_musicAudios = new AudioClip[otelulGalatiSoundFX.Length];
                new_musicAudios[0] = otelulGalatiSoundFX[0];
            }
            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }

    }
}
