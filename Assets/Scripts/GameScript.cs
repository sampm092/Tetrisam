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
    public int currentLevel = 0;
    public int numLinesCleared = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public float displayDuration = 2f; // Seconds
    private int RowErased = 0;
    private int startingHighScore;
    private int startingHighScore2;
    private int startingHighScore3;
    private int startingHighScore4;
    int randomIndex;
    private AudioSource Sonsistem;
    public AudioClip OneRow;
    public AudioClip TwoRow;
    public AudioClip ThreeRow;
    public AudioClip FourRow;
    public AudioClip Drop;
    public float fallSpeed = 1.0f;
    private GameObject previewMino;
    private GameObject spawnTetrisam;
    public GameObject PauseMenu;
    public GameObject newTetro;
    public GameObject newTetro2;
    public GameObject newTetro3;
    public GameObject newTetro4;
    public bool isPaused = false;
    private bool gamestarted = false;
    private bool popupShown1 = false;
    private bool popupShown2 = false;
    private bool popupShown3 = false;
    private bool popupShown4 = false;
    private Vector3 prevPlace = new Vector3(15f, 15f, 1f);

    void Start()
    {
        updateSpeed();
        SpawnTet();
        startingHighScore = PlayerPrefs.GetInt("highscore");
        startingHighScore2 = PlayerPrefs.GetInt("highscore2");
        startingHighScore3 = PlayerPrefs.GetInt("highscore3");
        startingHighScore4 = PlayerPrefs.GetInt("highscore4");
    }

    void Update()
    {
        updateLevel();
        updateSpeed();

        if (score >= 5000 && !popupShown1)
        {
            DisplayPopup1();
            popupShown1 = true; // prevent repeat
        }
        else if (score >= 7500 && !popupShown2)
        {
            DisplayPopup2();
            popupShown2 = true; // prevent repeat
        }
        else if (score >= 9000 && !popupShown3)
        {
            DisplayPopup3();
            popupShown3 = true; // prevent repeat
        }
        else if (score >= 12000 && !popupShown4)
        {
            DisplayPopup4();
            popupShown4 = true; // prevent repeat
        }
    }

    [ContextMenu("Increase Score")]
    public void AddScore()
    {
        Sonsistem = FindObjectOfType<AudioSource>();
        switch (RowErased)
        {
            case 1:
                score += 100 + (currentLevel * 10);
                numLinesCleared++;
                if (Sonsistem != null && OneRow != null)
                {
                    Sonsistem.PlayOneShot(OneRow);
                }
                break;
            case 2:
                score += 300 + (currentLevel * 15);
                numLinesCleared += 2;
                if (Sonsistem != null && TwoRow != null)
                {
                    Sonsistem.PlayOneShot(TwoRow);
                }
                break;
            case 3:
                score += 600 + (currentLevel * 25);
                numLinesCleared += 3;
                if (Sonsistem != null && ThreeRow != null)
                {
                    Sonsistem.PlayOneShot(ThreeRow);
                }
                break;
            case 4:
                score += 1000 + (currentLevel * 45);
                numLinesCleared += 4;
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
        if (Sonsistem != null && Drop != null)
        {
            Sonsistem.PlayOneShot(Drop);
        }
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

    public void DisplayPopup1()
    {
        StopAllCoroutines(); // Prevent overlap if already running
        StartCoroutine(DisplayActivePopup(newTetro));
    }

    public void DisplayPopup2()
    {
        StopAllCoroutines(); // Prevent overlap if already running
        StartCoroutine(DisplayActivePopup(newTetro2));
    }

    public void DisplayPopup3()
    {
        StopAllCoroutines(); // Prevent overlap if already running
        StartCoroutine(DisplayActivePopup(newTetro3));
    }

    public void DisplayPopup4()
    {
        StopAllCoroutines(); // Prevent overlap if already running
        StartCoroutine(DisplayActivePopup(newTetro4));
    }

    private System.Collections.IEnumerator DisplayActivePopup(GameObject TheTetro)
    {
        TheTetro.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        TheTetro.SetActive(false);
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

        if (score > 12000)
            maxRange = 14; //harder stage, till 13
        else if (score > 9000)
            maxRange = 13; //till 12
        else if (score > 7500)
            maxRange = 11; // till 10
        else if (score > 5000)
            maxRange = 10; //till 9

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
                RandomTetroFilename = "U";
                break;
            case 9:
                RandomTetroFilename = "Elbow";
                break;
            case 10:
                RandomTetroFilename = "TwoL";
                break;
            case 11:
                RandomTetroFilename = "ThreeL";
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
        Sonsistem = FindObjectOfType<AudioSource>();
        if (Sonsistem != null && Drop != null)
        {
            Sonsistem.PlayOneShot(Drop);
        }
        Vector3 spawnPosition = new Vector3(5f, 22f, 1f); //not using Vector2 anymore
        Quaternion spawnRotation = Quaternion.identity; //for No Rotation
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + RandomTetroName()); //taking random prefab from Resources folder
        if (!gamestarted)
        {
            gamestarted = true;
            spawnTetrisam = Instantiate(prefab, spawnPosition, spawnRotation); //Instantiate the random prefab
            previewMino = Instantiate(prefab, prevPlace, spawnRotation);
            previewMino.GetComponent<TetroScript>().enabled = false;
        }
        else
        {
            previewMino.transform.localPosition = spawnPosition;
            spawnTetrisam = previewMino;
            spawnTetrisam.GetComponent<TetroScript>().enabled = true;
            previewMino = Instantiate(prefab, prevPlace, spawnRotation);
            previewMino.GetComponent<TetroScript>().enabled = false;
        }
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

    public void UpdateHighscore()
    {
        if (score > startingHighScore)
        {
            PlayerPrefs.SetInt("highscore4", startingHighScore3);
            PlayerPrefs.SetInt("highscore3", startingHighScore2);
            PlayerPrefs.SetInt("highscore2", startingHighScore);
            PlayerPrefs.SetInt("highscore", score);
        }
        else if (score > startingHighScore2)
        {
            PlayerPrefs.SetInt("highscore4", startingHighScore3);
            PlayerPrefs.SetInt("highscore3", startingHighScore2);
            PlayerPrefs.SetInt("highscore2", score);
        }
        else if (score > startingHighScore3)
        {
            PlayerPrefs.SetInt("highscore4", startingHighScore3);
            PlayerPrefs.SetInt("highscore3", score);
        }
        else if (score > startingHighScore4)
        {
            PlayerPrefs.SetInt("highscore4", score);
        }
    }

    void updateLevel()
    {
        currentLevel = numLinesCleared / 10;
        levelText.text = currentLevel.ToString();
    }

    void updateSpeed()
    {
        fallSpeed = 1.0f - ((float)currentLevel * 0.1f);
    }
}
