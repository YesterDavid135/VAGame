using UnityEngine;

public class SpawnerRotation : MonoBehaviour
{
    [SerializeField] private Transform objectToSpin;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float timeToDecreaseSpawnTime = 30;

    private void Update()
    {
        if (objectToSpin != null)
        {
            objectToSpin.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        if (Time.time >= timeToDecreaseSpawnTime)
        {
            DecreaseSpawnTimeForChildren();
            timeToDecreaseSpawnTime = Time.time + (timeToDecreaseSpawnTime * (float)1.6);
        }
    }
    private void DecreaseSpawnTimeForChildren()
    {
        Spawner[] spawners = GetComponentsInChildren<Spawner>();
        foreach (Spawner spawner in spawners)
        {
            spawner.SetTimeBetweenSpawns(spawner.CalculateNewSpawnTime());
        }
    }
}
