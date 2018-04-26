using UnityEngine;
using System.Collections;

public class AIComportement : MonoBehaviour
{
    [SerializeField]
    ActionEnum.Action[] action;

    [SerializeField] AnimationCurve[] humor;
    [SerializeField] AnimationCurve[] alcool;

    public float GetPourcentage(ActionEnum.Action type, float currentHumor, float currentAlcool)
    {
        int actionNbr = 0;
        while (actionNbr < action.Length && type != action[actionNbr])
            actionNbr++;
        if (actionNbr >= action.Length)
            return 0.0f;
        return ((humor[actionNbr].Evaluate(currentHumor) + alcool[actionNbr].Evaluate(currentAlcool)) / 2);
    }
}
