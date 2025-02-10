using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform movePoint;
    public Animator animations;
    public GameObject MenuCanvas;
    public GameObject ActionPointer;
    public TextMeshProUGUI Magic;
    public TextMeshProUGUI Order;
    public TextMeshProUGUI Row;
    public TextMeshProUGUI Config;
    public TextMeshProUGUI Back;
    private bool menuOpen;
    private int actionChoice;
    private RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        rt = ActionPointer.GetComponent<RectTransform>();
        rt.transform.localPosition = new Vector2(-829, 0);
        //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -220.5f, 573.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuOpen)
        {
            ActionPointer.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                actionChoice = 1;
                MenuCanvas.SetActive(true);
                menuOpen = true;
            }
        }
        else
        {
            ActionPointer.SetActive(true);
            ActionPointerPos();
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                actionChoice++;
                if (actionChoice > 5)
                {
                    actionChoice = 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                actionChoice--;
                if (actionChoice < 1)
                {
                    actionChoice = 5;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {

            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                MenuCanvas.SetActive(false);
                menuOpen = false;
            }
        }
    }

    void ActionPointerPos()
    {
        if (actionChoice == 1)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, 379);
            Magic.color = Color.yellow;
        }
        if (actionChoice == 2)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, 274);
            Order.color = Color.yellow;
        }
        if (actionChoice == 3)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, 171);
            Row.color = Color.yellow;
        }
        if (actionChoice == 4)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, 70);
            Config.color = Color.yellow;
        }
        if (actionChoice == 5)
        {
            rt.localPosition = new Vector2(rt.localPosition.x, -31);
            Back.color = Color.yellow;
        }
        if (actionChoice != 1)
        {
            Magic.color = Color.white;
        }
        if (actionChoice != 2)
        {
            Order.color = Color.white;
        }
        if (actionChoice != 3)
        {
            Row.color = Color.white;
        }
        if (actionChoice != 4)
        {
            Config.color = Color.white;
        }
        if (actionChoice != 5)
        {
            Back.color = Color.white;
        }
    }
}
