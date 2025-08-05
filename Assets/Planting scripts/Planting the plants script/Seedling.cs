using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedling : MonoBehaviour
{
    public float growthDuration = 60f;
    private float timer = 0f;
    private bool isGrowing = true;

    void Update()
    {
        if (!isGrowing) return;

        timer += Time.deltaTime;
        if (timer >= growthDuration)
        {
            GrowToMature();
        }
    }

    void GrowToMature()
    {
        isGrowing = false;
        // Replace this object with a mature plant prefab if needed
    }
}

