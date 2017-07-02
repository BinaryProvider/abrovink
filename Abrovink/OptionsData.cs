using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abrovink
{
    public class OptionsData
    {
        public SerializableDictionary<string, string> StringVals { get; set; }
        public SerializableDictionary<string, int> IntVals { get; set; }
        public SerializableDictionary<string, bool> BoolVals { get; set; }

        public OptionsData()
        {
            StringVals = new SerializableDictionary<string, string>();
            IntVals = new SerializableDictionary<string, int>();
            BoolVals = new SerializableDictionary<string, bool>();
        }

        public static string LoadDataString(string dataToSerialize, string name)
        {
            var data = dataToSerialize.XmlDeserializeFromString<OptionsData>();
            if (data != null)
            {
                if(data.StringVals.ContainsKey(name))
                {
                    return data.StringVals[name];
                }
            }
            return string.Empty;
        }

        public static int LoadDataInt(string dataToSerialize, string name)
        {
            var data = dataToSerialize.XmlDeserializeFromString<OptionsData>();
            if (data != null)
            {
                if (data.IntVals.ContainsKey(name))
                {
                    return data.IntVals[name];
                }
            }
            return -1;
        }

        public static bool LoadDataBool(string dataToSerialize, string name)
        {
            var data = dataToSerialize.XmlDeserializeFromString<OptionsData>();
            if (data != null)
            {
                if (data.BoolVals.ContainsKey(name))
                {
                    return data.BoolVals[name];
                }
            }
            return false;
        }
    }
}
