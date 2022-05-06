using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem secondaryBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioEngine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioEngine = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
        void ProcessThrust()
        {    
            if (Input.GetKey(KeyCode.Space))
        {
            StartThrusters();
        }
        else
        {
            StopThrusters();
        }
    }
    void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }
        else if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
        else
            {
                StopRotation();
            }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void StopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    void StartThrusters()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioEngine.isPlaying)
        {
            audioEngine.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
        if (!secondaryBooster.isPlaying)
        {
            secondaryBooster.Play();
        }
    }

    void StopThrusters()
    {
        audioEngine.Stop();
        mainBooster.Stop();
        secondaryBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics can take over
    }
}
