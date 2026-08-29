using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StressData", menuName = "Data/StressObject", order = 0)]
public class StressScriptableObject : ScriptableObject
{
    [SerializeField] private StressData[] _stressData;

    public StressData[] StressData
    {
        get => _stressData;
    }
}

[Serializable]
public struct StressData
{
    public int StressLevel;
    public float HeartRate;
    public float BreathingRate;

}