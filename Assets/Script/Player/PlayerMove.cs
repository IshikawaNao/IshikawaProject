using UnityEngine;

class PlayerMove
{
    private IPlayerMover _playerMover;
    public PlayerMove(IPlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void ExcuteMove(Vector2 move, Animator anim)
    {
        _playerMover.Move(move, anim);
    }

    public void ChangeMove(IPlayerMover playerMover)
    {
        _playerMover = playerMover;
    }
}
