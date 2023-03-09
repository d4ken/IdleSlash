using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Breed")]
public class Breed : ScriptableObject
{
    public string Name => _Name;
    [SerializeField] private string _Name = "";

    public string AttackMessage => _AttackMessage;
    [SerializeField] private string _AttackMessage = "";

    public WeaponType Weakness => _Weakness;
    [SerializeField] private WeaponType _Weakness = WeaponType.None;
}