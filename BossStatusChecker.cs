using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using SharpDX;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static ExileCore.PoEMemory.MemoryObjects.ServerInventory;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Threading;
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
