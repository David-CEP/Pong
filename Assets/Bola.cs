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
    public TMP_Text leftWins;
    public TMP_Text rightWins;
    public TMP_Text restart;
    public TrailRenderer trailBola;
    public TrailRenderer trailOneTop;
    public ParticleSystem expolsionBola;
    public bool hitted;
    public int colorTracker = 0;
    public Color trailGreenStart = new Color(0.086f, 1f, 0f, 1f);
    public Color trailYellowStart = new Color(1f, 0.92f, 0.016f, 1f);
    public Color trailMagentaStart = new Color(1f, 0f, 1f, 1f);
    public Color trailEnd = new Color(0.235f, 0.231f, 0.231f, 0.224f);
    public Color tempSave;
    public bool wasEnabled;
    public bool paused;
    

    void Start()
    {
        expolsionBola = GetComponentInChildren<ParticleSystem>();
        trailBola = GetComponentInChildren<TrailRenderer>();
        pauseText.enabled = false;
        leftWins.enabled = false;
        rightWins.enabled = false;
        restart.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = true;
            pauseText.enabled = true;
            wasEnabled = scoreleft.enabled;
            screenBlack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (paused && wasEnabled)
            {
                Time.timeScale = 1;
                pauseText.enabled = false;
                scoreleft.enabled = true;
                scoreright.enabled = true;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject player in players)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().color = tempSave;
                }
                gameObject.GetComponent<SpriteRenderer>().color = tempSave;
                GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
                gameObject.GetComponent<TrailRenderer>().emitting = true;
                foreach (GameObject trail in trails)
                {
                    trail.GetComponent<TrailRenderer>().emitting = true;
                }
                paused = false;
            }

            if(paused)
            {
                paused = false;
                Time.timeScale = 1;
                pauseText.enabled = false;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject player in players)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().color = tempSave;
                }
                gameObject.GetComponent<SpriteRenderer>().color = tempSave;
                GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
                gameObject.GetComponent<TrailRenderer>().emitting = true;
                foreach (GameObject trail in trails)
                {
                    trail.GetComponent<TrailRenderer>().emitting = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && scoreleft.enabled)
        {
            initialPush();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            RestartGame();
        }

        if(hitted && expolsionBola.isStopped)
        {
            if(tempScoreLeft == 5)
            {
                screenBlack();
                leftWins.enabled = true;
                restart.enabled = true;
            }else if(tempScoreRight == 5)
            {
                screenBlack();
                rightWins.enabled = true;
                restart.enabled = true;
            }
            else
            {
                RespawnBall();
            }
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
                tempScoreLeft++;
                hitted = true;
            }
            if (rb2d.velocity.x < 0)
            {
                tempScoreRight++;
                hitted = true;
            }
            expolsionBola.Play();
            trailBola.emitting = false;
        }

        if(collision.gameObject.tag == "Player")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
            if(colorTracker < 1)
            {
                foreach(GameObject player in players)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    foreach(GameObject trail in trails)
                    {
                        trail.GetComponent<TrailRenderer>().startColor = trailGreenStart;
                        trail.GetComponent<TrailRenderer>().endColor = trailEnd;
                    }
                }
                trailBola.startColor = trailGreenStart;
                trailBola.endColor = trailEnd;
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                expolsionBola.startColor = Color.green;
                colorTracker++;
            }
            else if(colorTracker == 1)
            {
                foreach(GameObject player in players)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    foreach (GameObject trail in trails)
                    {
                        trail.GetComponent<TrailRenderer>().startColor = trailYellowStart;
                        trail.GetComponent<TrailRenderer>().endColor = trailEnd;
                    }
                }
                trailBola.startColor = trailYellowStart;
                trailBola.endColor = trailEnd;
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                expolsionBola.startColor = Color.yellow;
                colorTracker++;
            }
            else
            {
                foreach(GameObject player in players)
                {
                    player.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                    foreach (GameObject trail in trails)
                    {
                        trail.GetComponent<TrailRenderer>().startColor = trailMagentaStart;
                        trail.GetComponent<TrailRenderer>().endColor = trailEnd;
                    }
                }
                trailBola.startColor = trailMagentaStart;
                trailBola.endColor = trailEnd;
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                expolsionBola.startColor = Color.magenta;
                colorTracker = 0;
            }

            Vector2 tempDir = rb2d.position;
            tempDir.x *= -1;
            rb2d.velocity = tempDir;
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

private void RestartGame()
    {
        Time.timeScale = 1;
        restart.enabled = false;
        leftWins.enabled = false;
        rightWins.enabled = false;
        int counter = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
        expolsionBola.startColor = Color.white;
        foreach (GameObject trail in trails)
        {
            trail.GetComponent<TrailRenderer>().startColor = Color.white;
        }
        foreach (GameObject player in players)
        {
            counter++;
            if (counter == 1)
            {
                player.gameObject.transform.position = new Vector2(-7f, 0f);
            }
            else
            {
                player.gameObject.transform.position = new Vector2(7f, 0f);
            }
            player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        trailBola.startColor = Color.white;
        RespawnBall();
        tempScoreLeft = 0;
        tempScoreRight = 0;
        scoreleft.text = "0";
        scoreright.text = "0";
        colorTracker = 0;
    }

private void screenBlack()
    {
        Time.timeScale = 0;
        scoreleft.enabled = false;
        scoreright.enabled = false;
        Color tempColor = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color;
        if (tempColor != Color.black)
        {
            tempSave = tempColor;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
        trailBola.Clear();
        foreach (GameObject trail in trails)
        {
            trail.GetComponent<TrailRenderer>().Clear();
        }
        expolsionBola.Clear();
    }
}