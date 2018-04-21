using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    private GameObject[] hiddingSpot;

    private void Start()
    {
        hiddingSpot = GameObject.FindGameObjectsWithTag("Hidding");
    }

    public  GameObject getNearestThrowableObject(Vector3 position)
    {
        int tMin = 0;
        float minDist = Mathf.Infinity;
        GameObject[] throwable = GameObject.FindGameObjectsWithTag("Throwable");
        if (throwable.Length == 0)
            return null;

        for (int i = 0; i < throwable.Length; i++)
        {
            float dist = Vector3.Distance(throwable[i].transform.position, position);
            if (dist < minDist)
            {
                tMin = i;
                minDist = dist;
            }
        }

        return (throwable[tMin]);
    }

    public GameObject GetNearestHiddingSpot(Vector3 position)
    {
        int tMin = 0;
        float minDist = Mathf.Infinity;

        if (hiddingSpot.Length < 1)
            return null;

        for (int i = 0; i < hiddingSpot.Length; i++)
        {
            float dist = Vector3.Distance(hiddingSpot[i].transform.position, position);
            if (dist < minDist)
            {
                tMin = i;
                minDist = dist;
            }
        }

        return (hiddingSpot[tMin]);
    }

    public GameObject GetRandomHiddingSpot(Vector3 position)
    {
        if (hiddingSpot.Length < 1)
            return null;

        return (hiddingSpot[Random.Range(0, hiddingSpot.Length)]);
    }
}
