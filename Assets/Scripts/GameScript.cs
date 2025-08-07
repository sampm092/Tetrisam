using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static int gridWidth = 10;
    public static int gridHeight = 20;
    public Transform[,] grid = new Transform[gridWidth, gridHeight]; //creates a 2D array of Transform which contains all coordinate from [0,1] to [9,19]
    public int score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        SpawnTet();
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = score.ToString();
    }

    public bool AboveGrid(TetroScript tetro)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            foreach (Transform mino in tetro.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 1) //checking if the object already oustide (y) the stage
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AllRowDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
        {
            RowDownAftErased(i);
        }
    }

    public bool CheckInsideStage(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 1); // check if the object is inside grid/stage
    }

    public void EraseRow(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public Transform GetTransformAtGridPos(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        if (x < 0 || x >= gridWidth || y < 1 || y >= gridHeight) //make sure the mino inside the valid position of the stage left x, right gridwidth, top gridheight, y 1
        {
            return null;
        }
        else
        {
            return grid[x, y];
        }
    }

    public bool isRowFull(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null) //check every x position in y, if theres null, return false
            {
                return false;
            }
        }
        return true;
    }

    string RandomTetroName()
    {
        int randomIndex = UnityEngine.Random.Range(1, 8); //the usual one
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
                break;
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

    public void RemoveRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (isRowFull(y))
            {
                EraseRow(y);
                AllRowDown(y + 1);
                y--;
                AddScore(100);
            }
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Level");
    }

    public Vector2 Round(Vector2 pos) //get mino [x,y] position and round it
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y)); //make sure its a round number
    }

    public void RowDownAftErased(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void SpawnTet()
    {
        Vector3 spawnPosition = new Vector3(5f, 22f, 1f); //not using Vector2 anymore
        Quaternion spawnRotation = Quaternion.identity; //for No Rotation
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + RandomTetroName()); //taking random prefab from Resources folder

        GameObject spawnTetrisam = Instantiate(prefab, spawnPosition, spawnRotation); //Instantiate the random prefab
    }

    public void updateGrid(TetroScript tetroScript)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y] != null && grid[x, y].parent == tetroScript.transform) //each = block, parent = prefab
                {
                    grid[x, y] = null; // making sure the old position cleared so theres no collision or any trace
                }
            }
        }
        foreach (Transform mino in tetroScript.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino; //make sure that position already occupied
            }
        }
    }
}
