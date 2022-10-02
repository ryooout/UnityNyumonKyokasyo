using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    /// <summary>走るスピード</summary>
    [SerializeField] private float moveSpeed = 3;
    /// <summary>ジャンプ力</summary>
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
        //地面にいるかいないかをデバッグで出力
        Debug.Log(_characterController.isGrounded ? "地上にいます" : "空中です");

        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        //地面についていたら
        if(_characterController.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("ジャンプ");
                //y軸にジャンプ力を代入
                _moveVelocity.y = jumpPower;
            }
        }
        //地面についていなかったら
        else
        {
            //重力をかけて加速
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        _characterController.Move(_moveVelocity * Time.deltaTime);
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
