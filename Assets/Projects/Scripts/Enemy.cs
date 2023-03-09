using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string DisplayName => _DisplayName;
    [SerializeField] private string _DisplayName = "";

    public int MaxHp => _MaxHp;
    [SerializeField] private int _MaxHp = 0;
    
    public int Hp { get; set; }

    public int Atk => _Atk;
    [SerializeField] private int _Atk = 0;

    public Breed Breed => _Breed;
    [SerializeField] private Breed _Breed = null;
}
