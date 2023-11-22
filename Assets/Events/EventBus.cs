using System;

/// <summary>
/// A purely organizational EventBus class.
/// Totally ephemeral, holds no state.
/// This is maybe not best practice but I like to organize things
/// this way at least.
/// </summary>
public static class EventBus
{

  /// <summary>
  /// Invoked when the players health is changed. 
  /// Supplied parameter represents the percent health remaining.
  /// </summary>
  public static event Action<float> OnPlayerHealthChange;
  public static void ChangePlayerHealth(float percent)
  {
    OnPlayerHealthChange?.Invoke(percent);
  }

  /// <summary>
  /// Invoked when a day ticks.
  /// Supplied parameter represents the current percent through the day.
  /// </summary>
  public static event Action<float> OnDayTick;
  public static void TickDay(float percent)
  {
    OnDayTick?.Invoke(percent);
  }

  /// <summary>
  /// Invoked when a day starts.
  /// </summary>
  public static event Action OnDayStart;
  public static void StartDay()
  {
    OnDayStart?.Invoke();
  }

  /// <summary>
  /// Invoked when a day ends. 
  /// </summary>
  public static event Action OnDayEnd;
  public static void EndDay()
  {
    OnDayEnd?.Invoke();
  }

  /// <summary>
  /// Invoked when the build phase starts.
  /// Supplied parameter represents the upgrades which
  /// can be built in this build phase.
  /// </summary>
  public static event Action<UpgradeSO[]> OnBuildPhaseStart;
  public static void StartBuildPhase(UpgradeSO[] upgrades)
  {
    OnBuildPhaseStart?.Invoke(upgrades);
  }

  /// <summary>
  /// Invoked when the build phase ends.
  /// </summary>
  public static event Action OnBuildPhaseEnd;
  public static void EndBuildPhase()
  {
    OnBuildPhaseEnd?.Invoke();
  }

  /// <summary>
  /// Invoked when the upgrade phase ends.
  /// Supplied parameters represents the upgrades that were bought
  /// during this upgrade phase.
  /// </summary>
  public static event Action<UpgradeSO[]> OnUpgradePhaseEnd;
  public static void EndUpgradePhase(UpgradeSO[] upgrades)
  {
    OnUpgradePhaseEnd?.Invoke(upgrades);
  }

  /// <summary>
  /// Invoked when screen shake is started.
  /// The supplied parameter represents how long screen shake should last.
  /// </summary>
  public static event Action<float> OnScreenShakeFor;
  public static void ScreenShakeFor(float duration)
  {
    OnScreenShakeFor?.Invoke(duration);
  }

  /// <summary>
  /// Invoked when freeze frame is started.
  /// The supplied parameter represents how long the freeze frame should last.
  /// </summary>
  public static event Action<float> OnFreezeFrameFor;
  public static void FreezeFrameFor(float duration)
  {
    OnFreezeFrameFor?.Invoke(duration);
  }
}