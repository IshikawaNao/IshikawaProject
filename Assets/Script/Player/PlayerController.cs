using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    bool a = false;
    bool b = false;

    bool isMove = true;


    [SerializeField]
    GameObject cameraPos;

    Rigidbody rb;
    CapsuleCollider col;
    GroundLayer gl;
    KeyInput input;

    IPlayerMover iMover;
    IMoveObject iObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gl = GetComponent<GroundLayer>();
        input = GetComponent<KeyInput>();

        iMover = new PlayerMove(rb, this.gameObject);
        iObject = new PushObject();
    }

    
    void Update()
    {
        bool isPush = iObject.Push(this.gameObject);
        bool isClimb =  iObject.Climb(this.gameObject);
        if (input.InputJump)
        {
            if(gl.IsGround())
            {
                iMover.Jump();
            }
        }

        if (isPush)
        {
            if (input.PushAction)
            {
                a = true;
            }
            if(a)
            {
                iObject.Move(iObject.Box_rb(), input.InputMove, this.gameObject);
            }
        }
        else
        {
            a = false;
        }

        if(isClimb)
        {
            if(input.ClimbAction)
            {
                
                b = true;
                isMove = false;
            }
            if(b)
            {
                iObject.ClimbPlayer(rb, this.gameObject);
            }
        }
        else
        {
            if(b)
            {
                transform.DOMove(this.transform.position + 0.1f * Vector3.up + col.radius * 2f * this.transform.forward, 0.1f);
            }
            b = false;
            isMove = true;
        }
        
    }

    private void FixedUpdate()
    {
        if(isMove)
        {
            iMover.Move(input.InputMove, cameraPos);
        }
    }
}
