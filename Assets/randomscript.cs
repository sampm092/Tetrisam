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
        // Move the objects
        MyRigid.transform.position += Vector3.right * scrollSpeed1 * Time.deltaTime;

        // Update timer
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            RandomizeSpeeds();
            timer = 0f;
        }
    }

    void RandomizeSpeeds()
    {
        scrollSpeed1 = Random.Range(0.5f, 3f);
    }
}
