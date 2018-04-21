using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

    private GameObject[] hiddingSpot;
    private GameObject[] exits;

    private void Start()
    {
        hiddingSpot = GameObject.FindGameObjectsWithTag("Hidding");
        exits = GameObject.FindGameObjectsWithTag("Exit");
    }

    public  GameObject getNearestThrowableObject(Vector3 position)
    {
        return (getNearestObjectFromList(position, GameObject.FindGameObjectsWithTag("Throwable")));
    }

    public GameObject GetNearestHiddingSpot(Vector3 position)
    {
        return (getNearestObjectFromList(position, hiddingSpot));
    }

    public GameObject GetRandomHiddingSpot(Vector3 position)
    {
        return (getRandomObjectFromList(hiddingSpot));
    }

    public GameObject GetNearestLeavingExit(Vector3 position)
    {
        return (getNearestObjectFromList(position, exits));
    }

    //private usefull methode

    private GameObject getNearestObjectFromList(Vector3 position, GameObject[] list)
    {
        int tMin = 0;
        float minDist = Mathf.Infinity;

        if (list.Length < 1)
            return null;

        for (int i = 0; i < list.Length; i++)
        {
            float dist = Vector3.Distance(list[i].transform.position, position);
            if (dist < minDist)
            {
                tMin = i;
                minDist = dist;
            }
        }

        return (list[tMin]);
    }

    private GameObject getRandomObjectFromList(GameObject[] list)
    {
        if (list.Length < 1)
            return null;

        return (list[Random.Range(0, hiddingSpot.Length)]);
    }
}
