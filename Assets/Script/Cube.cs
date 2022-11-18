using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    float a = 0;
    
    Renderer rend;

    Material mate;

    KeyInput input;
  
    void Start()
    {
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        rend = GetComponent<Renderer>();
        mate = GetComponent<Renderer>().material; 
    }

    void Update()
    {
        if (input.SonarAction)
        {
            if (rend.isVisible){ a = 1;}
        }
        else { a = 0; }
        mate.SetFloat("_Boolean", a);
    }
}
