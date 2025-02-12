using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public TMP_Text damageDealt;
    public string damageText;

    public string PopUp(string damage)
    {
        damageText = damage;
        damageDealt.text = damageText;
        return damage;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
