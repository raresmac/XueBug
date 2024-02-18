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
    [HarmonyPatch(typeof(PufferAI))]
    internal class PufferAIPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void pufferAudioPatch(ref AudioClip[] ___frightenSFX, ref AudioClip ___puff)
        {
            ___frightenSFX = XueBugBase.new_frightenSFX;
            ___puff = XueBugBase.new_puff;
        }
    }
}