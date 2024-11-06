using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// Moves player based on keyboard input 
/// 
/// Only used for testing with keyboard 
/// @warning not implemented in game
public class PlayerMovementTest : MonoBehaviour
{
    [Header("Player Movement")]
    private Vector3 moveDir = Vector3.zero;
    private float moveSpeed = 5f;
    private float maxMoveTimer = 0.2f;
    private float moveTimer = 0;

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Bone Drop")]
    [SerializeField] private GameObject bonePrefab;

    private void Start()
    {
        moveTimer = maxMoveTimer;
    }

    private void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        if (moveDir != Vector3.zero)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer < 0)
            {
                moveTimer = maxMoveTimer;
                moveDir = Vector3.zero;
                anim.Play("idle");
            }
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

        if( Input.GetKeyUp(KeyCode.A)) 
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Drop();
        }
    }

    private void Drop()
    {
        anim.Play("drop");
        GameObject bone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
    }

    private void MoveLeft()
    {
        moveDir = Vector3.left;
        anim.Play("run");
        sprite.flipX = true;

    }

    private void MoveRight()
    {
        moveDir = Vector3.right;
        anim.Play("run");
        sprite.flipX = false;

    }

    private void Jump()
    {
        anim.Play("jump");
    }
}
