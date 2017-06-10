using KenticoCloud.Compose.RichText.Models;
using KenticoCloud.Compose.RichText.Resolvers;
using System;
using System.Collections.Generic;

namespace KenticoCloud.Compose.RichText
{
    public class RichTextProcessor<THelper>
    {
        public static readonly RichTextProcessor<THelper> Default = new RichTextProcessor<THelper>();

        private readonly Dictionary<Type, Func<THelper, object, string>> _typeResolver = new Dictionary<Type, Func<THelper, object, string>>();


        public IInlineContentResolver<THelper, IInlineImage> ImageResolver = new DefaultImageResolver<THelper>();

        public IInlineContentResolver<THelper, object> DefaultTypeResolver;


        internal string ResolveModular(THelper helper, IInlineContentItem modular)
        {
            Func<THelper, object, string> resolver;
            if ((modular.ContentItem != null) && _typeResolver.TryGetValue(modular.ContentItem.GetType(), out resolver))
            {
                return resolver(helper, modular.ContentItem);
            }

            return DefaultTypeResolver?.Resolve(helper, new ResolvedInlineData<object> { Data = modular.ContentItem });
        }

        public void RegisterTypeResolver<T>(IInlineContentResolver<THelper, T> resolver)
        {
            _typeResolver.Add(typeof(T), (helper,  item) => resolver.Resolve(helper, new ResolvedInlineData<T> { Data = (T)item }));
        }

        internal string ResolveImage(THelper helper, IInlineImage image)
        {
            return ImageResolver?.Resolve(helper, new ResolvedInlineData<IInlineImage> { Data = image });            
        }
    }
}
