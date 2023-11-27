using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorUno : MonoBehaviour
{
    public Rigidbody2D rb2d;
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 tempPos = rb2d.velocity;
        tempPos.y = Input.GetAxis("movimientoUno") * 5f;
        rb2d.velocity = tempPos;
    }
}
