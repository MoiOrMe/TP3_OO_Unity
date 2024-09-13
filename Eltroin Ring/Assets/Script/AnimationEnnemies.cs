using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnnemies : MonoBehaviour
{
    public Animator _animator;
    public EnnemiesMovement _ennemi;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetBool("isWalking", true);
    }
}
