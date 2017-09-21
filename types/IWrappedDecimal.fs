namespace VietnetSuaveApp.Types

module IWrappedDouble =
    
    
    /// An interface that all wrapped doubles support
    type IWrappedDouble =
        abstract Value:double

    /// Create a wrapped value option
    /// 1) canonicalize the input first
    /// 2) If the validation succeeds, return Some of the given constructor
    /// 3) If the validation fails, return None
    /// Null values are never valid.
    let create canonicalize isValid ctor (d:double) =
        let d' = canonicalize d
        if isValid d'
        then Some (ctor d')
        else None

    /// Apply given function to the wrapped value
    let apply f (d:IWrappedDouble) =
        d.Value |> f

    /// Get the wrapped value
    let value d = 
        apply id d

    /// Equality test
    let equals left right =
        (value left) = (value right)


    /// Comparison
    let compareTo left right = 
        (value left).CompareTo (value right)