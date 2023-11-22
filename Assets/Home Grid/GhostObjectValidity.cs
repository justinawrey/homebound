using System.Collections.Generic;
using UnityEngine;

public class GhostObjectValidity : MonoBehaviour
{
    [SerializeField] private List<Component> _ghostComponents;

    public List<Component> GetGhostComponents()
    {
        return _ghostComponents;
    }
}
