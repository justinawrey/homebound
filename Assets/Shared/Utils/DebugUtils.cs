using System;
using UnityEngine;

public static class DebugUtils
{
  [Serializable]
  public class ConsoleDebugger
  {
    [SerializeField] private bool _debug = false;

    public void Log(object message)
    {
      if (!_debug)
      {
        return;
      }

      Debug.Log(message);
    }
  }
}