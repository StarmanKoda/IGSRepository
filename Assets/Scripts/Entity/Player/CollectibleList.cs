using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleList : MonoBehaviour
{
    public List<String> collected;
    public static CollectibleList instance;
    public void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void addObject(String objectID)
    {
        collected.Add(objectID);
    }

    public Boolean containsObject(String objectID)
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
