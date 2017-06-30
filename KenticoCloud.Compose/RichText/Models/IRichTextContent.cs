using System.Collections.Generic;

namespace KenticoCloud.Compose.RichText.Models
{
    public interface IRichTextContent : IEnumerable<IRichTextBlock>
    {
        IEnumerable<IRichTextBlock> Blocks { get; set; }
    }
}
