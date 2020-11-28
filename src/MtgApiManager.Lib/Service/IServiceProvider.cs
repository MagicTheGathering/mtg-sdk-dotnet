namespace MtgApiManager.Lib.Service
{
    /// <summary>
    /// Used to get an instance of a service class.
    /// </summary>
    public interface IMtgServiceProvider
    {
        /// <summary>
        /// Gets an instance of the card service.
        /// </summary>
        /// <returns>An instance of the card service.</returns>
        ICardService GetCardService();

        /// <summary>
        /// Gets an instance of the set service.
        /// </summary>
        /// <returns>An instance of the set service.</returns>
        ISetService GetSetService();
    }
}