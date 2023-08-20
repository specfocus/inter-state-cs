using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace XState.Dynamic
{
    public class Class1
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        public T Deserialize<T>(string json)
        {
            var root = JsonNode.Parse(json);
            if (root!.GetType() == typeof(JsonArray))
            {
                var array = root.AsArray();
                foreach (var item in array)
                {
                    if (item!.GetType() == typeof(JsonObject))
                    {
                        var obj = item.AsObject();
                        obj.Add("test", "test");
                    }
                }
            }
            else if (root.GetType() == typeof(JsonObject))
            {
                var obj = root.AsObject();
                obj.Add("test", "test");
            }
            else
            {
                root!.AsObject().Add("test", "test");
            }
            root!.AsObject().Add("test", "test");
            JsonDocument doc = JsonDocument.Parse(json);
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
            }
            return JsonSerializer.Deserialize<T>(json, options);
        }

    }
}