public sealed class Key
{
    public string ApiKey { get; set; } = string.Empty;
    public string CapiHost { get; set; } = string.Empty;
    public int Port { get; set; }
}

public sealed class NextDeparturesRequest
{
    public string? Location { get; set; }
    public string? StopId { get; set; }
    public string? RegionName { get; set; }
    public string? CountryIso { get; set; }
    public string? RequestTime { get; set; }
    public int? Radius { get; set; }
    public int? Results { get; set; }
    public int? BarrierMode { get; set; }
    public string? Lang { get; set; }
    public Key? Key { get; set; }
}

public sealed class NextDeparturesResponse
{
    public List<StopDeparture> StopDepartures { get; set; } = [];
    public bool Imperial { get; set; }
    public string LocalDate { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public List<Alert> Alerts { get; set; } = [];
    public long RequestTime { get; set; }
    public double ProcessingTimeMs { get; set; }
    public string LocalTime { get; set; } = string.Empty;
}

public sealed class StopDeparture
{
    public List<DepartureItem> DepartureList { get; set; } = [];
    public string CountryIso { get; set; } = string.Empty;
    public string StopId { get; set; } = string.Empty;
    public double StopLon { get; set; }
    public string StopName { get; set; } = string.Empty;
    public double StopLat { get; set; }
    public string CountryUrl { get; set; } = string.Empty;
    public int WheelchairBoarding { get; set; }
    public string StopCode { get; set; } = string.Empty;
    public string StopDesc { get; set; } = string.Empty;
    public string? UrlStopName { get; set; }
    public List<Agency> Agencies { get; set; } = [];
}

public sealed class DepartureItem
{
    public string CountryIso { get; set; } = string.Empty;
    public string RtDate { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string TripId { get; set; } = string.Empty;
    public string? RouteShortName { get; set; }
    public string? RouteColor { get; set; }
    public string RtDepartureTime { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public string TripHeadsign { get; set; } = string.Empty;
    public int WheelchairAccessible { get; set; }
    public string AgencyId { get; set; } = string.Empty;
    public string? RouteTextColor { get; set; }
    public string? RouteDesc { get; set; }
    public string? UrlRouteShortName { get; set; }
    public string CountryUrl { get; set; } = string.Empty;
    public string RouteId { get; set; } = string.Empty;
    public string RouteType { get; set; } = string.Empty;
    public string PlatformCode { get; set; } = string.Empty;
    public string? RouteLongName { get; set; }
    public string? UrlRouteLongName { get; set; }
}

public sealed class Agency
{
    public string AgencyUrl { get; set; } = string.Empty;
    public string CountryIso { get; set; } = string.Empty;
    public string AgencyId { get; set; } = string.Empty;
    public string? UrlAgencyName { get; set; }
    public string CountryUrl { get; set; } = string.Empty;
    public string? AgencyPhone { get; set; }
    public string AgencyName { get; set; } = string.Empty;
}

public sealed class Alert
{
    public string Id { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Cause { get; set; } = string.Empty;
    public string Effect { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidUntil { get; set; } = string.Empty;
    public string OperatorName { get; set; } = string.Empty;
    public string? CountryIso { get; set; }
    public string? CountryUrl { get; set; }
    public string? SectionIds { get; set; }
    public List<InformedEntity> InformedEntity { get; set; } = [];
}

public sealed class InformedEntity
{
    public string RouteId { get; set; } = string.Empty;
    public string? CountryIso { get; set; }
    public string? CountryUrl { get; set; }
    public string? StopId { get; set; }
}