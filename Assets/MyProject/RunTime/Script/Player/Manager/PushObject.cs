using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// オブジェクトを押す
/// </summary>
public class PushObject 
{
    // 要調整
    const float speed = 2.95f;

    const float rayLength = 1f;
    bool isPush = false;        // オブジェクトが動くフラグ
    public bool IsPush { get { return isPush; } }

    Vector3 checkVec = new(1, 0, 1);
    
    RaycastHit hit;
    Ray ray;

    // Pushオブジェクト
    GameObject pushObj = null;
    public GameObject PushObj { get {return pushObj; } }
    // プレイヤー
    GameObject player = null;
    // プレイヤーアニメーション
    Animator anim;

    Rigidbody rb = null;
    public Rigidbody PushRb { get { return rb; } }

    public PushObject(GameObject _player, Animator _anim)
    {
        player = _player;
        anim = _anim;
    }

    // プッシュオブジェクト移動
    public void Move(Vector2 move) 
    {
        if(move != Vector2.zero && isPush)
        {
            Vector3 playerForward = Vector3.Scale(player.transform.forward, checkVec);
            Vector3 moveForward = playerForward * move.y + player.transform.right * move.x;
            Vector3 moveVector = moveForward.normalized * speed;   //移動速度
            rb.velocity = moveVector;
        }
    }


    // playerの正面に動かせるオブジェクトがあるか
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // 動かせるオブジェクトの場合そのオブジェクトの情報を取得
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                pushObj = hit.collider.gameObject;
                rb = hit.rigidbody;
                return true;
            }
        }
        pushObj = null;
        rb = null;
        return false;
    }

    // プッシュが選択された時の処理
    public void PushAnimationChange(bool ground, bool isClimb, bool isPushAction)
    {
        if (CanPush() && ground && !isClimb)
        {
            if (isPushAction)
            {
                isPush = true;
            }
            else
            {
                anim.SetBool("IsObjectMove", false);
                isPush = false;
            }
        }
        else
        {
            anim.SetBool("IsObjectMove", false);
            isPush = false;
        }
    }
}
