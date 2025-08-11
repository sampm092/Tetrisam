using UnityEngine;

public class randomscript : MonoBehaviour
{
    public Rigidbody2D MyRigid;
    public float scrollSpeed1;
    public float changeInterval = 1f; // seconds between randomizations

    private float timer;

    void Start()
    {
        RandomizeSpeeds();
    }

    void Update()
    {
        // Move the object
        MyRigid.transform.position += Vector3.right * scrollSpeed1 * Time.deltaTime;

        // Update timer
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            RandomizeSpeeds();
            timer = 0f;
        }

        // If Sommth() is true, stop movement
        if (Sommth())
        {
            scrollSpeed1 = 0;
        }
    }

    void RandomizeSpeeds()
    {
        scrollSpeed1 = Random.Range(0.5f, 3f);
    }

    bool Sommth()
    {
        foreach (Transform mino in transform) // check every block in tetromino
        {
            Vector2 pos = mino.position;

            if (CheckInsideStage(pos)) // check if it's inside the stage grid
            {
                return true;
            }
        }
        return false; // default if nothing matched
    }

    public bool CheckInsideStage(Vector2 pos)
    {
        return ((int)pos.x == 8); // example: inside stage if x = 8
    }
}
