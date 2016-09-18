// <copyright file="WebUtility.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Utility
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Utility class related to serialization.
    /// </summary>
    public class SerializationUtility
    {
        /// <summary>
        /// Deserialize the JSON content string into the given type.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize into.</typeparam>
        /// <param name="content">The string content to deserialize</param>
        /// <returns>A <see cref="{DeSerializeJson<T>}"/> filled with the content of the json string.</returns>
        public static T DeserializeDataContractJson<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true
            };

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), settings);

                return (T)serializer.ReadObject(stream);
            }
        }
    }
}