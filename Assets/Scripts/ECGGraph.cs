using UnityEngine;
using UnityEngine.UI;

public class ECGGraph : MaskableGraphic
{
    [Header("Display")]
    [SerializeField] private float _scrollSpeed = 100f;
    [SerializeField] private float _amplitude = 40f;
    [SerializeField] private float _lineThickness = 2f;
    [SerializeField] private int _samples = 500;

    [Header("Heart")]
    [SerializeField] private float _bpm = 75f;

    private float[] _values;

    private float _samplePosition;
    private float _heartbeatTime;

    float _heartbeatDuration => 60f / _bpm;

    protected override void Awake()
    {
        base.Awake();

        _values = new float[_samples];

        for (int i = 0; i < _samples; i++)
        {
            _values[i] = 0f;
        }
    }

    protected override void Start()
    {
        base.Start();
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
                _values[i] = _values[i+1];
            }

            _values[_samples - 1] = GetHeartbeatValue(_heartbeatTime);

            _heartbeatTime += pixelsPerSample / _scrollSpeed;

            if (_heartbeatTime >= _heartbeatDuration)
            {
                _heartbeatTime -= _heartbeatDuration;
            }
        }

        SetVerticesDirty();
    }

    private float GetHeartbeatValue(float time)
    {
        float normalized = time / _heartbeatDuration;

        // P wave
        if (normalized >= 0.20f && normalized < 0.27f)
        {
            float t = (normalized - 0.20f) / 0.07f;

            return Mathf.Sin(t * Mathf.PI) * 0.2f;
        }

        // Q wave
        if (normalized >= 0.32f && normalized < 0.35f)
        {
            float t = (normalized - 0.32f) / 0.03f;

            return Mathf.Lerp(0f, -0.25f, t);
        }

        // R wave
        if (normalized >= 0.35f && normalized < 0.38f)
        {
            float t = (normalized - 0.35f) / 0.03f;

            return Mathf.Lerp(-0.25f, 1f, t);
        }

        // S wave
        if (normalized >= 0.38f && normalized < 0.42f)
        {
            float t = (normalized - 0.38f) / 0.04f;

            return Mathf.Lerp(1f, -0.35f, t);
        }

        // Return to baseline
        if (normalized >= 0.42f && normalized < 0.48f)
        {
            float t = (normalized - 0.42f) / 0.06f;

            return Mathf.Lerp(-0.35f, 0f, t);
        }

        // T wave
        if (normalized >= 0.55f && normalized < 0.68f)
        {
            float t = (normalized - 0.55f) / 0.13f;

            return Mathf.Sin(t * Mathf.PI) * 0.3f;
        }

        return 0f;
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

        for (int i = 0; i < _samples - 1; i++) 
        {
            float x1 = -width / 2f + i * xStep;
            float x2 = -width / 2f + (i + 1) * xStep;

            float y1 = _values[i] * _amplitude;
            float y2 = _values[i + 1] * _amplitude;

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

    public void SetBPM(float newBpm)
    {
        _bpm = Mathf.Max(1f, newBpm);
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _samples = Mathf.Max(2, _samples);

        if (_values == null || _values.Length != _samples)
        {
            _values = new float[_samples];
        }

        SetVerticesDirty();
    }
}
