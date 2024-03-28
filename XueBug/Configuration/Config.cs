using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XueBug.Configuration
{
    public class Config
    {
        // Terminal
        public ConfigEntry<bool> configDisplayASCII;

        // HoarderBugAI
        public ConfigEntry<bool> configChitter;
        public ConfigEntry<bool> configAngryScreech;

        // BoomboxItem
        public ConfigEntry<bool> configMusicAudios0;
        public ConfigEntry<bool> configMusicAudios1;
        public ConfigEntry<bool> configMusicAudios2;
        public ConfigEntry<bool> configMusicAudios3;
        public ConfigEntry<bool> configMusicAudios4;
        public ConfigEntry<bool> configMusicAudios5;

        // PufferAI
        public ConfigEntry<bool> configFrighten;
        public ConfigEntry<bool> configPuff;

        // SpringManAI
        public ConfigEntry<bool> configSpringNoises;

        public Config(ConfigFile cfg)
        {
            // ASCII Art
            {
                configDisplayASCII = cfg.Bind(
                    "General",                      // Config section
                    "DisplayASCII",                  // Key of this config
                    true,                               // Default value
                    "To show the ASCII art in BepInEx terminal"         // Description
                );
            }

            // HoarderBugAI
            {
                configChitter = cfg.Bind(
                    "Audio.HoarderBug",                  // Config subsection
                    "chitter",                  // Key of this config
                    true,                               // Default value
                    "To change chitterSFX in the HoarderBugAI sounds"         // Description
                );

                configAngryScreech = cfg.Bind(
                    "Audio.HoarderBug",                  // Config subsection
                    "angryScreech",                  // Key of this config
                    true,                               // Default value
                    "To change angryScreechSFX in the HoarderBugAI sounds"         // Description
                );
            }

            // BoomboxItem
            {
                configMusicAudios0 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox0",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios0 in the BoomBox sounds"         // Description
                );

                configMusicAudios1 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox1",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios1 in the BoomBox sounds"         // Description
                );

                configMusicAudios2 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox2",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios2 in the BoomBox sounds"         // Description
                );

                configMusicAudios3 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox3",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios3 in the BoomBox sounds"         // Description
                );

                configMusicAudios4 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox4",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios4 in the BoomBox sounds"         // Description
                );

                configMusicAudios5 = cfg.Bind(
                    "Audio.Boombox",                  // Config subsection
                    "Boombox5",                  // Key of this config
                    true,                               // Default value
                    "To change musicAudios5 in the BoomBox sounds"         // Description
                );
            }

            // PufferAI
            {
                configFrighten = cfg.Bind(
                    "Audio.Puffer",                  // Config subsection
                    "frighten",                  // Key of this config
                    true,                               // Default value
                    "To change frightenSFX in the PufferAI sounds"         // Description
                );

                configPuff = cfg.Bind(
                    "Audio.Puffer",                  // Config subsection
                    "angryScreech",                  // Key of this config
                    true,                               // Default value
                    "To change puff in the PufferAI sounds"         // Description
                );
            }

            // SpringManAI
            {
                configSpringNoises = cfg.Bind(
                    "Audio.SpringMan",                  // Config subsection
                    "springNoises",                  // Key of this config
                    true,                               // Default value
                    "To change springNoises in the SpringManAI sounds"         // Description
                );
            }
        }
    }
}
