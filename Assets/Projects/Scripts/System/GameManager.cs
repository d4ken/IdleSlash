using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;



public class GameManager : Singleton<GameManager>
{
    public event UnityAction OnInitializeAction;
    public event UnityAction OnSleepAction;
    public event UnityAction OnInBattlePhaseAction;
    public event UnityAction OnEndBattlePhaseAction;
    public event UnityAction OnGameOverAction;
    public event UnityAction<int> OnRegularAction;
    public event UnityAction<int> OnRestTimeAction;
    
    public GameStateRp currentGameState;

    private bool _isEndPhase = false;
    public ReactiveProperty<int> InBattleSeconds = new ReactiveProperty<int>();
    public ReactiveProperty<int> InRestSeconds = new ReactiveProperty<int>();
    

    public void SetLastSeconds(int time)
    {
        InBattleSeconds.Value = time;
    }
    
    void Start()
    {
        InBattleSeconds.Value = 0;
        InRestSeconds.Value = 0;
        Application.targetFrameRate = 30;
        SetCurrentState(GameState.RestTime);
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => currentGameState.Value == GameState.InBattle)
            .Subscribe(_ => RegularExec())
            .AddTo(this);
        
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => currentGameState.Value == GameState.RestTime)
            .Subscribe(_ => RestExec())
            .AddTo(this);
    }

    private void RegularExec()
    {
        OnRegularAction?.Invoke(++InBattleSeconds.Value);
    }

    private void RestExec()
    {
        OnRestTimeAction?.Invoke(++InRestSeconds.Value);
    }
    
    public void SetCurrentState(GameState state)
    {
        currentGameState.Value = state;
        OnGameStateChanged(currentGameState.Value);
    }

    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            
            case GameState.Initialize:
                OnInitializeAction?.Invoke();
                break;
            
            case GameState.RestTime:
                OnSleepAction?.Invoke();
                break;

            case GameState.InBattle:
                OnInBattlePhaseAction?.Invoke();
                break;
            
            case GameState.EndBattle:
                OnEndBattlePhaseAction?.Invoke();
                break;
            
            case GameState.GameOver:
                OnGameOverAction?.Invoke();
                break;
        }
    }
}
