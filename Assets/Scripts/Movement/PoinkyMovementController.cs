using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PoinkyMovementController : MonoBehaviour
{
    public GameObject decal;
    public GameObject WallColliders;
    private int numberOfMagnetsCollected;
    private int numberOfShieldsCollected;

    public int NumberOfMagnetsCollected
    {
        get
        {
            return numberOfMagnetsCollected;
        }
        set
        {
            numberOfMagnetsCollected = value;
            if (value > 1)
            {

            }
        }
    }
    public int NumberOfShieldsCollected
    {
        get { return numberOfShieldsCollected; }
        set { numberOfShieldsCollected = value; }
    }

    public static bool Hitile;
    float decalWidth;
    Rigidbody myRigidBody;
    Animator myAnimator;
    GameObject lastCollision;
    public bool isInSpiral = false;

    // Use this for initialization
    void Start()
    {
        lastCollision = null;
        myRigidBody = this.GetComponent<Rigidbody>();
        Hitile = false;
        WallColliders.SetActive(true);
        myAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (!GameManager.instance.isStarted)
        {
            this.transform.position = new Vector3(0, 1, 0);
            this.myRigidBody.velocity = Vector3.zero;
            this.NumberOfMagnetsCollected = 0;
            this.numberOfShieldsCollected = 0;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        myAnimator.SetFloat("speedY", this.GetComponent<Rigidbody>().velocity.y);
        if (this.transform.position.y < -5 && GameManager.instance.NumOfPoinky == 1)
        {
            isInSpiral = false;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameManager.instance.Powerup = PowerUps.None;
            PowerUpGenerator.generator.StopGenerate();
            this.transform.position = new Vector3(0, 10, 0);
            WinningScreen.screen.gameObject.SetActive(true);
            CameraMover.instance.Death();
        }
        else if (GameManager.instance.isStarted && this.transform.position.y < -5)
        {
            Destroy(this.gameObject);
            GameManager.instance.NumOfPoinky--;
        }

        CollectablesGenerator.generator.collectableCounter += Time.deltaTime;

        if (GameManager.instance.GameMode == Mode.MainMode)
        {
            WallColliders.SetActive(true);
        }

    }
    void FixedUpdate()
    {
        this.myRigidBody.velocity += new Vector3(0, GameManager.instance.gravity, 0) * Time.deltaTime; //Physics.gravity*Time.deltaTime;

        if (isInSpiral)
        {
            Vector3 position = this.transform.position;
            //this.transform.up = position;

            position = new Vector3(0, position.y, position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, position, Time.deltaTime *4);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile") || other.gameObject.CompareTag("SpiralTile"))
        {
            myAnimator.SetTrigger("hitTarget");
        }
        if (GameManager.instance.isStarted)
        {

            if (other.gameObject.CompareTag("Magnet"))
            {
                GameManager.instance.Powerup = PowerUps.Magnet;
                PowerUpGenerator.generator.EatPowerup(other.gameObject);
                AchievementsHandler.instance.NumberOfMagnits++;
                AchievementsHandler.instance.ReportMagnetAchivement();

            }
            else if (other.gameObject.CompareTag("Sliding"))
            {
                GameManager.instance.Powerup = PowerUps.SafetyNet;
                PowerUpGenerator.generator.EatPowerup(other.gameObject);
                PowerUpManager.Manager.GenerateNet();

            }
            else if (other.gameObject.CompareTag("PoinkyMultiplier"))
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                PowerUpGenerator.generator.EatPowerup(other.gameObject);
                PowerUpManager.Manager.MultiplyPoinky(this.gameObject);
            }
            else if (other.CompareTag("Room"))
            {
                if (this.GetComponent<Rigidbody>().velocity.x > 0)
                    myAnimator.SetTrigger("leftWall");
                else
                    myAnimator.SetTrigger("rightWall");
            }

            else if (other.gameObject.CompareTag("DesertDoorIn"))
            {
                WallColliders.SetActive(false);
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
            if (other.gameObject.CompareTag("Tile") && other.gameObject != lastCollision)
            {
                CollectablesGenerator.generator.collectableCounter = 0;
                lastCollision = other.gameObject;
                float x = gameObject.transform.position.x - other.transform.position.x;
                myRigidBody.velocity = new Vector3(2 * x, 0, 0) + GameManager.instance.poinkySpeed;
                JumpForward();
                var trail = GameObject.Instantiate(decal, other.contacts[0].point, Quaternion.identity) as GameObject;
                if (Mathf.Abs(trail.transform.position.x - other.transform.position.x) > .51f)
                {
                    var pos = trail.transform.position;
                    pos.x = other.transform.position.x + Mathf.Sign(trail.transform.position.x - other.transform.position.x) * .52f;
                    trail.transform.position = pos;
                }

                trail.transform.parent = other.transform;

            }
            else if (other.gameObject.CompareTag("Room"))
            {
                myRigidBody.velocity = new Vector3(this.transform.position.x - other.contacts[0].point.x * 1.5f, myRigidBody.velocity.y, 0);
               // AudioManager.instance. ();

            }
            else if (other.gameObject.CompareTag("SafetyNet"))
            {
                AchievementsHandler.instance.NumberOfSaftyNets++;
                AchievementsHandler.instance.ReportShieldAchivement();
                myRigidBody.velocity = new Vector3(0, 0, 0) + GameManager.instance.poinkySpeed;
                //GameObject.Destroy(other.gameObject);
                GameManager.instance.IsMoving = true;
            }
            else if (other.gameObject.CompareTag("SpiralTile"))
            {

                myRigidBody.velocity = new Vector3(0, 0, 0) + GameManager.instance.poinkySpeed;
                JumpForward();

            }
        }

        else
        {
            myRigidBody.velocity = GameManager.instance.poinkySpeed;
        }
    }
    IEnumerator ResetCollisionStatus()
    {
        yield return new WaitForSeconds(.35f);
        Hitile = false;
    }
    void JumpForward()
    {
        HUDManager.instance.increaseScore(1);
        if (!Hitile)
        {
            Hitile = true;
            StartCoroutine("ResetCollisionStatus");
            GameManager.instance.IsMoving = true;
        }
        AudioManager.instance.Jump();

    }
}
