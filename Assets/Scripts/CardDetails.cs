using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDetails
{
    private static string moraleDescription = "Adds + 1 to all units in the row (excluding itself).";
    private static string bondDescription = "Place next to a card with the same name to double the strength of both cards.";
    private static string spyDescription = "Place on your oponnent's battlefield (counts towards oponnent's total) and draw 2 cards from your deck.";
    private static string medicDescription = "Choose one card from your discard pile and play it instantly (no Heroes or Special Cards).";
    private static string heroDescription = "Not affected by any Special Cards or abilities.";
    private static string hornDescription = "Doubles the strength of all unit cards in that row. Limite to 1 per row.";
    private static string musterDescription = "Find any cards with the same name in your deck and play them instantly.";
    private static string scorchDescription = "Destroy your enemy's strongest Close Combats unit(s) if the combined strength of all his or her Close Combat units is 10 or more.";
    private static string decoyDescription = "Swap with a card on the battlefield to return it to your hand.";
    private static string weatherCloseDescription = "Sets the strength of all Close Combat cards to 1 for both players.";
    private static string weatherRangedDescription = "Sets the strength of all Ranged Combat cards to 1 for both players.";
    private static string weatherSiegeDescription = "Sets the strength of all Siege Combat cards to 1 for both players.";
    private static string weatherClearDescription = "Removes all Weather Card(Biting Frost, Impenetrable Fog and Torrential Rain) effects.";
    private static string agileDescription = "Can be placed in either the Close Combat or the Ranged Combat Row. Cannot be moved once placed.";



    public static string GetDetails(Card cardForDetails)
    {
        if (cardForDetails.Ability == Ability.Morale)
        {
            return moraleDescription;
        }
        else if (cardForDetails.Ability == Ability.Bond)
        {
            return bondDescription;
        }
        else if (cardForDetails.Ability == Ability.Spy)
        {
            return spyDescription;
        }
        else if (cardForDetails.Ability == Ability.Medic)
        {
            return medicDescription;
        }
        else if (cardForDetails.Ability == Ability.Horn)
        {
            return hornDescription;
        }
        else if (cardForDetails.Ability == Ability.Muster)
        {
            return musterDescription;
        }
        else if (cardForDetails.Ability == Ability.Scorch)
        {
            return scorchDescription;
        }
        else if (cardForDetails.Ability == Ability.Agile)
        {
            return agileDescription;
        }
        else if (cardForDetails.Rank == Rank.Decoy)
        {
            return decoyDescription;
        }
        else if (cardForDetails.Rank == Rank.Weather && cardForDetails.Ability == Ability.Close)
        {
            return weatherCloseDescription;
        }
        else if (cardForDetails.Rank == Rank.Weather && cardForDetails.Ability == Ability.Ranged)
        {
            return weatherRangedDescription;
        }
        else if (cardForDetails.Rank == Rank.Weather && cardForDetails.Ability == Ability.Siege)
        {
            return weatherSiegeDescription;
        }
        else if (cardForDetails.Rank == Rank.Weather && cardForDetails.Ability == Ability.Clear)
        {
            return weatherClearDescription;
        }
        else if (cardForDetails.IsHero)
        {
            return heroDescription;
        }
        return string.Empty;
    }
}
