using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /// <summary>����X�s�[�h</summary>
    [SerializeField] private float moveSpeed = 3;
    /// <summary>�W�����v��</summary>
    [SerializeField] private float jumpPower = 3;
    private CharacterController _characterController;
    private Transform _transform;
    private Vector3 _moveVelocity;
    [SerializeField] private Animator animator;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�n�ʂɂ��邩���Ȃ������f�o�b�O�ŏo��
        Debug.Log(_characterController.isGrounded ? "�n��ɂ��܂�" : "�󒆂ł�");

        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        //�n�ʂɂ��Ă�����
        if(_characterController.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("�W�����v");
                //y���ɃW�����v�͂���
                _moveVelocity.y = jumpPower;
            }
        }
        //�n�ʂɂ��Ă��Ȃ�������
        else
        {
            //�d�͂������ĉ���
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        _characterController.Move(_moveVelocity * Time.deltaTime);
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
