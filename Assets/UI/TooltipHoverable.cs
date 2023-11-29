using UnityEngine;

public class TooltipHoverable : MonoBehaviour, IHoverable
{
    [SerializeField] private ScreenSpaceUI _screenSpaceUi;

    public void OnHoverEnter(RaycastHit hit)
    {
        _screenSpaceUi.enabled = true;
    }

    public void OnHoverExit(RaycastHit hit)
    {
        _screenSpaceUi.enabled = false;
    }
}