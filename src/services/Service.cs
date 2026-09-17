using System.Text;
using System.Text.Json;

namespace Transports;

public abstract class ServiceHandler<IN, OUT>
{
    private readonly HttpClient _httpClient;

    public ServiceHandler(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<OUT> GetNextDepartures(
        IN request,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(request) ?? throw new InvalidOperationException("Request key is required.");
        var url = new StringBuilder($"https://capi.{key.CapiHost}:{key.Port}/{GetEndpoint()}");

        AppendArgumentsToUrl(request, url);

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url.ToString());
        httpRequest.Headers.Add("capi-key", $"Bearer {key.ApiKey}");
        httpRequest.Headers.Add("capi-host", key.CapiHost);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        return JsonSerializer.Deserialize<OUT>(body, jsonOptions)
            ?? throw new InvalidOperationException("Unable to deserialize response.");
    }

    protected abstract string GetEndpoint();
    protected abstract Key GetKey(IN request);
    protected abstract void AppendArgumentsToUrl(IN request, StringBuilder url);

    protected static void AppendQueryParam(StringBuilder builder, string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        builder.Append(builder.ToString().Contains("?") ? '&' : '?');
        builder.Append(name).Append('=').Append(Uri.EscapeDataString(value));
    }

    protected static void AppendQueryParam(StringBuilder builder, string name, int? value)
    {
        if (!value.HasValue)
            return;

        builder.Append(builder.ToString().Contains("?") ? '&' : '?');
        builder.Append(name).Append('=').Append(value.Value);
    }

    protected static void AppendQueryParam(StringBuilder builder, string name, bool value)
    {
        if (!value)
            return;

        builder.Append(builder.ToString().Contains("?") ? '&' : '?');
        builder.Append(name).Append('=').Append(value.ToString().ToLowerInvariant());
    }
}
