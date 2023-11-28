using UnityEditor;

[CustomEditor(typeof(PlayerStatsSO))]
public class PlayerStatsSOEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    // EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
  }
}