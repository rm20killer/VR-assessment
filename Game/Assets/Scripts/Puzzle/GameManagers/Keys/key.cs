using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers.Keys
{
    public class key
    {
        public int KeyID;
        public int[] KeyCode;

        void Setkey()
        {
            //generate a random number between 1 and 9 and store it in the variable key
            KeyCode = new int[4];
            for (int i = 0; i < 4; i++)
            {
                KeyCode[i] = Random.Range(1, 9);
            }
        }
        
        void SetKeyID(int id)
        {
            //set the key id
            KeyID = id;
        }
        
        void setKey(int[] key)
        {
            //set the keycode
            KeyCode = key;
        }
        int[] GetKey()
        {
            //return the keycode
            return KeyCode;
        }
        
        int GetKeyID()
        {
            //return the key id
            return KeyID;
        }
        
    }
}