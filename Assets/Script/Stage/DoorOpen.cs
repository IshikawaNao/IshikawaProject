using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    private void OnCollisionEnter(Collision collision)
    {
        print(collision);
        if(collision.gameObject.name == "Player")
        {

            anim.SetBool("Open", true);
        }
    }
}
