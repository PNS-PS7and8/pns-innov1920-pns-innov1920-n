using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class History
{
    public List<string> HistoryList = new List<string>();

    public bool isAvailable(string action){
        if(HistoryList.Contains(action)){
            return false;
        }
        return true;
    }

    public void addHistory(string action){
        HistoryList.Add(action);
    }

    public void resetHistory(){
        HistoryList = new List<string>();
    }
}
