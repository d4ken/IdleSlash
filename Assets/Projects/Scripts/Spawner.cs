using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CharacterStatus enemy;
    void Start()
    {
        GameManager.Instance.currentGameState
            .DistinctUntilChanged()
            .Where(state => state == GameState.EndBattle)
            .Subscribe(_ => SpawnEnemy());

    }

    void SpawnEnemy()
    {
        Debug.Log("敵を生成");
        enemy.hp.Value = enemy.maxHp;
        GameManager.Instance.SetCurrentState(GameState.InBattle);
    }
}
