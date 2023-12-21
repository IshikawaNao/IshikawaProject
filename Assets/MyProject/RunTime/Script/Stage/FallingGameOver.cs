using System.Collections;
using UnityEngine;
using UniRx;
using System;

public class FallingGameOver : MonoBehaviour
{
    [SerializeField, Header("リセットポジション")]
    Transform resetPosition;
    public Vector3 ResetPosition { get { return resetPosition.position; } } 
    WaitForSeconds waitForSeconds;
    const float WaitTime = 3;

    bool isFalling = false;
    public bool IsFalling { get { return isFalling; } }

    ReactiveProperty<int> count = new ReactiveProperty<int>();
    public IObservable<int> Count { get { return count; } }

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(WaitTime);
    }

    IEnumerator FallDeray(GameObject obj)
    {
        yield return waitForSeconds;
        obj.gameObject.transform.position = resetPosition.position;
        obj.gameObject.transform.rotation = this.transform.rotation;
        isFalling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            count.Value++;
            StartCoroutine(FallDeray(other.gameObject));
        }
        else if(other.gameObject.CompareTag("Move"))
        {
            StartCoroutine(FallDeray(other.gameObject));
        }
    }
}
