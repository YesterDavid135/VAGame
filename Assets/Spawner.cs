using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float timeToSpawn;
    private float currentTimeToSpawn;
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position,transform.rotation);
    }
}
