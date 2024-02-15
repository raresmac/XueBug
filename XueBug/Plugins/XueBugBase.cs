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
        private const string modVersion = "0.0.4";

        //HoarderBugAIPatch
        internal static AssetBundle xueHuaBundle, xueHuaOrigBundle;
        internal static AudioClip[] xueHuaSoundFX, xueHuaOrigSoundFX;
        internal static AudioClip[] new_chitterSFX, new_angryScreechSFX;

        //BoomboxItemPatch
        internal static AssetBundle otelulGalatiBundle, rapBattleBundle;
        internal static AudioClip[] otelulGalatiSoundFX, rapBattleSoundFX;
        internal static AudioClip[] new_musicAudios;


        private readonly Harmony harmony = new Harmony(modGUID);

        void Awake()
        {
            harmony.PatchAll(typeof(XueBugBase));
            harmony.PatchAll(typeof(HoarderBugAIPatch));
            harmony.PatchAll(typeof(BoomboxItemPatch));
            string thisLocation = this.Info.Location;
            thisLocation = thisLocation.TrimEnd("XueBug.dll".ToCharArray());

            // HoarderBugAI
            xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "chitterSFX"); // xuehuapiaopiao
            xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "angryScreechSFX"); // xuehuapiaopiao original

            // BoomboxItem
            otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios0"); // otelul galati
            rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios1"); // rap battle bahoi

            if (xueHuaBundle == null || xueHuaOrigBundle == null)
            {
                this.Logger.LogError("Failed to load xueHua asset bundle!");
                return;
            }
            else
            {
                xueHuaSoundFX = new AudioClip[1];
                xueHuaOrigSoundFX = new AudioClip[1];

                xueHuaSoundFX = xueHuaBundle.LoadAllAssets<AudioClip>();
                xueHuaOrigSoundFX = xueHuaOrigBundle.LoadAllAssets<AudioClip>();

                new_chitterSFX = new AudioClip[xueHuaSoundFX.Length];
                new_chitterSFX[0] = xueHuaSoundFX[0];

                new_angryScreechSFX = new AudioClip[xueHuaOrigSoundFX.Length];
                new_angryScreechSFX[0] = xueHuaOrigSoundFX[0];
            }

            if (!otelulGalatiBundle || !rapBattleBundle)
            {
                this.Logger.LogError("Failed to load otelulGalati asset bundle!");
                return;
            }
            else
            {
                otelulGalatiSoundFX = new AudioClip[1];
                rapBattleSoundFX = new AudioClip[1];

                otelulGalatiSoundFX = otelulGalatiBundle.LoadAllAssets<AudioClip>();
                rapBattleSoundFX = rapBattleBundle.LoadAllAssets<AudioClip>();

                int length_new_musicAudios = 2;
                new_musicAudios = new AudioClip[length_new_musicAudios];
                new_musicAudios[0] = otelulGalatiSoundFX[0];
                new_musicAudios[1] = rapBattleSoundFX[0];
            }
            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }

    }
}
