using ExileCore;

public class BossStatusChecker
{
    private readonly GameController game;

    public BossStatusChecker(GameController controller)
    {
        game = controller;
    }

    public bool IsBossAlive(string nameFragment)
    {
        return game.EntityListWrapper.ValidEntities.Any(e =>
            e.IsAlive && e.Metadata.Contains("Legion") && e.Metadata.Contains(nameFragment));
    }
}
