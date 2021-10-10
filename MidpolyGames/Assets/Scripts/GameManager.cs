using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    [SerializeField] ColisionController collisionController;

    public GameObject endGamePanel;
    public GameObject robot;
    public GameObject failedImage;
    public GameObject successImage;
    public Text scoreText;
   
    //Set 'failed' and 'success' images deactive
    private void Start()
    {
        failedImage.SetActive(false);
        successImage.SetActive(false);
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            endGamePanel.SetActive(true);
            //if score lower than 50 it will be failed
            if (collisionController.score < 50)
                failedImage.SetActive(true);
            //if score greater than 50 it will be success
            else if (collisionController.score > 50)
                successImage.SetActive(true);
            
            scoreText.text=("Score\n"+collisionController.score+"%");

        }
    }
    //Method that re-open the scene 
    public void Restart()
    {
        //Scenemanager function that load active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
