using System;
namespace Igloo.Events
{
    /// <summary>
    /// Used to detect when a page document title has been changed
    /// </summary>
    public class DocumentTitleChange : EventArgs
    {
        public string DocumentTitle;
    }
}
