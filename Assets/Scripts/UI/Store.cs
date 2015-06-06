using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Store : MonoBehaviour
{

    public static Store instance;

    public GameObject[] degreeMagnetImgs;
    public GameObject[] degreeSafetyImgs;

    public int currentDegreeMagnet;
    public int currentDegreeSafety;

    int collectablesCount;
    public Text txtCollectablesCount;
    public Text txtSafetyPrice, txtMagnetPrice;

    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        if (!PlayerPrefs.HasKey("CurrentMagnetLevel"))
        {
            PlayerPrefs.SetInt("CurrentMagnetLevel", 0);
        }

        if (!PlayerPrefs.HasKey("CurrentShieldLevel"))
        {
            PlayerPrefs.SetInt("CurrentShieldLevel", 0);
        }

        currentDegreeMagnet = PlayerPrefs.GetInt("CurrentMagnetLevel");
        currentDegreeSafety = PlayerPrefs.GetInt("CurrentShieldLevel");
        txtMagnetPrice.text = getPowerPrice(currentDegreeMagnet).ToString();
        txtSafetyPrice.text = getPowerPrice(currentDegreeSafety).ToString();

        if (currentDegreeMagnet < 5)
        {
            if (!degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().isActiveAndEnabled)
            {
                for (int i = 0; i < currentDegreeMagnet; i++)
                {
                    degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().gameObject.SetActive(true);
                }
                for (int i = 0; i < currentDegreeMagnet; i++)
                {
                    degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().gameObject.SetActive(true);
                }
            }
        }

    }
    void Start()
    {
        getCollectablesCount();

       

        //for (int i = 0; i < 5; i++) //degreeMagnetImgs.Length
        //{
        //    GameObject imgMag = degreeMagnetImgs.GetValue(i) as GameObject;
        //    GameObject imgSaf = degreeSafetyImgs.GetValue(i) as GameObject;

        //    //if they are not bought, or if they are expired//need to be renewed
        //    imgMag.SetActive(false);
        //    imgSaf.SetActive(false);
        //}
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
                    for (int i = 0; i < currentDegreeMagnet; i++)
                    {
                        degreeMagnetImgs[currentDegreeMagnet].GetComponent<Image>().gameObject.SetActive(true);                                                
                    }

                    //actuallyBuy
                    currentDegreeMagnet++;
                    PlayerPrefs.SetInt("CurrentMagnetLevel", currentDegreeMagnet);

                    txtMagnetPrice.text = getPowerPrice(currentDegreeMagnet).ToString();
                }
            }

        }

    }

    public void OnSafetyNetPressed()
    {

        if (minusCollectables(currentDegreeSafety))
        {
            if (currentDegreeSafety < 5)
            {
                if (!degreeSafetyImgs[currentDegreeSafety].GetComponent<Image>().isActiveAndEnabled)
                {
                    for (int i = 0; i < currentDegreeSafety; i++)
                    {
                        degreeSafetyImgs[currentDegreeSafety].GetComponent<Image>().gameObject.SetActive(true);                        
                    }

                    //actuallyBuy
                    currentDegreeSafety++;
                    PlayerPrefs.SetInt("CurrentShieldLevel", currentDegreeSafety);

                    txtSafetyPrice.text = getPowerPrice(currentDegreeSafety).ToString();
                }
            }
        }
    }

    int getPowerPrice(int degree)
    {
        if(degree<4)
        {
            return (degree + 1) * 500;
        }
        else
        {
            return (5) * 500;
        }
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
        if (collectablesCount >= 500 * (degree + 1) && degree < 5) //because the 1st digree is 0
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
