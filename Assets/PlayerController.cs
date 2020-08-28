using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// adds a rigidbody component
[RequireComponent(typeof(Rigidbody))]           
public class PlayerController : MonoBehaviour
{
    [Range(5,50)]
    public float jumpSpeed = 50f;
    [Range(.25f,5)]
    public float fallSpeedMultiplier = 1f;
    [Range(1,20)]
    public float forwardSpeed = 5f;

    Rigidbody rb;
    bool isGrounded = false;

    public int score = 0;
    TextMeshPro scoreText;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Controller Starting Up!");

        rb = this.GetComponent<Rigidbody>();

        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshPro>();

        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time since last frame = " + Time.deltaTime);

        if(Input.anyKeyDown) Jump();
    }

    // FixedUpdate is for phyiscs, it runs 50 times / second.
    void FixedUpdate() {
        // Debug.Log("Fixed Update frame time = " + Time.deltaTime);

        // adding forward force
        rb.AddRelativeForce(Vector3.right * forwardSpeed);

        if(isGrounded == false) {
            rb.AddRelativeForce(Vector3.down * jumpSpeed * fallSpeedMultiplier);
        }
    }

    void Jump() {
        if(isGrounded) {
            rb.AddRelativeForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
        else if(other.gameObject.CompareTag("Pickup")){
            score += 100;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Finish")){
            //Resets game,, Let player keep High Score
            Debug.Log("Starting Level Over");
            score = 0;
            this.transform.position = startPosition;
            Application.LoadLevel(0);
        }
    }
}
