using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goodObject;
    [SerializeField] private GameObject badObject;
    [SerializeField] private Rect spawnZone;
    [Range(-1,1)]
    [SerializeField] private float goodBadRatio;
    [SerializeField] private float maxSpawnRatio;
    [SerializeField] private float roundDuration;
    [SerializeField] private AnimationCurve frequencyOverTime;

    private bool isActive = true;

    private int objectsSpwned = 0;
    private int badObjectsSpwned = 0;
    private float currentRoundTime = 0;
    private float nextSpawn = 0f;
    private float lastSpawn = 0f;

    void Update()
    {
        Tick();
    }

    private void Tick()
    {
        if (!isActive) return;

        currentRoundTime += Time.deltaTime;

        if(currentRoundTime > nextSpawn)
        {
            Spawn();
            lastSpawn = currentRoundTime;
            nextSpawn = currentRoundTime + SpawnIntervalAtTime(currentRoundTime/ roundDuration);
        }
       
        if(currentRoundTime >= roundDuration)
        { 
            EndRound();
        }
    }

    private void EndRound()
    {
        print("Round ended!!");
        isActive = false;
    }

    private float SpawnIntervalAtTime(float roundPercentage)
    {
        return (1 - frequencyOverTime.Evaluate(roundPercentage)) * maxSpawnRatio;
    }

    private void Spawn()
    {
        Instantiate(SelectObjectToSpawn(), SelectPositoin(), Quaternion.identity);
        print("Spaaawn!");
    }

    private GameObject SelectObjectToSpawn()
    {
        if (objectsSpwned == 0)
        {
            objectsSpwned++;
            return goodObject;
        }

        float currentRatio = (float)badObjectsSpwned / objectsSpwned;
        if(currentRatio < goodBadRatio)
        {
            badObjectsSpwned++;
           //objectsSpwned = 0;
            return badObject;
        }

        objectsSpwned++;
        return goodObject;
    }


    private Vector3 SelectPositoin()
    {
        return new Vector3(
            UnityEngine.Random.Range(spawnZone.xMin, spawnZone.xMax),
            UnityEngine.Random.Range(spawnZone.yMin, spawnZone.yMax),
            0f
            );
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(spawnZone.center, spawnZone.size);
    }
}
