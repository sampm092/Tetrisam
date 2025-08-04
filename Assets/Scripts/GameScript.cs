using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static int gridWidth = 10;
    public static int gridHeight = 20;
    public Transform[,] grid = new Transform[gridWidth, gridHeight];

    void Start()
    {
        SpawnTet();
    }

    void Update()
    {

    }

    public void updateGrid(TetroScript tetroScript)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetroScript)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetroScript.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPos(Vector2 pos)
    {
        if (pos.y > gridHeight -1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public void SpawnTet()
    {
        Vector3 spawnPosition = new Vector3(5f, 20f, 1f); //not using Vector2 anymore
        Quaternion spawnRotation = Quaternion.identity; //for No Rotation 
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + RandomTetroName()); //taking random prefab from Resources folder

        GameObject spawnTetrisam = Instantiate(prefab, spawnPosition, spawnRotation); //Instantiate the random prefab
    }

    string RandomTetroName()
    {
        int randomIndex = UnityEngine.Random.Range(1, 8);
        string RandomTetroFilename = "J";
        switch (randomIndex)
        {
            case 1:
                RandomTetroFilename = "J";
                break;
            case 2:
                RandomTetroFilename = "L";
                break;
            case 3:
                RandomTetroFilename = "Long";
                return RandomTetroFilename;
            case 4:
                RandomTetroFilename = "S";
                break;
            case 5:
                RandomTetroFilename = "Z";
                break;
            case 6:
                RandomTetroFilename = "Square";
                break;
            case 7:
                RandomTetroFilename = "T";
                break;
            case 8:
                RandomTetroFilename = "Cross";
                break;
            case 9:
                RandomTetroFilename = "U";
                break;
        }
        return RandomTetroFilename;

    }

    public bool CheckInsideStage(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= -9); // check if the object is inside grid/stage
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y)); //make sure its a round number
    }
}
