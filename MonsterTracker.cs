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
