﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementManager : MonoBehaviour {

    [SerializeField]
    private UnityEvent AIdieEvent;

    private GameObject[] hiddingSpot;
    private GameObject[] exits;
    private GameObject[] danceFloors;
    private GameObject[] dartGames;
    private GameObject[] bottlePile;
    
    private void Start()
    {
        hiddingSpot = GameObject.FindGameObjectsWithTag("Hidding");
        exits = GameObject.FindGameObjectsWithTag("Exit");
        danceFloors = GameObject.FindGameObjectsWithTag("DanceFloor");
        dartGames = GameObject.FindGameObjectsWithTag("DartGame");
        bottlePile = GameObject.FindGameObjectsWithTag("BottlePile");
    }

    public void AIdie()
    {
        AIdieEvent.Invoke();
    }

    public Vector3 getPlaceToPlayDart(Vector3 position)
    {
        return getPositionAroundCollider(position, dartGames);
    }

    public Vector3 getPlaceToDance(Vector3 position)
    {
        return getPositionAroundCollider(position, danceFloors);
    }

    public  GameObject getNearestThrowableObject(Vector3 position)
    {
        GameObject pile = getNearestObjectFromList(position, bottlePile);
        GameObject bottle = getNearestObjectFromList(position, GameObject.FindGameObjectsWithTag("Throwable"));
        if (pile == null)
            return bottle;
        if (bottle == null)
            return pile;
        return Vector3.Distance(pile.transform.position, position) < Vector3.Distance(bottle.transform.position, position) ? pile : bottle;
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

    private Vector3 getPositionAroundCollider(Vector3 position, GameObject[] list)
    {
        GameObject go = getNearestObjectFromList(position, list);
        if (go == null)
            return Vector3.up;
        float x = go.GetComponent<BoxCollider>().size.x / 2;
        float z = go.GetComponent<BoxCollider>().size.z / 2;
        Vector3 offset = go.transform.position + go.GetComponent<BoxCollider>().center;
        return new Vector3(offset.x + Random.Range(-x, x), offset.y, offset.z + Random.Range(-z, z));
    }

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
