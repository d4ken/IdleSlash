using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum GameState
{
    Initialize,
    RestTime,
    InBattle,
    EndBattle,
    GameOver,
}

[System.Serializable]
public class GameStateRp : ReactiveProperty<GameState>
{
    public GameStateRp()
    {
    }

    public GameStateRp(GameState initialValue) : base(initialValue)
    {
    }
}