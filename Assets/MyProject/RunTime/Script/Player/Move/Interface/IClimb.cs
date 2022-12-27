using UnityEngine;

public interface IClimb
{
    void ClimbPlayer(Rigidbody _rb, Animator anim);
    bool Climb(GameObject _player);
}
