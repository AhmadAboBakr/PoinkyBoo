using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectablesGenerator : MonoBehaviour
{

    public static CollectablesGenerator generator;
    public GameObject collectablePrefab;
    float speed;
    float time;
    public List<GameObject> collectables;
    public float collectableCounter;
    bool flag;
    float random;
    float newrandom;
    int counter = 0;
    RaycastHit r;
    void Awake()
    {
        if (!CollectablesGenerator.generator)
            CollectablesGenerator.generator = this;
        GameManager.clear += Clear;
    }

    void Start()
    {
        //StartCoroutine(Generatecollectable());
        speed = GameManager.instance.poinkySpeed.y;
        time = -speed / (0.5f * GameManager.instance.gravity);
        collectables = new List<GameObject>();
        random = 0;
        newrandom = 0;
        for (int i = 1; i <= 18; i++)
        {
            if (i % 6 == 0)
            {
                random = newrandom;
                newrandom = Random.Range(-1, 2) * 3;
            }
            
            if (!(i % 6 == 0))//|| i % 6 == 2 || i % 6 == 4 ))
            {
                collectables.Add(Instantiate(collectablePrefab, new Vector3(random + (newrandom - random) / 6 * (i % 6), (speed * 2 / 6 * (i % 6) + 0.5f * GameManager.instance.gravity * 2 / 6 * (i % 6) * 2 / 6 * (i % 6)), (speed / 6 * i)), Quaternion.identity) as GameObject);
            }
            
        }
        for (int j = 1; j <= 18; j++)
        {
            if (j % 6 == 0)
            {
                random = newrandom;
                newrandom = Random.Range(-1, 2) * 3;
            }

            if (!(j % 6 == 0))//|| i % 6 == 2 || i % 6 == 4 ))
            {
                collectables.Add(Instantiate(collectablePrefab, new Vector3(random + (newrandom - random) / 6 * (j % 6), (speed * 2 / 6 * (j % 6) + 0.5f * GameManager.instance.gravity * 2 / 6 * (j % 6) * 2 / 6 * (j % 6)), (speed / 6 * j) + 70), Quaternion.identity) as GameObject);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        speed = GameManager.instance.poinkySpeed.y;
        time = -speed / (0.5f * GameManager.instance.gravity);
        if (collectables.Count > 0 && collectables[0].transform.position.z < Camera.main.transform.position.z)
        {
            Destroy(collectables[0]);
            collectables.Remove(collectables[0]);
        }

        //foreach (var collectable in collectables)
        //{
        //    if (GameManager.manager.IsMoving)
        //    {
        //        collectable.transform.position = Vector3.Lerp(collectable.transform.position, collectable.transform.position + new Vector3(0, 0, -speed), Time.deltaTime / time);
        //    }
        //}

    }
    public void EatCollectable(GameObject collectable)
    {
        Destroy(collectable);
        collectables.Remove(collectable);
        
    }
    public void generate()
    {
        if (Physics.Raycast(new Vector3(0, -3, speed*15), -Vector3.up, out r, float.MaxValue, 1 << 12))
        {
            if (r.collider.tag != "Spiral")
            {
                counter++;
                if (counter == 6)
                {
                    counter = 0;
                    random = newrandom;
                    newrandom = Random.Range(-1, 2) * 3;
                    for (int i = 1; i < 18; i++)
                    {
                        if (!(i % 6 == 0))
                            collectables.Add(Instantiate(collectablePrefab, new Vector3((random + (newrandom - random) / 6 * (i % 6)), (speed * 2 / 6 * (i % 6) + 0.5f * GameManager.instance.gravity * 2 / 6 * (i % 6) * 2 / 6 * (i % 6)), ((speed / 6) * i) + speed * 13), Quaternion.identity) as GameObject);
                    }
                }
            }
        }
    }
    //IEnumerator Generatecollectable()
    //{
    //    while (true)
    //    {
    //        random = newrandom;
    //        newrandom = Random.Range(-1, 2) * 3;
    //        yield return new WaitForSeconds(5);
    //        for (int i = 1; i < 6; i++)
    //        {
    //            if (!(i % 6 == 0))// || i % 6 == 2 || i % 6 == 4))
    //                collectables.Add(Instantiate(collectablePrefab, new Vector3((random + (newrandom - random) / 6 * (i % 6)), (speed * 2 / 6 * (i % 6) + 0.5f * Physics.gravity.y * 2 / 6 * (i % 6) * 2 / 6 * (i % 6)), ((speed / 6) * i) + speed * 6), Quaternion.identity) as GameObject);
    //        }
    //    }
    //}
    void Clear()
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            GameObject.Destroy(collectables[i].gameObject);
        }
        collectables.Clear();
        Start();
    }
}