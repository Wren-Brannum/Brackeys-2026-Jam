using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IntervieweeController : MonoBehaviour
{
    public UnityEvent<float> HeartRateChange = new UnityEvent<float>();

    [Header("Heart rate")]
    [SerializeField] private float _restingHeartRate = 75f;
    [SerializeField] private float _minHeartRate = 60f;
    [SerializeField] private float _maxHeartRate = 160f;

    [SerializeField] private float _dialogueChangeHeartRate = 15f;
    [SerializeField] private float _accuseSuccessChangeHeartRate = 40f;
    [SerializeField] private float _accuseFailureChangeHeartRate = 10f;

    [Header("References")]
    [SerializeField] private ECGGraph _heartRateGraph;

    [Header("Poke")]
    [SerializeField] private float _heartRateIncrease = 5f;
    [SerializeField] private float _pokeDuration = 2f;
    public int lyingEyesIndex = -1;
    public Sprite[] beautifulEyes;
    public Image leftEye;
    public Image rightEye;

    private float _currentHeartRate;

    private Animator _animator;

    private Coroutine _activePoke;

    private void Awake()
    {
        SetHeartRate(_restingHeartRate);

        _animator = GetComponent<Animator>();

        // GetComponent<Button>().onClick.AddListener(Poke);
    }

    private void Poke()
    {
        if (_activePoke != null) 
        {
            return;
        }

        SetHeartRate(Mathf.Min(_currentHeartRate + _heartRateIncrease, _maxHeartRate));

        _activePoke = StartCoroutine(PlayPokeAnimation());
    }

    private IEnumerator PlayPokeAnimation()
    {
        _animator.SetBool("Stressed", true);

        yield return new WaitForSeconds(_pokeDuration);

        _animator.SetBool("Stressed", false);

        _activePoke = null;
    }

    public void IncreaseHeartRate(float heartRate)
    {
        SetHeartRate(Mathf.Min(_currentHeartRate + _heartRateIncrease, _maxHeartRate));
    }

    public void DecreaseHeartRate(float heartRate)
    {
        SetHeartRate(Mathf.Max(_currentHeartRate - _heartRateIncrease, _minHeartRate));
    }

    public void Accuse(bool success)
    {
        if (success)
        {
            IncreaseHeartRate(_accuseSuccessChangeHeartRate);
            _animator.SetTrigger("Stress");
        } else
        {
            DecreaseHeartRate(_accuseFailureChangeHeartRate);
        }
    }

    private void SetHeartRate(float newHeartRate)
    {
        _currentHeartRate = newHeartRate;

        HeartRateChange.Invoke(newHeartRate);
        _heartRateGraph.SetBPM(newHeartRate);
    }

    public void AffectStressFromStatement(StatementType _statementType)
    {
        if (_statementType == StatementType.TRUTH)
        {
            DecreaseHeartRate(_dialogueChangeHeartRate);
        } else if (_statementType == StatementType.LIE)
        {
            IncreaseHeartRate(_dialogueChangeHeartRate);
        }
        if(lyingEyesIndex != -1)
        {
            ChangeBothEyesViaIndex(lyingEyesIndex);
        }
    }
    public void ChangeBothEyesViaIndex(int eyeIndex)
    {
        leftEye.sprite = beautifulEyes[eyeIndex];
        rightEye.sprite = beautifulEyes[eyeIndex];
    }
}
