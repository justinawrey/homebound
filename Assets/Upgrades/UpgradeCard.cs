using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCard : MonoBehaviour, IUiSelectable
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    // TODO: this can just be cached in parent
    [SerializeField] public UpgradeSO Upgrade;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private float _startAnchorPosY = 500;
    [SerializeField] private float _endAchorPosY = -180;
    [SerializeField] private float _centerPosX = 290;
    [SerializeField] private float _tweenDuration = 0.5f;
    [SerializeField] private float _shakeDuration = 0.1f;
    [SerializeField] private float _shakeStrength = 1f;

    [Header("Debug Options")]
    [SerializeField] private bool _initializeComponentsOnStart = false;
    [SerializeField] private bool _bypassMoneyCost = false;

    private RectTransform _rectTransform;
    private Action _upgradeSelectedCb;
    private Vector2 _originalAnchorPos;

    private void Awake()
    {
        UpgradeCards.OnUpgradePhaseEnd += ResetCardPosition;
        _rectTransform = GetComponent<RectTransform>();
        _originalAnchorPos = _rectTransform.anchoredPosition;
    }

    private void Start()
    {
        if (_initializeComponentsOnStart)
        {
            InitializeUpgradeComponents();
        }
    }

    private void OnDestroy()
    {
        UpgradeCards.OnUpgradePhaseEnd -= ResetCardPosition;
    }

    private void ResetCardPosition(UpgradeSO[] _)
    {
        _rectTransform.anchoredPosition = _originalAnchorPos;
    }

    public void InitializeUpgrade(UpgradeSO upgrade, Action upgradeSelectedCb)
    {
        Upgrade = upgrade;
        _upgradeSelectedCb = upgradeSelectedCb;
        InitializeUpgradeComponents();
    }

    private void TweenIn()
    {
        _rectTransform.DOAnchorPosY(_endAchorPosY, _tweenDuration);
    }

    public void TweenOut()
    {
        _rectTransform.DOAnchorPosY(_startAnchorPosY, _tweenDuration);
    }

    // TODO: what! this is unclean lol
    public void TweenToCenterAndOut(Action cb)
    {
        _rectTransform.DOAnchorPosX(_centerPosX, _tweenDuration).OnComplete(() =>
        {
            _rectTransform.DOAnchorPosY(_startAnchorPosY, _tweenDuration).OnComplete(() => cb());
        });
    }

    private void InitializeUpgradeComponents()
    {
        _descriptionText.text = Upgrade.Description;
        _costText.text = GetCostString();
        TweenIn();
    }

    private string GetCostString()
    {
        return $"CO$T: {Upgrade.BaseCost}$";
    }


    public void OnUiSelect(RaycastResult raycastResult)
    {
        if (_bypassMoneyCost)
        {
            _upgradeSelectedCb();
            return;
        }

        if (_playerStatsSO.Money.Value < Upgrade.BaseCost)
        {
            // Can't buy
            // TODO: with this code, repeated clicking can shake out of control
            _rectTransform.DOShakeAnchorPos(_shakeDuration, _shakeStrength);
            return;
        }

        _playerStatsSO.Money.Value -= Upgrade.BaseCost;
        _upgradeSelectedCb();
    }
}