using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IntervieweeController : MonoBehaviour
{
    [Header("Stress")]
    [SerializeField] private StressScriptableObject _stressConfig;
    [SerializeField] private int _stressOnLying = 20;
    [SerializeField] private int _stressChangeOnSuccessfulAccuse = 30;
    [SerializeField] private int _stressChangeOnFailedAccuse = 0;
    [SerializeField] private int _lookStressedLevel = 60;

    [Header("References")]
    [SerializeField] private ECGGraph _heartRateGraph;
    [SerializeField] private BreathingGraph _breathingGraph;

    public int lyingEyesIndex = -1;
    public int eyeBlinkIndex = -1;
    public Sprite[] beautifulEyes;
    public Image leftEye;
    public Image rightEye;

    private int _permStressLevel;
    private int _tempStressLevel;
    private int _totalStressLevel => _permStressLevel + _tempStressLevel;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();;
    }

    private void Start()
    {
        SetPermStressRate(0);
    }

    public void DecreaseStressRate(int stressRate)
    {
        SetPermStressRate(Mathf.Clamp(_permStressLevel - stressRate, 0, 100));
    }

    public void Accuse(bool success)
    {
        if (success)
        {
            SetPermStressRate(Mathf.Clamp(_permStressLevel + _stressChangeOnSuccessfulAccuse, 0, 100));
            _animator.SetTrigger("Anger");
        } else
        {
            SetPermStressRate(Mathf.Clamp(_permStressLevel - _stressChangeOnFailedAccuse, 0, 100));
        }
    }

    private void SetPermStressRate(int newStressRate)
    {
        _permStressLevel = newStressRate;

        StressRateSet();
    }

    public void SetTempStressRate(int tempStressRate)
    {
        _tempStressLevel = tempStressRate;

        StressRateSet();

    }

    private void StressRateSet()
    {
        var stressData = _stressConfig.StressData.LastOrDefault(
                x => x.StressLevel <= _totalStressLevel);

        _heartRateGraph.SetBPM(stressData.HeartRate);
        _breathingGraph.SetBreathsPerMinute(stressData.BreathingRate);

        if (_totalStressLevel >= _lookStressedLevel)
        {
            _animator.SetBool("Stressed", true);
        } else
        {
            _animator.SetBool("Stressed", false);
        }
    }

    public void AffectStressFromStatement(StatementType _statementType)
    {
        if (_statementType == StatementType.LIE)
        {
            SetTempStressRate(_stressOnLying);

            if (lyingEyesIndex != -1)
            {
                //ChangeBothEyesViaIndex(lyingEyesIndex);
            }
        } else
        {
            SetTempStressRate(0);
        }
    }

    public void ChangeBothEyesViaIndex(int eyeIndex)
    {
        playEyeSFX();
        leftEye.sprite = beautifulEyes[eyeIndex];
        rightEye.sprite = beautifulEyes[eyeIndex];
    }

    private void playEyeSFX()
    {
        AudioManager.Instance.PlayEyeSound();
    }
}
