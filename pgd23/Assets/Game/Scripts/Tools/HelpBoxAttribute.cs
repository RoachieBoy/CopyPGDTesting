using UnityEngine;

namespace Game.Scripts.Tools
{
    public class HelpBoxAttribute: PropertyAttribute
    {
        /// <summary>
        ///     Text to be added to the help box 
        /// </summary>
        public readonly string Text;
        /// <summary>
        ///     Which type of message is contained in the help box 
        /// </summary>
        public readonly HelpBoxMessageType MessageType;

        public HelpBoxAttribute(string text, HelpBoxMessageType messageType = HelpBoxMessageType.None)
        {
            Text = text;
            MessageType = messageType;
        }
    }
}