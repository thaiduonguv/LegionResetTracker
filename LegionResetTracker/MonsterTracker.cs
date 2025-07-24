using ExileCore;

public class MonsterTracker
{
    private readonly GameController game;

    public MonsterTracker(GameController controller)
    {
        game = controller;
    }

    public int GetMonsterCount()
    {
        return game.EntityListWrapper.ValidEntities.Count(e =>
            e.IsAlive && e.IsHostile && e.Path.Contains("Legion"));
    }
}
