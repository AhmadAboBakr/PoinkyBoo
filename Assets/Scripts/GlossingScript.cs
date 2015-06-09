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
        colors[0] = new Color(231 / 255f, 161 / 255f, 12 / 255f);//orange
        colors[1] = new Color(216 / 255f, 25 / 255f , 216 / 255f);//pink
        colors[2] = new Color(37 / 255f , 133 / 255f, 65 / 255f);//green
        colors[3] = new Color(49 / 255f , 93 / 255f , 186 / 255f);//blue
        colors[4] = new Color(227 / 255f, 87 / 255f , 87 / 255f);//red
        colors[5] = new Color(9 / 255f  , 177 / 255f, 184 / 255f);
        colors[6] = new Color(87 / 255f , 56 / 255f , 101 / 255f);

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
