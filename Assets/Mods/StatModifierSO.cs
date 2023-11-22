using System;
using UnityEngine;

public enum ModifierType
{
    Identity,
    Addition,
    Subtraction,
    Multiplication,
    Division,
}

// Why are there no generic type constraints for numeric types!!!!!!!
[Serializable]
public struct IntModifier
{
    public int ModifierValue;
    public ModifierType ModifierType;

    public int Modify(int initial)
    {
        switch (ModifierType)
        {
            case ModifierType.Addition:
                return initial + ModifierValue;
            case ModifierType.Subtraction:
                return initial - ModifierValue;
            case ModifierType.Multiplication:
                return initial * ModifierValue;
            case ModifierType.Division:
                return initial / ModifierValue;
            case ModifierType.Identity:
            default:
                return initial;
        }
    }
}

[Serializable]
public struct FloatModifier
{
    public float ModifierValue;
    public ModifierType ModifierType;

    public float Modify(float initial)
    {
        switch (ModifierType)
        {
            case ModifierType.Addition:
                return initial + ModifierValue;
            case ModifierType.Subtraction:
                return initial - ModifierValue;
            case ModifierType.Multiplication:
                return initial * ModifierValue;
            case ModifierType.Division:
                return initial / ModifierValue;
            case ModifierType.Identity:
            default:
                return initial;
        }
    }
}

[CreateAssetMenu(fileName = "StatModifierSO", menuName = "Assets/Mods/Stat Modifier")]
public class StatModifierSO : ModSO
{
    public FloatModifier SpeedModifier;
    public FloatModifier DamageModifier;
    public FloatModifier FireIntervalModifier;
    public IntModifier LifetimeModifier;

    public override void Apply(GameObject gameObject)
    {
        BaseStats stats = gameObject.GetComponent<BaseStats>();
        if (stats == null)
        {
            return;
        }

        stats.Speed = SpeedModifier.Modify(stats.Speed);
        stats.Damage = DamageModifier.Modify(stats.Damage);
        stats.FireInterval = FireIntervalModifier.Modify(stats.FireInterval);
        stats.Lifetime = LifetimeModifier.Modify(stats.Lifetime);
    }
}

