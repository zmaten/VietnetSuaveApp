﻿namespace VietnetSuaveApp.Rest

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

[<AutoOpen>]
module RestFul =

  type RestResource<'a> = {
    GetAll : unit -> 'a seq
    Create: 'a -> 'a
  }

  let fromJson<'a> json =
    JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

  let getResourceFromReq<'a> (req: HttpRequest) =
    let getString rawForm =
      System.Text.Encoding.UTF8.GetString(rawForm)
    req.rawForm |> getString |> fromJson<'a>


  // 'a -> WebPart
  let JSON v =
      let jsonSerializerSettings = JsonSerializerSettings()
      jsonSerializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()

      JsonConvert.SerializeObject(v, jsonSerializerSettings)
      |> OK
      >=> Writers.setMimeType "application/json; charset=utf-8"


  // string -> RestResource<'a> -> WebPart
  let rest resourceName resource =
      let resourcePath = "/" + resourceName
      let getAll = warbler (fun _ -> resource.GetAll () |> JSON)
      path resourcePath >=> choose [
        GET >=> getAll
        POST >=> request (getResourceFromReq >> resource.Create >> JSON)
      ]
