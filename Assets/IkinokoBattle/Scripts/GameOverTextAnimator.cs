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
        //�������W��ێ�
        var defaultPosition = transformCache.localPosition;
        //��̕��Ɉړ�������
        transformCache.localPosition = new Vector3(0, 300);
        //�A�j���[�V�����J�n
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("GAME OVER!!");
                //�V�F�C�N�A�j���[�V����
                transformCache.DOShakePosition(1.5f, 100);
            });
        //DoTween�ɂ́ACoroutine���g�킸�ɔC�ӂ̕b����҂Ă郁�\�b�h������
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
