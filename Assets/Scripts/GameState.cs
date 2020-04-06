using System;
using System.Collections.Generic;

public enum GameMode
{
    None,
    City,
    Office
}
public class GameState
{
    private static GameMode _gameMode = GameMode.None;

    public static Dictionary<GameMode, Action> OnGameModeSet { get; set; }
    public static bool IsPaused { get; set; } = false;

    public static GameMode GameMode
    {
        get => _gameMode;
        set
        {
            if (_gameMode != value) OnGameModeSet[value]?.Invoke();
            _gameMode = value;
        }
    }

    static GameState()
    {
        OnGameModeSet = new Dictionary<GameMode, Action>();
        foreach (GameMode gameMode in Enum.GetValues(typeof(GameMode)))
        {
            OnGameModeSet[gameMode] = null;
        }
    }
}
