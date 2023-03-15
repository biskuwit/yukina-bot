using F23.StringSimilarity;
using YukinaBot.Models.AniList;

namespace YukinaBot.Utility
{
    public class LevenshteinUtility
    {
        private readonly NormalizedLevenshtein normalizedLevenshtein = new();

        public Media GetSingleBestResult(string searchQuery, List<Media>? mediaList)
        {
            // TODO: Improve this
            var bestResult = 0.0;
            Media mediaResult = null;
            foreach (var media in mediaList)
            {
                var possibleTitles = media.Synonyms;
                possibleTitles.Add(media.Title.English);
                possibleTitles.Add(media.Title.Native);
                possibleTitles.Add(media.Title.Romaji);

                foreach (var possibleTitle in possibleTitles)
                {
                    if (searchQuery != null && possibleTitle != null)
                    {
                        var current = normalizedLevenshtein.Distance(searchQuery, possibleTitle);

                        if (current > bestResult)
                        {
                            bestResult = current;
                            mediaResult = media;
                        }
                    }
                }
            }

            return mediaResult;
        }

        private int CalculateDistance(string a, string b)
        {
            // https://stackoverflow.com/a/9453762
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)) return 0;

            if (string.IsNullOrEmpty(a)) return b.Length;

            if (string.IsNullOrEmpty(b)) return a.Length;

            a = a.ToLower();
            b = b.ToLower();

            var lengthA = a.Length;
            var lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (var i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (var j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (var i = 1; i <= lengthA; i++)
            for (var j = 1; j <= lengthB; j++)
            {
                var cost = b[j - 1] == a[i - 1] ? 0 : 1;
                distances[i, j] = Math.Min
                (
                    Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost
                );
            }

            return distances[lengthA, lengthB];
        }
    }
}
