using UnityEngine;
using System.Collections;

public class ShaderHandle : MonoBehaviour {
    public float deseriedYBend;
    public float deseriedXBend;
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {

	}
    IEnumerator changeRotation()
    {
        while (true)
        {
            deseriedYBend = Random.Range(-10, 11);
            deseriedXBend = Random.Range(-10, 11);

            yield return new WaitForSeconds(Random.Range(5,10));
        }
    }
}
