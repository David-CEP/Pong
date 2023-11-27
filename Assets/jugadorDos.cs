using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorDos : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Bola pelota;


    void Update()
    {
        Move();
        Vector2 tempBola = pelota.Test();
        Debug.Log(tempBola.y);
    }

    private void Move()
    {
        Vector2 tempPos = rb2d.velocity;
        tempPos.y = Input.GetAxis("movimientoDos") * 5f;
        rb2d.velocity = tempPos;
    }
}
