using UnityEngine;

public class ModSO : IdentifiableSO
{
    public bool CanMultiApply = false;
    public virtual void Apply(GameObject gameObject) { }
}