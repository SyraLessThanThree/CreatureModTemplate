using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using CreatureModTemplate.CreatureModTemplateCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace CreatureModTemplate.CreatureModTemplateCode.Monster;

public class StaticStickman : CustomMonsterModel {
    public override int MinInitialHp => 50;
    public override int MaxInitialHp => 55;
    private int AttackDamage => 10;

//public override string? CustomVisualPath => "res://CreatureModTemplate/images/scary_stickman.png";//doesnt work

//System.InvalidCastException: Unable to cast object of type 'Godot.CompressedTexture2D' to type 'Godot.PackedScene'
//public override string? CustomVisualPath => ResourceLoader.Load("res://CreatureModTemplate/images/scary_stickman.png").ResourcePath;

/*works
public override NCreatureVisuals? CreateCustomVisuals() {
    return NodeFactory<NCreatureVisuals>.CreateFromResource(ResourceLoader.Load("res://CreatureModTemplate/images/scary_stickman.png"));
}
*/
/*doesnt works
public override NCreatureVisuals? CreateCustomVisuals() {
    return NodeFactory<NCreatureVisuals>.CreateFromResource("res://CreatureModTemplate/images/scary_stickman.png");
}
*/
    public override NCreatureVisuals? CreateCustomVisuals() {
        return NodeFactory<NCreatureVisuals>.CreateFromResource("res://CreatureModTemplate/images/scary_stickman.png");
    }

    protected override MonsterMoveStateMachine GenerateMoveStateMachine() {
        var strike = new MoveState(
            "STRIKE",
            Strike,
            new SingleAttackIntent(AttackDamage)
        );
        return new MonsterMoveStateMachine([
            strike
        ], strike);
    }
    private async Task Strike(IReadOnlyList<Creature> targets) {
        await DamageCmd.Attack(AttackDamage)
            .OnlyPlayAnimOnce()
            .FromMonster(this)
            .Execute(null);
    }
}