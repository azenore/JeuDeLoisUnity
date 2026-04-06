using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _duration = 30f;

    public UnityEvent OnTimeUp;

    private float _remaining;
    private bool _isRunning;

    [SerializeField] private bool _autoStart = true;

    private void Start()
    {
        if (_autoStart)
            StartTimer();
    }

    public void StartTimer()
    {
        _remaining = _duration;
        _isRunning = true;
        UpdateDisplay();
    }

    
    public void StopTimer()
    {
        _isRunning = false;
    }

    private void Update()
    {
        if (!_isRunning)
            return;

        _remaining -= Time.deltaTime;

        if (_remaining <= 0f)
        {
            _remaining = 0f;
            _isRunning = false;
            UpdateDisplay();
            OnTimeUp?.Invoke();
            return;
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int seconds = Mathf.CeilToInt(_remaining);
        _timerText.text = $"{seconds}";
        _timerText.color = seconds <= 10 ? Color.red : Color.white;
    }
}
