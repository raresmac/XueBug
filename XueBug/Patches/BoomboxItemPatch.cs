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
    [HarmonyPatch(typeof(BoomboxItem))]
    internal class BoomboxItemPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void BoomboxAudioPatch(ref AudioClip[] ___musicAudios)
        {
            ___musicAudios = new AudioClip[XueBugBase.new_musicAudios.Length];
            for(int i = 0; i < XueBugBase.new_musicAudios.Length; i++)
            {
                ___musicAudios[i] = XueBugBase.new_musicAudios[i];
            }
        }
    }
}