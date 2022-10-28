using UnityEngine;

public interface IMoveObject
{
    void Move(Rigidbody _rb, Vector2 move, GameObject _player);
    bool Push(GameObject _player);
    Rigidbody Box_rb();
}
