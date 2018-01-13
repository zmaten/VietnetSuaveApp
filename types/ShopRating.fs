namespace VietnetSuaveApp.Types

type ShopRating(value) =
  do
    if value > 5.0 then
      invalidArg "value" "Limited to 1-5"
  member x.Value = value