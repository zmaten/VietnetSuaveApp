open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open VietnetSuaveApp.Rest
open VietnetSuaveApp.Db

let app =
    choose
        [ GET >=> choose
            [ path "/" >=> OK "Index"
              path "/hello" >=> OK "Hello!"
              path "/test" >=> JSON "Test successful!" ]
          POST >=> choose
            [ path "/hello" >=> OK "Hello POST!" ] ]



[<EntryPoint>]
let main argv =
    let shopWebPart = rest "shops" {
        GetAll = Db.getShops
        Create = Db.createShop
    }
    startWebServer defaultConfig shopWebPart
    0