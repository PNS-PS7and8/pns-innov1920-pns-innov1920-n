using Photon.Pun;
using System.Net.Http;
using TMPro;
using UnityEngine;

public class changeUsername : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private Canvas MenuCanvas;

    private void Start() {
        if (PlayerPrefs.HasKey("username")) {
            username.text = PlayerPrefs.GetString("username");
        }
    }

    public void On_Click_Change_Username() {
        PlayerPrefs.SetString("username", username.text.Replace("\u200b", ""));
        PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        MenuCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);

        Connect();
    }

    private void Connect() {
        var client = new HttpClient();
        var response = client.PostAsync("http://games.strange-nebula.com:3000/u/" + PlayerPrefs.GetString("username"), null).Result;

        var responseString = response.Content.ReadAsStringAsync().Result;
        Debug.Log(responseString);
    }
}
