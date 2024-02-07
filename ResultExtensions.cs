namespace Redirector
{
    using System.Net.Mime;
    using System.Text;
    static class ResultsExtensions
    {
        private const string Path = "Pages";

        public static IResult Html(this IResultExtensions resultExtensions, string htmlFileName)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions);

            var content = File.ReadAllText($"{Path}/{htmlFileName}");

            return new HtmlResult(content);
        }
    }

    class HtmlResult : IResult
    {
        private readonly string _html;

        public HtmlResult(string html)
        {
            _html = html;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Html;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);
            return httpContext.Response.WriteAsync(_html);
        }
    }
}
