using System.Collections;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Animations;

public class FallingGameOver : MonoBehaviour
{
    [SerializeField, Header("リセットポジション")]
    Transform resetPosition;
    public Vector3 ResetPosition { get { return resetPosition.position; } } 
    WaitForSeconds waitForSeconds;
    const float waitTime = 3;

    bool isFalling = false;
    public bool IsFalling { get { return isFalling; } }

    ReactiveProperty<int> count = new ReactiveProperty<int>();
    public IObservable<int> Count { get { return count; } }

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
    }

    IEnumerator FallDeray(GameObject player)
    {
        yield return waitForSeconds;
        player.gameObject.transform.position = resetPosition.position;
        player.gameObject.transform.rotation = this.transform.rotation;
        count.Value++; 
        isFalling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(FallDeray(other.gameObject));
        }
    }
}
