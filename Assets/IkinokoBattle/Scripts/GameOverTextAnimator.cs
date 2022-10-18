using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameOverTextAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var transformCache = transform;
        //初期座標を保持
        var defaultPosition = transformCache.localPosition;
        //上の方に移動させる
        transformCache.localPosition = new Vector3(0, 300);
        //アニメーション開始
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("GAME OVER!!");
                //シェイクアニメーション
                transformCache.DOShakePosition(1.5f, 100);
            });
        //DoTweenには、Coroutineを使わずに任意の秒数を待てるメソッドもある
        DOVirtual.DelayedCall(10, () =>
         {
             SceneManager.LoadScene("TitleScene");
         });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
