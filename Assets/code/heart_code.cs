using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_code : MonoBehaviour
{
    public float worldSize;
    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-worldSize, worldSize), 2, Random.Range(-worldSize, worldSize));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 3 && transform.position.y < -1) transform.position = new Vector3(Random.Range(-worldSize, worldSize), 2, Random.Range(-worldSize, worldSize));
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            transform.position = new Vector3(Random.Range(-worldSize, worldSize), 2, Random.Range(-worldSize, worldSize));
        }
    }
}
