using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Spawner : MonoBehaviour {
    public IEnemy objectToSpawn;
    public GameObject objectToSpawnGO;

    public PlayerController player;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    

    private void FixedUpdate() {
        if (currentTimeToSpawn > 0) {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            objectToSpawn = objectToSpawnGO.GetComponent<IEnemy>();
            if ((objectToSpawn.getname() == "Buggy" && player.currentLevel > 40)
                ||(objectToSpawn.getname() == "Robot" && player.currentLevel > 25)
                ||(objectToSpawn.getname() == "LongRange" && player.currentLevel > 10)
                ||(objectToSpawn.getname() == "ShortRange"))
            {
                SpawnObject();
            }

            currentTimeToSpawn = timeToSpawn;
        }
    }
    public void SetTimeBetweenSpawns(float newTime)
    {
        currentTimeToSpawn = newTime;
    }

    public float CalculateNewSpawnTime()
    {
        return Mathf.Max(1f, currentTimeToSpawn - 0.1f);
    }

    public void SpawnObject() {
        objectToSpawn.setHealth(player.currentLevel + 5);
        if (player.currentLevel > 9)
        {
            objectToSpawn.addDamage(player.currentLevel / 5);
        }
        Instantiate(objectToSpawn as MonoBehaviour, transform.position, transform.rotation);
    }
}