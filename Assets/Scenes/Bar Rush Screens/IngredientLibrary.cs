using System.Collections.Generic;

public static class IngredientLibrary
{
    public static readonly HashSet<string> Alcohols = new()
    {
        "Tequila", "Triple sec", "Campari", "Sweet Vermouth", "Scotch", "Drambuie", "Brandy", "White Creme de Menthe",
        "Gin", "gin", "Bourbon", "Dry Vermouth", "Blended whiskey", "Light rum", "Cognac", "Cointreau", "Apricot brandy", "Apple brandy",
        "Creme de Cacao", "Benedictine", "Dark Rum", "Ricard", "Port", "Maraschino liqueur", "Vodka"
    };

    public static readonly HashSet<string> Mixers = new()
    {
        "Lime juice", "Angostura bitters", "Pineapple juice", "Grenadine", "Light cream", "Orange bitters",
        "Orange Juice", "Lemon juice", "Club soda", "Soda Water", "Sugar syrup", "Peach Bitters", "Peychaud bitters", "Water",
        "Cream", "Vanilla extract", "Carbonated water", "Egg white", "Egg Yolk", "Sugar"
    };

    public static readonly HashSet<string> Garnishes = new()
    {
        "Salt", "Orange peel", "Lemon peel", "Olive", "Maraschino cherry", "Lemon", "Cherry", "Lime",
        "Powdered sugar", "Nutmeg", "Anis", "Orange", "Mint"
    };
}