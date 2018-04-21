using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourcentageMaker : MonoBehaviour {

    [SerializeField] AnimationCurve humor;
    [SerializeField] AnimationCurve alcool;

    public float GetPourcentage(float currentHumor, float currentAlcool)
    {
        return ((humor.Evaluate(currentHumor) + alcool.Evaluate(currentAlcool)) / 2);
    }
}
