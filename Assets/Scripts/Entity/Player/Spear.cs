using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Spear : Upgrades
{

    Melee melee = null;

    public float attkDel = 0.1f;  //Time between player input and attack damaging
    public float attkDur = 0.2f;  //Time attack collider is active
    public float attkRate = 0.5f; //Time between attack end and next attack
    float nextAttack = 0;
    bool canAtk = false;

    GameObject spearObj;

    public UpgradeEnum getId()
    {
        return UpgradeEnum.SPEAR;
    }

    public void upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        if(melee == null)
        {
            melee = obj.GetComponent<Melee>();
        }

        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
            if(nextAttack <= 0)
            {
                melee.noPogo = false;
                melee.enabled = true;
                nextAttack = 0;
            }
            if(nextAttack <= attkRate + attkDur && canAtk)
            {
                
                atk();
                canAtk = false;
            }
            if(nextAttack <= attkRate && spearObj.activeSelf)
            {
                spearObj.SetActive(false);
            }
        }
        spearObj = inv.spearObj;
        if (nextAttack <= 0 && melee.nextAttack <= 0)
        {
            if (Input.GetButtonDown("Attack2")) //"Attack1"
            {
                canAtk = true;
                nextAttack = attkDel + attkDur + attkRate;
                melee.noPogo = true;
                melee.enabled = false;
            }
        }
    }

    public void atk()
    {
        spearObj.SetActive(true);
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        if( y == 0 && x == 0 )
        {
            //Attack straight in direction facing
            if (Movement.getinstance().facingRight)
            {
                x = 1;
            }
            else
            {
                x = -1;
            }
        }
        float angle = Mathf.Atan2(y, x);
        Quaternion rotation = Quaternion.AngleAxis((angle*180)/Mathf.PI+90, Vector3.forward);
        spearObj.transform.rotation = rotation;
    }
}
