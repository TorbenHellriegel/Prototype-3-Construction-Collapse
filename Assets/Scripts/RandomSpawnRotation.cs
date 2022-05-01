using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Starts the object with a random spawn rotation
        transform.rotation = Random.rotation;
    }
}
