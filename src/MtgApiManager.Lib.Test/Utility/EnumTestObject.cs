namespace MtgApiManager.Lib.Test.Utility
{
    using System.ComponentModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Enumeration used for testing
    /// </summary>
    public enum EnumTestObject
    {
        /// <summary>
        /// No description.
        /// </summary>
        NoDescription = 0,

        [JsonPropertyName("notDescriptionAttribute")]
        DifferentAttribute = 1,

        /// <summary>
        /// Has description.
        /// </summary>
        [Description("greatDescription")]
        HasDescription = 2,
    }
}