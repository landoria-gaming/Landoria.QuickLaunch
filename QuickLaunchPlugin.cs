using BepInEx;
using HarmonyLib;

namespace Landoria.QuickLaunch
{
    // Loads and unloads QuickLaunch.
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class QuickLaunchPlugin : BaseUnityPlugin
    {
        private const string PluginGuid = "Landoria.QuickLaunch";
        private const string PluginName = "Landoria.QuickLaunch";
        private const string PluginVersion = "1.0.11";

        private Harmony _harmony;

        // Loads the plugin.
        private void Awake()
        {
            QuickLaunchSession.Log = Logger;
            RememberedPassword.Log = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            _harmony.PatchAll();
            Logger.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        // Unloads the plugin.
        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            _harmony = null;
            Logger.LogInfo($"{PluginName} {PluginVersion} is unloaded.");
            QuickLaunchSession.Log = null;
            RememberedPassword.Log = null;
        }
    }
}
