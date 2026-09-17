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
        private const string PluginVersion = "1.0.10";

        private Harmony _harmony;

        // Loads the plugin.
        private void Awake()
        {
            QuickLaunchSession.Log = Logger;
            RememberedPassword.Log = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            RegisterPatches();
            Logger.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        private void RegisterPatches()
        {
            _harmony.CreateClassProcessor(typeof(StartPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(LocalSessionPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(MultiplayerSessionPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(ConnectedServerPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(LoadingWorldLabel)).Patch();
            _harmony.CreateClassProcessor(typeof(CapturePasswordPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(SubmitPasswordPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(InvalidPasswordPatch)).Patch();
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
