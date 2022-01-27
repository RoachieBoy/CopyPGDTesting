namespace Game.Scripts.VisualEffects.TextEffects
{
    /// <summary>
    ///     Used to determine what type of text manipulation should take place 
    /// </summary>
    public enum TextManipulationType
    {
        /// <summary>
        ///     Wobbles the text vertex
        /// </summary>
        VertexWobble,
        /// <summary>
        ///     Wobbles individual words 
        /// </summary>
        WordWobble,
        /// <summary>
        ///     Wobbles individual characters of words 
        /// </summary>
        CharacterWobble
    }
}