namespace netstd Bus

service BusService {
  string getBusStatus(1: i32 busId)
}