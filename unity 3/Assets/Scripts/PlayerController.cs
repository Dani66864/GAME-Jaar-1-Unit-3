using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playeranim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticale;
    public ParticleSystem dirtParticale;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool GemeOver;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playeranim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !GemeOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playeranim.SetTrigger("Jump_trig");
            dirtParticale.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticale.Play();
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("game over");
            GemeOver = true;
            playeranim.SetBool("Death_b", true);
            playeranim.SetInteger("DeathType_int", 1);
            explosionParticale.Play();
            dirtParticale.Stop();
        }
    }
}
