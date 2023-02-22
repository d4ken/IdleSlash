using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleButton : MonoBehaviour
{
    [SerializeField] private CustomButton _customButton;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private void Start()
    {
        _customButton.OnClickCallback = () =>
        {
            if (GameManager.Instance.currentGameState.Value == GameState.RestTime)
            {
                _textMeshPro.text = "Rest.";
                GameManager.Instance.SetCurrentState(GameState.InBattle);
                GameManager.Instance.OnRegularAction += Battle;
                GameManager.Instance.OnRestTimeAction -= Rest;
            }
            else if (GameManager.Instance.currentGameState.Value == GameState.InBattle)
            {
                _textMeshPro.text = "Buttle!";
                GameManager.Instance.SetLastSeconds(0);
                GameManager.Instance.SetCurrentState(GameState.RestTime);
                GameManager.Instance.OnRegularAction -= Battle;
                GameManager.Instance.OnRestTimeAction += Rest;

            }
        };
    }

    void Battle(int time)
    {
    }

    void Rest(int time)
    {
    }
}
