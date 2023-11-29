using System.Collections.Generic;
using UnityEngine;

public class ModController : MonoBehaviour
{
  private HashSet<ModSO> _appliedMods = new HashSet<ModSO>();

  // note: you can only apply a single mod once,
  // unless CanMultiApply is true on the mod!
  public void Apply(ModSO mod)
  {
    if (!mod.CanMultiApply && _appliedMods.Contains(mod))
    {
      return;
    }

    _appliedMods.Add(mod);
    mod.Apply(gameObject);
  }

  [ContextMenu("Print mods")]
  public void PrintMods()
  {
    foreach (var modSO in _appliedMods)
    {
      print(modSO.Name);
    }
  }
}