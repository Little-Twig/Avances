using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;


public class MainCharController : MonoBehaviour
{
    public CharacterController controller;
    private Animator anim;
    public HealthBarController healthbar;

    [SerializeField] Transform attackPoint;
    private float attackRange = .5f;
    [SerializeField]private LayerMask enemyLayers;

    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    [SerializeField] int attackDmg = 1;
    [SerializeField] private float attackRate = 2f;
    private float attackCD = 0;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    private Vector3 Velocity;
    private float smoothTurnTime = 0.1f;
    private float smoothTurnVelocity;

    //EVENTS
    public static event Action onDeath;
    [SerializeField] private UnityEvent onDeathUnity; 



    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        if (currentHealth > 0)
        {
            Move();
            if (Time.time >= attackCD)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    AnimAttack();
                    Attack();
                    attackCD = Time.time + 1f / attackRate;
                }
            }
            
        }
        if (currentHealth <= 0)
        {
            Die();
        }
       
        
        
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        
            if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            if (direction == Vector3.zero)
            {
                Idle();
            }
        
        

        direction *= moveSpeed;
        controller.Move(direction * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

        

       
    }
    private void Idle()
    {
        anim.SetFloat("Speed", 0.1f);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1f);
    }

    private void AnimAttack()
    {
        anim.SetTrigger("Attack");
    }
    
    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<MimicController>().TakeDamage(attackDmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int Dmg)
    {
        
        currentHealth -= Dmg;
        healthbar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            

            anim.SetTrigger("Hurt");
            
            
        }
    
        
    }

    private void Die()
    {

        anim.SetBool("isDead", true);
        onDeath?.Invoke();
        onDeathUnity?.Invoke();
        Debug.Log("Player has died");

    }
}
