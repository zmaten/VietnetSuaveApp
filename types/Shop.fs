namespace VietnetSuaveApp.Types

open VietnetSuaveApp.Types.IWrappedDouble
open VietnetSuaveApp.Types.Units
// open VietnetSuaveApp.Types.Rating5


type Shop = {
  Id : int
  Name : string
  Coordinates : LatLong
  Rating : Rate option
//   Rating : Rating5.T option
}