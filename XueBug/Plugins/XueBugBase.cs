using BepInEx;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XueBug.Patches;
using System.IO;

namespace XueBug.Plugins
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class XueBugBase : BaseUnityPlugin
    {
        private const string modGUID = "HuaPiaoPiao.XueBug";
        private const string modName = "XueBug";
        private const string modVersion = "0.1.5";

        // HoarderBugAIPatch
        internal static AssetBundle xueHuaBundle, xueHuaOrigBundle;
        internal static AudioClip[] xueHuaSoundFX, xueHuaOrigSoundFX;
        internal static AudioClip[] new_chitterSFX, new_angryScreechSFX;

        // BoomboxItemPatch
        internal static AssetBundle otelulGalatiBundle, rapBattleBundle, mansNotHotBundle, triPoloskiBundle, allStarBundle, broMomentoBundle;
        internal static AudioClip[] otelulGalatiSoundFX, rapBattleSoundFX, mansNotHotSoundFX, triPoloskiSoundFX, allStarSoundFX, broMomentoSoundFX;
        internal static AudioClip[] new_musicAudios;

        // PufferAIPatch
        internal static AssetBundle beatboxBundle, beatboxTusindBundle;
        internal static AudioClip[] beatboxSoundFX, beatboxTusindSoundFX;
        internal static AudioClip[] new_frightenSFX;
        internal static AudioClip new_puff;

        // SpringManAIPatch
        internal static AssetBundle bingChillingBundle;
        internal static AudioClip[] bingChillingSoundFX;
        internal static AudioClip[] new_springNoises;

        private readonly Harmony harmony = new Harmony(modGUID);

        void Awake()
        {
            harmony.PatchAll(typeof(XueBugBase));
            harmony.PatchAll(typeof(HoarderBugAIPatch));
            harmony.PatchAll(typeof(BoomboxItemPatch));
            harmony.PatchAll(typeof(PufferAIPatch));
            harmony.PatchAll(typeof(SpringManAIPatch));
            string thisLocation = this.Info.Location;
            thisLocation = thisLocation.TrimEnd("XueBug.dll".ToCharArray());

            // ASCII Art
            {
                this.Logger.LogInfo("...............................         .. .....-===---------.........         ..");
                this.Logger.LogInfo("...............................         .....-============---=-......          ..");
                this.Logger.LogInfo("...............................         ...-+++++===============-.....         ..");
                this.Logger.LogInfo("................................. .. ....:=+++++++++++++==========:...         ..");
                this.Logger.LogInfo("................................... ....-++++++++++++++++++========-..         ..");
                this.Logger.LogInfo(".......................................-++++++++++++++++++++========-...       ..");
                this.Logger.LogInfo("......................................-===+++++++++++++++++==========:.....    ..");
                this.Logger.LogInfo(".....................................-============+==================-.....    ..");
                this.Logger.LogInfo("....................................-=================================:......  ..");
                this.Logger.LogInfo("...................................:==================================-....... ..");
                this.Logger.LogInfo("...................................====================================..... . ..");
                this.Logger.LogInfo(".................................::====================================:.... . ..");
                this.Logger.LogInfo("............::..................-=-= *####*+============================:.... ...");
                this.Logger.LogInfo("................................:=+*###****++==========================-....   ..");
                this.Logger.LogInfo("........:.......................-======++++++======+++***####*+========-...... ..");
                this.Logger.LogInfo("..............:::..............:=====++++++=++=====+++=++++**#*+===---=-.:--.....");
                this.Logger.LogInfo(".......................:.......:-=== +#%%%##*+++===+++++++=========-----====-....");
                this.Logger.LogInfo("...............................:-=====+++++++++===+++*##***+==--==-----====:.....");
                this.Logger.LogInfo("...............................-=======++++++======+++++*#####*==------=++=:.....");
                this.Logger.LogInfo("...............................-=====================+++++======-------=++=......");
                this.Logger.LogInfo("..::::.........................:=================================------=+=:... ..");
                this.Logger.LogInfo("...............................:-=======++========================-----=-:.... ..");
                this.Logger.LogInfo("...............................:-======+===========+++============------:........");
                this.Logger.LogInfo("....................::.........:-====+++==+=========+++================--::......");
                this.Logger.LogInfo("................................-====+++++**++++**==++++==============--=-:-:....");
                this.Logger.LogInfo("................................-===++++++++******++++++==============---=--:::..");
                this.Logger.LogInfo("................................:===+++++++++*++++++++++===============------::::");
                this.Logger.LogInfo(".................................-==++=++++++++++++++++++===============----:::::");
                this.Logger.LogInfo("..................................-=+++*###**++**++++++++++=======++===----------");
                this.Logger.LogInfo("..................................-===++**#####%##%##*++++++++=+++++===--------::");
                this.Logger.LogInfo(".................................:++===+++***********+++++++++++**+========--::::");
                this.Logger.LogInfo(".................................=**+===+++****+++++++++++++++*#++========-------");
                this.Logger.LogInfo("................................:+*#*==+++******++++++++++++***++==========------");
                this.Logger.LogInfo("..............................::=+**#%*=+++*****++++++++++***++++=++======-------");
                this.Logger.LogInfo("..............................=+++*###@*++++****+++++++***+++++++=========-------");
                this.Logger.LogInfo("...............................--= *#+=*@%*+++++++++++**++++++++++=========------");
                this.Logger.LogInfo("...............................--=+*===+====++++*****+++++++++++=========--------");
                this.Logger.LogInfo("..............................:-== +#======++++*****++++++++++++==========-------");
                this.Logger.LogInfo(".............................:--==+*+===++++++*+++===++++++==============--------");
                this.Logger.LogInfo(".............................:-===+*======+++++===========================-------");
                this.Logger.LogInfo("............................:--===+=====++================================-------");
                this.Logger.LogInfo("............................---===**+=====================================-------");
                this.Logger.LogInfo("..........................:----==+++-=====================================-------");
                this.Logger.LogInfo("::::::::::::::...........:======++*++=++======================================== ");
            }

            // HoarderBugAI
            if (Directory.Exists(thisLocation + "HoarderBugAI"))
            {
                // Manual mode
                xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\chitterSFX");          // xuehuapiaopiao
                xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\angryScreechSFX"); // xuehuapiaopiao original
            }
            else
            {
                // Thunderstore Mod Manager workaround
                xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "chitterSFX");
                xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "angryScreechSFX");
            }

            // BoomboxItem
            if (Directory.Exists(thisLocation + "BoomboxItem"))
            {
                // Manual mode
                otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios0"); // otelul galati
                rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios1");    // rap battle bahoi
                mansNotHotBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios2");   // mans not hot
                triPoloskiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios3");   // tri poloski
                allStarBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios4");      // smash mouth - all star
                broMomentoBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios5");   // bro momento
            }
            else
            {
                // Thunderstore Mod Manager workaround
                otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios0");
                rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios1");
                mansNotHotBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios2");
                triPoloskiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios3");
                allStarBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios4");
                broMomentoBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios5");
            }
                
            // PufferAI
            if (Directory.Exists(thisLocation + "PufferAI"))
            {
                // Manual mode
                beatboxBundle = AssetBundle.LoadFromFile(thisLocation + "PufferAI\\frightenSFX"); // beatbox
                beatboxTusindBundle = AssetBundle.LoadFromFile(thisLocation + "PufferAI\\puff");  // beatbox tusind
            }
            else
            {
                // Thunderstore Mod Manager workaround
                beatboxBundle = AssetBundle.LoadFromFile(thisLocation + "frightenSFX");
                beatboxTusindBundle = AssetBundle.LoadFromFile(thisLocation + "puff");
            }

            // SpringManAI
            if (Directory.Exists(thisLocation + "SpringManAI"))
            {
                // Manual mode
                bingChillingBundle = AssetBundle.LoadFromFile(thisLocation + "SpringManAI\\springNoises"); // bing chilling
            }
            else
            {
                // Thunderstore Mod Manager workaround
                bingChillingBundle = AssetBundle.LoadFromFile(thisLocation + "springNoises");
            }

            if (!xueHuaBundle || !xueHuaOrigBundle)
            {
                this.Logger.LogError("## Failed to load HoarderBugAI assets!");

                if (!xueHuaBundle)
                {
                    this.Logger.LogError("Failed to load chitterSFX asset bundle!");
                }
                if (!xueHuaOrigBundle)
                {
                    this.Logger.LogError("Failed to load angryScreechSFX asset bundle!");
                }
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

                this.Logger.LogInfo("HoarderBugAI sounds were loaded succesfully!");
            }

            if (!otelulGalatiBundle || !rapBattleBundle || !mansNotHotBundle || !triPoloskiBundle || !allStarBundle || !broMomentoBundle)
            {
                this.Logger.LogError("## Failed to load BoomboxItem assets!");

                if(!otelulGalatiBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios0 asset bundle!");
                }
                if(!rapBattleBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios1 asset bundle!");
                }
                if(!mansNotHotBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios2 asset bundle!");
                }
                if(!triPoloskiBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios3 asset bundle!");
                }
                if(!allStarBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios4 asset bundle!");
                }
                if (!broMomentoBundle)
                {
                    this.Logger.LogError("Failed to load musicAudios5 asset bundle!");
                }
            }
            else
            {
                otelulGalatiSoundFX = new AudioClip[1];
                rapBattleSoundFX = new AudioClip[1];
                mansNotHotSoundFX = new AudioClip[1];
                triPoloskiSoundFX = new AudioClip[1];
                allStarSoundFX = new AudioClip[1];
                broMomentoSoundFX = new AudioClip[1];

                otelulGalatiSoundFX = otelulGalatiBundle.LoadAllAssets<AudioClip>();
                rapBattleSoundFX = rapBattleBundle.LoadAllAssets<AudioClip>();
                mansNotHotSoundFX = mansNotHotBundle.LoadAllAssets<AudioClip>();
                triPoloskiSoundFX = triPoloskiBundle.LoadAllAssets<AudioClip>();
                allStarSoundFX = allStarBundle.LoadAllAssets<AudioClip>();
                broMomentoSoundFX = broMomentoBundle.LoadAllAssets<AudioClip>();

                int length_new_musicAudios = 6;
                new_musicAudios = new AudioClip[length_new_musicAudios];
                new_musicAudios[0] = otelulGalatiSoundFX[0];
                new_musicAudios[1] = rapBattleSoundFX[0];
                new_musicAudios[2] = mansNotHotSoundFX[0];
                new_musicAudios[3] = triPoloskiSoundFX[0];
                new_musicAudios[4] = allStarSoundFX[0];
                new_musicAudios[5] = broMomentoSoundFX[0];

                this.Logger.LogInfo("BoomboxItem sounds were loaded succesfully!");
            }

            if (!beatboxBundle || !beatboxTusindBundle)
            {
                this.Logger.LogError("## Failed to load PufferAI assets!");

                if(!beatboxBundle)
                {
                    this.Logger.LogError("Failed to load frightenSFX asset bundle!");
                }
                if(!beatboxTusindBundle)
                {
                    this.Logger.LogError("Failed to load nervousMumbling asset bundle!");
                }
            }
            else
            {
                beatboxSoundFX = new AudioClip[4];
                beatboxTusindSoundFX = new AudioClip[1];

                beatboxSoundFX = beatboxBundle.LoadAllAssets<AudioClip>();
                beatboxTusindSoundFX = beatboxBundle.LoadAllAssets<AudioClip>();
                int beatboxLength = beatboxSoundFX.Length;

                new_frightenSFX = new AudioClip[beatboxLength];
                for (int i = 0; i < beatboxLength; i++)
                {
                    new_frightenSFX[i] = beatboxSoundFX[i];
                }

                new_puff = beatboxTusindSoundFX[0];

                this.Logger.LogInfo("PufferAI sounds were loaded succesfully!");
            }

            if(!bingChillingBundle)
            {
                this.Logger.LogError("## Failed to load SpringManAI assets!");

                if (!bingChillingBundle)
                {
                    this.Logger.LogError("Failed to load springNoises asset bundle!");
                }
            }
            else
            {
                bingChillingSoundFX = new AudioClip[1];
                bingChillingSoundFX = bingChillingBundle.LoadAllAssets<AudioClip>();

                new_springNoises = new AudioClip[bingChillingSoundFX.Length];
                new_springNoises[0] = bingChillingSoundFX[0];

                this.Logger.LogInfo("SpringManAI sounds were loaded succesfully!");
            }

            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }
    }
}
