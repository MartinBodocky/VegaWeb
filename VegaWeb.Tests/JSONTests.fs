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
    member test.``Convert orintation object to JSON string``() =
        let innerPadding  = Orientation({ Top = 10; Left = 30; Bottom = 30; Right = 10})
        let Json = innerPadding |> toJSON
        Json |> should equal <| "{\r\n  \"top\": 10,\r\n  \"left\": 30,\r\n  \"right\": 10,\r\n  \"bottom\": 30\r\n}"

    [<Test>]
    member test.``Convert simple axis data to JSON string``() =
        let axesX = { DefaultAxis with Type = X; Scale = "X" }
        let Json = axesX |> toJSON
        Json |> should equal <| "{\r\n  \"type\": \"x\",\r\n  \"scale\": \"X\"\r\n}"

    [<Test>]
    member test.``Convert scale data to JSON string``() =
        let scaleX = 
            { 
                DefaultScale with
                    Name = "x"
                    Type = Ordinal
                    Range = Some(Field(Width))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + "X"})))
            }
        let Json = scaleX |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"x\",\r\n  \"type\": \"ordinal\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.X\"\r\n  },\r\n  \"range\": \"width\"\r\n}"

    [<Test>]
    member test.``Convert second scale data to JSON string``()=
        let scaleY =
            {
                DefaultScale with
                    Name = "y"
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + "Y"})))
                    Nice = Some(True)
            }
        let Json = scaleY |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"y\",\r\n  \"type\": \"linear\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.Y\"\r\n  },\r\n  \"range\": \"height\",\r\n  \"nice\": \"true\"\r\n}"
