using UnityEngine;
using UnityEngine.InputSystem;
using R3;               // R3 core
using R3.Triggers;
using System.Security.Cryptography;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    public int Attackpower;
    public Renderer targetRenderer;
    public Renderer targetRenderer1;
    public Renderer targetRenderer2;
    public Renderer targetRenderer3;
    public Color normalColor = Color.white;
    public Color changeColor = Color.red;
    public float changeTime = 2f;

    private bool isGrounded = false;
    private bool isChanging = false;
    public bool isAttacking = false;
    public bool IsAttacking()
    {
        return isAttacking;
    }

    public float MaxLife => 100f;
    public ReactiveProperty<float> life { get; private set; } = new();

    PlayerInput playerInput;
    Rigidbody2D rb;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        life.Value = MaxLife;
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        if (move.x != 0f)
        {
            rb.linearVelocityX = move.x * speed;
            // 向き
            var localScale = transform.localScale;
            if (move.x < 0)
            {
                animator.Play("Run");
                localScale.x = 1f;
            }
            else
            {
                animator.Play("Run");
                localScale.x = -1f;
            }
            transform.localScale = localScale;
        }

        // ジャンプ
        if (playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocityY = jumpSpeed;
            isGrounded = false;
            animator.Play("Jump");
        }

        //攻撃
        if (playerInput.actions["Attack"].WasPressedThisFrame())
        {
            StartCoroutine(AttackRoutine());
            animator.Play("Attack");
        }

    }
    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.3f); // 攻撃判定時間

        isAttacking = false;
    }

    private void FixedUpdate()
    {
        isGrounded = false;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(life.Value > 0 && !isChanging)
            {
                life.Value -= 10;
                animator.Play("Die");
                StartCoroutine(ChangeColorTemporarily());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    IEnumerator ChangeColorTemporarily()
    {
        isChanging = true;

        // 色を変更
        targetRenderer.material.color = changeColor;
        targetRenderer1.material.color = changeColor;
        targetRenderer2.material.color = changeColor;
        targetRenderer3.material.color = changeColor;

        // 指定時間待つ
        yield return new WaitForSeconds(changeTime);

        // 元に戻す
        targetRenderer.material.color = normalColor;
        targetRenderer1.material.color = normalColor;
        targetRenderer2.material.color = normalColor;
        targetRenderer3.material.color = normalColor;

        isChanging = false;
    }
}
