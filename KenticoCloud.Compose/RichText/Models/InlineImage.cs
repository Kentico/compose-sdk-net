using KenticoCloud.Compose.RichText.Models;

namespace KenticoCloud.Compose.RichText
{
    internal class InlineImage : IInlineImage
    {
        public string AltText { get; set; }

        public string Src { get; set; }
    }
}