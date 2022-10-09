using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    {
        None,
        Normal,//通常
        Attack,//攻撃中
        Die//死亡
    }
    /// <summary>移動可能かどうか</summary>
    public bool IsMovable => StateEnum.Normal == _state;
    /// <summary>攻撃可能かどうか</summary>
    public bool IsAttackable => StateEnum.Normal == _state;
    /// <summary>ライフの最大値を返す</summary>
    public float LifeMax => lifeMax;
    /// <summary>ライフの値を返す</summary>
    public float Life => _life;
    [SerializeField] private float lifeMax = 10;//ライフ最大値
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; //Mob状態
    private float _life;//現在のライフ値（Hp）
    void Start()
    {
        _life = lifeMax;
        _animator = GetComponent<Animator>();
    }
    /// <summary>キャラクターが倒れたとき</summary>
    protected virtual void OnDie()
    {

    }
    /// <summary>ダメージ関数</summary>
    public void Damage(int damage)
    {
        //死んでいたら戻り値を返す
        if (_state == StateEnum.Die) return;

        _life -= damage;
        //ダメージを受けた時点で0より大きかったら戻り値を返す
        if (_life > 0) return;
        //lifeが0以下の場合の処理
        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }
    /// <summary>攻撃中の状態に遷移</summary>
    public void GoToAttackStateIfPossible()
    {
        //攻撃可能でなければ戻り値を返す
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }
    /// <summary>Idle状態に戻す</summary>
    public void GoToNormalStateIfPossible()
    {
        //死亡アニメーションに遷移した場合戻り値を返す
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Normal;
    }
}
