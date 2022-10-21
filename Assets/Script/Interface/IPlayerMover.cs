using UnityEngine;

public interface IPlayerMover
{
    void Move(Vector2 move, GameObject camera);
    void Jump();
}

