using CreatureModTemplate.CreatureModTemplateCode.Monster;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace AnimatedEnemyTutorial.Code.Encounters;

public sealed class StaticStickmanOnly : EncounterModel
{
    public override RoomType RoomType => RoomType.Monster;

    public override IEnumerable<MonsterModel> AllPossibleMonsters
    {
        get { yield return ModelDb.Monster<StaticStickman>(); }
    }

    protected override IReadOnlyList<(MonsterModel, string?)> GenerateMonsters()
    {
        return new List<(MonsterModel, string?)>
        {
            (ModelDb.Monster<StaticStickman>().ToMutable(), null)
        };
    }
}