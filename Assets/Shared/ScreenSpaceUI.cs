using UnityEngine;

public interface IUiInitializer
{
    public void Initialize(GameObject gameObject);
}

public class ScreenSpaceUI : MonoBehaviour
{
    [SerializeField] private GameObject _contextualUiPrefab;
    [SerializeField] private Vector2 _screenSpaceOffset = Vector2.up * 50f;

    private Transform _screenSpaceUiTransform;
    private Camera _screenSpaceCanvasCamera;
    private GameObject _uiInstance;
    private RectTransform _uiRectTransform;

    private void Start()
    {
        GameObject canvas = TagUtils.FindWithTag(TagName.ScreenSpaceCanvas);
        _screenSpaceUiTransform = TagUtils.FindWithTag(TagName.ScreenSpaceUiContainer).transform;
        _screenSpaceCanvasCamera = canvas.GetComponent<Canvas>().worldCamera;
        _uiInstance = Instantiate(_contextualUiPrefab, _screenSpaceUiTransform);
        _uiRectTransform = _uiInstance.GetComponent<RectTransform>();
        InitializeUi();
    }

    private void Update()
    {
        Vector3 screenPos = _screenSpaceCanvasCamera.WorldToScreenPoint(transform.position);
        _uiRectTransform.anchoredPosition = new Vector2(screenPos.x, screenPos.y) + _screenSpaceOffset;
    }

    private void OnDisable()
    {
        if (_uiInstance != null)
        {
            _uiInstance.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (_uiInstance != null)
        {
            _uiInstance.SetActive(true);
        }
    }

    private void InitializeUi()
    {
        IUiInitializer uiInitializer = _uiInstance.GetComponent<IUiInitializer>();
        if (uiInitializer == null)
        {
            return;
        }

        uiInitializer.Initialize(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(_uiInstance);
    }

    public GameObject GetUiInstance()
    {
        return _uiInstance;
    }
}