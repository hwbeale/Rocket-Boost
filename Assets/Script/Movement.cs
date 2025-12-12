using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    //Variables//
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float ThrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    //Variables//

    //Chache//
    Rigidbody rb;
    AudioSource audioSource;
    //Chache//


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * ThrustStrength * Time.fixedDeltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if(!mainBooster.isPlaying)
            {
                mainBooster.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }

    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);

            if(!rightBooster.isPlaying)
            {
                leftBooster.Stop();
                rightBooster.Play();
            }
        }
        else if(rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);

            if(!leftBooster.isPlaying)
            {
                rightBooster.Stop();
                leftBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
