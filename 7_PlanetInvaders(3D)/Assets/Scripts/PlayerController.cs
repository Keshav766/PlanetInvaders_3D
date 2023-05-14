using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("player speed")][SerializeField] float controlSpeed = 20f;
    [Tooltip("Ship horizontal move limit")][SerializeField] float xRange = 5f;
    [Tooltip("Ship vertical move limit")][SerializeField] float yRange = 3f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -10f;

    [Header("Player lasers array")]
    [Tooltip("Add all player lasers")][SerializeField] GameObject[] lasers;
    [SerializeField] ParticleSystem laserParticlesleft;
    [SerializeField] ParticleSystem laserParticlesright;

    float xThrow, yThrow;


    void Start()
    {

    }


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        // getting keyboard input
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // defining the changed x position acc. to input
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        // defining for y
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        // applying the defined position
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        // this stuff is damn confusing
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor; // not only this line doesnt make any sense its also useless
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;

        // float yawDueToPosition = transform.localPosition.x + positionYawFactor;
        // float yawDueToControl = xThrow * controlYawFactor;
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        bool fire = Input.GetButton("Jump");
        if (fire)
        {
            SetLasers(true);
        }
        else
        {
            SetLasers(false);
        }
    }

    void SetLasers(bool laserstate)
    {
        foreach (GameObject x in lasers)
        {
            var emissionModule = x.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = laserstate;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy collision");
        }
        else if(other.tag == "Obstacles")
        {
            Debug.Log("Obsticle Collision");
        }
    }
}
