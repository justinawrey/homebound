using System.Collections.Generic;
using UnityEngine;

public class HomeGrid : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private Transform _placementContainer;

    private void Start()
    {
        InstantiateFromHouseBuildSO();
    }

    [ContextMenu("Init 3x3 on HouseBuildSO")]
    private void Init3x3()
    {
        _playerStatsSO.HouseBuild.Init3x3();
    }

    [ContextMenu("Setup from HouseBuildSO")]
    private void InstantiateFromHouseBuildSO()
    {
        DestroyChildrenInEditor();

        foreach (KeyValuePair<Vector3Int, PlacementSO> placement in _playerStatsSO.HouseBuild.Placements)
        {
            Vector3Int position = placement.Key;
            PlacementSO placementSO = placement.Value;

            GameObject instantiatedPlacement = Instantiate(placementSO.Prefab, _placementContainer.TransformPoint(position), Quaternion.identity, _placementContainer);
            ApplyModsOnPlacement(instantiatedPlacement, placementSO.Mods);
        }
    }

    private void ApplyModsOnPlacement(GameObject placement, List<ModSO> mods)
    {
        ModController modController = placement.GetComponent<ModController>();
        if (modController == null)
        {
            return;
        }

        foreach (ModSO mod in mods)
        {
            modController.Apply(mod);
        }
    }

    private void DestroyChildrenInEditor()
    {
        for (int i = _placementContainer.childCount; i > 0; --i)
        {
            DestroyImmediate(_placementContainer.GetChild(0).gameObject);
        }
    }

    [ContextMenu("Save to disk")]
    private void SaveToDisk()
    {
        _playerStatsSO.SaveToDisk(typeof(PlayerStatsSO));
    }
}
