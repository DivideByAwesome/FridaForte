public static void testContentJSON()
{
    string jsonFile = @"C:\Users\Student\Source\Repos\FridaForte\FridaForte\FridaForte\GameContent.json";
    Location[] locations = JsonConvert.DeserializeObject<Location[]>(File.ReadAllText(jsonFile));
    for (int i = 0; i < locations.Length; i++)
    {
        WriteLine(locations[i].Name);
        WriteLine(locations[i].Message);
        WriteLine("\n\n***********");
        WriteLine("Choices");
        WriteLine("***********");
        WriteLine(locations[i].Choices[0]);
        WriteLine(locations[i].Choices[1]);
    }
}