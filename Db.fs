namespace VietnetSuaveApp.Db

open System.Collections.Generic
open VietnetSuaveApp.Types



module Db =
  let private shopStorage = new Dictionary<int, Shop>()

  let getShops () =
    shopStorage.Values |> Seq.map (id)

  let createShop shop =
    let id = shopStorage.Values.Count + 1
    let shopToSave = {
        Id = id
        Name = shop.Name
        Coordinates = shop.Coordinates
        Rating = Some (ShopRating(4.0))
    }
    shopStorage.Add(id, shopToSave)
    shopToSave