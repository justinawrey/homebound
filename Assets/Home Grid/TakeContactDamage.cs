using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public interface IDamageDealer
{
    public void DealDamage(Health health);
}

public class TakeContactDamage : MonoBehaviour
{
    [SerializeField] private float _iframesLength = 1f;
    [SerializeField] private float _invisibleTime = 0.1f;
    [SerializeField] private float _visibleTime = 0.1f;
    [SerializeField] private float _screenShakeTime = 0.1f;
    [SerializeField] private float _freezeFrameTime = 0.1f;
    [SerializeField] private MMF_Player _mmPlayer;

    private Health _health;
    private bool _invulnerable = false;
    private MeshRenderer[] _meshRenderers;

    private void Awake()
    {
        EventBus.OnDayStart += RefreshMeshRenderers;
        _health = GetComponent<Health>();
    }

    private void OnDestroy()
    {
        EventBus.OnDayStart -= RefreshMeshRenderers;
    }

    private void RefreshMeshRenderers()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_invulnerable)
        {
            return;
        }

        // TODO: there could be a better way to filter out receving damage than this.
        // this is a future problem lol famous last words!
        if (!TagUtils.CompareTag(other.gameObject, TagName.Enemy))
        {
            return;
        }

        IDamageDealer[] dealers = other.gameObject.GetComponentsInChildren<IDamageDealer>();
        if (dealers.Length <= 0)
        {
            return;
        }

        foreach (IDamageDealer dealer in dealers)
        {
            dealer.DealDamage(_health);
        }

        _mmPlayer.PlayFeedbacks();
        EventBus.ScreenShakeFor(_screenShakeTime);
        EventBus.FreezeFrameFor(_freezeFrameTime);
        StartCoroutine(InvulnerableRoutine());
    }

    private IEnumerator InvulnerableRoutine()
    {
        _invulnerable = true;
        float count = 0;
        while (count < _iframesLength)
        {
            SetMeshRenderersTo(false);
            yield return new WaitForSeconds(_invisibleTime);
            SetMeshRenderersTo(true);
            yield return new WaitForSeconds(_visibleTime);
            count += _invisibleTime + _visibleTime;
        }

        _invulnerable = false;
    }

    private void SetMeshRenderersTo(bool active)
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.enabled = active;
        }
    }
}
