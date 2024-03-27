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
    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("__initializeVariables")]
        [HarmonyPostfix]
        public static void SpringManAudioPatch(ref AudioClip[] ___springNoises)
        {
            ___springNoises = XueBugBase.new_springNoises;
        }
    }
}