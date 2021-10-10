using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ColisionController : MonoBehaviour
{
    public Slider slider;
    public Text garbageProgress;
    public Transform fireSparkle;
    public Rigidbody robot_Rigidbody;
    public ParticleSystem explosion;
    public ParticleSystem robotParticle;
    public GameObject pathCreatorObject;

    [SerializeField] PathCreator pathCreator;
    [SerializeField] GameManager gameManager;
    [SerializeField] private NavMeshAgent navmeshagent;



    bool exploded;
    int sliderProgress = 0;
    public int score = 0;
    int numberOfGarbages;

    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        
    }




    // Start is called before the first frame update
    void Start()
    {
        exploded = false;
        //Number of the 'garbage' objects in the scene.        
        numberOfGarbages = GameObject.FindGameObjectsWithTag("garbage").Length;
        //Equalize the max value of slider to number of the 'garbage' objects in the sccene. 
        slider.maxValue = numberOfGarbages;
        //'Fire' sparkle deactived because of there is no crash at the start.
        fireSparkle.GetComponent<ParticleSystem>().enableEmission = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //If condition that controls is robot collide with any garbage in the room that tagged as 'garbage'.
        if (collision.gameObject.tag == "garbage")
        {   
            
            //Destroy the garbage that robot on it.
            Destroy(collision.gameObject);
            //Increase the sliderprogress as +1.
            sliderProgress=sliderProgress+1;
            //MakeSlider
            slider.value = sliderProgress;

            robotParticle.Play();
        }

        //If condition that controls is robot collide with any object in the room that tagged as 'room'.
        else if(collision.gameObject.tag == "room")
        {
            //pathCreator.PathCleaner();

            pathCreatorObject.SetActive(false);

            navmeshagent.speed=0;

            Explosion();           

            StartSparkle();
            
            gameManager.EndGame();



        }
    }
    //Method that calculates percentage value of garbages that robot pull.
    public void ScorePercent()
    {
        score = (sliderProgress * 100) / numberOfGarbages;
        garbageProgress.text = score + "%";
    }

    // Method that make active 'Fire Sparkle' when the robot crashes.
    public void StartSparkle()
    {
        fireSparkle.GetComponent<ParticleSystem>().enableEmission = true;
       
    }
    //Method that play the 'Explosion' particle system.
    public void Explosion()
    {
        //Check the 'exploded' boolean to recognize it is already crashed or not.
        if (exploded == false)
            explosion.Play();
    }
    
 
}
