namespace netstd transports

service NextDeparturesService {
  NextDeparturesResponse GetNextDepartures(1: NextDeparturesRequest request)
}

struct NextDeparturesRequest {
  1: string location
  2: string stopId
  3: string regionName
  4: Key key
  5: string countryIso
  6: optional string requestTime
  7: optional i32 radius
  8: optional i32 results
  9: optional i32 barrierMode
  10: optional string lang
}

struct NextDeparturesResponse {
  1: list<StopDeparture> stop_departures
  2: bool imperial
  3: string local_date
  4: string region_name
  5: list<Alert> alerts
  6: i64 request_time
  7: double processing_time_ms
  8: string local_time
}

struct Key {
  1: string apiKey
  2: string capiHost
  3: i32 port
}

struct Alert {
  1: string id
  2: string origin
  3: string cause
  4: string effect
  5: string header
  6: string description
  7: string url
  8: string validFrom
  9: string validUntil
  10: string operator
  11: optional string countryIso
  12: optional string countryUrl
  13: optional string sectionIds
  14: list<InformedEntity> informedEntity
}

struct InformedEntity {
  1: string routeId
  2: optional string countryIso
  3: optional string countryUrl
  4: optional string stopId
}

struct Agency {
  1: string agency_url
  2: string country_iso
  3: string agency_id
  4: optional string url_agency_name
  5: string country_url
  6: optional string agency_phone
  7: string agency_name
}

struct DepartureItem {
  1: string country_iso
  2: string rt_date
  3: string date
  4: string trip_id
  5: optional string route_short_name
  6: optional string route_color
  7: string rt_departure_time
  8: string departure_time
  9: string trip_headsign
  10: i32 wheelchair_accessible
  11: string agency_id
  12: optional string route_text_color
  13: optional string route_desc
  14: optional string url_route_short_name
  15: string country_url
  16: string route_id
  17: string route_type
  18: string platform_code
  19: optional string route_long_name
  20: optional string url_route_long_name
}

struct StopDeparture {
  1: list<DepartureItem> departure_list
  2: string country_iso
  3: string stop_id
  4: double stop_lon
  5: string stop_name
  6: double stop_lat
  7: string country_url
  8: i32 wheelchair_boarding
  9: string stop_code
  10: string stop_desc
  11: optional string url_stop_name
  12: list<Agency> agencies
}
