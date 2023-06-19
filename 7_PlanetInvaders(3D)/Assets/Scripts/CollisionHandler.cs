using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticles;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void OnCollisionEnter(Collision other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashParticles.Play();
        DisabelingPlayer();
        Invoke("RestartLevel", 1);
    }

    void RestartLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    void DisabelingPlayer()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
