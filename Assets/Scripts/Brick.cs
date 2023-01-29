using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private int health = 2;
    private Renderer render;
    [SerializeField] GameObject burstParticleSystem;


    // Start is called before the first frame updates
    void Start()
    {
        render = GetComponent<Renderer>();
        Physics.IgnoreLayerCollision(7, 8, ignore:true);
    }
    // Called when a collision is detected
    private void OnCollisionEnter(Collision hittingObj) {
        health --;

        Color col = render.material.color;
        col.a = col.a / 2.0f;
        render.material.color = col;


        if(health <= 0){
            var ballObj = hittingObj.gameObject.GetComponent<Ball>();
            if (ballObj != null){
                AudioManager.instance.brick();
                Destroy(gameObject);
                GameObject burstObj = Instantiate(burstParticleSystem, transform.position, transform.rotation);  
                Destroy(burstObj, 1.0f);
                GameManager.instance.isPowerUp(transform.position, transform.rotation);
                GameManager.instance.brickDestroyed();
            }
        }
    }
}