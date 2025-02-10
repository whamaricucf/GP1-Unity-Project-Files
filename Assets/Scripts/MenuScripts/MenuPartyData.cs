using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class MenuPartyData : MonoBehaviour
{

    public PlayerID Knightro;
    public PlayerID Pegasus;
    public PlayerID RubberDuck;
    public PlayerID Citronaut;

    private PlayerID Hero1;
    private PlayerID Hero2;
    private PlayerID Hero3;
    private PlayerID Hero4;

    private bool kInParty;
    private bool pInParty;
    private bool rdInParty;
    private bool cInParty;

    private int kSlot;
    private int pSlot;
    private int rdSlot;
    private int cSlot;

    private bool kRow;
    private bool pRow;
    private bool rdRow;
    private bool cRow;

    public GameObject PCBar1;
    public GameObject PCBar2;
    public GameObject PCBar3;
    public GameObject PCBar4;

    public TextMeshProUGUI Hero1Class;
    public TextMeshProUGUI Hero1Name;
    public TextMeshProUGUI Hero1HP;
    public TextMeshProUGUI Hero1MaxHP;
    public TextMeshProUGUI Hero1MP;
    public TextMeshProUGUI Hero1Lvl;
    public TextMeshProUGUI Hero1STR;
    public TextMeshProUGUI Hero1MAG;
    public TextMeshProUGUI Hero1DEF;
    public TextMeshProUGUI Hero1AGI;
    public GameObject Hero1Front;
    public GameObject Hero1Back;
    private Sprite Hero1Sprite;

    public TextMeshProUGUI Hero2Class;
    public TextMeshProUGUI Hero2Name;
    public TextMeshProUGUI Hero2HP;
    public TextMeshProUGUI Hero2MaxHP;
    public TextMeshProUGUI Hero2MP;
    public TextMeshProUGUI Hero2Lvl;
    public TextMeshProUGUI Hero2STR;
    public TextMeshProUGUI Hero2MAG;
    public TextMeshProUGUI Hero2DEF;
    public TextMeshProUGUI Hero2AGI;
    public GameObject Hero2Front;
    public GameObject Hero2Back;
    private Sprite Hero2Sprite;

    public TextMeshProUGUI Hero3Class;
    public TextMeshProUGUI Hero3Name;
    public TextMeshProUGUI Hero3HP;
    public TextMeshProUGUI Hero3MaxHP;
    public TextMeshProUGUI Hero3MP;
    public TextMeshProUGUI Hero3Lvl;
    public TextMeshProUGUI Hero3STR;
    public TextMeshProUGUI Hero3MAG;
    public TextMeshProUGUI Hero3DEF;
    public TextMeshProUGUI Hero3AGI;
    public GameObject Hero3Front;
    public GameObject Hero3Back;
    private Sprite Hero3Sprite;

    public TextMeshProUGUI Hero4Class;
    public TextMeshProUGUI Hero4Name;
    public TextMeshProUGUI Hero4HP;
    public TextMeshProUGUI Hero4MaxHP;
    public TextMeshProUGUI Hero4MP;
    public TextMeshProUGUI Hero4Lvl;
    public TextMeshProUGUI Hero4STR;
    public TextMeshProUGUI Hero4MAG;
    public TextMeshProUGUI Hero4DEF;
    public TextMeshProUGUI Hero4AGI;
    public GameObject Hero4Front;
    public GameObject Hero4Back;
    private Sprite Hero4Sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetData();
        AssignData();
        PartyOrderRegulator();
        Set();
    }

    void GetData()
    {
        kInParty = Knightro.data.inParty;
        pInParty = Pegasus.data.inParty;
        rdInParty = RubberDuck.data.inParty;
        cInParty = Citronaut.data.inParty;
        kSlot = Knightro.data.partySlot;
        pSlot = Pegasus.data.partySlot;
        rdSlot = RubberDuck.data.partySlot;
        cSlot = Citronaut.data.partySlot;
        kRow = Knightro.data.backRow;
        pRow = Pegasus.data.backRow;
        rdRow = RubberDuck.data.backRow;
        cRow = Citronaut.data.backRow;
    }

    void AssignData()
    {
        DataAssigner(Knightro);
        DataAssigner(Pegasus);
        DataAssigner(RubberDuck);
        DataAssigner(Citronaut);
    }


    //This is to ensure that none of the party members are in the same "slot" of the party order.
    void PartyOrderRegulator()
    {
        if (kInParty)
        {
            if (kSlot == pSlot || kSlot == rdSlot || kSlot == cSlot)
            {
                if (kSlot > 0)
                {
                    kSlot--;
                }
                if (kSlot < 1)
                {
                    kSlot = 4;
                }
                Knightro.data.partySlot = kSlot;
            }
        }
        if (pInParty)
        {
            if (pSlot == kSlot || pSlot == rdSlot || pSlot == cSlot)
            {
                if (pSlot > 0)
                {
                    pSlot--;
                }
                if (pSlot < 1)
                {
                    pSlot = 4;
                }
                Pegasus.data.partySlot = pSlot;
            }
        }
        if (rdInParty)
        {
            if (rdSlot == pSlot || rdSlot == kSlot || rdSlot == cSlot)
            {
                if (rdSlot > 0)
                {
                    rdSlot--;
                }
                if (rdSlot < 1)
                {
                    rdSlot = 4;
                }
                RubberDuck.data.partySlot = rdSlot;
            }
        }
        if (cInParty)
        {
            if (cSlot == pSlot || cSlot == rdSlot || cSlot == kSlot)
            {
                if (cSlot > 0)
                {
                    cSlot--;
                }
                if (cSlot < 1)
                {
                    cSlot = 4;
                }
                Citronaut.data.partySlot = cSlot;
            }
        }
    }

    void Set()
    {
        if (Hero1.data.inParty)
        {
            Hero1Class.text = Hero1.data.className.ToString();
            Hero1Name.text = Hero1.data.heroName.ToString();
            Hero1HP.text = Hero1.stats.HP.ToString();
            Hero1MaxHP.text = Hero1.stats.maxHP.ToString();
            Hero1MP.text = Hero1.stats.MP.ToString();
            Hero1Lvl.text = Hero1.data.lvl.ToString();
            Hero1STR.text = Hero1.stats.str.ToString();
            Hero1MAG.text = Hero1.stats.mag.ToString();
            Hero1DEF.text = Hero1.stats.def.ToString();
            Hero1AGI.text = Hero1.stats.agi.ToString();
            Hero1Sprite = Hero1.data.menu;
            Image image1 = Hero1Front.GetComponent<Image>();
            image1.sprite = Hero1Sprite;
            Image image2 = Hero1Back.GetComponent<Image>();
            image2.sprite = Hero1Sprite;
            if (Hero1.data.backRow)
            {
                Hero1Front.SetActive(false);
                Hero1Back.SetActive(true);
            }
            else
            {
                Hero1Front.SetActive(true);
                Hero1Back.SetActive(false);
            }
            PCBar1.SetActive(true);
        }
        if (Hero2 != null)
        {
            if (Hero2.data.inParty)
            {
                Hero2Class.text = Hero2.data.className.ToString();
                Hero2Name.text = Hero2.data.heroName.ToString();
                Hero2HP.text = Hero2.stats.HP.ToString();
                Hero2MaxHP.text = Hero2.stats.maxHP.ToString();
                Hero2MP.text = Hero2.stats.MP.ToString();
                Hero2Lvl.text = Hero2.data.lvl.ToString();
                Hero2STR.text = Hero2.stats.str.ToString();
                Hero2MAG.text = Hero2.stats.mag.ToString();
                Hero2DEF.text = Hero2.stats.def.ToString();
                Hero2AGI.text = Hero2.stats.agi.ToString();
                Hero2Sprite = Hero2.data.menu;
                Image image1 = Hero2Front.GetComponent<Image>();
                image1.sprite = Hero2Sprite;
                Image image2 = Hero2Back.GetComponent<Image>();
                image2.sprite = Hero2Sprite;
                if (Hero2.data.backRow)
                {
                    Hero2Front.SetActive(false);
                    Hero2Back.SetActive(true);
                }
                else
                {
                    Hero2Front.SetActive(true);
                    Hero2Back.SetActive(false);
                }
                PCBar2.SetActive(true);
            }
        }
        else
        {
            PCBar2.SetActive(false);
        }
        if (Hero3 != null)
        {
            if (Hero3.data.inParty)
            {
                Hero3Class.text = Hero3.data.className.ToString();
                Hero3Name.text = Hero3.data.heroName.ToString();
                Hero3HP.text = Hero3.stats.HP.ToString();
                Hero3MaxHP.text = Hero3.stats.maxHP.ToString();
                Hero3MP.text = Hero3.stats.MP.ToString();
                Hero3Lvl.text = Hero3.data.lvl.ToString();
                Hero3STR.text = Hero3.stats.str.ToString();
                Hero3MAG.text = Hero3.stats.mag.ToString();
                Hero3DEF.text = Hero3.stats.def.ToString();
                Hero3AGI.text = Hero3.stats.agi.ToString();
                Hero3Sprite = Hero3.data.menu;
                Image image1 = Hero3Front.GetComponent<Image>();
                image1.sprite = Hero3Sprite;
                Image image2 = Hero3Back.GetComponent<Image>();
                image2.sprite = Hero3Sprite;
                if (Hero3.data.backRow)
                {
                    Hero3Front.SetActive(false);
                    Hero3Back.SetActive(true);
                }
                else
                {
                    Hero3Front.SetActive(true);
                    Hero3Back.SetActive(false);
                }
                PCBar3.SetActive(true);
            }
        }
        else
        {
            PCBar3.SetActive(false);
        }
        if (Hero4 != null)
        {
            if (Hero4.data.inParty)
            {
                Hero4Class.text = Hero4.data.className.ToString();
                Hero4Name.text = Hero4.data.heroName.ToString();
                Hero4HP.text = Hero4.stats.HP.ToString();
                Hero4MaxHP.text = Hero4.stats.maxHP.ToString();
                Hero4MP.text = Hero4.stats.MP.ToString();
                Hero4Lvl.text = Hero4.data.lvl.ToString();
                Hero4STR.text = Hero4.stats.str.ToString();
                Hero4MAG.text = Hero4.stats.mag.ToString();
                Hero4DEF.text = Hero4.stats.def.ToString();
                Hero4AGI.text = Hero4.stats.agi.ToString();
                Hero4Sprite = Hero4.data.menu;
                Image image1 = Hero4Front.GetComponent<Image>();
                image1.sprite = Hero4Sprite;
                Image image2 = Hero4Back.GetComponent<Image>();
                image2.sprite = Hero4Sprite;
                if (Hero4.data.backRow)
                {
                    Hero4Front.SetActive(false);
                    Hero4Back.SetActive(true);
                }
                else
                {
                    Hero4Front.SetActive(true);
                    Hero4Back.SetActive(false);
                }
                PCBar4.SetActive(true);
            }
        }
        else
        {
            PCBar4.SetActive(false);
        }
    }

    PlayerID DataAssigner(PlayerID ID)
    {
        if (ID.data.inParty)
        {
            if (ID.data.partySlot == 1)
            {
                Hero1 = ID;
            }
            if (ID.data.partySlot == 2)
            {
                Hero2 = ID;
            }
            if (ID.data.partySlot == 3)
            {
                Hero3 = ID;
            }
            if (ID.data.partySlot == 4)
            {
                Hero4 = ID;
            }
        }
        return ID;
    }
}
