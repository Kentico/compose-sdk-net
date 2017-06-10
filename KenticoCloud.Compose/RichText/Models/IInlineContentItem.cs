namespace KenticoCloud.Compose.RichText.Models
{
    public interface IInlineContentItem : IRichTextBlock
    {
        object ContentItem { get; set; }
    }
}