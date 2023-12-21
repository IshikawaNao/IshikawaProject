using UnityEngine;

/// <summary>
/// オブジェクトを押す
/// </summary>
public class PushObject 
{
    const float RayLength = 1f;
    bool isPush = false;        // オブジェクトが動くフラグ
    public bool IsPush { get { return isPush; } }

    Vector3 checkVec = new(1, 0, 1);
    
    RaycastHit hit;
    Ray ray;

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

    // playerの正面に動かせるオブジェクトがあるか
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, RayLength))
        {
            // 動かせるオブジェクトの場合そのオブジェクトの情報を取得
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                rb = hit.rigidbody;
                return true;
            }
        }
        rb = null;
        return false;
    }

    // プッシュが選択された時の処理
    public void PushAnimationChange(bool canPush ,bool ground, bool isClimb, bool isPushAction)
    {
        // Objectを移動中か
        if (canPush && ground && !isClimb)
        {
            if (isPushAction)
            {
                isPush = true;
                anim.SetBool("IsObjectMove", true);
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
