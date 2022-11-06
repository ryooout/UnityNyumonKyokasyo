using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Wood,//��
        Stone,
        ThrowAxe,//�����镀
    }
    [SerializeField] ItemType type;
    /// <summary>
    /// ����������
    /// </summary>
    public void Initialize()
    {
        //�A�j���[�V�������I���܂�Collider�𖳌�
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;
        //�o���A�j���[�V����
        var transformCache = transform;
        var dropPosition = transform.localPosition + 
        new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.DOScale(defaultScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                //�A�j���[�V�������I�������collider��L��������
                colliderCache.enabled = true;
            });
    }
    private void OnTriggerEnter(Collider other)
    {
        //�����������肪player����Ȃ�������Ԃ�
        if (!other.CompareTag("Player")) return;

        Destroy(gameObject);

    }
}
