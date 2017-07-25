using System;
using System.Collections.Generic;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */
        readonly List<IDisposable> disposableItems = new List<IDisposable>();
        public void AddItemsToDispose(object item)
        {
            var disposableItem = item as IDisposable;
            if (disposableItem != null)
            {
                disposableItems.Add(disposableItem);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                disposableItems.ForEach(item => item.Dispose());
            }
        }

        #endregion
    }
}