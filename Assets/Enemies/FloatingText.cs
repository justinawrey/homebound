using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour, IUiInitializer
{
    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(GameObject gameObject)
    {
        _textComponent.text = $"{gameObject.name}";
    }
}
