using KenticoCloud.Compose.RichText.Models;

namespace KenticoCloud.Compose.RichText.Resolvers
{
    /// <summary>
    /// An interface, implemented to be registered as resolver for specific content type of inline content item
    /// </summary>
    /// <typeparam name="T">Content type to be resolved</typeparam>
    public interface IInlineContentResolver<THelper, TItem>
    {
        /// <summary>
        /// Method implementing the resolving of inline content item. Result should be valid HTML code
        /// </summary>
        /// <param name="data">Content item to be resolved</param>
        /// <returns>HTML code</returns>
        string Resolve(THelper helper, ResolvedInlineData<TItem> data);
    }
}
