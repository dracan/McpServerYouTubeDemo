namespace PodcastMcpServer.Helpers;

public static class PodcastExtensions
{
    public static List<PodcastDetails> FilterByTags(
        this IEnumerable<FrontMatter> podcastDetails,
        List<string>? tags
    ) =>
        podcastDetails
            .Where(x => tags?
                .All(filterTag => x.Tags?
                    .Any(t => t.Equals(filterTag, StringComparison.InvariantCultureIgnoreCase)) == true) == true)
            .Select(x => new PodcastDetails
            {
                Title = x.Title,
                Tags = x.Tags ?? []
            }).ToList();
}
