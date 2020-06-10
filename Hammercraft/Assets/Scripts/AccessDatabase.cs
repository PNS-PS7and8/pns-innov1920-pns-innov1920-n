using UnityEngine;
using System;
using System.Net.Http;

public static class AccessDatabase {
    const string protocol = "http";
    const string host = "localhost";
    const string port = "3000";
    
    private class RemoteDeckList {
        public string[] decks = null;
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

    public static void SaveDeck(string user, Deck deck) {
        HttpClient client = new HttpClient();
        var url = GetUrl($"/u/{user}/{deck.Name}");
        var remoteDeck = new RemoteDeck() {
            owner = user,
            name = deck.Name,
            units = deck.SerializedUnits.ToArray(),
            spells = deck.SerializedSpells.ToArray()
        };
        var json = JsonUtility.ToJson(remoteDeck);
        var content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(json));
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var res = client.PostAsync(url, content).Result;
    }

    public static void DeleteDeck(string user, Deck deck) {
        HttpClient client = new HttpClient();
        var url = GetUrl($"/u/{user}/{deck.Name}");
        var res = client.DeleteAsync(url);
    }

    private static Uri GetUrl(string path) {
        return new Uri($"{protocol}://{host}:{port}{path}");
    }
}