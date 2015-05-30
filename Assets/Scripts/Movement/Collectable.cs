using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    float speed;
    float time;
    bool closeToPoinky = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStarted)
        {
            //this.transform.position = new Vector3(this.transform.position.x, PoinkyMovementController.poinky.transform.position.y, this.transform.position.z);
            speed = GameManager.instance.poinkySpeed.y;
            time = -speed / (0.51f * Physics.gravity.y);
            if (GameManager.instance.Powerup == PowerUps.Magnit)
            {
                if (gameObject.transform.position.z < 2)
                {
                    closeToPoinky = true;
                }
            }
            if (closeToPoinky)
            {
                AttractSelfToPoinky();
            }
            else if (this.transform.position.z < -1)
            {
                this.transform.position -= new Vector3(0, 0.1f, 0);
            }
            if (
                Vector3.Distance(this.transform.position, PoinkyMovementController.poinky.transform.position) < .7 ||
                (this.transform.position.z <= 0 && closeToPoinky)
                )
            {
                CollectablesGenerator.generator.EatCollectable(this.gameObject);
                PoinkyMovementController.poinky.Eat(this.gameObject);
                if (GameManager.instance.Powerup == PowerUps.Magnit)
                {
                    AchievementsHandler.instance.NumOfCollectablesWithMagnet++;
                    AchievementsHandler.instance.ReportCollectingCoinsWithMagnetInOneGame();

                }
                if (GameManager.instance.Powerup == PowerUps.Sliding)
                {
                    AchievementsHandler.instance.NumOfTilesWithSfatyNEt++;
                    AchievementsHandler.instance.ReportJumpingOnTilesWithSaftNetInOneGame();

                }
            }
            if (this.transform.position.z - 3 <= Camera.main.transform.position.z)
            {
                CollectablesGenerator.generator.EatCollectable(this.gameObject);
            }
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Poinky"))
    //    {
    //        CollectablesGenerator.generator.EatCollectable(this.gameObject);

    //       // closeToPoinky = true;
    //    }
    //}
    void AttractSelfToPoinky()
    {
        var position = gameObject.transform.position;
        position = Vector3.Lerp(position, PoinkyMovementController.poinky.transform.position, Time.deltaTime *5);
        gameObject.transform.position = position;
        if (GameManager.instance.Powerup == PowerUps.Magnit)
        {

        }

    }
}
