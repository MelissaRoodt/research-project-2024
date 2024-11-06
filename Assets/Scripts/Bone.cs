using UnityEngine;

/// A treat (bone) that the dog can eat.
/// 
/// A interactable object that rotates when falling and stays static when colliding with the ground.
/// Upon colliding with the dog gameobject it dissapears.
/// Lifecycle: falling --> stop --> disapear --> destroy.
public class Bone : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsDog;
    [SerializeField] private GameObject sprite;

    [SerializeField] private float colliderRadius = 0.2f;
    [SerializeField] private float maxDestroyTimer = 3f;
    private float destroyTimer;

    private void Start()
    {
        destroyTimer = maxDestroyTimer;
    }

    private void FixedUpdate()
    {
        bool isGrounded = Physics2D.OverlapCircle(transform.position, colliderRadius, whatIsGround);
        bool isDog = Physics2D.OverlapCircle(transform.position, colliderRadius, whatIsDog);

        if (!isGrounded)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            sprite.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        }
        else
        {
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
                destroyTimer = 0;
            }
            else
            {
                sprite.transform.rotation = new Quaternion(0, 0, 0, 0);
                float alpha = Mathf.Clamp01(destroyTimer / maxDestroyTimer);
                sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
                destroyTimer -= Time.deltaTime;
            }
        }

        if (isDog)
        {
            Destroy(gameObject);
        }
    }
}