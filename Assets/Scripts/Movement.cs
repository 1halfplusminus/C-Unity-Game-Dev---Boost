using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IUseInput, IUsePhysics
{
    [SerializeField]
    public Vector3 speed;

    [SerializeField]
    AudioClip thrustForwardSound;

    [SerializeField]
    ParticleSystem thrustForwardEffect;

    [SerializeField]
    ParticleSystem rotateLeftEffect;

    [SerializeField]
    ParticleSystem rotateRightEffect;
    private Vector3 inputDirection;
    public bool spacePressed { get; private set; }
    Vector3 IUseInput.inputDirection { get => inputDirection; set => inputDirection = value; }
    bool IUseInput.spacePressed { get => spacePressed; set => spacePressed = value; }
    Rigidbody IUsePhysics.rigidbody => rigidbody;
    AudioSource audioSource;
    new Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
        PlayEffect();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotate();
    }
    void ProcessThrust()
    {
        rigidbody.AddRelativeForce(inputDirection * speed.x * Time.deltaTime);
    }

    void ProcessRotate()
    {
        rigidbody.freezeRotation = true; // Blocking the rigidbody rotation
        transform.Rotate(new Vector3(0, 0, inputDirection.z * speed.z * Time.deltaTime));
        rigidbody.freezeRotation = false;
    }

    void PlayEffect()
    {
        PlayRotationEffect();
        PlayThrustForwardEffect();
    }

    private void PlayThrustForwardEffect()
    {
        if (spacePressed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustForwardSound);
            }
            if (!thrustForwardEffect.isPlaying)
            {
                thrustForwardEffect.Play();
            }

        }
        else
        {
            audioSource.Stop();
            thrustForwardEffect.Stop();
        }
    }

    private void PlayRotationEffect()
    {
        if (inputDirection.z <= -0.1)
        {
            rotateRightEffect.Play();
        }
        else if (inputDirection.z >= 0.1)
        {
            rotateLeftEffect.Play();
        }
    }
}
