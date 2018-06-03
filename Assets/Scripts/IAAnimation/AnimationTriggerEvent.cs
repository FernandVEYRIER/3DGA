using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTriggerEvent : MonoBehaviour {

    [SerializeField]
    private UnityEvent action;

    [SerializeField]
    private string[] tags;

    private void OnTriggerEnter(Collider other)
    {
        foreach(string tag in tags)
        {
            if (tag == other.gameObject.tag)
                action.Invoke();
        }
    }
}
