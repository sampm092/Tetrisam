using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static int gridWidth = 10;
    public static int gridHeight = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckInsideStage(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= -9);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
}
