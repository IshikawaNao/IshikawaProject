using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCameraSwitch : MonoBehaviour
{
    Canvas cv;
    Camera cm;
    void Start()
    {
        cm = Camera.main;
        cv = GetComponent<Canvas>();
        cv.worldCamera = cm;
    }
}
