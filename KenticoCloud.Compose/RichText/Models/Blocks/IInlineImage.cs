using System.Web;

namespace KenticoCloud.Compose.RichText.Models
{
    public interface IInlineImage : IRichTextBlock
    {
        string AltText { get; set; }

        string Src { get; set; }
    }
}