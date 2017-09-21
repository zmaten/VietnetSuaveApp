namespace VietnetSuaveApp.Types

type Rate(value) =
  do
    if value > 5.0 then
      invalidArg "value" "Limited to 1-5"
  member x.Value = value

module Rating5 =
    type T = Rating5 of double
        interface IWrappedDouble with
            member this.Value = let (Rating5 r) = this in r

    let create (d:double) =
        if (d <= 5.0)
        then Some (Rating5 d)
        else None