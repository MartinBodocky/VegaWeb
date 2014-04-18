namespace VegaWeb.Tests

open System
open NUnit.Framework
open FsUnit

open Newtonsoft.Json
open Newtonsoft.Json.FSharp
open VegaWeb.Grammar
open VegaWeb.Bar
open VegaWeb.JSON

[<TestFixture>]
type ``Testing my json converter``() =
    
    [<Test>]
    member test.``Convert orientation object to JSON string``() =
        let innerPadding  = Orientation({ top = 10; left = 30; bottom = 30; right = 10})
        let Json = innerPadding |> toJSON
        Json |> should equal <| "{\r\n  \"top\": 10,\r\n  \"left\": 30,\r\n  \"right\": 10,\r\n  \"bottom\": 30\r\n}"

    [<Test>]
    member test.``Convert simple axis data to JSON string``() =
        let axesX = { DefaultAxis with ``type`` = X; scale = "X" }
        let Json = axesX |> toJSON
        Json |> should equal <| "{\r\n  \"type\": \"x\",\r\n  \"scale\": \"X\"\r\n}"

    [<Test>]
    member test.``Convert scale data to JSON string``() =
        let scaleX = 
            { 
                DefaultScale with
                    name = "x"
                    ``type`` = Ordinal
                    range = Some(Field(Width))
                    domain = Some(DataRef(One({data = "table"; field = "data." + "X"})))
            }
        let Json = scaleX |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"x\",\r\n  \"type\": \"ordinal\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.X\"\r\n  },\r\n  \"range\": \"width\"\r\n}"

    [<Test>]
    member test.``Convert second scale data to JSON string``()=
        let scaleY =
            {
                DefaultScale with
                    name = "y"
                    range = Some(Field(Height))
                    domain = Some(DataRef(One({data = "table"; field = "data." + "Y"})))
                    nice = Some(True)
            }
        let Json = scaleY |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"y\",\r\n  \"type\": \"linear\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.Y\"\r\n  },\r\n  \"range\": \"height\",\r\n  \"nice\": \"true\"\r\n}"
