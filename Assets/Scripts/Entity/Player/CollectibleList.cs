using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleList : MonoBehaviour
{
    public List<int> collected;
    public static CollectibleList instance;
    public void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void addObject(int objectID)
    {
        collected.Add(objectID);
    }

    public Boolean containsObject(int objectID)
    {
        return collected.Contains(objectID);
    }

    public int getCollected()
    {
        return collected.Count;
    }

    public static CollectibleList getInstance()
    {
        return instance;
    }
}
