using System;
using UnityEngine;

public enum TagName
{
  VCam,
  House,
  ScreenSpaceCanvas,
  Enemy,
  EnemiesContainer,
  BulletsContainer,
  MoneyContainer,
  ScreenSpaceUiContainer,
  FloatingTextContainer,
  Money,
  EnemySpawner,
  SpawnMarker,
  MoneyMagnet,
  GhostObject,
}

public class TagUtils
{
  public static string GetTagString(TagName name)
  {
    return Enum.GetName(typeof(TagName), name);
  }

  public static GameObject FindWithTag(TagName name)
  {
    return GameObject.FindWithTag(GetTagString(name));
  }

  public static bool CompareTag(GameObject gameObject, TagName name)
  {
    return gameObject.CompareTag(GetTagString(name));
  }
}