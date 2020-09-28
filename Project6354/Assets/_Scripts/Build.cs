using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private int moneyGoBrrrrr = 1000;
    
    [SerializeField] private int wallPrice = 100;
    [SerializeField] private int towerDPSPrice = 200;
    [SerializeField] private int towerAoEPrice = 250;
    [SerializeField] private int towerAuraPrice = 300;
    
    [SerializeField] private int towerDPSUpgradeCost = 200;
    [SerializeField] private int towerAoEUpgradeCost = 250;
    [SerializeField] private int towerAuraUpgradeCost = 300;
    
    [SerializeField] private GameObject buildParent;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject towerDPS;
    [SerializeField] private GameObject towerAoE;
    [SerializeField] private GameObject towerAura;
    [SerializeField] private GameObject moneyCounter;
    
    private Quaternion rotation;
    
    private Click cc;
    
    private void Start()
    {
        cc = Camera.main.GetComponent<Click>();
        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }

    /*
    // TODO: Implement hotkeys for building shit.
    private void FixedUpdate()
    {
        throw new NotImplementedException();
    }
    */

    public void BuildWall()
    {
        if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false  && moneyGoBrrrrr >= wallPrice)
                {
                    moneyGoBrrrrr -= wallPrice;
                    Vector3 pos = new Vector3(0, 6.5f, 0);
                    Instantiate(wallPrefab, pos + selected.transform.position, rotation, buildParent.transform);
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
            }
        }

        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }
    
    public void BuildTowerDPS()
    {
        if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false  && moneyGoBrrrrr >= towerDPSPrice)
                {
                    moneyGoBrrrrr -= towerDPSPrice;
                    Vector3 pos = new Vector3(0, 7.5f, 0);
                    Instantiate(towerDPS, pos + selected.transform.position, rotation, buildParent.transform);
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
            }
        }

        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }
    
    public void BuildTowerAoE()
    {
        if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false  && moneyGoBrrrrr >= towerAoEPrice)
                {
                    moneyGoBrrrrr -= towerAoEPrice;
                    Vector3 pos = new Vector3(0, 7.5f, 0);
                    Instantiate(towerAoE, pos + selected.transform.position, rotation, buildParent.transform);
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
            }
        }

        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }
    
    public void BuildTowerAura()
    {
        if (cc.selectedObjects.Count > 0)
        {
            foreach (GameObject selected in cc.selectedObjects)
            {
                if (selected.CompareTag("Node")  && selected.GetComponent<Clickable>().built == false  && moneyGoBrrrrr >= towerAuraPrice)
                {
                    moneyGoBrrrrr -= towerAuraPrice;
                    Vector3 pos = new Vector3(0, 7.5f, 0);
                    Instantiate(towerAura, pos + selected.transform.position, rotation, buildParent.transform);
                }
                else
                {
                    Debug.Log("Invalid selected object");
                }
            }
        }

        moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
    }

    public void Delete()
    {
        List<GameObject> selectedObjects = cc.selectedObjects;
        foreach (GameObject select in selectedObjects.ToList())
        {
            if (select.CompareTag("Wall"))
            {
                selectedObjects.Remove(select);
                Destroy(select);
            }
            else if (select.CompareTag("Tower DPS"))
            {
                selectedObjects.Remove(select);
                Destroy(select);
            }
            else if (select.CompareTag("Tower Aura"))
            {
                selectedObjects.Remove(select);
                Destroy(select);
            }
            else if (select.CompareTag("Tower AoE"))
            {
                selectedObjects.Remove(select);
                Destroy(select);
            }
        }

        cc.selectedObjects = selectedObjects;
    }

    public void Upgrade() // TODO: Implement.
    {
        //throw new NotImplementedException();
        List<GameObject> selectedObjects = cc.selectedObjects;
        foreach (GameObject select in selectedObjects.ToList())
        {
            if (select.CompareTag("Tower DPS"))
            {
                if (moneyGoBrrrrr >= towerDPSUpgradeCost)
                {
                    select.GetComponent<TowerDPS>().level += 1;
                    moneyGoBrrrrr -= towerDPSUpgradeCost;
                    moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
                }
            }
            else if (select.CompareTag("Tower Aura"))
            {
                
            }
            else if (select.CompareTag("Tower AoE"))
            {
                
            }
        }
    }
    
    public void Downgrade() // TODO: Implement.
    {
        //throw new NotImplementedException();
        List<GameObject> selectedObjects = cc.selectedObjects;
        foreach (GameObject select in selectedObjects.ToList())
        {
            if (select.CompareTag("Tower DPS"))
            {
                if (select.GetComponent<TowerDPS>().level > 1)
                {
                    select.GetComponent<TowerDPS>().level -= 1;
                    moneyGoBrrrrr += towerDPSUpgradeCost / 2;
                    moneyCounter.GetComponent<UnityEngine.UI.Text>().text = "Money: " + moneyGoBrrrrr.ToString();
                }
            }
            else if (select.CompareTag("Tower Aura"))
            {
                
            }
            else if (select.CompareTag("Tower AoE"))
            {
                
            }
        }
    }
}
