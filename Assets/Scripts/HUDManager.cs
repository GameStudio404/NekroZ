using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private List<GameObject> teamList;
    public GameObject teamMate;
    public Transform team;

    void Start()
    {
        teamList = new List<GameObject>();
        GameObject tmp = Instantiate(teamMate, teamMate.transform);
        tmp.SetActive(true);
        teamList.Add(tmp);

        foreach (GameObject item in teamList)
        {
            item.transform.SetParent(team);
        }
    }
    
    public void AddMate()
    {
        Debug.Log("You have " + teamList.Count + " mates !");
        if (teamList.Count < 7)
        {
            GameObject tmp = Instantiate(teamMate, teamMate.transform);
            tmp.SetActive(true);
            tmp.transform.SetParent(team);
            teamList.Add(tmp);
        } else
        {
            Debug.Log("You already have 7 mates !");
        }
    }
}
