using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_code : MonoBehaviour
{
    int speed = 10;
    public bool possession = false;



    //------------
    int RotationSpeed = 200;
    float rotx = 0;
    float roty = 0;
    float zMove = 0;
    float xMove = 0;
    float Move = 0;
    //------------

    public GameObject gameObject;
    character_code code;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (possession)
            {
                code.possession = false;
                possession = false;
            }
            else
            {
                code.possession = true;
                possession = true;
            }
        }

        if (!possession)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "character")
                    {
                        code = GameObject.Find(hit.transform.gameObject.name).GetComponent<character_code>();
                        gameObject = hit.transform.gameObject;
                    }
                }
            }


            if (Input.GetMouseButton(1)) rotx = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
            else rotx = 0f;
            if (Input.GetMouseButton(1)) roty = Input.GetAxis("Mouse Y") * RotationSpeed * Mathf.Deg2Rad;
            else roty = 0f;

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x - roty, transform.eulerAngles.y + rotx, 0);

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && Move < speed) Move += 0.1f * Time.deltaTime;
            else if (Move > 0.5f) Move -= 5f * Time.deltaTime;

            if (Input.GetKey(KeyCode.D)) if (xMove <= speed) xMove += 100 * Time.deltaTime;
            if (Input.GetKey(KeyCode.A)) if (xMove >= -speed) xMove -= 100 * Time.deltaTime;
            if (Input.GetKey(KeyCode.W)) if (zMove <= speed) zMove += 100 * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) if (zMove >= -speed) zMove -= 100 * Time.deltaTime;
            if (xMove > 0) xMove -= 50 * Time.deltaTime;
            else if (xMove < 0) xMove += 50 * Time.deltaTime;
            if (zMove > 0) zMove -= 50 * Time.deltaTime;
            else if (zMove < 0) zMove += 50 * Time.deltaTime;

            if (xMove > -0.01f && xMove < 0.01f) xMove = 0;
            if (zMove > -0.01f && zMove < 0.01f) zMove = 0;

            transform.Translate(new Vector3(xMove * Move * Time.deltaTime, 0, zMove * Move * Time.deltaTime));
        }
    }
    void LateUpdate()
    {
        if (possession)
        {
            transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.8f, gameObject.transform.position.z);
            transform.rotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y + 90, 0);
        }
    }
}
