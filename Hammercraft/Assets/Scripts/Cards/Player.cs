public class Player {
    public Deck originalDeck  { get; private set; }
    public Deck deck { get; private set; }

    public Player(Deck deck) {
        originalDeck = deck;
        this.deck = new Deck(deck);
        this.deck.Shuffle();
    }
}