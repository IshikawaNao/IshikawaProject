using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFly
{
    void Fly(Rigidbody _rb, bool isFly, Animator anim);
    bool FlyFrag(GameObject _plyer);
}