using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Notes.Data.Converter
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            DateTime datetime = DateTime.MinValue;
            if(value != null && DateTime.TryParse(value.ToString(), out datetime))
            {
                return datetime;
            }
#if DEBUG
            Debug.WriteLine("#################################################");
            Debug.WriteLine("#################################################");
            Debug.WriteLine("");
            Debug.WriteLine("Could not parse Datetime from string: " + value == null ? value.ToString() : "null");
            Debug.WriteLine("");
            Debug.WriteLine("#################################################");
            Debug.WriteLine("#################################################");
#endif
            return datetime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((DateTime)value);
        }
    }
}
