using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(
            UnityEngine.Random.Range(-4f, 4f),
            UnityEngine.Random.Range(-4f, 4f),
            0
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
