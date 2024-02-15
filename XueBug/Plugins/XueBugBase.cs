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
        private const string modVersion = "0.0.7";

        //HoarderBugAIPatch
        internal static AssetBundle xueHuaBundle, xueHuaOrigBundle;
        internal static AudioClip[] xueHuaSoundFX, xueHuaOrigSoundFX;
        internal static AudioClip[] new_chitterSFX, new_angryScreechSFX;

        //BoomboxItemPatch
        internal static AssetBundle otelulGalatiBundle, rapBattleBundle, mansNotHotBundle, triPoloskiBundle, allStarBundle;
        internal static AudioClip[] otelulGalatiSoundFX, rapBattleSoundFX, mansNotHotSoundFX, triPoloskiSoundFX, allStarSoundFX;
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
            xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\chitterSFX"); // xuehuapiaopiao
            xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\angryScreechSFX"); // xuehuapiaopiao original

            // BoomboxItem
            otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios0"); // otelul galati
            rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios1"); // rap battle bahoi
            mansNotHotBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios2"); // mans not hot
            triPoloskiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios3"); // tri poloski
            allStarBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios4"); // smash mouth - all star

            if (!xueHuaBundle || !xueHuaOrigBundle)
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

            if (!otelulGalatiBundle || !rapBattleBundle || !mansNotHotBundle || !triPoloskiBundle || !allStarBundle)
            {
                this.Logger.LogError("Failed to load otelulGalati asset bundle!");
                return;
            }
            else
            {
                otelulGalatiSoundFX = new AudioClip[1];
                rapBattleSoundFX = new AudioClip[1];
                mansNotHotSoundFX = new AudioClip[1];
                triPoloskiSoundFX = new AudioClip[1];
                allStarSoundFX = new AudioClip[1];

                otelulGalatiSoundFX = otelulGalatiBundle.LoadAllAssets<AudioClip>();
                rapBattleSoundFX = rapBattleBundle.LoadAllAssets<AudioClip>();
                mansNotHotSoundFX = mansNotHotBundle.LoadAllAssets<AudioClip>();
                triPoloskiSoundFX = triPoloskiBundle.LoadAllAssets<AudioClip>();
                allStarSoundFX = allStarBundle.LoadAllAssets<AudioClip>();

                int length_new_musicAudios = 5;
                new_musicAudios = new AudioClip[length_new_musicAudios];
                new_musicAudios[0] = otelulGalatiSoundFX[0];
                new_musicAudios[1] = rapBattleSoundFX[0];
                new_musicAudios[2] = mansNotHotSoundFX[0];
                new_musicAudios[3] = triPoloskiSoundFX[0];
                new_musicAudios[4] = allStarSoundFX[0];
            }
            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }

    }
}
