using NetTutorialLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

Stopwatch sw = Stopwatch.StartNew();
Suits().LogQuery($"{sw.Elapsed.TotalMilliseconds} ms");
static IEnumerable<string> Suits()
{
    yield return "clubs";
    yield return "diamonds";
    yield return "hearts";
    yield return "spades";
}

static IEnumerable<string> Ranks()
 {
    yield return "two";
    yield return "three";
    yield return "four";
    yield return "five";
    yield return "six";
    yield return "seven";
    yield return "eight";
    yield return "nine";
    yield return "ten";
    yield return "jack";
    yield return "queen";
    yield return "king";
    yield return "ace";
}


//var sutest = su.ToList();
var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                    from r in Ranks().LogQuery("Rank Generation")
                    select new { Suit = s, Rank = r }).LogQuery("Starting Deck").ToList(); //sin ToList() el proceso es más rápido hasta el punto 1

// Display each card that we've generated and placed in startingDeck in the console
foreach (var card in startingDeck)
{
    Console.WriteLine(card);
}
startingDeck.LogQuery($"Time: {sw.Elapsed.TotalMilliseconds} ms"); /* punto 1*/

Console.WriteLine();

var times = 0;
// We can re-use the shuffle variable from earlier, or you can make a new one
var shuffle = startingDeck;
do
{
    shuffle = shuffle.Skip(26).LogQuery($"Bottom Half")
            .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
            .LogQuery("Shuffle").ToList(); //Si no llevamos a memoria startingDeck con ToList() volverá a generarse toda la baraja en cada iteración!

    foreach (var card in shuffle)
    {
        Console.WriteLine(card);
    }
    Console.WriteLine();
    times++;

} while (!startingDeck.SequenceEquals(shuffle));

Console.WriteLine(times);
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
