﻿using UnityEngine;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour
{
    [Header("Biome Setup")]
    public int selectBiomeSet;
    public BiomeSet[] availableBiomes;

    private BiomeSet biomeSet;

    [Header("Level Builder Objects")]
    public GameObject blankPlane;

    //Cached References
    private Material roadMat;
    private Material wallMat;
    private Material groundMat;
    private GameObject horizonObj;
    private GameObject[] wallProps;
    private GameObject[] archProps;
    private GameObject[] groundProps;
    private GameObject[] obstacles;
    private GameObject[] enemies;

    //Level
    private GameObject level;

    [Header("Level Properties")]
    public float levelLength;
    public float startLevelBuffer;
    public float endLevelBuffer;

    [Header("Road Properties")]
    public bool generateRoad = true;
    public float roadWidth;
    public float roadWidthBuffer;

    [Header("Ground Properties")]
    public bool generateGround;

    [Header("Wall Properties")]
    public bool generateWalls;
    public float wallPropSpacing;
    public float wallPropOffset;
    [Range(0, 1)]
    public float archToWallRatio;

    [Header("Horizon Object Properties")]
    public bool generateHorizon;
    public float horizonZPos;

    [Header("Ground Prop Properties")]
    public bool generateGroundProps;
    public float groundPropSpacing;
    public float minGroundPropOffset;
    public float maxGroundPropOffset;

    [Header("Obstacle Properties")]
    public bool generateObstacles;
    public int obstacleSpawnBuffer;
    [Range(0, 1)]
    public float obstacleSpawnRate;
    public int maxObstaclesPerRow;

    [Header("Enemy Properties")]
    public bool generateEnemies;
    public int enemySpawnBuffer;
    public float enemySpawnXOffset;
    [Range(0, 1)]
    public float enemySpawnRate;


    public void Init()
    {
        biomeSet = availableBiomes[selectBiomeSet];

        roadMat = biomeSet.roadMaterial;
        wallMat = biomeSet.wallMaterial;
        groundMat = biomeSet.groundMaterial;
        horizonObj = biomeSet.horizonObject;
        wallProps = biomeSet.wallProps;
        archProps = biomeSet.archProps;
        groundProps = biomeSet.groundProps;
        obstacles = biomeSet.obstacles;
        enemies = biomeSet.enemies;

        BuildLevel(levelLength, roadWidth, Grid.instance);
    }

    void BuildLevel(float levelLength, float roadWidth, Grid targetGrid)
    {
        BuildRoad(levelLength + startLevelBuffer + endLevelBuffer, roadWidth);
        BuildGround(levelLength + startLevelBuffer + endLevelBuffer);
        BuildWalls(levelLength + startLevelBuffer + endLevelBuffer, roadWidth + roadWidthBuffer);
        SpawnGroundProps(levelLength, roadWidth + roadWidthBuffer);
        SpawnHorizonObject(levelLength, targetGrid);
        SpawnObstacles(levelLength, targetGrid);
        SpawnEnemies(levelLength, roadWidth, targetGrid);
    }

    void BuildRoad(float length, float width)
    {
        if(roadMat && generateRoad)
        {
            GameObject newRoad = Instantiate(blankPlane);

            Vector3 scale = new Vector3(length, width, 1);
            newRoad.transform.localScale = scale;
            newRoad.transform.position = new Vector3(0, -0.25f, 0);
            newRoad.transform.rotation = Quaternion.Euler(90, 90, 0);

            Material newRoadMat = new Material(roadMat);
            Vector2 texScale = newRoadMat.mainTextureScale;
            texScale.x *= length;
            newRoadMat.mainTextureScale = texScale;

            newRoad.GetComponent<MeshRenderer>().material = newRoadMat;

            Vector3 adjPos = newRoad.transform.position;
            adjPos.z += length / 2;
            newRoad.transform.position = adjPos;
        }
        else
        {
            Debug.LogError("No Road Material Selected, Robot Be Flyin'");
        }
    }

    void BuildGround(float length)
    {
        if (groundMat && generateGround)
        {
            GameObject newGround = Instantiate(blankPlane);

            Vector3 scale = new Vector3(length * 2, length * 2, length * 2);
            newGround.transform.localScale = scale;
            newGround.transform.position = new Vector3(0, -1.5f, 0);
            newGround.transform.rotation = Quaternion.Euler(90, 0, 0);

            Material newGroundMat = new Material(groundMat);
            Vector2 texScale = newGroundMat.mainTextureScale;
            texScale *= length;
            newGroundMat.mainTextureScale = texScale;
            newGround.GetComponent<MeshRenderer>().material = newGroundMat;
        }
    }
    void BuildWalls(float length, float roadWidth)
    {
        if(wallMat && generateWalls)
        {
            GameObject leftWall = Instantiate(blankPlane);
            GameObject rightWall = Instantiate(blankPlane);

            Vector3 scale = new Vector3(length, 45, 1);

            leftWall.transform.localScale = scale;
            rightWall.transform.localScale = scale;

            leftWall.transform.rotation = Quaternion.Euler(0, -90, 0);
            rightWall.transform.rotation = Quaternion.Euler(0, 90, 0);

            leftWall.transform.position = new Vector3(-roadWidth / 2, 22, 0);
            rightWall.transform.position = new Vector3(roadWidth / 2, 22, 0);
            Material newWallMat = new Material(wallMat);
            Vector2 texScale = newWallMat.mainTextureScale;
            texScale.x *= length;
            newWallMat.mainTextureScale = texScale;

            leftWall.GetComponent<MeshRenderer>().material = newWallMat;
            rightWall.GetComponent<MeshRenderer>().material = newWallMat;

            Vector3 adjPos = leftWall.transform.position;
            adjPos.z += length / 2;
            leftWall.transform.position = adjPos;

            adjPos = rightWall.transform.position;
            adjPos.z += length / 2;
            rightWall.transform.position = adjPos;
        }
        SpawnWallProps(length, roadWidth);
    }
    void SpawnHorizonObject(float length, Grid grid)
    {
        if(horizonObj && generateHorizon)
        {
            GameObject horizon = Instantiate(horizonObj);
            Vector3 startPosition = new Vector3(0, 0, horizonZPos);
            horizon.GetComponent<HorizonController>().Init(startPosition, grid.transform);
        }
    }
    void SpawnWallProps(float length, float roadWidth)
    {
        if (wallProps.Length > 0 && generateWalls)
        {
            int total = Mathf.RoundToInt(length / wallPropSpacing);

            for (int i = 0; i <= total; ++i)
            {
                if (archProps.Length > 0)
                {
                    float type = Random.value;
                    if (type > archToWallRatio)
                    {
                        int select = Random.Range(0, wallProps.Length);
                        GameObject left = Instantiate(wallProps[select]);
                        float xPos = -roadWidth / 2;
                        float offset = xPos + Random.Range(-wallPropOffset, wallPropOffset);
                        left.transform.position = new Vector3(offset, 20, wallPropSpacing * i);
                        left.transform.rotation = Quaternion.Euler(0, 180, 0);

                        select = Random.Range(0, wallProps.Length);
                        GameObject right = Instantiate(wallProps[select]);
                        xPos = roadWidth / 2;
                        offset = xPos + Random.Range(-wallPropOffset, wallPropOffset);
                        right.transform.position = new Vector3(offset, 20, wallPropSpacing * i);
                    }
                    else
                    {
                        int select = Random.Range(0, archProps.Length);
                        GameObject arch = Instantiate(archProps[select]);
                        Vector3 newScale = arch.transform.localScale;
                        newScale.x = roadWidth/2;
                        arch.transform.localScale = newScale;
                        arch.transform.position = new Vector3(0, 0, wallPropSpacing * i);
                    }
                }
                else
                {
                    int select = Random.Range(0, wallProps.Length);
                    GameObject left = Instantiate(wallProps[select]);
                    float xPos = -roadWidth / 2;
                    float offset = xPos + Random.Range(-wallPropOffset, wallPropOffset);
                    left.transform.position = new Vector3(offset, 20, wallPropSpacing * i);
                    left.transform.rotation = Quaternion.Euler(0, 180, 0);

                    select = Random.Range(0, wallProps.Length);
                    GameObject right = Instantiate(wallProps[select]);
                    xPos = roadWidth / 2;
                    offset = xPos + Random.Range(-wallPropOffset, wallPropOffset);
                    right.transform.position = new Vector3(offset, 20, wallPropSpacing * i);
                }

            }
        }
    }
    void SpawnGroundProps(float length, float width)
    {
       if(groundProps.Length > 0 && generateGroundProps)
        {
            int total = Mathf.RoundToInt(levelLength / groundPropSpacing);

            for (int i = 0; i <= total; ++i)
            {
                int select = Random.Range(0, groundProps.Length);
                GameObject left = Instantiate(groundProps[select]);
                float xPos = -roadWidth / 2;
                float offset = xPos + Random.Range(-maxGroundPropOffset, -minGroundPropOffset);
                left.transform.position = new Vector3(offset, 0, groundPropSpacing * i);

                select = Random.Range(0, groundProps.Length);
                GameObject right = Instantiate(groundProps[select]);
                xPos = roadWidth / 2;
                offset = xPos + Random.Range(minGroundPropOffset, maxGroundPropOffset);
                right.transform.position = new Vector3(offset, 0, startLevelBuffer + groundPropSpacing * i);
            }
        }

    }
    void SpawnObstacles(float length, Grid grid)
    {
        if (obstacles.Length > 0 && generateObstacles)
        {
            float[] gridXPoints = new float[grid.xUnits];

            for (int i = 0; i < gridXPoints.Length; i++)
            {
                gridXPoints[i] = grid.gridUnits[i, 0].position.x;
            }

            int totalRows = Mathf.RoundToInt(length / grid.unitSize);

            for (int i = 0; i < totalRows; ++i)
            {
                float canSpawn = i % obstacleSpawnBuffer;

                if (canSpawn == 0)
                {
                    float chance = Random.value;
                    if (chance <= obstacleSpawnRate)
                    {
                        int amount = Random.Range(1, maxObstaclesPerRow + 1);
                        int[] slots = new int[amount];
                        for (int j = 0; j < slots.Length; ++j)
                        {
                            int pos = Random.Range(1, 6);
                            for (int k = 0; k < slots.Length; ++k)
                            {
                                if (slots[k] == pos)
                                {
                                    pos = Random.Range(1, 6);
                                    k = -1;
                                }
                            }
                            slots[j] = pos;
                        }
                        for (int l = 0; l < slots.Length; ++l)
                        {
                            int select = Random.Range(0, obstacles.Length);
                            GameObject newObs = Instantiate(obstacles[select]);
                            float xPos = gridXPoints[slots[l] - 1];
                            float zPos = startLevelBuffer + grid.unitSize * i;
                            newObs.transform.position = new Vector3(xPos, 2, zPos);
                        }
                    }
                }
            }
        }
    }
    void SpawnEnemies(float length, float width, Grid grid)
    {
        if (enemies.Length > 0 && generateEnemies)
        {
            int totalRows = Mathf.RoundToInt(length / grid.unitSize);

            for (int i = 0; i < totalRows; ++i)
            {
                float canSpawn = i % enemySpawnBuffer;

                if (canSpawn == 0)
                {
                    float chance = Random.value;
                    if (chance <= enemySpawnRate)
                    {
                        float side = Random.value;
                        float xPos = width / 2;
                        if (side <= .5f)
                        {
                            xPos = -xPos - enemySpawnXOffset;
                        }
                        else
                        {
                            xPos = xPos + enemySpawnXOffset;
                        }

                        int select = Random.Range(0, enemies.Length);
                        GameObject newEne = Instantiate(enemies[select]);
                        float zPos = startLevelBuffer + grid.unitSize * i;
                        newEne.transform.position = new Vector3(xPos, 2, zPos);
                        newEne.GetComponent<Enemy>().Init();
                    }
                }
            }
        }
    }
}
