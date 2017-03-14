using System;

namespace KenticoCloud
{
    public sealed class ExtensionPoint<T> where T : class
    {
        internal ExtensionPoint(T target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Target = target;
        }

        /// <summary>
        /// Gets instance of class that is a target of extension methods.
        /// </summary>
        public T Target { get; }
    }
}