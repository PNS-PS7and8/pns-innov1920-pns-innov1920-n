using UnityEngine;
using System;
using System.Net.Http;

public static class AccessDatabase {
    const string protocol = "http";
    const string host = "games.strange-nebula.com";
    const string port = "3000";
    
    private class RemoteDeckList {
        public string[] decks;
    }

    private class RemoteDeck {
        public string owner;
        public string name;
        public string[] units;
        public string[] spells;
    }
    
    public static string[] GetDecksOf(string user) {
        HttpClient client = new HttpClient();
        var url = GetUrl($"/u/{user}");
        var res = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        Debug.Log(res);
        var list = JsonUtility.FromJson<RemoteDeckList>(res);
        return list.decks;
    }

    public static Deck GetDeck(string user, string name) {
        HttpClient client = new HttpClient();
        var url = GetUrl($"/u/{user}/{name}");
        var res = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        var deck = JsonUtility.FromJson<RemoteDeck>(res);
        return new Deck(deck.name, deck.units, deck.spells);
    }

    private static Uri GetUrl(string path) {
        return new Uri($"{protocol}://{host}:{port}{path}");
    }
}