using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFly
{
    void Fly(Rigidbody _rb, bool isFly);
    bool FlyFrag(GameObject _plyer);
}