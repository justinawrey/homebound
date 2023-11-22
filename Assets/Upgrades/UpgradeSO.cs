using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Assets/Upgrades/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public string Description;
    public int BaseCost = 1;
    public GameObject UpgradePrefab;
}