using UnityEngine;

public interface IClimb
{
    void ClimbPlayer(Rigidbody _rb);
    bool Climb(GameObject _player);
}
