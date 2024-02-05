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
        private const string modVersion = "0.0.1";
        internal static AssetBundle Bundle;
        internal static AudioClip[] soundFX;


        private readonly Harmony harmony = new Harmony(modGUID);

        void Awake()
        {
            harmony.PatchAll(typeof(XueBugBase));
            harmony.PatchAll(typeof(HoarderBugAIPatch));

            soundFX = new AudioClip[1];
            string folderLocation = this.Info.Location;
            folderLocation = folderLocation.TrimEnd("XueBug.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(folderLocation + "xue");
            
            if(Bundle == null)
            {
                this.Logger.LogError("Failed to load asset bundle!");
                return;
            }
            else
            {
                soundFX = Bundle.LoadAllAssets<AudioClip>();
            }
            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }

    }
}
