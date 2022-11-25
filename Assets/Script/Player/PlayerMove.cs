using UnityEngine;

class PlayerMove
{
    private IPlayerMover _playerMover;
    public PlayerMove(IPlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void ExcuteMove(Vector2 move, GameObject camera, Animator anim)
    {
        _playerMover.Move(move, camera, anim);
    }

    public void ChangeMove(IPlayerMover playerMover)
    {
        _playerMover = playerMover;
    }
}
