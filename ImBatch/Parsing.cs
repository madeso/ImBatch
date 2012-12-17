// File: Parsing.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    internal static class Parsing
    {
        public static string Expand(IEnumerable<string> values)
        {
            string temp = "";
            bool first = true;
            foreach (var value in values)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    temp += ", ";
                }
                temp += value;
            }

            return "[" + temp + "]";
        }

        public static Color ParseColor(XmlElement element, string color)
        {
            return Color.FromName(ParseEnum<KnownColor>(element, color).ToString());
        }

        public static double ParseDouble(XmlElement act, string p1)
        {
            var data = act.GetAttribute(p1);
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("missing attribute " + p1);
            }
            try
            {
                return double.Parse(data, CultureInfo.InvariantCulture);
            }
            catch
            {
                throw new Exception(p1 + " is not not a double " + data);
            }
        }

        public static T ParseEnum<T>(XmlElement act, string name) where T : struct
        {
            var type = typeof(T);
            var value = act.GetAttribute(name);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Missing argument " + name);
            }

            T r;
            if (false == System.Enum.TryParse(value, true, out r))
            {
                var values = System.Enum.GetNames(type);
                throw new Exception(value + " is not a valid " + type.Name + " in attribute " + name + ": " + Expand(values));
            }
            return r;
        }

        public static int ParseInt(XmlElement act, string p1)
        {
            var data = act.GetAttribute(p1);
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("missing attribute " + p1);
            }
            int r;
            if (false == Int32.TryParse(data, out r))
            {
                throw new Exception(p1 + " is not not a integer " + data);
            }
            return r;
        }

        public static string ParseString(XmlElement xmlElement, string font)
        {
            return xmlElement.GetAttribute(font);
        }
    }
}
