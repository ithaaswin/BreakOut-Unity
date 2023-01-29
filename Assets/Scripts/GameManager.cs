using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MultiPlayer;
    public GameObject StartScreen;
    public GameObject EndScreen;
    public GameObject LevelScreen;
    public GameObject WinScreen;
    public TextMeshProUGUI LevelBoard;
    public TextMeshProUGUI ScoreBoard;
    
    public GameObject Ball;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Paddle2;
    public GameObject PowerUpBall;
    public GameObject powerUpPaddle;
    public bool gameStarted = true;
    
    
    private GameObject ballObject;
    private GameObject ballObject2;
    private GameObject levelObject;

    
    private int score = 0;
    private int countBricks = 0;
    private int ballLoc;
    private int paddleLoc;
    private int curLevel = 1;
    private int maxLevel = 2;
    private bool multiplayer = true;
    

    void Awake() {
        instance = this;
        levelObject = Instantiate(Level1) as GameObject;
        MultiPlayer.SetActive(true);
    }

    void Update()
    {
        if (multiplayer){
            if(Input.GetButton("Single")){
                multiplayer = false;
                MultiPlayer.SetActive(false);
                getReqObjects();
            }
            else if(Input.GetButton("Multi")){
                Instantiate(Paddle2);
                multiplayer = false;
                MultiPlayer.SetActive(false);
                getReqObjects();
            }
        }

        if(!gameStarted && Input.GetButton("Jump")){
            gameStarted = true;
            StartScreen.SetActive(false);
            ballObject.GetComponent<Ball>().accelerateBall();
        }
    }

    void getReqObjects(){
        StartScreen.SetActive(true);
        gameStarted = false;
        countBricks = GameObject.FindObjectsOfType<Brick>().Length + countBricks;
        powerUpLoc();
        createBall();
    }

    void powerUpLoc(){
        do{
            ballLoc = Random.Range(score + 1, countBricks - 1);
            paddleLoc = Random.Range(score + 1, countBricks - 1);
            if (ballLoc != paddleLoc || countBricks <= 2)
                break;
        } while(true);
    }

    public void brickDestroyed(){
        score ++;
        ScoreBoard.text = "Score: " + score.ToString();
        ballObject.GetComponent<Ball>().alterBallSpeed();
        if (score >= countBricks){
            if (ballObject)
                Destroy(ballObject.gameObject);
            StartCoroutine(killExtraBall(0.0001f));
            if (curLevel < maxLevel){
                LevelScreen.SetActive(true);
                AudioManager.instance.levelUp();
                StartCoroutine(toNextLevel());
            } else {
                AudioManager.instance.stopMainMusic();
                AudioManager.instance.gameWon();
                WinScreen.SetActive(true);
            }
            curLevel ++;
        }
    }


    IEnumerator toNextLevel(){
        yield return new WaitForSeconds(5);
        Destroy(levelObject);
        LevelScreen.SetActive(false);
        levelObject = Instantiate(Level2);
        LevelBoard.text = "Level: " + curLevel.ToString();
        getReqObjects();
        ballObject.GetComponent<Ball>().minSpeed = 25f;
        ballObject.GetComponent<Ball>().maxSpeed = 35f;
        
    }

    public void gameContinue(){
        createBall();
        StartCoroutine(restartBall());
    }

    IEnumerator restartBall(){
        yield return new WaitForSeconds(1f);
        ballObject.GetComponent<Ball>().accelerateBall();
    }

    public void createBall(){
        ballObject = Instantiate(Ball);
    }

    public void isPowerUp(Vector3 pos, Quaternion rot){
        if (score == ballLoc && score < countBricks){
            var ballCube = Instantiate(PowerUpBall, pos, rot);
            ballCube.name = "PowerUpBall";
        } else if (score == paddleLoc && score < countBricks){
            var paddleCube = Instantiate(powerUpPaddle, pos, rot);
            paddleCube.name = "PowerUpPaddle";
        }
    }

    
    public void addBall(){
        ballObject2 = Instantiate(Ball);
        ballObject2.name = "ExtraBall";
        ballObject2.GetComponent<Renderer>().material.color = new Color(229, 46, 208);
        ballObject2.GetComponent<Ball>().accelerateBall();
        StartCoroutine(killExtraBall(10.0f));
    }

    IEnumerator killExtraBall(float waitTime){
        yield return new WaitForSeconds(waitTime);
        AudioManager.instance.powerDown();
        if(ballObject2){}
            Destroy(ballObject2);
    }
}
