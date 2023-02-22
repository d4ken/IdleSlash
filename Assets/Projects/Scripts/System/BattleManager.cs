using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private CharacterStatus _player;
    [SerializeField] private CharacterStatus _enemy;
    void Start()
    {
        _player.onDefeatedAction += OnPlayerDefeatAction;
        _enemy.onDefeatedAction += OnEnemyDefeatAction;
        
        // lastsecondsの変化を監視する
        var inBattle = GameManager.Instance.InBattleSeconds
            .Where(x => x != 0)
            .Subscribe(x =>
            {
                if (_player.speed >= _enemy.speed)
                {
                    _player.Attack(_enemy);
                    _enemy.Attack(_player);
                }
                else
                {
                    _player.Attack(_enemy);
                    _enemy.Attack(_player);
                }
            });

        var inRest = GameManager.Instance.InRestSeconds
            .Where(x => x != 0)
            .Subscribe(x =>
            {
                _player.Rest();
            });
    }

    void OnPlayerDefeatAction()
    {
        Debug.Log($"{_player.charName}は倒された.");
        _enemy.currentExp.Value += _player.dropExp;
        GameManager.Instance.SetCurrentState(GameState.EndBattle);
    }

    void OnEnemyDefeatAction()
    {
        Debug.Log($"{_enemy.charName}を倒した.");
        _player.currentExp.Value += _enemy.dropExp;
        GameManager.Instance.SetCurrentState(GameState.EndBattle);
    }
}
