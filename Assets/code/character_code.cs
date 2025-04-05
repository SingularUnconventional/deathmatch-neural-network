using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ĳ���� ���������� �� ��Ʈ �������� ��������
public class character_code : MonoBehaviour
{
    int eyesight = 100;

    public float[] eyesStreet = new float[10];
    public float[] eyesObject = new float[10];
    public Vector3[] RayPosition = new Vector3[10];
    RaycastHit hit;
    public LayerMask LayerMask;


    int speed = 5;
    int RotationSpeed = 500;

    public float heart;
    public float magazine = 0;
    float magazine_t = 0;

    float rotx = 0;
    float zMove = 0;
    float xMove = 0;

    bulletPooling bulletPooling;
    character_code character;
    heredity_code heredity_Code;
    ObjectPooling heartPooling;

    public bool possession = false;



    public float[] InputValue = new float[22];
    public float[] DNA = new float[1670];
    int[] layer = { 25, 20, 15, 10, 5 };
    //1575, 75

    public float outputValue_2;

    public bool[] outputValue;
    int yNumber = 0;
    float[] y;
    int[] wer;
    int werNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        //�����Լ�----------------------
        outputValue = new bool[5];

        //�Ű�����
        for (int i = 0; i < layer.Length; i++) yNumber += layer[i];
        y = new float[yNumber];

        //��ü ����ġ�� ��
        wer = new int[layer.Length + 1];
        wer[0] = InputValue.Length * layer[0];
        for (int i = 0; i < layer.Length - 1; i++) wer[i + 1] = layer[i] * layer[i + 1];
        wer[wer.Length - 1] = layer[layer.Length - 1] * outputValue.Length;

        for (int i = 0; i < wer.Length; i++) werNumber += wer[i];
        //----------------------------
    }

    void Update()
    {
        if (heart <= 0)
        {
            heredity_Code = GameObject.Find("heredity").GetComponent<heredity_code>();
            int[] a = { heredity_Code.numbersProbability[Random.Range(0, heredity_Code.numbersProbability.Length - 1)], heredity_Code.numbersProbability[Random.Range(0, heredity_Code.numbersProbability.Length - 1)] };

            //����
            for (int l = 0; l < 1670; l++) heredity_Code.DNA[int.Parse(transform.name), l] = heredity_Code.DNA[a[Random.Range(0, 1)], l];

            //����
            for (int i = 0; i < heredity_Code.rateOfMutation; i++) heredity_Code.DNA[int.Parse(transform.name), Random.Range(0, 1669)] = Random.Range(-3.0f, 3.0f);


            for (int l = 0; l < 1670; l++) DNA[l] = heredity_Code.DNA[int.Parse(transform.name), l];
            heredity_Code.killCount[int.Parse(transform.name)] = 0;
            Debug.Log("Ŭ����");

            heartPooling = GameObject.Find("heartPooling").GetComponent<ObjectPooling>();
            Vector3 v = heartPooling.pooledObjects[Random.Range(0, heartPooling.pooledObjects.Count - 1)].transform.position;
            if (heart < 3) heart = 6;
            else heart = 1;
            zMove = 0;
            xMove = 0;
            transform.position = new Vector3(v.x, 0.5f, v.z);
        }

        for (int i = 0; i < 10; i++)
        {
            RayPosition[i] = new Vector3(Mathf.Cos(-(transform.eulerAngles.y + (eyesight / 9 * i) - (eyesight / 2)) * Mathf.Deg2Rad), 0, Mathf.Sin(-(transform.eulerAngles.y + (eyesight / 9 * i) - (eyesight / 2)) * Mathf.Deg2Rad));

            if (Physics.Raycast(transform.position, RayPosition[i], out hit))
            {
                Debug.DrawRay(transform.position, RayPosition[i]* hit.distance, Color.red);
                eyesStreet[i] = hit.distance;
                if (hit.collider.gameObject.tag == "heart") eyesObject[i] = -2;
                else if (hit.collider.gameObject.tag == "magazine") eyesObject[i] = -1;
                else if (hit.collider.gameObject.tag == "terrain") eyesObject[i] = 0;
                else if (hit.collider.gameObject.tag == "character") 
                {
                    character = GameObject.Find(hit.collider.gameObject.name).GetComponent<character_code>();
                    eyesObject[i] = character.heart;
                }
            }
        }

        for (int i = 0; i < 10; i++) InputValue[i] = eyesStreet[i];
        for (int i = 10; i < 20; i++) InputValue[i] = eyesObject[i-10];
        InputValue[20] = heart;
        InputValue[21] = magazine;

        NeuralNetwork();

        if (possession)
        {
            if (Input.GetKey(KeyCode.Space) && magazine > 0 && magazine_t >= 0.5)
            {
                bulletPooling = GameObject.Find("bulletPooling").GetComponent<bulletPooling>();

                GameObject GameObject_code = bulletPooling.GetPooledObject();
                GameObject_code.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                GameObject_code.transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
                GameObject_code.name = transform.name;
                GameObject_code.SetActive(true);

                magazine -= 1;
                magazine_t = 0;
            }

            if (Input.GetMouseButton(1)) rotx = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
            else rotx = 0f;

            transform.Rotate(Vector3.up, rotx);

            if (Input.GetKey(KeyCode.W)) if (xMove <= speed) xMove += 24 * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) if (xMove >= -speed) xMove -= 24 * Time.deltaTime;
            if (Input.GetKey(KeyCode.A)) if (zMove <= speed) zMove += 24 * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) if (zMove >= -speed) zMove -= 24 * Time.deltaTime;
        }
        else
        {
            if (outputValue[4] && magazine > 0 && magazine_t >= 0.5)
            {
                bulletPooling = GameObject.Find("bulletPooling").GetComponent<bulletPooling>();

                GameObject GameObject_code = bulletPooling.GetPooledObject();
                GameObject_code.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                GameObject_code.transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
                GameObject_code.name = transform.name;
                GameObject_code.SetActive(true);

                magazine -= 1;
                magazine_t = 0;
            }

            transform.Rotate(Vector3.up, outputValue_2 * Time.deltaTime);

            if (outputValue[0] && xMove <= speed) xMove += 24 * Time.deltaTime;
            if (outputValue[1] && xMove >= -speed) xMove -= 24 * Time.deltaTime;
            if (outputValue[2] && zMove <= speed) zMove += 24 * Time.deltaTime;
            if (outputValue[3] && zMove >= -speed) zMove -= 24 * Time.deltaTime;
        }

        if (xMove > 0) xMove -= 12 * Time.deltaTime;
        else if (xMove < 0) xMove += 12 * Time.deltaTime;
        if (zMove > 0) zMove -= 12 * Time.deltaTime;
        else if (zMove < 0) zMove += 12 * Time.deltaTime;

        if (xMove > -0.1f && xMove < 0.1f) xMove = 0;
        if (zMove > -0.1f && zMove < 0.1f) zMove = 0;

        this.transform.Translate(new Vector3(xMove * Time.deltaTime, 0, zMove * Time.deltaTime));  //�̵�


        heart -= Time.deltaTime;
        magazine_t += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "heart")
        {
            if (heart < 15) heart += 5;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "magazine")
        {
            magazine = 30;
            collision.gameObject.SetActive(false);
        }
    }


    private void NeuralNetwork()
    {
        for (int i = 0; i < yNumber; i++) y[i] = 0;

        //�Է����� ù ������
        for (int l = 0; l < layer[0]; l++)
        {
            for (int i = 0; i < InputValue.Length; i++)
            {
                y[l] += InputValue[i] * DNA[(l * InputValue.Length) + i];
            }
            // ��ġ ���
            y[l] += DNA[werNumber + l];

            // ReLU Ȱ�� �Լ�
            //if (y[l] < 0) y[l] = 0;

            //�ñ׸��̵� �Լ�
            y[l] = 1 / (1 + Mathf.Exp(-y[l]));
        }


        // ù �������� �ι�° ������
        for (int l = 0; l < layer[1]; l++)
        {
            for (int i = 0; i < layer[0]; i++)
            {
                //��ȣ�� ����ġ ���
                y[layer[0] + l] += y[i] * DNA[(l * layer[0]) + wer[1] + i];
            }

            // ��ġ ���
            y[layer[0] + l] += DNA[werNumber + layer[0] + l];

            // ReLU Ȱ�� �Լ�
            //if (y[layer[0] + l] < 0) y[layer[0] + l] = 0;

            //�ñ׸��̵� �Լ�
            y[layer[0] + l] = 1 / (1 + Mathf.Exp(-y[layer[0] + l]));
        }


        // �ι�° �������� n��° ������
        for (int j = 0; j < layer.Length - 2; j++)
        {
            int layerNumber = 0;
            int weightNumder = 0;

            for (int d = 0; d < j + 2; d++)
            {
                layerNumber += layer[d];
                weightNumder += wer[d];
            }

            for (int l = 0; l < layer[j + 2]; l++)
            {
                for (int i = 0; i < layer[j + 1]; i++)
                {
                    //��ȣ�� ����ġ ���
                    y[layerNumber + l] += y[layerNumber - layer[j + 1] + i] * DNA[(l * layer[j + 1]) + weightNumder + i];
                }

                // ��ġ ���
                y[layerNumber + l] += DNA[werNumber + layerNumber + l];

                // ReLU Ȱ�� �Լ�
                //if (y[layerNumber + l] < 0) y[layerNumber + l] = 0;

                //�ñ׸��̵� �Լ�
                y[layerNumber + l] = 1 / (1 + Mathf.Exp(-y[layerNumber + l]));
            }

        }


        // ������ �������� �����
        for (int l = 0; l < outputValue.Length; l++)
        {
            float outputValue_re = 0;
            for (int i = 0; i < layer[layer.Length - 1]; i++)
            {
                //��ȣ�� ����ġ ���
                outputValue_re += y[y.Length - layer[layer.Length - 1] + i] * DNA[werNumber - wer[wer.Length - 1] + (l * layer[layer.Length - 1]) + i];
            }

            // ��ġ ���
            outputValue_re += DNA[werNumber + yNumber + l];

            //Debug.Log(outputValue_re);
            //��� �Լ�
            if (outputValue_re >= 0) outputValue[l] = true;
            else if (outputValue_re < 0) outputValue[l] = false;
        }

        outputValue_2 = 0;

        for (int i = 0; i < layer[layer.Length - 1]; i++)
        {
            //��ȣ�� ����ġ ���
            outputValue_2 += y[y.Length - layer[layer.Length - 1] + i] * DNA[werNumber - wer[wer.Length - 1] + (5 * layer[layer.Length - 1]) + i];
        }
        // ��ġ ���
        outputValue_2 *= 150;
        outputValue_2 += DNA[werNumber + yNumber + 5];
        //----------------------------
    }

}
