using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ILevelable
{
  public void LevelUp(int toLevel);
}

[Serializable]
public class LevelUpModsApplier
{
  public int OnLevel;
  public List<ModSO> ModsToApply;
}

// You can level up a placement in a couple of ways:
// 1) through mods assigned in the editor.  this is the main way
// 2) through custom interface logic.  this is not the main way!
public class LevelController : MonoBehaviour
{
  [SerializeField] private List<LevelUpModsApplier> _levelUpMods;

  private ModController _modController;
  private int _currLevel = 1;

  private void Awake()
  {
    _modController = GetComponent<ModController>();
  }

  public void LevelUp()
  {
    _currLevel += 1;
    LevelUpMods();
    LevelUpCustom();
  }

  private void LevelUpMods()
  {
    List<ModSO> mods = new List<ModSO>();
    List<LevelUpModsApplier> modsForLevel = _levelUpMods.Where(levelUpMod => levelUpMod.OnLevel == _currLevel).ToList();
    foreach (var _mods in modsForLevel)
    {
      mods = mods.Concat(_mods.ModsToApply).ToList();
    }

    foreach (ModSO mod in mods)
    {
      _modController.Apply(mod);
    }
  }

  private void LevelUpCustom()
  {
    ILevelable[] levelables = GetComponentsInChildren<ILevelable>();
    if (levelables.Length <= 0)
    {
      return;
    }

    foreach (ILevelable level in levelables)
    {
      level.LevelUp(_currLevel);
    }
  }

  [ContextMenu("Force level up")]
  private void ForceLevelUp()
  {
    LevelUp();
  }
}