using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Store : MonoBehaviour {

    public static Store instance;

    public GameObject[] degreeMagnetImgs;
    public GameObject[] degreeSafetyImgs;

    public int currentDegreeMagnet;
    public  int currentDegreeSafety;

    int collectablesCount;
    public Text txtCollectablesCount;
    public Text txtSafetyPrice, txtMagnetPrice;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //just to set the page
        getCollectablesCount();

        //
        currentDegreeMagnet = 0;
        currentDegreeSafety = 0;

    
        //getDegree();
        //PlayerPrefs.HasKey("degreeMagnet")
        txtSafetyPrice.text = getPowerPrice(0).ToString();
        txtMagnetPrice.text = getPowerPrice(0).ToString();


        for (int i = 0; i < 5; i++) //degreeMagnetImgs.Length
        {
            GameObject imgMag =  degreeMagnetImgs.GetValue(i) as GameObject;
            GameObject imgSaf = degreeSafetyImgs.GetValue(i) as GameObject;

            //if they are not bought, or if they are expired//need to be renewed
            imgMag.SetActive(false);
            imgSaf.SetActive(false);
        }
    }

    public void OnMagnetPressed()
    {
        //set degree in player prefs, and then minus the collectables count
        //currentdegree = playerprefs.degree; (get degree)

        //if (!PlayerPrefs.HasKey("MagnetDegree"))
        //{
        //    PlayerPrefs.SetInt("MagnetDegree", currentDegreeMagnet);
        //}
        //else
        //{
        //    currentDegreeMagnet = PlayerPrefs.GetInt("MagnetDegree");
        //}

        if (minusCollectables(currentDegreeMagnet))
        {
            if (currentDegreeMagnet < 5)
            {
                if (!degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().isActiveAndEnabled)
                {
                    degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().gameObject.SetActive(true);
                   
                    //actuallyBuy
                    currentDegreeMagnet++;
                    txtMagnetPrice.text = getPowerPrice(currentDegreeMagnet).ToString();
                }
            }   
 
        }
          
    }

    public void OnSafetyNetPressed()
    {
        //player prefs, and then minus the collectables count

        //if (!PlayerPrefs.HasKey("SafetyDegree"))
        //{
        //    PlayerPrefs.SetInt("SafetyDegree", currentDegreeSafety);
        //}
        //else
        //{
        //   currentDegreeSafety = PlayerPrefs.GetInt("SafetyDegree");
        //}

        if (minusCollectables(currentDegreeSafety))
        {
            if (currentDegreeSafety < 5)
            {
                if (!degreeSafetyImgs[currentDegreeSafety].GetComponent<Image>().isActiveAndEnabled)
                {
                    degreeSafetyImgs[currentDegreeSafety].GetComponent<Image>().gameObject.SetActive(true);
                                    
                    //actuallyBuy
                    currentDegreeSafety++;
                    txtSafetyPrice.text = getPowerPrice(currentDegreeSafety).ToString();
                }
            }    
        }        
    }

    int getPowerPrice(int degree)
    {
        return (degree + 1) * 500;
    }
    void getCollectablesCount()
    {
        txtCollectablesCount = GameObject.Find("txtCollectablesCount").GetComponent<Text>();   

        if (PlayerPrefs.GetInt("Collectables") != null)
        {
            collectablesCount = PlayerPrefs.GetInt("Collectables");
            txtCollectablesCount.text = collectablesCount.ToString();
        }
        else
            txtCollectablesCount.text = "0";
    }
     bool minusCollectables(int degree)
    {
        //PlayerPrefs.collectibles.toInt -= amount;

       // getCollectablesCount();
        if(collectablesCount >= 500 * (degree+1) &&degree<5) //because the 1st digree is 0
        {
            collectablesCount = collectablesCount - 500 * (degree + 1);
            txtCollectablesCount.text = collectablesCount.ToString();
            collectablesTotal.instance.change(collectablesCount);
            return true;

        }
        return false;
    }

    //public bool isConsumed(int powerIndex)
    //{
    //    //magnet 1, safety 2

    //    return true;
    //}
    public void OnBackPressed()
    {   
        Store.instance.gameObject.SetActive(false);
        MainMenu.menu.gameObject.SetActive(true);
       // HUDManager.instance.collectablesText.text = PlayerPrefs.GetInt("Collectables").ToString();
    }

}
