using System;
namespace Igloo.Events
{
    /// <summary>
    /// Used to change the DocumentURL
    /// </summary>
    public class DocumentURLChange : EventArgs
    {
        public string DocumentURL;
    }
}
