using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_code : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-50.0f, 50.0f), 2, Random.Range(-50.0f, 50.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 3 && transform.position.y < -1) transform.position = new Vector3(Random.Range(-50.0f, 50.0f), 2, Random.Range(-50.0f, 50.0f));
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            gameObject.SetActive(false);
        }
    }
}
