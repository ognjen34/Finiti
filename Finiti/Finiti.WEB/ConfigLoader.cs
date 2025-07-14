namespace Finiti.WEB
{
    public static class ConfigLoader
    {
        public static Dictionary<string, string> LoadSettings(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1) // Skip header
                .Select(line => line.Split(',', 2))
                .ToDictionary(
                    parts => parts[0],
                    parts => parts[1].Trim('"')
                );
        }
    }
}
