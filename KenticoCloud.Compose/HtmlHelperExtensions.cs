using System.Web.Mvc;

namespace KenticoCloud
{
    public static class HtmlHelperExtensions
    {
        private static readonly object Lock = new object();

        private static ExtensionPoint<HtmlHelper> extensionPoint;

        public static ExtensionPoint<HtmlHelper> Kentico(this HtmlHelper target)
        {
            lock (Lock)
            {
                if (extensionPoint == null || extensionPoint.Target != target)
                {
                    extensionPoint = new ExtensionPoint<HtmlHelper>(target);
                }

                return extensionPoint;
            }
        }
    }
}