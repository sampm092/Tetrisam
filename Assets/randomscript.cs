using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomscript : MonoBehaviour
{
    public Rigidbody2D MyRigid;
    public float scrollSpeed = 0.5f; // fot background speed
    public float resetPositionX = -8; // Where to reset the tile
    public float startPositionX = 5; // Where to move it back to
    private float BgWidth;
    // Start is called before the first frame update

    void Start()
    {

    }
    void Update()
    {
        MyRigid.transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // if (MyRigid.transform.position.x <= resetPositionX)
        // {
        //     MyRigid.transform.position = new Vector3(startPositionX, MyRigid.transform.position.y, MyRigid.transform.position.z); // resetting background t
        // }
    }
}