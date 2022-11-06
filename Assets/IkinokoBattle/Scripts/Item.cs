using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Wood,//木
        Stone,
        ThrowAxe,//投げる斧
    }
    [SerializeField] ItemType type;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        //アニメーションが終わるまでColliderを無効
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;
        //出現アニメーション
        var transformCache = transform;
        var dropPosition = transform.localPosition + 
        new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.DOScale(defaultScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                //アニメーションが終わったらcolliderを有効化する
                colliderCache.enabled = true;
            });
    }
    private void OnTriggerEnter(Collider other)
    {
        //当たった相手がplayerじゃなかったら返す
        if (!other.CompareTag("Player")) return;

        Destroy(gameObject);

    }
}
