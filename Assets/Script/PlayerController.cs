using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    
    [SerializeField] float speed;

    [SerializeField] float projectileSpeed;

    Animator animator;
    Rigidbody2D rb;

    float vAxis, hAxis, atkDirH, atkDirV;

    Vector2 lastMove = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Attack() 
    {
        atkDirH = Input.GetAxisRaw("Horizontal");
        atkDirV = Input.GetAxisRaw("Vertical");

        Vector2 atkDir = new Vector2(atkDirH, atkDirV).normalized;

        if (atkDir != Vector2.zero)
        {
            lastMove = atkDir;
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetTrigger("Atk");

            GameObject projectile = Instantiate(projectilePrefab, 
                                        transform.position, 
                                        Quaternion.identity);

            ProjectileController projectileController =
                projectile.GetComponent<ProjectileController>();

            projectileController.SetDirection(lastMove, projectileSpeed);

        }
    }

    void Move() 
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        if(Mathf.Abs(hAxis) >= 0.01) {
            animator.SetFloat("X", hAxis);
            animator.SetFloat("Y", 0);
        }

        if (Mathf.Abs(vAxis) >= 0.01) {
            
            animator.SetFloat("Y", vAxis);
            animator.SetFloat("X", 0);
        }

        Vector2 newvelocity = new Vector2(hAxis, vAxis).normalized;

        rb.velocity = newvelocity * speed;
    }

}
