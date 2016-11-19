using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace Files
{
    public class CsvObject
    {
        public string Name { get; set; }
        public int? Ozone { get; set; }
        public int? SolarR { get; set; }
        public double Wind { get; set; }
        public int Temp { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

    public static class Csv
    {
        private static string[] ParseString(string s, int countColumns)
        {
            var result = new string[countColumns];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = s.Split(',')[i].Trim();
            }
            return result;
        }

        private static int CountColumnsInFile(string str)
        {
            var countColumns = 1;
            foreach (var substr in str)
            {
                if (substr == ',')
                    countColumns++;
            }

            return countColumns;
            //_amountOfColumns = str.Split(',').Length;
        }

        private static void ReplaceNullValues(ref string s)
        {
            s = s.Replace("NA", null);
        }

        private static void RemoveExtraSymbolsInRow(ref string[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Replace(",", ".");
                values[i] = values[i].Replace("\"", "");
            }
        }

        private static void RemoveExtraSymbolsInColumn(ref string[] columns)
        {
            for (var i = 0; i < columns.Length; i++)
            {
                columns[i] = columns[i].Replace(".", "");
                columns[i] = columns[i].Replace("\"", "");
            }
        }

        private static object DefinePropertyType(Type type, string value)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                if (value == "NA")
                {
                    return null;
                }
                switch (Type.GetTypeCode(type.GenericTypeArguments[0]))
                {
                    case TypeCode.Int32:
                        return Convert.ToInt32(value);
                    case TypeCode.Double:
                        return Convert.ToDouble(value);
                    default:
                        return null;
                }
            }

            if (value == "NA")
                throw new ArgumentException("cannot substitute null value to not null field");

            switch (type.Name)
            {
                case "Int32":
                    return Convert.ToInt32(value);
                case "Double":
                    return Convert.ToDouble(value);
                default:
                    return value;
            }
        }

        private static T FillObject<T>(IReadOnlyList<string> columns, IReadOnlyList<string> values) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            for (var i = 0; i < columns.Count; i++)
            {
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Name != columns[i]) continue;
                    var value = DefinePropertyType(propertyInfo.PropertyType, values[i]);
                    propertyInfo.SetValue(obj, value, null);
                    break;
                }
            }
            return obj;
        }

        private static object DefineObject(string value)
        {
            int intResult;
            if (int.TryParse(value, out intResult))
            {
                return intResult;
            }

            double doubleResult;
            if (double.TryParse(value, out doubleResult))
            {
                return doubleResult;
            }
            return value;
        }

        private static IEnumerable<string> ReadFile(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                while (true)
                {
                    var str = reader.ReadLine();
                    if (str == null)
                    {
                        reader.Close();
                        yield break;
                    }
                    yield return str;
                }
            }
        }

        public static IEnumerable<string[]> ReadCsv1(string filename)
        {
            var countColumns = CountColumnsInFile(File.ReadLines(filename).First());
            var readFileRes = ReadFile(filename);
            foreach (var str in readFileRes)
            {
                if (readFileRes.First() == str)
                    continue;

                var s = str;
                ReplaceNullValues(ref s);
                var values = ParseString(s, countColumns);
                RemoveExtraSymbolsInRow(ref values);
                yield return values;
            }
        }

        public static IEnumerable<T> ReadCsv2<T>(string filename) where T : new()
        {
            var type = typeof(T);

            var properties = type.GetProperties();

            if (properties.Length == 0)
            {
                throw new ArgumentException("Object doesn't contain fields or properties");
            }

            var firstLineInFile = File.ReadLines(filename).First();
            var countColumns = CountColumnsInFile(firstLineInFile);
            var columns = ParseString(firstLineInFile, countColumns);
            RemoveExtraSymbolsInColumn(ref columns);

            var readFileRes = ReadFile(filename);

            foreach (var str in readFileRes)
            {
                if (readFileRes.First() == str)
                    continue;
                var s = str;
                var values = ParseString(s, countColumns);
                RemoveExtraSymbolsInRow(ref values);
                yield return FillObject<T>(columns, values);
            }
        }

        public static IEnumerable<Dictionary<string, object>> ReadCsv3(string filename)
        {
            var firstLineInFile = File.ReadLines(filename).First();
            var countColumns = CountColumnsInFile(File.ReadLines(filename).First());
            var columns = ParseString(firstLineInFile, countColumns);
            RemoveExtraSymbolsInColumn(ref columns);
            var readFileRes = ReadFile(filename);

            foreach (var str in readFileRes)
            {
                if (readFileRes.First() == str)
                    continue;

                var s = str;
                ReplaceNullValues(ref s);
                var values = ParseString(s, countColumns);
                RemoveExtraSymbolsInRow(ref values);
                var dict = new Dictionary<string, object>();
                for (var i = 0; i < countColumns; i++)
                {
                    dict.Add(columns[i], DefineObject(values[i]));
                }
                yield return dict;
            }
        }

        public static IEnumerable<dynamic> ReadCsv4(string filename)
        {
            var enumDictonaries = ReadCsv3(filename);
            var expandoObject = new ExpandoObject();
            foreach (var dict in enumDictonaries)
            {
                var expandoObjectDictionary = (IDictionary<string, object>) expandoObject;
                expandoObjectDictionary.Clear();

                foreach (var kvp in dict)
                {
                    expandoObjectDictionary.Add(kvp);
                }

                dynamic expandoObjectDynamic = expandoObject;
                yield return expandoObjectDynamic;
            }
        }
    }
}