using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XueBug.Plugins;

namespace XueBug.Patches
{
    [HarmonyPatch(typeof(HoarderBugAI))]
    internal class HoarderBugAIPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void hoarderBugAudioPatch(ref AudioClip[] ___chitterSFX)
        {
            ___chitterSFX = XueBugBase.soundFX;
        }
    }
}