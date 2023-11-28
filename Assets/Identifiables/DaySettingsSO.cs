using UnityEngine;

[CreateAssetMenu(fileName = "DaySettingsSO", menuName = "Assets/DaySettingsSO")]
public class DaySettingsSO : IdentifiableSO
{
    public float SpawnInterval; // per second
    public float DayLength; // seconds
}