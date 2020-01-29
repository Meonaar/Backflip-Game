using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Healp Settings")]
    private Rigidbody2D rb;
    public Transform startPosition;
    private Animation ani;
    public Vector2 speed;

    [Header("Scriptable objects")]
    public Platforms plat1;
    public Platforms plat2;
    public Platforms plat3;
    private GameObject platform;

    [Header("UI and Score")]
    public Text scoreNumber;
    int score;

    [Header("Logic")]
    private bool isRun;
    private bool ready;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();

        scoreNumber.text = PlayerPrefs.GetInt("Score").ToString();
        score = PlayerPrefs.GetInt("Score");

        platform = Instantiate(plat1.Platform);
        height = plat1.time;
    }

    // Update is called once per frame
    void Update()
    {
        Launch();

        scoreNumber.text = score.ToString();
    }

    void Launch()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 Vo = CalculateVelocity(mousePos, transform.position, height);

        if (Input.GetMouseButtonDown(0) & ready == true)
        {
            isRun = false;
            rb.velocity = Vo;
            ani.Play("Jump_Up");
            ready = false;
            
            score++;
            PlayerPrefs.SetInt("Score", score);
        }
    }

    Vector2 CalculateVelocity(Vector2 target, Vector2 origin, float time)
    {
        Vector2 distance = target - origin;
        Vector2 distanceX = distance;
        distanceX.y = 0f;

        float Sx = distanceX.magnitude;

        float Vx = Sx / time;
        float Vy = 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 velocity = distanceX.normalized;
        velocity *= Vx;
        velocity.y = Vy;

        return velocity;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {

            if (Input.GetMouseButtonDown(0))
            {
                ready = true;

                if (platform.name == "Platform_Big(Clone)")
                {
                    Destroy(platform);
                    platform = Instantiate(plat2.Platform);
                    height = plat1.time;
                }

                else if (platform.name == "Platform_Medium(Clone)")
                {
                    Destroy(platform);
                    platform = Instantiate(plat3.Platform);
                    height = plat2.time;
                }

                else if (platform.name == "Platform_Small(Clone)")
                {
                    Destroy(platform);
                    platform = Instantiate(plat1.Platform);
                    height = plat3.time;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" & isRun == true)
        {
            rb.velocity = speed;
            
        }
        else if (collision.transform.tag == "Ground" & isRun == false)
        {
            isRun = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (startPosition != null)
        {
            transform.position = startPosition.position;
            ready = false;
        }
    }
}   
