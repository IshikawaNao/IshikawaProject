using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public float _anglePerFrame = 0.1f; 
    float _rot = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        _rot += _anglePerFrame;
        if (_rot >= 360.0f)
        { 
            _rot -= 360.0f;
        }
        RenderSettings.skybox.SetFloat("_Rotation", _rot); 
    }
}
