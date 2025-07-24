using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using ExileCore;
using ImGuiNET;

public class LegionResetTracker : BasePlugin
{
    private Stopwatch waveTimer;
    private int currentWave;
    private MonsterTracker monsterTracker;
    private BossStatusChecker bossChecker;
    private string logPath;

    public override bool Initialise()
    {
        waveTimer = new Stopwatch();
        monsterTracker = new MonsterTracker(GameController);
        bossChecker = new BossStatusChecker(GameController);
        currentWave = 0;

        var logsDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logsDir))
            Directory.CreateDirectory(logsDir);

        logPath = Path.Combine(logsDir, "LegionLog.txt");
        if (!File.Exists(logPath))
            File.WriteAllText(logPath, "Wave\tTime\tMonsterCount\tBossStatus\n");

        return true;
    }

    public override void Render()
    {
        if (!GameController.Game.IngameState.InGame)
            return;

        ImGui.Begin("Legion Reset Tracker");

        if (ImGui.Button("Start Wave"))
        {
            waveTimer.Restart();
            currentWave++;
        }

        if (ImGui.Button("End Wave"))
        {
            waveTimer.Stop();

            int monsterCount = monsterTracker.GetMonsterCount();
            string bossSummary = string.Join(",", new[] { "Stone", "Vagan", "Marceus" }.Select(b =>
                bossChecker.IsBossAlive(b) ? $"{b}:OK" : $"{b}:MISS"));

            string logLine = $"{currentWave}\t{waveTimer.Elapsed.TotalSeconds:F2}\t{monsterCount}\t{bossSummary}";
            File.AppendAllText(logPath, logLine + Environment.NewLine);
        }

        ImGui.Text($"Wave: {currentWave}");
        ImGui.Text($"Time: {waveTimer.Elapsed.TotalSeconds:F2}s");

        double time = waveTimer.Elapsed.TotalSeconds;
        if (time > 20)
            ImGui.TextColored(new Vector4(1f, 0f, 0f, 1f), "⛔ Reset Delay!");
        else if (time > 17)
            ImGui.TextColored(new Vector4(1f, 1f, 0f, 1f), "⚠️ Might Be Late");
        else
            ImGui.TextColored(new Vector4(0f, 1f, 0f, 1f), "✅ On Time");

        int monsterCount = monsterTracker.GetMonsterCount();
        ImGui.Text($"🧟 Legion Monsters: {monsterCount}");

        ImGui.Text("📋 Boss Status:");
        foreach (var boss in new[] { "Stone", "Vagan", "Marceus" })
        {
            bool alive = bossChecker.IsBossAlive(boss);
            Vector4 color = alive ? new Vector4(0f, 1f, 0f, 1f) : new Vector4(1f, 0f, 0f, 1f);
            ImGui.TextColored(color, $"{boss}: {(alive ? "✅ Present" : "❌ Missing")}");
        }

        ImGui.End();
    }
}
