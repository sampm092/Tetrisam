using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    private static BGMScript instance;

    void Awake()
    // Awake() is called before Start(), as soon as the object is created.
    {
        if (instance == null) //The first time a BGMScript spawns, instance is null, so this object becomes the “official” instance.
        {
            instance = this; //assign this crip
            DontDestroyOnLoad(gameObject); // keep this across scenes even after loading other gameobjects
        }
        else
        {
            Destroy(gameObject); // destroy other gameObject (another bgm) if theres another instance
        }
    }
}
