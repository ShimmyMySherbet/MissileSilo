# MissileSilo
A loader for Rocketmod that also extends RocketMod's features

This module is intended to be used instead of the rocketmod module directly. This module will load RocketMod directly from Unturned's Extras folder, allowing for always-up-to-date Rocketmod.

Modifications are made to rocketmod externally, similarly to plugins. So your server still runs Legally Distinct Missile maintained by Nelson.

Features:
* Always up to date Rocketmod
* Plugin loading re-work (in progress)

I also plan to intergrate some features of my various plugins and modules.

These include:

* Logging Overhaul. See <a href="https://github.com/ShimmyMySherbet/RocketSeriLogging">Rocket SeriLogging</a>  This module would only be enabled if Openmod is not present, and OM does this already.
* Rocket Extensions. See <a href="https://github.com/ShimmyMySherbet/RocketExtensions">RocketExtensions</a>. This would just be the inclusion of up-to-date RocketExtension libraries
* Console title metrics. See <a href="https://github.com/ShimmyMySherbet/UnturnedConsoleMetrics">UnturnedConsoleMetrics</a>. This provides live server metrics in the title bar of Unturned's console

# Plugin loading re-work

Currently, RocketMod scans for any assemblies in the Plugins folder and sub folders, and loads them as plugins. This means if you were to put a .dll in the same folder as a plugin's config, RocketMod would try to load it as a plugin.

This rework changes that, by only loading assemblies as plugins in the plugins folder and not sub folders. And then loads assemblies in sub folders as libraries.

### What does this mean?

This means that instead of having all of your plugin libraries in Rocket's libraries folder, you can keep them in the config folder for each of your plugins, to organize them for the plugins that require them.

So you don't have to worry about the mess of random dlls in the libraries folder. 
Or overwriting one version of a library with another when adding a new plugin.
