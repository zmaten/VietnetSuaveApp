namespace VietnetSuaveApp.Types

module Units =
    [<Measure>] type radian
    [<Measure>] type degree
    [<Measure>] type km

    type LatLong = { Lat: float<degree>; Long: float<degree>}

    let SphereSurfaceDistance<[<Measure>]'t> (radius : float<'t>) (loc1 : LatLong) (loc2 : LatLong) =
        // Convert latitude and longitude to
        // spherical coordinates in radians.
        let degreesToRadians (d : float<degree>) =
            d * System.Math.PI / 180.0<degree/radian>
     
        // phi = 90 - latitude (coded F# pipeline style)
        let phi1 = (90.0<degree> - loc1.Lat) |> degreesToRadians
        let phi2 = (90.0<degree> - loc2.Lat) |> degreesToRadians
     
        // theta = longitude (coded F# function call style)
        let theta1 = degreesToRadians loc1.Long
        let theta2 = degreesToRadians loc2.Long
     
        // Compute spherical distance from spherical coordinates.
        
        // For two locations in spherical coordinates      
        // (1, theta, phi) and (1, theta, phi)     
        // cosine( arc length ) =      
        //    sin phi sin phi' cos(theta-theta') + cos phi cos phi'     
        // distance = rho * arc length
        
        radius * acos(sin(float(phi1)) * sin(float(phi2)) *
            cos(float(theta1 - theta2)) + 
            cos(float(phi1)) * cos(float(phi2)))
     
    // instantiate Earth-relative functions in km and miles
    let SurfaceDistanceOnEarthKm = SphereSurfaceDistance 6371.0<km>
     
     
    let test1SurfaceDistanceOnEarthKm =
        let richmond = { Lat = 37.542979<degree>; Long = -77.469092<degree> }
        let saopaulo = { Lat = -23.548943<degree>; Long = -46.638818<degree> }
     
        // distance between Richmond, Virginia USA and S o Paulo, Brazil
        // Google Earth says this should be about 7527.806 km
        let tstDist = SurfaceDistanceOnEarthKm richmond saopaulo
        let stdDist = 7527.806<km>
        let delta = abs (tstDist - stdDist) / stdDist
        // assert delta from standard < 1/4 of a percent
        delta < 0.0025