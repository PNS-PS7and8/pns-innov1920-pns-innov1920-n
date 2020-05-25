using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }

    [SerializeField]
    private string _nickname = "AAA";

    public string Nickname
    {
        get
        {
            
            return _nickname;
        }
    }

    public void setNickname(string nickname)
    {
        _nickname = nickname;
    }

}
