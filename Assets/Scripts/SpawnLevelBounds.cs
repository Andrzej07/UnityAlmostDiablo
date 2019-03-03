using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelBounds : MonoBehaviour {
    public GameObject wallPrefab;

    private float wallHeight = 100f;
    private void Awake()
    {
        float sizeX = transform.localScale.x*10;
        float sizeY = transform.localScale.z*10;
        SpawnWall(new Vector3(sizeX*1.1f, 1, wallHeight), new Vector3(0, 0, -sizeY/2));
        SpawnWall(new Vector3(sizeX*1.1f, 1, wallHeight), new Vector3(0, 0, sizeY/2));
        SpawnWall(new Vector3(1, sizeY*1.1f, wallHeight), new Vector3(sizeX/2, 0, 0));
        SpawnWall(new Vector3(1, sizeY*1.1f, wallHeight), new Vector3(-sizeX/2, 0, 0));
    }

    private void SpawnWall(Vector3 size, Vector3 position)
    {
        GameObject wall = Instantiate(wallPrefab, transform.parent);
        wall.transform.localScale = size;
        wall.transform.position = position;
        wall.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
}
