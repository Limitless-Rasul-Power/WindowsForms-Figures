using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsForms_Figures
{
    #region JSON        

    public static class JsonFileHelper
    {
        public static void JSONSerialization<T>(List<T> datas, string fileName)
        {
            if (datas == null || string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidOperationException("Data is null or file name is null or white space.");
            }

            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter($"{fileName}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, datas);
                }
            }
        }

        public static void JSONDeSerialization<T>(ref List<T> datas, string fileName)
        {
            if (File.Exists($"{fileName}.json") == false)
            {
                throw new InvalidOperationException("File didn't exist.");
            }


            var serializer = new JsonSerializer();
            using (var sr = new StreamReader($"{fileName}.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    datas = serializer.Deserialize<List<T>>(jr);
                }
            }
        }
    }

    #endregion
}
