namespace Embeddings;

public static class MarkdownReader
{
    public static IEnumerable<(string Filename, string Content)> ReadAll()
    {
        var files = Directory.GetFiles( Path.Combine(Directory.GetCurrentDirectory(),"Data"), "*.md");
        foreach (var file in files)
        {
            var filename = Path.GetFileName(file);
            yield return (filename,ReadFile(file));
        }
    }

    private static  string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }
}