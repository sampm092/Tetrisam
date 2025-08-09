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
    private int RowErased = 0;
    int randomIndex;
    private AudioSource Sonsistem;
    public AudioClip OneRow;
    public AudioClip TwoRow;
    public AudioClip ThreeRow;
    public AudioClip FourRow;

    void Start()
    {
        SpawnTet();
    }

    [ContextMenu("Increase Score")]
    public void AddScore()
    {
        Sonsistem = FindObjectOfType<AudioSource>();
        switch (RowErased)
        {
            case 1:
                score += 100;
                if (Sonsistem != null && OneRow != null)
                {
                    Sonsistem.PlayOneShot(OneRow);
                }
                break;
            case 2:
                score += 300;

                if (Sonsistem != null && TwoRow != null)
                {
                    Sonsistem.PlayOneShot(TwoRow);
                }
                break;
            case 3:
                score += 600;

                if (Sonsistem != null && ThreeRow != null)
                {
                    Sonsistem.PlayOneShot(ThreeRow);
                }
                break;
            case 4:
                score += 1000;

                if (Sonsistem != null && FourRow != null)
                {
                    Sonsistem.PlayOneShot(FourRow);
                }
                break;
        }

        scoreText.text = score.ToString();
        RowErased = 0;
    }

    public void AddScoreDrop(int x)
    {
        score += x;
        scoreText.text = score.ToString();
        RowErased = 0;
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
        // RowErased++;
        return true;
    }

    string RandomTetroName()
    {
        int maxRange = 8; //usual blocks

        if (score > 12000) maxRange = 14; //harder stage
        else if (score > 9000) maxRange = 13;
        else if (score > 7500) maxRange = 11;
        else if (score > 5000) maxRange = 10;

        randomIndex = UnityEngine.Random.Range(1, maxRange);
        string RandomTetroFilename = "";
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
                RandomTetroFilename = "ThreeL";
                break;
            case 9:
                RandomTetroFilename = "Elbow";
                break;
            case 10:
                RandomTetroFilename = "TwoL";
                break;
            case 11:
                RandomTetroFilename = "U";
                break;
            case 12:
                RandomTetroFilename = "Dot";
                break;
            case 13:
                RandomTetroFilename = "Cross";
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
                RowErased++;
                // AddScore();
            }
        }
        if (RowErased > 0)
        {
            AddScore();
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
