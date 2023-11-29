using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorUno : MonoBehaviour
{
    public Rigidbody2D rb2d;
    void Update()
    {
        Move();
        if (transform.position.y < -3.25f)
        {
            transform.position = new Vector3(transform.position.x, -3.25f, transform.position.z);
        }

        if (transform.position.y > 3.25f)
        {
            transform.position = new Vector3(transform.position.x, 3.25f, transform.position.z);
        }
    }

    private void Move()
    {
        Vector2 tempPos = rb2d.velocity;
        tempPos.y = Input.GetAxis("movimientoUno") * 5f;
        rb2d.velocity = tempPos;
    }
}
