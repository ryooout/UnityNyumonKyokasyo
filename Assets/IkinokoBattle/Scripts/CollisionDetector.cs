using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    /// <summary>isTrigger��ON�ő���Collider���d�Ȃ��Ă���Ƃ��́A���̃��\�b�h����ɃR�[�������</summary>
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    private void OnTriggerStay(Collider other)
    {
        //OntriggetStay�Ŏw�肳�ꂽ���������s����
        onTriggerStay.Invoke(other);
    }
    /// <summary>�C���X�y�N�^�[���烁�\�b�h��ݒ�ł���悤�ɂȂ���</summary>
    [Serializable]
    public class TriggerEvent:UnityEvent<Collider>
    {

    }
}
