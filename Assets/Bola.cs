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
    public TrailRenderer trailBola;
    public ParticleSystem expolsionBola;
    public bool hitted;

    void Start()
    {
        expolsionBola = GetComponentInChildren<ParticleSystem>();
        trailBola = GetComponentInChildren<TrailRenderer>();
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

        if (Input.GetKeyDown(KeyCode.Space) && scoreleft.enabled)
        {
            initialPush();
        }

        if(hitted && expolsionBola.isStopped)
        {
            RespawnBall();
            hitted = false;
        }

    }
    private void initialPush()
    {
        Vector2 dir;
        if (Random.Range(0, 2) > 0)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }
        dir.y = Random.Range(-initialAngle, initialAngle);
        rb2d.velocity = dir * startVelocity;
        scoreleft.enabled = false;
        scoreright.enabled = false;
        trailBola.emitting = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            if (rb2d.velocity.x > 0)
            {
                tempScoreRight++;
                hitted = true;
            }
            if (rb2d.velocity.x < 0)
            {
                tempScoreLeft++;
                hitted = true;
            }
            expolsionBola.Play();
            trailBola.emitting = false;
        }
    }

private void RespawnBall()
    {
        scoreleft.text = tempScoreLeft.ToString();
        scoreright.text = tempScoreRight.ToString();
        Vector2 reset = new Vector2(0, 0);
        rb2d.position = reset;
        rb2d.velocity = reset * 0f;
        scoreleft.enabled = true;
        scoreright.enabled = true;
    }
}


/*
Limites eje Y jugador IA 

cambio de color al chocar con bola

Itween
*/