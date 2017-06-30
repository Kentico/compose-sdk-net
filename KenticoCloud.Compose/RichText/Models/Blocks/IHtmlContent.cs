namespace KenticoCloud.Compose.RichText.Models
{
    public interface IHtmlContent : IRichTextBlock
    {
        string Html { get; }
    }
}