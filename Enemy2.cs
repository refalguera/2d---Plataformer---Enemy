using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyController))]

public class Enemy2 : MonoBehaviour
{
    public enum State { Pause, Chasing, Attacking };
    State currentState;

    Transform target;
    Transform myTransform;
    public Material playercolor;
    Rigidbody2D myRigidBody2D;
    public Rigidbody2D playerrigid;
    RaycastHit2D hit;
    public GameObject player;
    BoxCollider2D boxcollider;
    EnemyController controller;

    Vector2 originalposition;
    bool collisionplayer = false;
    public float enemylife = 10;
    public bool damage = false;

    bool timerenemyattack = false;

    public float MaxDist;
    public float MinDist;
    public float x;
    float maxRange = 10;
    float minRange = 8;
    float moveSpeed = 2;
    float speed = 2;
    float speedplayer = 0.25f;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        myRigidBody2D = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        controller = GetComponent<EnemyController>();    
           }

    void Update()
    {
        //Debug.DrawLine(target.position, myTransform.position, Color.yellow);
        FollowTargetWitouthRotation(target, MinDist, moveSpeed);
       /* if ((Vector3.Distance(transform.position, target.position) < maxRange)
    && (Vector3.Distance(transform.position, target.position) > minRange))
        {
            currentState = State.Chasing;
            transform.LookAt(target);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }*/

        if(player.GetComponent<Player>().Enemy)
        {
            Damage();
        }

    }

    void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
         Debug.Log("OKOK");
         Vector2 enemyposition = transform.position;
         Vector2 force = new Vector2(1000, 0);
         var direction = Vector3.zero;
         //Vector2 position;

        direction = target.position - transform.position;
        
         Vector2 facedirection = direction.normalized.x > 0 ? Vector2.right : Vector2.left;
         Vector2 facedirectionup = direction.normalized.y > 0 ? Vector2.up : Vector2.down;
         Vector2 position;

             if (direction.normalized.x > 0)
         {
               enemyposition.x += transform.localScale.x / 2 + 0.1f;
             }
             else
             {
                enemyposition.x -= transform.localScale.x / 2 + 0.1f;
             }

         RaycastHit2D hit = Physics2D.Raycast(enemyposition, facedirection, distanceToStop);
         RaycastHit2D hit2 = Physics2D.Raycast(enemyposition, facedirection, 4);
         RaycastHit2D hitup = Physics2D.Raycast(enemyposition, facedirectionup, distanceToStop);
         Debug.DrawRay(enemyposition, facedirection * distanceToStop, Color.red);
         Debug.DrawRay(enemyposition, facedirectionup * distanceToStop, Color.yellow);

        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                controller.foundplayer = true;
                if (Vector2.Distance(myRigidBody2D.position, target.position) <= 8 && Vector2.Distance(myRigidBody2D.position, target.position) > 4)
                {
                    //transform.LookAt(target.position);
                    direction = target.position - transform.position;
                    if (direction.normalized.x > 0)
                    {
                        position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                    }
                    else
                    {
                        position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                    }
                    myRigidBody2D.MovePosition(position);
                    //myRigidBody2D.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);myRigidBody2D.position * speed * Time.deltaTime
                }

                else
                {
                    if (Vector2.Distance(myRigidBody2D.position, target.position) <= 4 && Vector2.Distance(myRigidBody2D.position, target.position) >= 1)
                    {
                        direction = target.position - transform.position;
                        controller.foundplayer = true;
                        Debug.Log("Hit2");
                        Rigidbody2D rigidbody2D = hit2.rigidbody;
                        StartCoroutine(TimerAttack());

                        Debug.Log("Timer = "+timerenemyattack);
                        if (timerenemyattack == true)
                        {
                            if (direction.normalized.x > 0)
                            {
                                //rigidbody2D.AddForce(new Vector3(x, target.transform.position.y), ForceMode2D.Impulse);
                                rigidbody2D.AddForce(force);
                            }
                            else
                            {
                                rigidbody2D.AddForce(-force);
                            }

                            player.GetComponent<Renderer>().material.color = Color.yellow;
                        }
                        else
                        {
                            Debug.Log("timer2");
                            player.GetComponent<Renderer>().material.color = playercolor.color;
                        }
                    }
                    else
                    {
                        controller.foundplayer = false;
                    }
                }
            }
        }

                /* if (Vector2.Distance(myRigidBody2D.position, target.position) >=6 && Vector2.Distance(myRigidBody2D.position, target.position) <= 9)
                 {
                     //transform.LookAt(target.position);
                     direction = target.position - transform.position;
                     if (direction.normalized.x > 0)
                     {
                         position = new Vector2(transform.position.x + 4 * Time.deltaTime, transform.position.y);
                     }
                     else
                     {
                         position = new Vector2(transform.position.x - 4 * Time.deltaTime, transform.position.y);
                     }
                     myRigidBody2D.MovePosition(position);
                     player.GetComponent<Renderer>().material.color = playercolor.color;
                 }*/

             else
             {
                 controller.foundplayer = false;
             }

        /*      if(hit.collider.tag == "Obstaculo")
             {
                 Debug.Log("Collision with obstacule");
             }
         
           /*  if (hit2)
             {
            print("eoq");
                 if (hit2.collider.tag == "Player")
                 {
                  controller.foundplayer = true;
                         Debug.Log("Hit2");
                         Rigidbody2D rigidbody2D = hit2.rigidbody;
                     if (target.position.normalized.x > 0)
                     {
                    rigidbody2D.AddForce(new Vector3(x, target.transform.position.y), ForceMode2D.Impulse);
                         //rigidbody2D.AddForceAtPosition(force, target.position);
                     }
                     else
                     {
                         rigidbody2D.AddForceAtPosition(-force, target.position);
                     }

                     player.GetComponent<Renderer>().material.color = Color.yellow;
                 }
             else
             {
                 controller.foundplayer = false;
             }
         }*/

         if (hitup)
         {
             if(hitup.collider.tag == "Player")
             {
                 controller.foundplayer = true;
             }

             else
             {
                 controller.foundplayer = false;
             }
         }
         Debug.Log(controller.foundplayer);
         
        /*if (Vector2.Distance(myRigidBody2D.position, target.position) <= MaxDist)
        {
            if (Vector2.Distance(myRigidBody2D.position, target.position) <= MaxDist && Vector2.Distance(myRigidBody2D.position, target.position) >= MinDist)
            {
                controller.foundplayer = true;
                if (Vector2.Distance(myRigidBody2D.position, target.position) >= 6 && Vector2.Distance(myRigidBody2D.position, target.position) <= 9)
                {
                    // transform.LookAt(target.position);
                    direction = target.position - transform.position;
                    if (direction.normalized.x > 0)
                    {
                        position = new Vector2(transform.position.x + 4 * Time.deltaTime, transform.position.y);
                    }
                    else
                    {
                        position = new Vector2(transform.position.x - 4 * Time.deltaTime, transform.position.y);
                    }
                    print(position);
                    myRigidBody2D.MovePosition(position);
                    player.GetComponent<Renderer>().material.color = playercolor.color;
                }
                else
                {
                    if (Vector2.Distance(myRigidBody2D.position, target.position) >= 2 && Vector2.Distance(myRigidBody2D.position, target.position) <= 6 ||
                        Vector2.Distance(myRigidBody2D.position, target.position) >= 9 && Vector2.Distance(myRigidBody2D.position, target.position) <= MaxDist)
                    {
                        //  transform.LookAt(target.position);
                        direction = target.position - transform.position;
                        if (direction.normalized.x > 0)
                        {
                            position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                        }
                        else
                        {
                            position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                        }
                        myRigidBody2D.MovePosition(position);
                    }
                  
                    else if (Vector2.Distance(myRigidBody2D.position, target.position) >= MinDist && Vector2.Distance(myRigidBody2D.position, target.position) <= 2)
                    {
                        if (target.position.normalized.x > 0)
                        {
                            player.GetComponent<Rigidbody2D>().AddForceAtPosition(force, target.position);
                        }
                        else
                        {
                            player.GetComponent<Rigidbody2D>().AddForceAtPosition(-force, target.position);
                        }

                        player.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                }
            }
            else
            {
                controller.foundplayer = false;
               /* if (Vector2.Distance(myRigidBody2D.position, target.position) <= MinDist && Vector2.Distance(myRigidBody2D.position, target.position) >= 1)
                {
                    if (target.position.normalized.x > 0)
                    {
                        player.GetComponent<Rigidbody2D>().AddForceAtPosition(force, target.position);
                    }
                    else
                    {
                        player.GetComponent<Rigidbody2D>().AddForceAtPosition(-force, target.position);
                    }

                    player.GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
        else
        {
            controller.foundplayer = false;
        }*/
 }

    public void Damage()
    {
        if(enemylife > 0)
        {
            enemylife -= 1;
            player.GetComponent<Player>().Enemy = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator TimerAttack()
    {
        float refreshRate = 1;

        refreshRate -= Time.deltaTime;

         if(refreshRate < 0)
        {
            timerenemyattack = false;
        }
        else
        {
            timerenemyattack = true;
        }

        yield return new WaitForSeconds(1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Collsion Player");
            playerrigid.AddForce(-target.position * speed);
        }
    }
}
