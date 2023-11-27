using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bola : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float initialAngle = 0.67f;
    public float startVelocity = 7f;
    public int tempScoreRight = 0;
    public int tempScoreLeft = 0;
    public TMP_Text scoreleft;
    public TMP_Text scoreright;
    public TMP_Text pauseText;
    public TrailRenderer trail;
    

    void Start()
    { 
        initialPush();
        pauseText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseText.enabled = true;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            pauseText.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInChildren<TrailRenderer>().emitting = !GetComponentInChildren<TrailRenderer>().emitting;
        }
    }

    public Vector2 Test()
    {
        return rb2d.position;
    }

    private void initialPush()
    {
        Vector2 dir;
        if(Random.Range(0, 2) > 0)
        {
            dir = Vector2.left;
        }else
        {
            dir = Vector2.right;
        }
        dir.y = Random.Range(-initialAngle, initialAngle);
        rb2d.velocity = dir * startVelocity;
        GetComponentInChildren<TrailRenderer>().emitting = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // GetComponentInChildren<TrailRenderer>().emitting = false;
        if (collision.gameObject.tag == "Respawn")
        {
            if (rb2d.velocity.x > 0)
            {
                tempScoreRight++;
            }
            if (rb2d.velocity.x < 0)
            {
                tempScoreLeft++;

            }
            GetComponentInChildren<TrailRenderer>().emitting = false;

            scoreleft.text = tempScoreLeft.ToString();
            scoreright.text = tempScoreRight.ToString();
            Vector2 reset = new Vector2(0, 0);
            rb2d.position = reset;
            initialPush();
        }
    }
}

/*
cuando apreto un boton el p2 se convierte en ia

cuando marcas gol spawnee bola centro, no initial push hasta dar a boton

Itween
*/