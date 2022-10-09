using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    {
        None,
        Normal,//�ʏ�
        Attack,//�U����
        Die//���S
    }
    /// <summary>�ړ��\���ǂ���</summary>
    public bool IsMovable => StateEnum.Normal == _state;
    /// <summary>�U���\���ǂ���</summary>
    public bool IsAttackable => StateEnum.Normal == _state;
    /// <summary>���C�t�̍ő�l��Ԃ�</summary>
    public float LifeMax => lifeMax;
    /// <summary>���C�t�̒l��Ԃ�</summary>
    public float Life => _life;
    [SerializeField] private float lifeMax = 10;//���C�t�ő�l
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; //Mob���
    private float _life;//���݂̃��C�t�l�iHp�j
    void Start()
    {
        _life = lifeMax;
        _animator = GetComponent<Animator>();
    }
    /// <summary>�L�����N�^�[���|�ꂽ�Ƃ�</summary>
    protected virtual void OnDie()
    {

    }
    /// <summary>�_���[�W�֐�</summary>
    public void Damage(int damage)
    {
        //����ł�����߂�l��Ԃ�
        if (_state == StateEnum.Die) return;

        _life -= damage;
        //�_���[�W���󂯂����_��0���傫��������߂�l��Ԃ�
        if (_life > 0) return;
        //life��0�ȉ��̏ꍇ�̏���
        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }
    /// <summary>�U�����̏�ԂɑJ��</summary>
    public void GoToAttackStateIfPossible()
    {
        //�U���\�łȂ���Ζ߂�l��Ԃ�
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }
    /// <summary>Idle��Ԃɖ߂�</summary>
    public void GoToNormalStateIfPossible()
    {
        //���S�A�j���[�V�����ɑJ�ڂ����ꍇ�߂�l��Ԃ�
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Normal;
    }
}
