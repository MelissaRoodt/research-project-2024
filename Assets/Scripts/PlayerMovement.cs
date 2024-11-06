using UnityEngine;
using Unity.Netcode;

/// Movement controller for the player
/// 
/// Is a singleton
public class PlayerMovement : NetworkBehaviour
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

    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        moveTimer = maxMoveTimer;
    }

    private void Update()
    {
        if (!IsServer) return;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        if(moveDir != Vector3.zero )
        {
            moveTimer -= Time.deltaTime;
            if( moveTimer < 0 ) {
                moveTimer = maxMoveTimer;
                moveDir = Vector3.zero;
                anim.Play("idle");
            }
        }
    }

    /// ServerRPC method:drop bone when drop action event triggered by client 
    /// see NetworkController() on how client triggers this method
    [ServerRpc(RequireOwnership = false)]
    public void DropServerRpc() 
    {
        anim.Play("drop");
        GameObject bone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
    }

    /// ServerRPC method:move left when left action event triggered by client 
    /// see NetworkController() on how client triggers this method
    [ServerRpc(RequireOwnership = false)]
    public void MoveLeftServerRpc()
    {
        moveDir = Vector3.left;
        anim.Play("run");
        sprite.flipX = true;

    }

    /// ServerRPC method:move right  when right action event triggered by client 
    /// see NetworkController() on how client triggers this method
    [ServerRpc(RequireOwnership = false)]
    public void MoveRightServerRpc()
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
