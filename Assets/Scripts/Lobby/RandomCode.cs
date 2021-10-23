using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCode : MonoBehaviour
{
    public static RandomCode Instance;
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private void Start()
    {
        Instance = this;
    }

    public string NewRandomCode()
    {
        string code = "";

        for (int i = 0; i < 4; i++)
        {
            code = code + chars[Random.Range(0,chars.Length)];
        }
        
        return code;
    }
    
}
