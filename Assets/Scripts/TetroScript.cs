using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroScript : MonoBehaviour
{
    // Start is called before the first frame update
    float fall = 0;
    public GameScript GScript;
    public float fallSpeed = 1;
    void Start()
    {
        GScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CheckValidPosition())
            {

            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (CheckValidPosition())
            {

            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            transform.position += new Vector3(0, -1, 0);

            if (CheckValidPosition())
            {

            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
            }
            fall = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
        }
    }

    bool CheckValidPosition()
    {
        if (GScript == null)
        {
            Debug.LogError("GameScript not found in scene!");
            return false;
        }

        foreach (Transform mino in transform)
        {
            Vector2 pos = GScript.Round(mino.position);

            if (GScript.CheckInsideStage(pos) == false)
            {
                return false;
            }
        }
        return true;
    }

}
