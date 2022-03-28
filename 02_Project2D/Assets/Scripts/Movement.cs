using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float speed = 5f;

    private void Start()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(3.0f, 2.0f, 0.0f);
        transform.Translate(new Vector3(10.0f, 0.0f, 0.0f));
        Debug.Log(position);
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right * x * speed * Time.deltaTime);
        transform.Translate(Vector3.up * y * speed * Time.deltaTime);
    }
}