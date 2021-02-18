using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private List<GameObject> teamList;
    public GameObject teamMate;
    public Transform team;
    public Image map;
    public Sprite[] mapSprites;
    private int idx;

    void Start()
    {
        idx = 0;
        teamList = new List<GameObject>();
        GameObject tmp = Instantiate(teamMate, teamMate.transform);
        tmp.SetActive(true);
        teamList.Add(tmp);

        foreach (GameObject item in teamList)
        {
            item.transform.SetParent(team);
        }
    }

    public void changeMap()
    {
        //string nameEnd = "E0" + idx;

        //List<string> files = GetFileList("Assets/Map");
        idx += 1;

        if (idx == 6)
        {
            idx = 0;
        }

        map.sprite = mapSprites[idx];

        //foreach (string file in files)
        //{
        //    if (file.EndsWith(name))
        //    {
        //        map.sprite = file;
        //    }
        //}
    }

    private static List<string> GetFileList(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/" + path);
        FileInfo[] fileEntries = dir.GetFiles("*.*");
        List<string> ret = new List<string>();

        foreach (FileInfo file in fileEntries)
        {
            ret.Add(file.Name);
        }

        return ret;
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
