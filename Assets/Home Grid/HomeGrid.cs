using System.Collections.Generic;
using UnityEngine;

public class HomeGrid : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private Transform _placementContainer;

    private void Start()
    {
        InstantiateFromHouseBuildSO();
        _playerStatsSO.HouseBuild.Placements.OnAdd((pos, placement) => InstantiateInPlacementContainer(pos, placement.Quaternion, placement.PlacementSO));
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

        foreach (KeyValuePair<Vector3Int, Placement> pair in _playerStatsSO.HouseBuild.Placements)
        {
            Vector3Int position = pair.Key;
            Placement placement = pair.Value;

            GameObject instantiatedPlacement = InstantiateInPlacementContainer(position, placement.Quaternion, placement.PlacementSO);
            ApplyModsOnPlacement(instantiatedPlacement, placement.PlacementSO.Mods);
        }
    }

    private GameObject InstantiateInPlacementContainer(Vector3Int position, Quaternion quaternion, PlacementSO placementSO)
    {
        return Instantiate(placementSO.Prefab, _placementContainer.TransformPoint(position), quaternion, _placementContainer);
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
