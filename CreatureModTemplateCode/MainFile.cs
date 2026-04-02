using CreatureModTemplate.CreatureModTemplateCode.Extensions;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;

namespace CreatureModTemplate.CreatureModTemplateCode;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node {
    public const string ModId = "CreatureModTemplate"; //At the moment, this is used only for the Logger and harmony names.

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize() {
        Harmony harmony = new(ModId);

        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(NMainMenu), nameof(NMainMenu._Ready))]
class MainMenu {
    [HarmonyPostfix]
    public static void Postfix() {
        var path = "scary_stickman.png".ImagePath().Replace("\\","/");
        path = "res://CreatureModTemplate/images/scary_stickman.png";
        var texture = ResourceLoader.Exists(path);
        MainFile.Logger.Info($"Loaded Texture {path}:{texture}");
        path += ".import";
        texture = ResourceLoader.Exists(path);
        MainFile.Logger.Info($"Loaded Texture {path}:{texture}");
    }
}
