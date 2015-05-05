using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PoinkyMovementController : MonoBehaviour
{
    public static PoinkyMovementController poinky;
    public GameObject decal;
    public AudioClip soundTileColl;
    public AudioClip soundWallColl;
    public AudioClip soundCollectableColl;
    float decalWidth;
    Rigidbody myRigidBody;
    GameObject lastCollision;
    public bool isInSpiral=false;

    private AudioSource source;


    void Awake()
    {
        source = GetComponent<AudioSource>();
        poinky = this;
    }

    // Use this for initialization
    void Start()
    {

        lastCollision = null;
        myRigidBody = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!GameManager.instance.isStarted)
        {
            this.transform.position = new Vector3(0, 1, 0);
            this.myRigidBody.velocity = Vector3.zero;

        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        this.GetComponent<Animator>().SetFloat("speedY", this.GetComponent<Rigidbody>().velocity.y);
        if (this.transform.position.y < -5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameManager.instance.Powerup = PowerUps.None;
            this.transform.position = new Vector3(0, 10, 0);
            WinningScreen.screen.gameObject.SetActive(true);


        }
        CollectablesGenerator.generator.collectableCounter += Time.deltaTime;
    }
    void FixedUpdate()
    {

        if (!isInSpiral)
        {
            this.myRigidBody.velocity += new Vector3(0, GameManager.instance.gravity, 0) * Time.deltaTime; //Physics.gravity*Time.deltaTime;
        }
        else
        {
            this.myRigidBody.velocity += this.transform.position.normalized * GameManager.instance.gravity * Time.deltaTime;
            this.transform.up = this.transform.position;
        }
    }
   
    public void Eat(GameObject collectable)
    {
        if (GameManager.instance.isStarted)
        {
            HUDManager.manager.increaseCollectables();
            CollectablesGenerator.generator.EatCollectable(collectable.gameObject);
            source.PlayOneShot(soundCollectableColl, source.volume);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            this.GetComponent<Animator>().SetTrigger("hitTarget");
        }
        if (GameManager.instance.isStarted)
        {

            if (other.gameObject.CompareTag("Magnet"))
            {
                GameManager.instance.Powerup = PowerUps.Magnit;
                PowerUpGenerator.generator.EatPowerup(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Sliding"))
            {
                GameManager.instance.Powerup = PowerUps.Sliding;
                PowerUpGenerator.generator.EatPowerup(other.gameObject);
                PowerUpManager.Manager.GenerateNet();
            }
            else if (other.CompareTag("Room"))
            {
                if (this.GetComponent<Rigidbody>().velocity.x > 0)
                    this.GetComponent<Animator>().SetTrigger("leftWall");
                else
                    this.GetComponent<Animator>().SetTrigger("rightWall");
            }
            else if (other.CompareTag("Spiral"))
            {
                isInSpiral = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spiral"))
            {
                isInSpiral = false;
            }
    }
    void OnCollisionEnter(Collision other)
    {
        if (GameManager.instance.isStarted)
        {
            //if (GameManager.manager.Powerup == PowerUps.None)
            //{
            if (other.gameObject.CompareTag("Tile") && other.gameObject != lastCollision)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false; //to stop from multiple collisions (should not happen)

                CollectablesGenerator.generator.collectableCounter = 0;
                lastCollision = other.gameObject;
                float x = gameObject.transform.position.x - other.transform.position.x;
                myRigidBody.velocity = new Vector3(2 * x, 0, 0) + GameManager.instance.poinkySpeed;

                HUDManager.manager.increaseScore(1);

                GameManager.instance.IsMoving = true;
                var trail = GameObject.Instantiate(decal, other.contacts[0].point, Quaternion.identity) as GameObject;
                if (Mathf.Abs(trail.transform.position.x - other.transform.position.x) > .51f)
                {
                    var pos = trail.transform.position;
                    pos.x = other.transform.position.x + Mathf.Sign(trail.transform.position.x - other.transform.position.x) * .52f;
                    trail.transform.position = pos;
                }

                trail.transform.parent = other.transform;
                //trail.transform.position = new Vector3(trail.transform.position.x, other.transform.position.y + 0.127f, trail.transform.position.z);

                //source.volume = PlayerPrefs.GetFloat("MusicVol") ;
                source.PlayOneShot(soundTileColl, source.volume);
                //  AudioSource.PlayClipAtPoint(soundTileColl, new Vector3(0,0,150));
            }
            else if (other.gameObject.CompareTag("Room"))
            {
                myRigidBody.velocity = new Vector3(this.transform.position.x - other.contacts[0].point.x * 1.5f, myRigidBody.velocity.y, 0);
                source.PlayOneShot(soundWallColl, source.volume);
            }
            else if (other.gameObject.CompareTag("SafetyNet"))
            {

                myRigidBody.velocity = new Vector3(0, 0, 0) + GameManager.instance.poinkySpeed;
                GameObject.Destroy(other.gameObject);
                GameManager.instance.IsMoving = true;
            }
        }
        else
        {
            myRigidBody.velocity = GameManager.instance.poinkySpeed;
        }
    }
}
