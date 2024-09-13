using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{

    public Animator _animator;
    public PlayerMovement _player;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("isRunning", Input.GetKey(KeyCode.W));
        _animator.SetBool("isRight", Input.GetKey(KeyCode.D));
        _animator.SetBool("isLeft", Input.GetKey(KeyCode.A));
        _animator.SetBool("isBack", Input.GetKey(KeyCode.S));
        _animator.SetBool("isJumping", Input.GetKey(KeyCode.Space));
    }
}
