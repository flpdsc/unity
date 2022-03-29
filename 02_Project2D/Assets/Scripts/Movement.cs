using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
    }

    private void Update()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * x * speed * Time.deltaTime);

        anim.SetInteger("horizontal", x);
        if (x != 0)
            spriteRenderer.flipX = (x == -1);
    }
}