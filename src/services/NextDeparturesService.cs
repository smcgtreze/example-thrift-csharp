using System.Text;

namespace Transports;

public class NextDeparturesServiceHandler : ServiceHandler<NextDeparturesRequest, NextDeparturesResponse>
{
    public NextDeparturesServiceHandler(HttpClient? httpClient = null) : base(httpClient)
    {
    }
    protected override string GetEndpoint()
    {
        return "nextDepartures";
    }

    protected override Key GetKey(NextDeparturesRequest request)
    {
        return request.Key ?? throw new InvalidOperationException("Request key is required.");
    }


    protected override void AppendArgumentsToUrl(NextDeparturesRequest request, StringBuilder url)
    {
        AppendQueryParam(url, "location", request.Location);
        AppendQueryParam(url, "countryIso", request.CountryIso);
        AppendQueryParam(url, "stopId", request.StopId);
        AppendQueryParam(url, "regionName", request.RegionName);
        AppendQueryParam(url, "requestTime", request.RequestTime);
        AppendQueryParam(url, "radius", request.Radius);
        AppendQueryParam(url, "results", request.Results);
        AppendQueryParam(url, "barrierMode", request.BarrierMode);
        AppendQueryParam(url, "lang", request.Lang);
    }
}
