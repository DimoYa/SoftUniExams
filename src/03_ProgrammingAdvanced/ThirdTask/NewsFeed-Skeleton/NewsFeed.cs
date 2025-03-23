using System.Text;

namespace NewsFeed
{
    public class NewsFeed
    {
        public NewsFeed(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Articles = new List<Article>();
        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public List<Article> Articles  { get; set; }

        public void AddArticle(Article article)
        {
            if (Articles.Count >= Capacity || Articles.Any(a => a.Title == article.Title))
            {
                return; 
            }

            Articles.Add(article);
        }

        public bool DeleteArticle(string title)
        {
            var currentArticle = Articles.FirstOrDefault(a => a.Title == title);

            if (currentArticle is null)
            {
                return false;
            }

            Articles.Remove(currentArticle);
            return true;
        }

        public Article GetShortestArticle()
        {
            return Articles.OrderBy(a => a.WordCount).FirstOrDefault();
        }

        public string GetArticleDetails(string title)
        {
            var currentArticle = Articles.FirstOrDefault(a => a.Title == title);

            if (currentArticle is null)
            {
                return $"Article with title '{title}' not found.";
            }

            return currentArticle.ToString();
        }

        public int GetArticlesCount()
        {
            return Articles.Count;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{Name} newsfeed content:");

            Articles
                 .OrderBy(x => x.WordCount)
                 .ToList()
                 .ForEach(x=> {
                     sb.AppendLine($"{x.Author}: {x.Title}");
                 });

            return sb.ToString().TrimEnd();
        }
    }
}
