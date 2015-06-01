using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    bool closeToPoinky = false;
    public CapsuleCollider magnetTrigger;
    Transform poinky;

    // Use this for initialization
    void Start()
    {
        magnetTrigger = this.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStarted)
        {
            if (GameManager.instance.Powerup == PowerUps.Magnit)
            {
                magnetTrigger.enabled = true;
            }
            else {
                magnetTrigger.enabled = false;
            }
            if (closeToPoinky)
            {
                AttractSelfToPoinky();
            }
            else if (this.transform.position.z < -1)
            {
                this.transform.position -= new Vector3(0, 0.1f, 0);
            }
            if (this.transform.position.z - 3 <= Camera.main.transform.position.z)
            {
                CollectablesGenerator.generator.EatCollectable(this.gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //CollectablesGenerator.generator.EatCollectable(this.gameObject);

        if (!closeToPoinky) {
            closeToPoinky = true;
            poinky = other.gameObject.transform;
        }
    }
    void AttractSelfToPoinky()
    {
        var position = gameObject.transform.position;
        position = Vector3.Lerp(position, poinky.transform.position, Time.deltaTime * 5);
        gameObject.transform.position = position;
        if (
            Vector3.Distance(this.transform.position, poinky.transform.position) < .7 ||
            (this.transform.position.z <= 0 && closeToPoinky)
            )
        {
            //CollectablesGenerator.generator.EatCollectable(this.gameObject);
            GetEaten();
            if (GameManager.instance.Powerup == PowerUps.Magnit)
            {
                AchievementsHandler.instance.NumOfCollectablesWithMagnet++;
                AchievementsHandler.instance.ReportCollectingCoinsWithMagnetInOneGame();

            }
            if (GameManager.instance.Powerup == PowerUps.SafetyNet)
            {
                AchievementsHandler.instance.NumOfTilesWithSfatyNet++;
                AchievementsHandler.instance.ReportJumpingOnTilesWithSaftNetInOneGame();

            }
        }
    }
    void GetEaten(){
        if (GameManager.instance.isStarted)
        {
            HUDManager.instance.increaseCollectables();
            CollectablesGenerator.generator.EatCollectable(gameObject);
            
            AudioManager.instance.CoinCollect();
            AchievementsHandler.instance.ReportCollectingCoinsInOneGame();
        }

    }
}
