using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasChildren : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!HasActiveChildren(transform))
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }

    private bool HasActiveChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject childObject = parent.GetChild(i).gameObject;

            // Check if the child object is active
            if (childObject.activeSelf)
            {
                return true; // Return true if any active child is found
            }
        }

        return false; // Return false if no active child is found
    }
}
