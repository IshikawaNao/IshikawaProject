using UnityEngine;

public interface IMoveObject
{
    void Move(Rigidbody _rb, Vector2 move, GameObject _player);
    void ClimbPlayer(Rigidbody _rb, GameObject _player);
    Rigidbody Box_rb();
    bool Push(GameObject _player);
    bool Climb(GameObject _player);

}
