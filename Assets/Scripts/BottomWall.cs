using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWall : MonoBehaviour
{
    // Called when a collision is detected
    private void OnCollisionEnter(Collision collider) {
        var ballObject = collider.gameObject.GetComponent<Ball>();
        if (ballObject != null && ballObject.name != "ExtraBall"){
            Destroy(collider.gameObject);
            GameManager.instance.gameContinue();
        } else if (collider.transform.gameObject.name == "PowerUpBall" || collider.transform.gameObject.name == "PowerUpPaddle"){
            Destroy(collider.gameObject);
        } else if(collider.transform.gameObject.name == "ExtraBall"){
            Destroy(collider.gameObject);
            AudioManager.instance.powerDown();
        }
    }
}
