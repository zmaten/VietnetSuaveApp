namespace VietnetSuaveApp.Types

open VietnetSuaveApp.Types.IWrappedDouble
open VietnetSuaveApp.Types.Units


type Shop = {
  Id : int
  Name : string
  Coordinates : LatLong
  Rating : ShopRating option
}