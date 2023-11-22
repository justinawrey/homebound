using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private float _blinkOffInterval = 0.1f;
    [SerializeField] private float _blinkOnInterval = 0.1f;
    [SerializeField] private string _text;

    private TextMeshProUGUI _textComponent;
    private Coroutine _blinkRoutine;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _textComponent.text = _text;
        _blinkRoutine = StartCoroutine(BlinkRoutine());
    }

    private void OnDisable()
    {
        _textComponent.text = "";
        StopCoroutine(_blinkRoutine);
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            _textComponent.text = _text;
            yield return new WaitForSeconds(_blinkOnInterval);
            _textComponent.text = "";
            yield return new WaitForSeconds(_blinkOffInterval);
        }
    }
}
