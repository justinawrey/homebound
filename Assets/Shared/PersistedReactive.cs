using System;
using ReactiveUnity;

public interface Persistable<T>
{
  public T GetPersistedVal();
  public void SetPersistedVal(T to);
}

[Serializable]
public class PersistedReactive<T> : Reactive<T>, Persistable<T>
{
  public PersistedReactive(T val) : base(val) { }

  public T GetPersistedVal()
  {
    return Value;
  }

  public void SetPersistedVal(T to)
  {
    Value = to;
  }
}