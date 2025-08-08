using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroScript : MonoBehaviour
{
    // Start is called before the first frame update
    float fall = 0;
    public GameScript GScript;
    public float fallSpeed = 0.5f;
    public bool Rotatable = true;
    public bool LimitedRotate = false;
    private float verticalSpeed = 0.05f;
    private float verticalTimer = 0;
    private float horizontalSpeed = 0.1f;
    private float horizontalTimer = 0;

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
        //  HORIZONTAL MOVEMENT
        if (Input.GetKey(KeyCode.RightArrow)) //GetKey for continuous movement
        {
            horizontalTimer += Time.deltaTime;
            if (horizontalTimer >= horizontalSpeed)
            {
                horizontalTimer = 0;
                transform.position += new Vector3(1, 0, 0);
            }
            if (CheckValidPosition())
            {
                GScript.updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalTimer += Time.deltaTime;
            if (horizontalTimer >= horizontalSpeed)
            {
                horizontalTimer = 0;
                transform.position += new Vector3(-1, 0, 0);
            }
            if (CheckValidPosition())
            {
                GScript.updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else
        {
            horizontalTimer = horizontalSpeed; // allow instant move if pressed again
        }

        //  VERTICAL MOVEMENT
        if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed) //This logic makes the object fall 1 block every fallSpeed seconds (not falling like flappybird)
        {
            verticalTimer += Time.deltaTime;
            if (verticalTimer >= verticalSpeed)
            {
                verticalTimer = 0;
                transform.position += new Vector3(0, -1, 0);
            }

            if (CheckValidPosition())
            {
                GScript.updateGrid(this); //this = current tetromino
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                GScript.RemoveRow();
                if (GScript.AboveGrid(this))
                {
                    GScript.GameOver();
                }
                enabled = false;
                GScript.SpawnTet();
            }
            fall = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            while (true)
            {
                // Move down
                transform.position += Vector3.down;

                // If position is invalid (hit block or bottom)
                if (CheckValidPosition())
                {
                    GScript.updateGrid(this); //this = current tetromino
                }
                else
                {
                    // Move back up
                    transform.position += Vector3.up;
                    break; // Exit loop
                }
            }
        }
        else
        {
            verticalTimer = verticalSpeed; // reset to allow instant down again
        }

        //  ROTATION
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Rotatable) // make sure the object can be rotated or not
            {
                if (LimitedRotate) // make sure the object only able to rotate only between 90 and -90
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                if (CheckValidPosition())
                {
                    GScript.updateGrid(this);
                }
                else
                {
                    if (LimitedRotate)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }
    }

    bool CheckValidPosition() //checking the object is in a valid position to do a command
    {
        if (GScript == null)
        {
            Debug.LogError("GameScript not found in scene!");
            return false;
        }

        foreach (Transform mino in transform) //check every block in tetromino
        {
            Vector2 pos = GScript.Round(mino.position);

            if (GScript.CheckInsideStage(pos) == false) //check if its inside the stage grid or not
            {
                return false;
            }
            //If the grid already has a block (mino) at that position && if that block is not part of this same tetromino:
            if (
                GScript.GetTransformAtGridPos(pos) != null
                && GScript.GetTransformAtGridPos(pos).parent != transform
            )
            {
                return false;
            }
        }
        return true;
    }
}
