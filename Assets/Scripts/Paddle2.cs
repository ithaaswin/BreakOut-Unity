using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle2 : MonoBehaviour
{
    public float speed = 125;
    // public float leftBoundary = -27.3f;
    // public float rightBoundary = 27.3f;

    public float leftWall = -30.3f + 0.5f;
    public float rightWall = 30.3f - 0.5f;
    private float halfPaddleSize;

    // Update is called once per frame
    void Start(){
        findPaddleSize();
    }
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X");
        // var something = Input.mousePosition.x;
        transform.Translate(moveX * speed * Time.deltaTime, 0, 0);
        
        var requestedPosition = transform.position;
        transform.position = new Vector3(Mathf.Clamp(requestedPosition.x, leftWall + halfPaddleSize, rightWall - halfPaddleSize), requestedPosition.y, requestedPosition.z);
    }

    void findPaddleSize(){
        halfPaddleSize = gameObject.transform.localScale.x / 2;
    }

    private void OnCollisionEnter(Collision hittingObj) {
        if (hittingObj.transform.gameObject.name == "PowerUpBall"){
            Destroy(hittingObj.gameObject);
            AudioManager.instance.powerUp();
            GameManager.instance.addBall();
        } else if (hittingObj.transform.gameObject.name == "PowerUpPaddle"){
            Destroy(hittingObj.gameObject);
            var oldPaddleSize = halfPaddleSize * 2;
            Vector3 local = transform.localScale;
            transform.localScale = new Vector3(oldPaddleSize + 10, local.y, local.z);
            findPaddleSize();
            AudioManager.instance.powerUp();
            StartCoroutine (getPaddleOriginalSize(oldPaddleSize));
        }
    }

    IEnumerator getPaddleOriginalSize(float oldPaddleSize){
        yield return new WaitForSeconds(10.0f);
        Vector3 local = transform.localScale;
        transform.localScale = new Vector3(oldPaddleSize, local.y, local.z);
        findPaddleSize();
        AudioManager.instance.powerDown();
    }
}
