using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    /// <summary>isTriggerがONで他のColliderを重なっているときは、このメソッドが常にコールされる</summary>
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    private void OnTriggerStay(Collider other)
    {
        //OntriggetStayで指定された処理を実行する
        onTriggerStay.Invoke(other);
    }
    /// <summary>インスペクターからメソッドを設定できるようになった</summary>
    [Serializable]
    public class TriggerEvent:UnityEvent<Collider>
    {

    }
}
