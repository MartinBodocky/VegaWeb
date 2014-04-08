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
    member test.``Convert visualization part of element to JSON string``() =
        let innerPadding  = Orientation({ Top = 10; Left = 30; Bottom = 30; Right = 10})
        let barElement : Element<'a> =
            {
                DefaultElement with
                    Padding = Some(innerPadding)
            }
        let Json = barElement |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"data\",\r\n  \"width\": 500,\r\n  \"height\": 500,\r\n  \"padding\": {\r\n    \"top\": 10,\r\n    \"left\": 30,\r\n    \"right\": 10,\r\n    \"bottom\": 30\r\n  }\r\n}"

    [<Test>]
    member test.``Convert simple axis data to JSON string``() =
        let axesX = { DefaultAxis with Type = X; Scale = "X" }
        let Json = axesX |> toJSON
        Json |> should equal <| "{\r\n  \"type\": \"x\",\r\n  \"scale\": \"x\"\r\n}"

    [<Test>]
    member test.``Convert scale data to JSON string``() =
        let scaleX = 
            { 
                DefaultScale with
                    Name = "X"
                    Type = Ordinal
                    Range = Some(Field(Width))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + "X"})))
            }
        let Json = scaleX |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"x\",\r\n  \"type\": \"ordinal\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.x\"\r\n  },\r\n  \"range\": \"width\"\r\n}"

    [<Test>]
    member test.``Convert second scale data to JSON string``()=
        let scaleY =
            {
                DefaultScale with
                    Name = "Y"
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + "Y"})))
                    Nice = Some(True)
            }
        let Json = scaleY |> toJSON
        Json |> should equal <| "{\r\n  \"name\": \"y\",\r\n  \"type\": \"linear\",\r\n  \"domain\": {\r\n    \"data\": \"table\",\r\n    \"field\": \"data.y\"\r\n  },\r\n  \"range\": \"height\",\r\n  \"nice\": \"true\"\r\n}"


(*

{
  "name": "y",
  "type": "linear",
  "domain": {
    "data": "table",
    "field": "data.y"
  },
  "range": "height",
  "nice": "true"
}

{
  "Type": "X",
  "Scale": "X"
}

{
  "Name": "data",
  "Width": 500,
  "Height": 500,
  "Padding": {
    "Top": 10,
    "Left": 30,
    "Right": 10,
    "Bottom": 30
  }
}

*)