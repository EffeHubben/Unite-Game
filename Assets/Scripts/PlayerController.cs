using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 input;
    private Animator animator;
    private bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input lezen
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Voorkomt diagonale beweging (laatste input telt)
        if (input.x != 0) input.y = 0;

        // Check of speler beweegt
        if (input != Vector2.zero)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
            isMoving = true;

            // Beweging toepassen
            Move();
        }
        else
        {
            isMoving = false;
        }

        // Animator bijwerken
        animator.SetBool("isMoving", isMoving);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(input.x, input.y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
