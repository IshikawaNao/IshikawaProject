public interface IPlayerState
{
    // このクラスの状態を取得する
    PlayerState State { get; }

    // 状態開始時に最初に実行される
    void Entry();

    // フレームごとに実行される
    void Update();

    // 一定時間ごと実行される
    void FixedUpdate();

    // 状態終了時に実行される
    void Exit();
}

// プレイヤーの状態
public enum PlayerState { Idle, Move, Climb, Push, Gliding, Sonar, Teleport , Pause}
