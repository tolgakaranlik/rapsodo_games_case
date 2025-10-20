using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallType1;
    public GameObject BallType2;
    public GameObject BallType3;
    public Transform BallArea3;
    public Transform BallArea2;
    public Transform BallArea1;
    public Transform SpawnRoot;

    public int MinNumberToSpawnBallType1 = 10;
    public int MaxNumberToSpawnBallType1 = 12;
    public int MinNumberToSpawnBallType2 = 7;
    public int MaxNumberToSpawnBallType2 = 9;
    public int MinNumberToSpawnBallType3 = 5;
    public int MaxNumberToSpawnBallType3 = 7;

    public void SpawnTheBalls()
    {
        int spawnFromBalls1 = UnityEngine.Random.Range(MinNumberToSpawnBallType1, MaxNumberToSpawnBallType1 + 1);
        int spawnFromBalls2 = UnityEngine.Random.Range(MinNumberToSpawnBallType1, MaxNumberToSpawnBallType1 + 1);
        int spawnFromBalls3 = UnityEngine.Random.Range(MinNumberToSpawnBallType1, MaxNumberToSpawnBallType1 + 1);

        GameObject go;
        Bounds bounds;
        Collider col = BallArea1.GetComponent<Collider>();
        bounds = col.bounds;

        for (int b1 = 0; b1 < spawnFromBalls1; b1++)
        {
            go = Instantiate(BallType1, SpawnRoot);
            go.transform.position = new Vector3(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), 4, UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
        }

        col = BallArea2.GetComponent<Collider>();
        bounds = col.bounds;
        for (int b2 = 0; b2 < spawnFromBalls2; b2++)
        {
            go = Instantiate(BallType2, SpawnRoot);
            go.transform.position = new Vector3(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), 4, UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
        }

        col = BallArea3.GetComponent<Collider>();
        bounds = col.bounds; for (int b3 = 0; b3 < spawnFromBalls3; b3++)
        {
            go = Instantiate(BallType3, SpawnRoot);
            go.transform.position = new Vector3(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), 4, UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
        }
    }
}
