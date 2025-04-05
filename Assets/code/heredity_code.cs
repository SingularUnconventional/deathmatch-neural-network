using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heredity_code : MonoBehaviour
{
    public int individualsNumber = 50;


    public float weight = 3f;
    public int rateOfMutation = 7;
    public int Royal = 5;

    public List<GameObject> individual = new List<GameObject>();
    character_code character_code;
    public float[,] DNA;
    public int[] killCount;
    public int[] numbersProbability;
    public int[] numbers;

    public GameObject gameObject;
    int i;

    float[,] GreatDNA;

    public int tossing = -1;

    // Start is called before the first frame update
    void Start()
    {
        DNA = new float[individualsNumber, 1670];
        killCount = new int[individualsNumber];
        numbers = new int[individualsNumber];
        GreatDNA = new float[Royal, 1670];

        //for (int l = 0; l < individualsNumber; l++)
        //{
        //    string[] dataArr = PlayerPrefs.GetString(l.ToString()).Split('/'); // PlayerPrefs���� �ҷ��� ���� Split �Լ��� ���� ���ڿ��� ,�� �����Ͽ� �迭�� ����

        //    for (int i = 0; i < 1670; i++)
        //    {
        //        DNA[l, i] = float.Parse(dataArr[i]); // ���ڿ� ���·� ����� ���� ���������� ��ȯ�� ����
        //    }
        //}


        for (int i = 0; i < individualsNumber; i++)
        {
            killCount[i] = 0;

            GameObject newObject = Instantiate(gameObject);
            individual.Add(newObject);

            newObject.name = i.ToString();

            character_code = GameObject.Find(i.ToString()).GetComponent<character_code>();
            for (int l = 0; l < 1670; l++)
            {
                //DNA[i, l] = Random.Range(-weight, weight);
                character_code.DNA[l] = DNA[i, l];
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < individualsNumber; i++) numbers[i] = killCount[i];

        //����
        for (int i = 0; i < individualsNumber - 1; i++)
        {
            for (int j = 0; j < individualsNumber - i - 1; j++)
            {
                if (numbers[j] < numbers[j + 1])
                {
                    int temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }
        int dr = 0;
        for (int i = 0; i < individualsNumber; i++) dr += (killCount[i] * 20)+1;

        numbersProbability = new int[dr];
        int ler = 0;
        for (int i = 0; i < individualsNumber; i++)
        {
            for (int l = 0; l < (killCount[i] * 20) + 1; l++) numbersProbability[ler + l] = i;
            ler += (killCount[i] * 20) + 1;
        }
    }
    void OnApplicationQuit()
    {
        
        for (int l = 0; l < individualsNumber; l++)
        {
            float[] number = new float[1670]; // ������ �迭 ����
            for (int i = 0; i < 1670; i++) number[i] = DNA[l,i];

            string strArr = ""; // ���ڿ� ����

            for (int i = 0; i < number.Length; i++) // �迭�� ','�� �����ư��� tempStr�� ����
            {
                strArr = strArr + number[i];
                if (i < number.Length - 1) // �ִ� ������ -1������ ,�� ����
                {
                    strArr = strArr + "/";
                }
            }

            PlayerPrefs.SetString(l.ToString(), strArr); // PlyerPrefs�� ���ڿ� ���·� ����
            Debug.Log(l.ToString());
        }
    }
}
