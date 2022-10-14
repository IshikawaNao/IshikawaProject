using UnityEngine;

public interface IPlayerMover
{
    void Move(Vector2 move, Camera camera, GameObject player);
    void Jump();
}

