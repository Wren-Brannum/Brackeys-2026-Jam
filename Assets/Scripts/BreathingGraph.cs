using UnityEngine;
using UnityEngine.UI;

public class BreathingGraph : MaskableGraphic
{
    [Header("Display")]
    [SerializeField] private float _scrollSpeed = 100f;
    [SerializeField, Range(0f, 1f)] private float _amplitude = 0.6f;
    [SerializeField] private float _lineThickness = 2f;
    [SerializeField] private int _samples = 500;

    [Header("Breathing")]
    [SerializeField] private float _breathsPerMinute = 12f;
    private float _pendingBreathsPerMinute;

    private float[] _values;

    private float _samplePosition;
    private float _breathingTime;

    private float _breathingDuration => 60f / _breathsPerMinute;

    protected override void Awake()
    {
        base.Awake();

        _values = new float[_samples];

        for (int i = 0; i < _samples; i++)
        {
            _values[i] = 0f;
        }

        _pendingBreathsPerMinute = _breathsPerMinute;
    }

    private void Update()
    {
        float width = rectTransform.rect.width;

        if (width <= 0)
        {
            return;
        }

        float pixelsPerSample = width / (_samples - 1);

        _samplePosition += _scrollSpeed * Time.deltaTime;

        while (_samplePosition >= pixelsPerSample)
        {
            _samplePosition -= pixelsPerSample;

            for (int i = 0; i < _samples - 1; i++)
            {
                _values[i] = _values[i + 1];
            }

            _values[_samples - 1] = GetBreathingValue(_breathingTime);

            _breathingTime += pixelsPerSample / _scrollSpeed;

            if (_breathingTime >= _breathingDuration)
            {
                _breathingTime -= _breathingDuration;

                if (_pendingBreathsPerMinute > 0f)
                {
                    _breathsPerMinute = _pendingBreathsPerMinute;
                    _pendingBreathsPerMinute = 0f;
                }
            }

            // Play breathing sound at the start of each inhale
            if (_breathingTime < pixelsPerSample / _scrollSpeed)
            {
                if (AudioManager.Instance)
                {
                    AudioManager.Instance.PlayInhale();
                }
            }
            // Play breathing sound at the start of each exhale
            else if (_breathingTime >= _breathingDuration / 2f && _breathingTime < _breathingDuration / 2f + pixelsPerSample / _scrollSpeed)
            {
                if (AudioManager.Instance)
                {
                    AudioManager.Instance.PlayExhale();
                }
            }
        }

        SetVerticesDirty();
    }

    private float GetBreathingValue(float time)
    {
        float normalized = time / _breathingDuration;

        return Mathf.Sin(normalized * Mathf.PI * 2f);
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (_values == null || _values.Length < 2)
        {
            return;
        }

        float width = rectTransform.rect.width;

        float xStep = width / (_samples - 1);

        float height = rectTransform.rect.height;
        float amplitude = height * (_amplitude / 2f);

        for (int i = 0; i < _samples - 1; i++)
        {
            float x1 = -width / 2f + i * xStep;
            float x2 = -width / 2f + (i + 1) * xStep;

            float y1 = _values[i] * amplitude;
            float y2 = _values[i + 1] * amplitude;

            Vector2 p1 = new Vector2(x1, y1);
            Vector2 p2 = new Vector2(x2, y2);

            AddLine(vh, p1, p2);
        }
    }

    private void AddLine(VertexHelper vh, Vector2 p1, Vector2 p2)
    {
        Vector2 direction = (p2 - p1).normalized;

        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        Vector2 offset = perpendicular * _lineThickness * 0.5f;

        int index = vh.currentVertCount;

        UIVertex vertex = UIVertex.simpleVert;

        vertex.color = color;

        vertex.position = p1 + offset;
        vh.AddVert(vertex);

        vertex.position = p1 - offset;
        vh.AddVert(vertex);

        vertex.position = p2 - offset;
        vh.AddVert(vertex);

        vertex.position = p2 + offset;
        vh.AddVert(vertex);

        vh.AddTriangle(index, index + 1, index + 2);
        vh.AddTriangle(index, index + 2, index + 3);
    }

    public void SetBreathsPerMinute(float newBreathsPerMinute)
    {
        _pendingBreathsPerMinute = Mathf.Max(1f, newBreathsPerMinute);
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _samples = Mathf.Max(2, _samples);
        _breathsPerMinute = Mathf.Max(1f, _breathsPerMinute);

        if (_values == null || _values.Length != _samples)
        {
            _values = new float[_samples];
        }

        SetVerticesDirty();
    }
}