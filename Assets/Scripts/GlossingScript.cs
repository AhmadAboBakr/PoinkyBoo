using UnityEngine;
using System.Collections;

public class GlossingScript : MonoBehaviour {
    public static GlossingScript glosser;
    public float scrollSpeed = 2F;
    public Renderer rend;
    Color[] colors = new Color[7];

    private int index;
    public float changeColourTime = 2.0f;
    private float lastChange = 0.0f;
    private float timer = 0.0f;
    void Awake()
    {
        glosser = this;
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        colors[0] = new Color(255/255,131/255f, 31/255f);//orange
        colors[1] = new Color(255/255f, 0, 255/255f);//pink
        colors[2] = new Color(0/255f, 153/255f, 0f);//green
        colors[3] = new Color(102/255f, 0/255f, 102/255f);//blue
        colors[4] = new Color(204/255f, 0/255f, 0/255f);//red
        colors[5] = new Color(0/255f, 153/255f, 153/255f);
        colors[6] = new Color(128/255f, 0/255f, 255/255f);

        index = 0;   

    }

    float offset = 1;
    void Update()
    {
        //float offset = Time.time * scrollSpeed;
        offset -= Time.deltaTime * scrollSpeed;
        if (offset <= 0) offset = 1;
       // offset=offset-(int)offset;
        this.rend.sharedMaterial.SetTextureOffset("_Illum", new Vector2(0, offset));
       // this.rend.sharedMaterial.SetTextureOffset("_Color", new Vector2(0, offset));
       
        //this.rend.sharedMaterial.color = colors[Random.Range(0, colors.Length)];  //("_Color", Color.magenta);

         timer += Time.deltaTime;

         //this.rend.sharedMaterial.color = colors[index];
         this.rend.sharedMaterial.color = Color.Lerp(this.rend.sharedMaterial.color, colors[index], Time.deltaTime*8);     
    }
    public void changeColor(){
        index = (index + 1) % colors.Length;
       // Debug.Log("changecolor");

    }
    public void reset()
    {
        index = 0;
    }
}
