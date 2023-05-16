using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyExplosionParticles;
    [SerializeField] Transform parent;
    [SerializeField] float enemyPoints = 100f;
    [SerializeField] int hitPoints = 10;

    ScoreBoard scoreBoardRef;
    Renderer enemyRendererRef;
    float lerpValue = 0.1f;

    void Start()
    {
        scoreBoardRef = FindObjectOfType<ScoreBoard>();
        enemyRendererRef = GetComponent<Renderer>();
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody enemyRB = gameObject.AddComponent<Rigidbody>();
        enemyRB.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        IncreaseScore();
        if (hitPoints > 0)
        {
            hitPoints -= 1;
            ChangeColor();
        }
        else
        {
            KillEnemy();
        }
    }


    void IncreaseScore()
    {
        scoreBoardRef.UpdateScore(enemyPoints);
    }

    void ChangeColor()
    {
        lerpValue += 0.1f;
        Vector4 newColor = Color.Lerp(Color.blue, Color.red, lerpValue);
        enemyRendererRef.material.color = newColor;
    }

    void KillEnemy()
    {
        ParticleSystem vfx = Instantiate(enemyExplosionParticles, gameObject.transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

}
