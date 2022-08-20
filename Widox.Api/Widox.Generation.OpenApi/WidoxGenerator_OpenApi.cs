using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using System.Text;
using Widox.Blocks.Impl;
using Widox.Blocks.Impl.Endpoints;
using Widox.Enums;
using Widox.Generation.Core;

namespace Widox.Generation
{
    public class WidoxGenerator_OpenApi : IBlockGenerator
    {

        Dictionary<string, string> Schemas = new();

        OpenApiJsonExampleGenerator _jsonGen = new();

        public async Task<IEnumerable<BlockBase>> GenerateBlocksAsync()
        {
            return await GetOpenApiDataAsync("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/main/examples/v3.0/petstore-expanded.yaml");
        }

        private async Task<IEnumerable<BlockBase>> GetOpenApiDataAsync(string openApiUrl)
        {
            var output = new List<EndpointBlock>();

            using (var client = new HttpClient() )
            {
                var stream = await client.GetStreamAsync(openApiUrl);

                var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

                _jsonGen.Initialize(openApiDocument);

                if (openApiDocument.Paths != null)
                {
                    foreach (var item in openApiDocument.Paths)
                        AddEndpointBlock(output, item);
                }

            }


            return output;

        }

        private void AddEndpointBlock(List<EndpointBlock> output, KeyValuePair<string, OpenApiPathItem> item)
        {
            var outputItem = new EndpointBlock()
            {
                Description = item.Value.Description,
                Title = item.Key,
                Identifier = item.Key,
            };

            foreach (var operation in item.Value.Operations)
            {
                EndpointMethodBlock methodBlock = AddEndpointMethod(operation);
                outputItem.Methods.Add(methodBlock.Method, methodBlock);
            }

            output.Add(outputItem);
        }

        private EndpointMethodBlock AddEndpointMethod(KeyValuePair<OperationType, OpenApiOperation> operation)
        {
            var httpMethod = ConvertOpenApiOperationTypeToHttpMethod(operation.Key);
            var methodBlock = new EndpointMethodBlock()
            {
                Method = httpMethod,
                Description = operation.Value.Description,
            };

            if (operation.Value.RequestBody?.Content?.Any() ?? false)
            {
                var firstBody = operation.Value.RequestBody.Content.FirstOrDefault();
                methodBlock.Example = _jsonGen.GetExample(firstBody.Value.Schema);
            }

            foreach (var response in operation.Value.Responses)
            {
                var responseBlocks = CreateResponseBlocks(httpMethod, response);
                methodBlock.Responses.AddRange(responseBlocks);
            }

            return methodBlock;
        }

        private List<HttpResponseBlock> CreateResponseBlocks(HttpRequestMethod method, KeyValuePair<string, OpenApiResponse> response)
        {
            var output = new List<HttpResponseBlock>();

            foreach (var item in response.Value.Content)
            {
                output.Add(new HttpResponseBlock()
                {
                    Method = method,
                    ResponseType = response.Key,
                    Title = item.Value.Schema?.Title,
                    Description = item.Value.Schema?.Description,
                    Body = _jsonGen.GetExample(item.Value.Schema!)
            });
            }

            return output;
        }

        private HttpRequestMethod ConvertOpenApiOperationTypeToHttpMethod(OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Get:
                    return HttpRequestMethod.GET;
                case OperationType.Put:
                    return HttpRequestMethod.PUT;
                case OperationType.Post:
                    return HttpRequestMethod.POST;
                case OperationType.Delete:
                    return HttpRequestMethod.DELETE;
                case OperationType.Options:
                    return HttpRequestMethod.OPTIONS;
                case OperationType.Head:
                    return HttpRequestMethod.HEAD;
                case OperationType.Patch:
                    return HttpRequestMethod.PATCH;
                case OperationType.Trace:
                    return HttpRequestMethod.TRACE;
                default:
                    throw new InvalidOperationException($"Unsupported HttpRequestMethod {operationType}");
            }
        }
    }
}