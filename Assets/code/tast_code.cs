using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tast_code : MonoBehaviour
{
    public bool[] e;

    float[] InputValue = { 1, 2, 3 };
    float[] DNA = { 1, 2, 3,-4,-5,-6,
                    3, 2, 1,-6,-5,-4,
                    -1,-2,-3, 4, 5, 6,
                    -10, 5,
                    1, 2, 3, 4, 5, 6, 7, 8};
    int amountOfOutput = 1;
    int[] layer = { 2, 3, 2 };


    bool[] outputValue;
    int yNumber = 0;
    float[] y;
    int[] wer;
    int werNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        //고정함수----------------------
        //출력 값 리스트 만들기
        outputValue = new bool[amountOfOutput];

        //신경정보
        for (int i = 0; i < layer.Length; i++) yNumber += layer[i];
        y = new float[yNumber];

        //전체 가중치의 수
        wer = new int[layer.Length + 1];
        wer[0] = InputValue.Length * layer[0];
        for (int i = 0; i < layer.Length - 1; i++) wer[i + 1] = layer[i] * layer[i + 1];
        wer[wer.Length - 1] = layer[layer.Length - 1] * outputValue.Length;

        for (int i = 0; i < wer.Length; i++) werNumber += wer[i];
        //----------------------------

        e = NeuralNetwork();
    }

    private bool[] NeuralNetwork()
    {
        for (int i = 0; i < yNumber; i++) y[i] = 0;

        //입력층과 첫 은닉층
        for (int l = 0; l < layer[0]; l++)
        {
            for (int i = 0; i < InputValue.Length; i++)
            {
                y[l] += InputValue[i] * DNA[(l * InputValue.Length) + i];
            }
            // 역치 계산
            y[l] += DNA[werNumber + l];

            // ReLU 활성 함수
            //if (y[l] < 0) y[l] = 0;

            //시그모이드 함수
            y[l] = 1 / (1 + Mathf.Exp(-y[l]));
        }


        // 첫 은닉층과 두번째 은닉층
        for (int l = 0; l < layer[1]; l++)
        {
            for (int i = 0; i < layer[0]; i++)
            {
                //신호와 가중치 계산
                y[layer[0] + l] += y[i] * DNA[(l * layer[0]) + wer[1] + i];
            }

            // 역치 계산
            y[layer[0] + l] += DNA[werNumber + layer[0] + l];

            // ReLU 활성 함수
            //if (y[layer[0] + l] < 0) y[layer[0] + l] = 0;

            //시그모이드 함수
            y[layer[0] + l] = 1 / (1 + Mathf.Exp(-y[layer[0] + l]));
        }


        // 두번째 은닉층과 n번째 은닉층
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
                    //신호와 가중치 계산
                    y[layerNumber + l] += y[layerNumber - layer[j + 1] + i] * DNA[(l * layer[j + 1]) + weightNumder + i];
                }

                // 역치 계산
                y[layerNumber + l] += DNA[werNumber + layerNumber + l];

                // ReLU 활성 함수
                //if (y[layerNumber + l] < 0) y[layerNumber + l] = 0;

                //시그모이드 함수
                y[layerNumber + l] = 1 / (1 + Mathf.Exp(-y[layerNumber + l]));
            }

        }


        // 마지막 은닉층과 출력층
        for (int l = 0; l < amountOfOutput; l++)
        {
            float outputValue_re = 0;
            for (int i = 0; i < layer[layer.Length - 1]; i++)
            {
                //신호와 가중치 계산
                outputValue_re += y[y.Length - layer[layer.Length - 1] + i] * DNA[werNumber - wer[wer.Length - 1] + (l * layer[layer.Length - 1]) + i];
            }

            // 역치 계산
            outputValue_re += DNA[werNumber + yNumber + l];

            //Debug.Log(outputValue_re);

            //계단 함수
            if (outputValue_re >= 0) outputValue[l] = true;
            else if (outputValue_re < 0) outputValue[l] = false;
        }
        //----------------------------

        return outputValue;
    }
}