using KenticoCloud.Compose.RichText.Models;

namespace KenticoCloud.Compose.RichText.Resolvers
{
    public class DefaultImageResolver<THelper> : IInlineContentResolver<THelper, IInlineImage>
    {
        public string Resolve(THelper helper, ResolvedInlineData<IInlineImage> image)
        {
            return $"<figure><img src=\"{image.Data.Src}\" alt=\"{image.Data.AltText}\"></figure>";
        }
    }
}