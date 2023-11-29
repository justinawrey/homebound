using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StagedPlacement
{
    public GameObject GameObject;
    public List<ModSO> Mods;

    public StagedPlacement(GameObject gameObject, List<ModSO> mods)
    {
        GameObject = gameObject;
        Mods = mods;
    }
}

public class HomeGrid : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private Transform _placementContainer;

    private Action _unsub;

    private void Start()
    {
        InstantiateFromHouseBuildSO();
        // _unsub = _playerStatsSO.HouseBuild.Placements.OnAdd((pos, placement) => InstantiateInPlacementContainer(pos, placement.Quaternion, placement.PlacementSO));
        _unsub = _playerStatsSO.HouseBuild.Placements.OnAdd((pos, placement) => InstantiateInPlacementContainer(pos, placement.PlacementSO));
    }

    private void OnDestroy()
    {
        _unsub();
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

        Dictionary<Vector3Int, StagedPlacement> stagedPlacements = new Dictionary<Vector3Int, StagedPlacement>();

        // Instantiate all the placements, and add them to a dictionary with their mods and their positiosn
        foreach (KeyValuePair<Vector3Int, Placement> pair in _playerStatsSO.HouseBuild.Placements)
        {
            Vector3Int position = pair.Key;
            Placement placement = pair.Value;

            // GameObject instantiatedPlacement = InstantiateInPlacementContainer(position, placement.Quaternion, placement.PlacementSO);
            GameObject instantiatedPlacement = InstantiateInPlacementContainer(position, placement.PlacementSO);

            StagedPlacement stagedPlacement = new StagedPlacement(instantiatedPlacement, placement.PlacementSO.Mods);
            stagedPlacements.Add(position, stagedPlacement);
        }

        // foreach (var p in stagedPlacements)
        // {
        //     print("position: " + p.Key);
        //     print("name: " + p.Value.GameObject.name);
        // }

        // Go through all of the placements again so we can apply spatial mods
        foreach (KeyValuePair<Vector3Int, Placement> pair in _playerStatsSO.HouseBuild.Placements)
        {
            Vector3Int position = pair.Key;
            Placement placement = pair.Value;

            if (!(typeof(SpatialEffectPlacementSO) == placement.PlacementSO.GetType()))
            {
                // we only care about spatial effects here.
                continue;
            }

            SpatialEffectPlacementSO spatialPlacement = (SpatialEffectPlacementSO)placement.PlacementSO;
            foreach (SpatialEffect spatialEffect in spatialPlacement.SpatialEffects)
            {
                // print(spatialEffect.Offset);
                Vector3Int effectedPos = position + spatialEffect.Offset;

                if (stagedPlacements.ContainsKey(effectedPos))
                {
                    StagedPlacement stagedPlacement = stagedPlacements[effectedPos];
                    stagedPlacement.Mods = stagedPlacement.Mods.Concat(spatialEffect.Mods).ToList();
                }
            }
        }

        // Actually apply baby... finally
        foreach (StagedPlacement stagedPlacement in stagedPlacements.Values)
        {
            ApplyModsOnPlacement(stagedPlacement.GameObject, stagedPlacement.Mods);
        }
    }

    // private GameObject InstantiateInPlacementContainer(Vector3Int position, Quaternion quaternion, PlacementSO placementSO)
    private GameObject InstantiateInPlacementContainer(Vector3Int position, PlacementSO placementSO)
    {
        // TODO: this rotation nonsense is janky but ok
        return Instantiate(placementSO.Prefab, _placementContainer.TransformPoint(position), _placementContainer.parent.transform.rotation, _placementContainer);
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
