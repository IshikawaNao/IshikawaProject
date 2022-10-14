using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float distance = 1.05f;
    bool isGround;

    [SerializeField]
    Camera camera;
    Rigidbody rb;
    GroundLayer gl;
    IPlayerMover iMover;
    InputKey input;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        gl = GetComponent<GroundLayer>();
        
        input = GetComponent<InputKey>();

        iMover = new PlayerMove(rb);
    }

    
    void Update()
    {
        isGround = gl.IsGround(distance);
        
        if (input.Jump)
        {
            if(isGround)
            {
                iMover.Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        iMover.Move(input.Move, camera, this.gameObject);
    }
}
