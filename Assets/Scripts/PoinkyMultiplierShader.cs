using UnityEngine;
using System.Collections;

public class PoinkyMultiplierShader : MonoBehaviour {
    public float scrollSpeed = -0.24f;
    public Renderer rend;
    float offset = 1;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
    void Update()
    {
        //float offset = Time.time * scrollSpeed;
        offset -= Time.deltaTime * scrollSpeed;
        if (offset <= 0) offset = 1;
        // offset=offset-(int)offset;
        this.rend.sharedMaterial.SetTextureOffset("_Illum", new Vector2(0, offset));
    }
}
