using BepInEx;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XueBug.Patches;
using XueBug.Configuration;
using System.IO;
using BepInEx.Configuration;

namespace XueBug.Plugins
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class XueBugBase : BaseUnityPlugin
    {
        private const string modGUID = "HuaPiaoPiao.XueBug";
        private const string modName = "XueBug";
        private const string modVersion = "0.2.0";

        // Config file
        public static Config MyConfig { get; internal set; }

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
            MyConfig = new Config(base.Config);

            harmony.PatchAll(typeof(XueBugBase));
            harmony.PatchAll(typeof(HoarderBugAIPatch));
            harmony.PatchAll(typeof(BoomboxItemPatch));
            harmony.PatchAll(typeof(PufferAIPatch));
            harmony.PatchAll(typeof(SpringManAIPatch));
            string thisLocation = this.Info.Location;
            thisLocation = thisLocation.TrimEnd("XueBug.dll".ToCharArray());

            // ASCII Art
            {
                if (MyConfig.configDisplayASCII.Value)
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
            }

            // HoarderBugAI
            {
                if (Directory.Exists(thisLocation + "HoarderBugAI"))
                {
                    // Manual mode
                    if (MyConfig.configChitter.Value)
                    {
                        xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\chitterSFX");          // xuehuapiaopiao
                    }
                    if (MyConfig.configAngryScreech.Value)
                    {
                        xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "HoarderBugAI\\angryScreechSFX"); // xuehuapiaopiao original
                    }
                }
                else
                {
                    // Thunderstore Mod Manager workaround
                    if (MyConfig.configChitter.Value)
                    {
                        xueHuaBundle = AssetBundle.LoadFromFile(thisLocation + "chitterSFX");          // xuehuapiaopiao
                    }
                    if (MyConfig.configAngryScreech.Value)
                    {
                        xueHuaOrigBundle = AssetBundle.LoadFromFile(thisLocation + "angryScreechSFX"); // xuehuapiaopiao original
                    }
                }

                if ((MyConfig.configChitter.Value && !xueHuaBundle)
                    || (MyConfig.configAngryScreech.Value && !xueHuaOrigBundle))
                {
                    this.Logger.LogError("## Failed to load HoarderBugAI assets!");

                    if (MyConfig.configChitter.Value && !xueHuaBundle)
                    {
                        this.Logger.LogError("Failed to load chitterSFX asset bundle!");
                    }
                    if (MyConfig.configAngryScreech.Value && !xueHuaOrigBundle)
                    {
                        this.Logger.LogError("Failed to load angryScreechSFX asset bundle!");
                    }
                }
                else
                {
                    // chitterSFX
                    if (MyConfig.configChitter.Value)
                    {
                        xueHuaSoundFX = new AudioClip[1];
                        xueHuaSoundFX = xueHuaBundle.LoadAllAssets<AudioClip>();

                        new_chitterSFX = new AudioClip[1];
                        new_chitterSFX[0] = xueHuaSoundFX[0];
                    }

                    // angryScreechSFX
                    if (MyConfig.configAngryScreech.Value)
                    {
                        xueHuaOrigSoundFX = new AudioClip[1];
                        xueHuaOrigSoundFX = xueHuaOrigBundle.LoadAllAssets<AudioClip>();

                        new_angryScreechSFX = new AudioClip[1];
                        new_angryScreechSFX[0] = xueHuaOrigSoundFX[0];
                    }

                    this.Logger.LogInfo("HoarderBugAI sounds were loaded succesfully!");
                }
            }

            // BoomboxItem
            {
                if (Directory.Exists(thisLocation + "BoomboxItem"))
                {
                    // Manual mode
                    if (MyConfig.configMusicAudios0.Value)
                    {
                        otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios0"); // otelul galati
                    }
                    if (MyConfig.configMusicAudios1.Value)
                    {
                        rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios1");    // rap battle bahoi
                    }
                    if (MyConfig.configMusicAudios2.Value)
                    {
                        mansNotHotBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios2");   // mans not hot
                    }
                    if (MyConfig.configMusicAudios3.Value)
                    {
                        triPoloskiBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios3");   // tri poloski
                    }
                    if (MyConfig.configMusicAudios4.Value)
                    {
                        allStarBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios4");      // smash mouth - all star
                    }
                    if (MyConfig.configMusicAudios5.Value)
                    {
                        broMomentoBundle = AssetBundle.LoadFromFile(thisLocation + "BoomboxItem\\musicAudios5");   // bro momento
                    }
                }
                else
                {
                    // Thunderstore Mod Manager workaround
                    if (MyConfig.configMusicAudios0.Value)
                    {
                        otelulGalatiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios0"); // otelul galati
                    }
                    if (MyConfig.configMusicAudios1.Value)
                    {
                        rapBattleBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios1");    // rap battle bahoi
                    }
                    if (MyConfig.configMusicAudios2.Value)
                    {
                        mansNotHotBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios2");   // mans not hot
                    }
                    if (MyConfig.configMusicAudios3.Value)
                    {
                        triPoloskiBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios3");   // tri poloski
                    }
                    if (MyConfig.configMusicAudios4.Value)
                    {
                        allStarBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios4");      // smash mouth - all star
                    }
                    if (MyConfig.configMusicAudios5.Value)
                    {
                        broMomentoBundle = AssetBundle.LoadFromFile(thisLocation + "musicAudios5");   // bro momento
                    }
                }

                if ((MyConfig.configMusicAudios0.Value && !otelulGalatiBundle)
                    || (MyConfig.configMusicAudios1.Value && !rapBattleBundle)
                    || (MyConfig.configMusicAudios2.Value && !mansNotHotBundle)
                    || (MyConfig.configMusicAudios3.Value && !triPoloskiBundle)
                    || (MyConfig.configMusicAudios4.Value && !allStarBundle)
                    || (MyConfig.configMusicAudios5.Value && !broMomentoBundle))
                {
                    this.Logger.LogError("## Failed to load BoomboxItem assets!");

                    if (MyConfig.configMusicAudios0.Value && !otelulGalatiBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios0 asset bundle!");
                    }
                    if (MyConfig.configMusicAudios1.Value && !rapBattleBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios1 asset bundle!");
                    }
                    if (MyConfig.configMusicAudios2.Value && !mansNotHotBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios2 asset bundle!");
                    }
                    if (MyConfig.configMusicAudios3.Value && !triPoloskiBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios3 asset bundle!");
                    }
                    if (MyConfig.configMusicAudios4.Value && !allStarBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios4 asset bundle!");
                    }
                    if (MyConfig.configMusicAudios5.Value && !broMomentoBundle)
                    {
                        this.Logger.LogError("Failed to load musicAudios5 asset bundle!");
                    }
                }
                else
                {
                    int length_new_musicAudios = 0;
                    // musicAudios0
                    if (MyConfig.configMusicAudios0.Value)
                    {
                        otelulGalatiSoundFX = new AudioClip[1];
                        otelulGalatiSoundFX = otelulGalatiBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    // musicAudios1
                    if (MyConfig.configMusicAudios1.Value)
                    {
                        rapBattleSoundFX = new AudioClip[1];
                        rapBattleSoundFX = rapBattleBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    // musicAudios2
                    if (MyConfig.configMusicAudios2.Value)
                    {
                        mansNotHotSoundFX = new AudioClip[1];
                        mansNotHotSoundFX = mansNotHotBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    // musicAudios3
                    if (MyConfig.configMusicAudios3.Value)
                    {
                        triPoloskiSoundFX = new AudioClip[1];
                        triPoloskiSoundFX = triPoloskiBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    // musicAudios4
                    if (MyConfig.configMusicAudios4.Value)
                    {
                        allStarSoundFX = new AudioClip[1];
                        allStarSoundFX = allStarBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    // musicAudios5
                    if (MyConfig.configMusicAudios5.Value)
                    {
                        broMomentoSoundFX = new AudioClip[1];
                        broMomentoSoundFX = broMomentoBundle.LoadAllAssets<AudioClip>();
                        length_new_musicAudios++;
                    }

                    new_musicAudios = new AudioClip[length_new_musicAudios];
                    int nr_audio = 0;
                    if (MyConfig.configMusicAudios0.Value)
                    {
                        new_musicAudios[nr_audio] = otelulGalatiSoundFX[0];
                        nr_audio++;
                    }
                    if (MyConfig.configMusicAudios1.Value)
                    {
                        new_musicAudios[nr_audio] = rapBattleSoundFX[0];
                        nr_audio++;
                    }
                    if (MyConfig.configMusicAudios2.Value)
                    {
                        new_musicAudios[nr_audio] = mansNotHotSoundFX[0];
                        nr_audio++;
                    }
                    if (MyConfig.configMusicAudios3.Value)
                    {
                        new_musicAudios[nr_audio] = triPoloskiSoundFX[0];
                        nr_audio++;
                    }
                    if (MyConfig.configMusicAudios4.Value)
                    {
                        new_musicAudios[nr_audio] = allStarSoundFX[0];
                        nr_audio++;
                    }
                    if (MyConfig.configMusicAudios5.Value)
                    {
                        new_musicAudios[nr_audio] = broMomentoSoundFX[0];
                        nr_audio++;
                    }

                    this.Logger.LogInfo("BoomboxItem sounds were loaded succesfully!");
                }
            }

            // PufferAI
            {
                if (Directory.Exists(thisLocation + "PufferAI"))
                {
                    // Manual mode
                    if(MyConfig.configFrighten.Value)
                    {
                        beatboxBundle = AssetBundle.LoadFromFile(thisLocation + "PufferAI\\frightenSFX"); // beatbox
                    }
                    if(MyConfig.configPuff.Value)
                    {
                        beatboxTusindBundle = AssetBundle.LoadFromFile(thisLocation + "PufferAI\\puff");  // beatbox tusind
                    }
                }
                else
                {
                    // Thunderstore Mod Manager workaround
                    if(MyConfig.configFrighten.Value)
                    {
                        beatboxBundle = AssetBundle.LoadFromFile(thisLocation + "frightenSFX"); // beatbox
                    }
                    if(MyConfig.configPuff.Value)
                    { 
                        beatboxTusindBundle = AssetBundle.LoadFromFile(thisLocation + "puff");  // beatbox tusind
                    }
                }

                if ((MyConfig.configFrighten.Value && !beatboxBundle) 
                    || (MyConfig.configPuff.Value && !beatboxTusindBundle))
                {
                    this.Logger.LogError("## Failed to load PufferAI assets!");

                    if (MyConfig.configFrighten.Value && !beatboxBundle)
                    {
                        this.Logger.LogError("Failed to load frightenSFX asset bundle!");
                    }
                    if (MyConfig.configPuff.Value && !beatboxTusindBundle)
                    {
                        this.Logger.LogError("Failed to load nervousMumbling asset bundle!");
                    }
                }
                else
                {
                    // frightenSFX
                    if(MyConfig.configFrighten.Value)
                    {
                        beatboxSoundFX = new AudioClip[4];
                        beatboxSoundFX = beatboxBundle.LoadAllAssets<AudioClip>();
                        new_frightenSFX = new AudioClip[beatboxSoundFX.Length];
                        for (int i = 0; i < beatboxSoundFX.Length; i++)
                        {
                            new_frightenSFX[i] = beatboxSoundFX[i];
                        }
                    }

                    // puff
                    if(MyConfig.configPuff.Value)
                    {
                        beatboxTusindSoundFX = new AudioClip[1];
                        beatboxTusindSoundFX = beatboxTusindBundle.LoadAllAssets<AudioClip>();
                        new_puff = beatboxTusindSoundFX[0];
                    }

                    this.Logger.LogInfo("PufferAI sounds were loaded succesfully!");
                }
            }

            // SpringManAI
            {
                if (Directory.Exists(thisLocation + "SpringManAI"))
                {
                    // Manual mode
                    if (MyConfig.configSpringNoises.Value)
                    {
                        bingChillingBundle = AssetBundle.LoadFromFile(thisLocation + "SpringManAI\\springNoises"); // bing chilling
                    }
                }
                else
                {
                    // Thunderstore Mod Manager workaround
                    if (MyConfig.configSpringNoises.Value)
                    {
                        bingChillingBundle = AssetBundle.LoadFromFile(thisLocation + "springNoises"); // bing chilling
                    }
                }

                if (MyConfig.configSpringNoises.Value && !bingChillingBundle)
                {
                    this.Logger.LogError("## Failed to load SpringManAI assets!");

                    if (MyConfig.configSpringNoises.Value && !bingChillingBundle)
                    {
                        this.Logger.LogError("Failed to load springNoises asset bundle!");
                    }
                }
                else
                {
                    if (MyConfig.configSpringNoises.Value)
                    {
                        // springNoises
                        bingChillingSoundFX = new AudioClip[1];
                        bingChillingSoundFX = bingChillingBundle.LoadAllAssets<AudioClip>();

                        new_springNoises = new AudioClip[bingChillingSoundFX.Length];
                        new_springNoises[0] = bingChillingSoundFX[0];

                        this.Logger.LogInfo("SpringManAI sounds were loaded succesfully!");
                    }
                }
            }

            // Final message
            this.Logger.LogInfo("Plugin " + modName + " (version " + modVersion + ") has been succesfully loaded!");
        }
    }
}
