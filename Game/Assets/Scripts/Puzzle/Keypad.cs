using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public int[] KeyPressed;
    //game manager
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //get the game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    /// <summary>
    /// When a key is pressed in a keypad this function is called
    /// check if the key pressed is correct
    /// if the key pressed is correct return true
    /// </summary>
    /// <param name="number"> the number that is pressed.</param>
    /// <returns>a bool if correct true</returns>
    public bool PressKey(int number)
    {
        //if key pressed is less than 4
        if (KeyPressed.Length < 4)
        {
            //add the number to the array
            KeyPressed[KeyPressed.Length] = number;
            //if the length of the array is 4
            if (KeyPressed.Length == 4)
            {
                //check if the key pressed is correct
                for (int i = 0; i < 4; i++)
                {
                    if (KeyPressed[i] != gameManager.Key[i])
                    {
                        //return false
                        return false;
                    }
                }
                //return true
                return true;
            }
            //return false
            return false;
        }
        else
        {
            //remove all the elements in the array
            Array.Clear(KeyPressed, 0, KeyPressed.Length);
            //add the number to the array
            KeyPressed[KeyPressed.Length] = number;
            //return false
            return false;
        }
        
    }
}
