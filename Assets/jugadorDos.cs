using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorDos : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Bola pelota;
    public float speedMultiplier;
    public bool manual = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            manual = !manual;
        }

        if (manual)
        {
            Move();
        }else
        {
            transform.position = new Vector3(transform.position.x, pelota.transform.position.y, transform.position.z);
        }
    }

    private void Move()
    {
        Vector2 tempPos = rb2d.velocity;
        tempPos.y = Input.GetAxis("movimientoDos") * speedMultiplier;
        rb2d.velocity = tempPos;
    }
}