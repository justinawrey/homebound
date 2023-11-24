using System;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    private GameObject _pendingObject = null;
    private Vector3 _prevPlacedPosition = Vector3.zero;
    private Vector3 _prevPlacedRotation = Vector3.zero;

    private void CreateGhostFromObject(GameObject gameObject)
    {
        GameObject instantiated = Instantiate(gameObject, transform);
        // List<Component> validGhostComponents = instantiated.GetComponent<GhostObjectValidity>().GetGhostComponents();

        foreach (Component component in instantiated.GetComponentsInChildren<Component>())
        {
            // Dont destroy transfrosm, mesh filters, or mesh renderers
            Type type = component.GetType();
            if (type == typeof(Transform) || type == typeof(MeshFilter) || type == typeof(MeshRenderer))
            {
                continue;
            }

            // dont destroy valid ghost components
            // if (validGhostComponents.Contains(component))
            // {
            //     continue;
            // }

            Destroy(component);
        }
    }

    public void StartPlacement(GameObject gameObject)
    {
        _pendingObject = gameObject;
        ClearGhost();
        CreateGhostFromObject(gameObject);
    }

    public GameObject GetPendingObject()
    {
        return _pendingObject;
    }

    public void SetGhostObjectPosition(Vector3 pos)
    {
        GameObject ghostObject = GetGhostObject();
        if (ghostObject == null)
        {
            return;
        }

        ghostObject.transform.position = pos;
    }

    public Vector3 GetGhostObjectPosition()
    {
        GameObject ghostObject = GetGhostObject();
        if (ghostObject == null)
        {
            return Vector3.zero;
        }

        return ghostObject.transform.position;
    }

    public void AlignGhostObjectRotationToNormal(Vector3 normal)
    {
        GameObject ghostObject = GetGhostObject();
        if (ghostObject == null)
        {
            return;
        }

        ghostObject.transform.forward = normal;
    }

    public GameObject GetGhostObject()
    {
        if (transform.childCount == 0)
        {
            return null;
        }

        return transform.GetChild(0).gameObject;
    }

    public void EndPlacement()
    {
        _prevPlacedPosition = GetGhostObject().transform.position;
        _prevPlacedRotation = GetGhostObject().transform.eulerAngles;
        ClearGhost();
        _pendingObject = null;
    }

    public Vector3 GetPrevPlacedPosition()
    {
        return _prevPlacedPosition;
    }

    public Vector3 GetPrevPlacedRotation()
    {
        return _prevPlacedRotation;
    }

    public bool IsPlaced()
    {
        return _pendingObject == null;
    }

    public void HideGhostObject()
    {
        GameObject ghostObject = GetGhostObject();
        if (ghostObject == null)
        {
            return;
        }

        ghostObject.SetActive(false);
    }

    public void ShowGhostObject()
    {
        GameObject ghostObject = GetGhostObject();
        if (ghostObject == null)
        {
            return;
        }

        ghostObject.SetActive(true);
    }

    private void ClearGhost()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}