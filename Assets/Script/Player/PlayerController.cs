using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float movespeed = 7;
    float pushSpeed = 3;
    float force = 7;
    float jumpPower = 5;
    float distance = 1.05f;
    float pushRayDistance = 0.5f;
    float drag = 5;

    bool isGround;
    bool isPush = false;
    
    Vector2 move;

    Rigidbody rb;
    PlayerJump pj;
    PlayerMove pm;
    GroundLayer gl;
    PlayerPush pp;
    Parachute pc;

    InputKey input;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pj = GetComponent<PlayerJump>();
        pm = GetComponent<PlayerMove>();
        gl = GetComponent<GroundLayer>();
        pp = GetComponent<PlayerPush>();
        pc = GetComponent<Parachute>();

        input = GetComponent<InputKey>();
    }

    
    void Update()
    {
        move = input.Move;

        isGround = gl.IsGround(distance);
        isPush = pp.Push(pushRayDistance);
        if (input.Jump)
        {
            if(isGround)
            {
                pj.Jump(rb, jumpPower);
            }
            else
            {
                pc.Para(rb, drag, isGround);
            }
        }
        if(isGround)
        {
            pc.Para(rb, drag, isGround);
        }
       
    }

    private void FixedUpdate()
    {
        if(isPush)
        {
            pm.Move(rb, move, pushSpeed, force);
        }
        else
        {
            pm.Move(rb, move, movespeed, force);
        }
    }
}
