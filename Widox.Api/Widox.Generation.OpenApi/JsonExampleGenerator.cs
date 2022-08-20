using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;

namespace Widox.Generation
{
    public class OpenApiJsonExampleGenerator
    {
        Dictionary<string, OpenApiSchema> Schemas = new Dictionary<string, OpenApiSchema>();

        public void Initialize(OpenApiDocument openApiDoc)
        {
            var schemas = openApiDoc.Components.Schemas;
            foreach (var schema in schemas)
            {
                Schemas.Add(schema.Key, schema.Value);
            }
        }

        public string GetExample(OpenApiSchema schema)
        {
            if (schema == null)
                return string.Empty;

            return JsonConvert.SerializeObject(SerializeObject(schema), Formatting.Indented);
        }

        private Dictionary<string, object> SerializeObject(OpenApiSchema schema)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (var property in schema.Properties)
            {
                if (property.Value.Reference == null)
                    result.Add(property.Key, property.Value.Type);
                else if (Schemas.ContainsKey(property.Value.Reference.Id))
                {
                    result.Add(property.Key, SerializeObject(Schemas[property.Value.Reference.Id]));
                }
            }

            return result;
        }
    }
}
