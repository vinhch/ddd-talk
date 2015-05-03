using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DDDTalk.UnitTests
{
    public static class JsonCompareHelper
    {
        public static void Compare(ICollection<string> logs, JToken expected, JToken actual)
        {
            if ((expected == null && actual != null)
                || (expected != null && actual == null)
                || (expected != null && expected.Type != actual.Type))
            {
                LogDiff(logs, expected, actual);
            }
            else if (expected != null)
            {
                switch (expected.Type)
                {
                    case JTokenType.Object:
                        Compare(logs, (JObject)expected, (JObject)actual);
                        break;
                    case JTokenType.Array:
                        Compare(logs, (JArray)expected, (JArray)actual);
                        break;
                    default:
                        if (!expected.Equals(actual))
                        {
                            LogDiff(logs, expected, actual);
                        }

                        break;
                }
            }
        }

        private static void Compare(ICollection<string> logs, JArray expected, JArray actual)
        {
            for (var i = 0; i < Math.Max(expected.Count(), actual.Count()); i++)
            {
                Compare(logs, expected.ElementAtOrDefault(i), actual.ElementAtOrDefault(i));
            }
        }

        private static void Compare(ICollection<string> logs, JObject expected, JObject actual)
        {
            foreach (var expectedProp in expected.Properties())
            {
                var actualProp = actual.Property(expectedProp.Name);
                Compare(logs, expectedProp.Value, actualProp.Value);
            }
        }

        private static void LogDiff(ICollection<string> logs, JToken expectedValue, JToken actualValue)
        {
            var path = expectedValue != null
                ? expectedValue.Path : actualValue != null
                ? actualValue.Path : "Unknown";

            var expected = expectedValue != null ? expectedValue.ToString(Formatting.None) : "null";
            var actual = actualValue != null ? actualValue.ToString(Formatting.None) : "null";

            logs.Add(string.Format("For path {0}", path));
            logs.Add(string.Format("  Expected: {0}", expected));
            logs.Add(string.Format("  But was: {0}", actual));
        }
    }
}
