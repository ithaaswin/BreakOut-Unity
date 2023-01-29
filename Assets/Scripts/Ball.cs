using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minSpeed = 20f;
    public float maxSpeed = 30f;
    private float curSpeed;
    private bool accelerate = true;
    Rigidbody rBody;

    void Awake(){
        rBody = GetComponent<Rigidbody>();
    }
    private void Start() {
        Physics.IgnoreLayerCollision(6, 7, ignore:true);
    }

    private void Update() {
        rBody.velocity = rBody.velocity.normalized * curSpeed;
    }

    public void accelerateBall(){
        // rBody.velocity = speed * Vector3.down;
        float xComponent = Random.Range(-1, 1) == 0 ? -1 : 1;
        float yComponent = Random.Range(-1, 1) == 0 ? -1 : 1;
        curSpeed = minSpeed;

        rBody.velocity = new Vector3(minSpeed * xComponent, minSpeed * yComponent, 0);
    }

    public void alterBallSpeed(){
        if (accelerate)
            curSpeed = curSpeed + 4f;
        else
            curSpeed = curSpeed - 4f;

        if (curSpeed <= minSpeed)
            accelerate = true;
        else if (curSpeed >= maxSpeed)
            accelerate = false;
        
        rBody.velocity = rBody.velocity.normalized * curSpeed;
    }
}
