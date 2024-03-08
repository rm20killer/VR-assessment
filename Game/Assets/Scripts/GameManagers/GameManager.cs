using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] Key;
    void Start()
    {
        //generate a random number between 1 and 9 and store it in the variable key
        Key = new int[4];
        for (int i = 0; i < 4; i++)
        {
            Key[i] = Random.Range(1, 9);
        }
    }

}