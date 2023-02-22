using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterType
{
    player,
    ally,
    enemy
}

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI characterHpText;
    [SerializeField] public TextMeshProUGUI characterExpText;
    public int requireExp;
    public ReactiveProperty<int> currentExp;
    public int dropExp;
    public int speed;
    public ReactiveProperty<int> hp;
    public int maxHp;
    public int strength;
    public CharacterType characterType;
    public string charName;
    public UnityAction onDefeatedAction;
    public int regenerationHp;
    
    private void Start()
    {
        hp
            .Where(x => x <= 0)
            .Subscribe(_ => onDefeatedAction?.Invoke());

        hp
            .Subscribe(x => characterHpText.text = x.ToString());

        currentExp
            .DistinctUntilChanged()
            .Subscribe(x => characterExpText.text = x.ToString());
    }

    public void Attack(CharacterStatus target)
    {
        target.hp.Value -= strength;
    }

    public void Rest()
    {
            hp.Value += regenerationHp;
            if (hp.Value > maxHp)
                hp.Value = maxHp;
    }

}
