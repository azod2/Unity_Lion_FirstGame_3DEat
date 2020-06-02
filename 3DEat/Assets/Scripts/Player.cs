using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(1,1000)]
    public float speed;
    [Header("跳躍高度"),Range(1,5000)]
    public float height;

    public float v;
    public float h;
    public float gravity;
    private Vector3 angle;
    private float time;
   

    /// <summary>
    /// 是否在地板上
    /// </summary>
    private bool isGround
    {
        get
        {
            if (transform.position.y < 0.071f) return true;
            else return false;
        }
    }

    private Animator ani;
    public Rigidbody body;
    private AudioSource aud;
    public GameManager gm;



    public AudioClip soundRuby, soundDiamondo;


    private void Hitprop(GameObject prop)
    {   

        //print("碰到的物件標籤為 : " + prop.name);
        if (prop.tag=="Ruby")
        {
            aud.PlayOneShot(soundDiamondo, 2);
            Destroy(prop);
        }
        else if (prop.tag=="Dia")
        {
            aud.PlayOneShot(soundRuby, 2);
            Destroy(prop);
        }

        Debug.Log("gm.Getprop : " + prop.tag);
        gm.Getprop(prop.tag);

    }



    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        body.AddForce(0, 0, speed * v);
        body.AddForce(speed * h, 0, 0);
        ani.SetBool("run", true);

        if (v == 1) angle = new Vector3(0, 0, 0);
        else if (v == -1) angle = new Vector3(0, 180, 0);
        else if (h == 1) angle = new Vector3(0, 90, 0);
        else if (h == -1) angle = new Vector3(0, 270, 0);
        /*
        else if (h == 1 && v == 1) angle = new Vector3(0, 45, 0);
        else if (h == 1 && v == -1) angle = new Vector3(0, 315, 0);
        else if (h == -1 && v == 1) angle = new Vector3(0, 135, 0);
        else if (h == -1 && v == -1) angle = new Vector3(0, 225, 0);
        */
        transform.eulerAngles = angle;       

    }   

    private void StopMove()
    {
        body.velocity = Vector3.zero;
        ani.SetBool("run", false);
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (isGround && Input.GetKeyDown("space"))
        {
            body.AddForce(0, height, 0);


            time = 0;

            if (!isGround)
            {
                time += Time.deltaTime;
            body.velocity +=new Vector3(0, gravity, 0);
            }
            ani.SetFloat("jumpforce", time);

            /*
            for (float i = 0; i <= 1.1; i += 0.1f)
            {
                ani.SetFloat("jumpforce", i);
            }
        }
        else
            ani.SetFloat("jumpforce", 0);
        */
        }
    }
    /// <summary>
    /// 吃道具
    /// </summary>


    

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        //gm = GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");

        if (v != 0 || h != 0)
            Move();
        else
            StopMove();


    }


    private void Update()
    {
         Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        Hitprop(other.gameObject);
    }


}
