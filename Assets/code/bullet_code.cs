using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_code : MonoBehaviour
{
    character_code code;
    heredity_code heredity;

    float i = 0;
    float speed = 30;

    private void OnEnable()
    {
        i = 0;
    }


    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        i += Time.deltaTime;
        if(transform.position.x > 50 || transform.position.x < -50 || transform.position.y > 50 || transform.position.y < -50) gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (i > 0.05)
        {
            if (other.tag == "character")
            {
                code = GameObject.Find(other.name).GetComponent<character_code>();
                code.heart -= 1;
                if (code.heart <= 0)
                {
                    heredity = GameObject.Find("heredity").GetComponent<heredity_code>();
                    heredity.killCount[int.Parse(transform.name)] += 1;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
