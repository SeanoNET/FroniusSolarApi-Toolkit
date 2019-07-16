using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FroniusSolarApi.Repository.ConsoleOut
{
    internal class ConsoleWriter<T>
    {

        /// <summary>
        /// Prints the data object to the console
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void WriteConsole(T data)
        {
            if (data == null)
                throw new ArgumentNullException($"Can not write null object {nameof(data)}");

            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            Console.Write(JsonConvert.SerializeObject(data, settings: settings, formatting: Formatting.Indented));
        }

    }
}
