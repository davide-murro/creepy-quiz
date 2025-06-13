using System;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [NonSerialized] public float minValue = 0f;
    [NonSerialized] public float maxValue = 10f;
    // progress bar values
    float value = 0f;


    // update timer
    public void IncrementProgress()
    {
        value++;
    }
    // get value
    public float GetValue()
    {
        return value;
    }
}
