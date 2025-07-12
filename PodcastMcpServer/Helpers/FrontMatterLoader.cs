using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PodcastMcpServer.Helpers;

public static class FrontMatterLoader
{
    private static readonly IDeserializer Deserializer =
        new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance) // matches "title", "date", "tags"
            .IgnoreUnmatchedProperties()
            .Build();

    public static IEnumerable<FrontMatter> Load(string folderPath)
    {
        foreach (var file in Directory.EnumerateFiles(folderPath, "*.md"))
        {
            if (!TryParseFrontMatter(file, out var fm))
                continue;

            yield return fm;
        }
    }

    private static bool TryParseFrontMatter(string filePath, [NotNullWhen(true)] out FrontMatter? frontMatter)
    {
        frontMatter = null;
        using var reader = new StreamReader(filePath);

        // Front-matter must start with '---'
        if (reader.ReadLine()?.Trim() != "---") return false;

        var yamlLines = new List<string>();

        while (reader.ReadLine() is { } line)
        {
            if (line.Trim() == "---")
                break; // Reached the end of front-matter

            yamlLines.Add(line);
        }

        if (yamlLines.Count == 0)
            return false; // Empty or missing front-matter section

        var yaml = string.Join(Environment.NewLine, yamlLines);
        frontMatter = Deserializer.Deserialize<FrontMatter>(yaml);
        return true;
    }
}

public class FrontMatter
{
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<string>? Tags { get; set; } = null!;
}
