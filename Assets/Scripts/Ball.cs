using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //For Text

/*
 * @author Abigail Tubay
 */
public class Ball : MonoBehaviour
{

    //float speed of the ball
    public float speed = 30;

    //score keeper
    public Text rightScore;
    private int rs = 0;
    public Text leftScore;
    private int ls = 0;
    
    //Audio
    public AudioClip ballHitsWalls;
    public AudioClip ballHitspads;
    public AudioClip gameOver;
    public AudioSource asource;

    private float f;

    /*
     * start
     */
    void Start()
    {
        speed = 2;
        float r = Random.Range(0,2);
        if(r < 1) { //to the left
            GetComponent<Rigidbody2D>().velocity = new Vector2(10,-15) * speed;
        }else { //to the right
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -15) * speed;
        }
        speed = 30;
        
    }
    
    void ReStart() { 
        speed = 2;
        if(f < 1) { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(10,-15) * speed;
        }else {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10, -15) * speed;
        }
        speed = 30;
    
    }

    /*
     * checking for collisions and sending appropriate respose
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision.gameObject is the racket
        // collision.transform.position is the racket's position.
        // collision.collider is the racket's collider.
        
        //Left Racket hit
        if (collision.gameObject.name == "LeftPad") 
        {
            float y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            
            //Audio Sounds
            PlaySound(ballHitspads);
        }
        
        //Right Racket hit
        if (collision.gameObject.name == "RightPad") 
        {
            float y = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            PlaySound(ballHitspads);
        }
        
        //Top/Bottom Wall hits
        if (collision.gameObject.name == "TopWall" || collision.gameObject.name == "BottomWall") 
        {
            PlaySound(ballHitsWalls);
        }

        //hit left wall
        if (collision.gameObject.name == "LeftWall")
        {
            rs++;
            rightScore.text = "" + rs;
            f = 0f;
            //Audio Sounds
            PlaySound(gameOver);
            TTime();


        }
        //hit right wall
        if (collision.gameObject.name == "RightWall")
        {
            ls++;
            leftScore.text = "" + ls;
            f = 2f;
            //Audio Sounds
            PlaySound(gameOver);
            TTime();
            
        }

    }

    /*
        restart ball
    */
    void TTime() 
    {
        StopBall();
        Invoke("ReStart", 1);
    }
    
    /*
        stop ball on origin before continuing the game
    */
    void StopBall() {
        speed = 0;
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(0, 0));
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * speed;
       
    }

    /*
     * play sound
     * @param sound audioclip to play
     */
    void PlaySound(AudioClip sound)
    {
        //play appropriate sound clip
        asource.clip = sound;
        asource.Play();
    }

    /*
     * Determining the position of the ball
     */
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) 
    {
        // 1 <- at the top of the racket
        // 0 <- at the middle of the racket
        // -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

}
