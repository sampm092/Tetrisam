using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomscript : MonoBehaviour
{
    public Rigidbody2D MyRigid;
    public float scrollSpeed1;
    public float scrollSpeed2;
    public float resetPositionX = -8; // Where to reset the tile
    public float startPositionX = 5; // Where to move it back to
    private float BgWidth;
    // Start is called before the first frame update

    void Start()
    {
        scrollSpeed1 = Random.Range(0.5f, 3f);
        scrollSpeed2 = Random.Range(0.5f, 3f);
    }
    void Update()
    {
        MyRigid.transform.position += Vector3.right * scrollSpeed1 * Time.deltaTime;
    }
}