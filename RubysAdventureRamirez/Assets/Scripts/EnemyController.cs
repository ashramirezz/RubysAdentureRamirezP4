using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
            //remember ! inverse the test, so if broken is true !brokern will be false and return won't be executed. 

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if(!broken)
        {
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won't be executed. 

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; ;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        if(!broken)
        {
            return;
        }

        rigidbody2D.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    //Public because we want to call it from elsewhere likee the projectile script. 
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");

        smokeEffect.Stop();
        
    }
}
