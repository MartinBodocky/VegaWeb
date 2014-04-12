namespace VegaWeb.Tests

open System
open NUnit.Framework
open FsUnit

open Newtonsoft.Json
open Newtonsoft.Json.FSharp
open VegaWeb.Grammar
open VegaWeb.Bar
open VegaWeb.JSON

type Item = { X: int; Y:int}

[<TestFixture>]
type ``Testing bar chart``() =

    [<Test>]
    member test.``Generate bar chart``() =
        let dataset = [ for i=1 to 10 do yield { X = i; Y = i*i } ]
        let barElement = bar dataset ("X", "Y")
        let Json = barElement |> toJSON
        Json |> should equal "{\r\n  \"name\": \"data\",\r\n  \"width\": 500,\r\n  \"height\": 500,\r\n  \"padding\": {\r\n    \"top\": 10,\r\n    \"left\": 30,\r\n    \"right\": 10,\r\n    \"bottom\": 30\r\n  },\r\n  \"data\": [\r\n    {\r\n      \"name\": \"table\",\r\n      \"values\": [\r\n        {\r\n          \"X\": 1,\r\n          \"Y\": 1\r\n        },\r\n        {\r\n          \"X\": 2,\r\n          \"Y\": 4\r\n        },\r\n        {\r\n          \"X\": 3,\r\n          \"Y\": 9\r\n        },\r\n        {\r\n          \"X\": 4,\r\n          \"Y\": 16\r\n        },\r\n        {\r\n          \"X\": 5,\r\n          \"Y\": 25\r\n        },\r\n        {\r\n          \"X\": 6,\r\n          \"Y\": 36\r\n        },\r\n        {\r\n          \"X\": 7,\r\n          \"Y\": 49\r\n        },\r\n        {\r\n          \"X\": 8,\r\n          \"Y\": 64\r\n        },\r\n        {\r\n          \"X\": 9,\r\n          \"Y\": 81\r\n        },\r\n        {\r\n          \"X\": 10,\r\n          \"Y\": 100\r\n        }\r\n      ]\r\n    }\r\n  ],\r\n  \"scales\": [\r\n    {\r\n      \"name\": \"x\",\r\n      \"type\": \"ordinal\",\r\n      \"domain\": {\r\n        \"data\": \"table\",\r\n        \"field\": \"data.X\"\r\n      },\r\n      \"range\": \"width\"\r\n    },\r\n    {\r\n      \"name\": \"y\",\r\n      \"type\": \"linear\",\r\n      \"domain\": {\r\n        \"data\": \"table\",\r\n        \"field\": \"data.Y\"\r\n      },\r\n      \"range\": \"height\",\r\n      \"nice\": \"true\"\r\n    }\r\n  ],\r\n  \"axes\": [\r\n    {\r\n      \"type\": \"x\",\r\n      \"scale\": \"x\"\r\n    },\r\n    {\r\n      \"type\": \"y\",\r\n      \"scale\": \"y\"\r\n    }\r\n  ],\r\n  \"marks\": [\r\n    {\r\n      \"type\": \"rect\",\r\n      \"from\": {\r\n        \"data\": \"table\"\r\n      },\r\n      \"properties\": {\r\n        \"update\": {\r\n          \"fill\": {\r\n            \"value\": \"steelblue\"\r\n          }\r\n        },\r\n        \"enter\": {\r\n          \"x\": {\r\n            \"field\": \"data.X\",\r\n            \"scale\": \"x\"\r\n          },\r\n          \"width\": {\r\n            \"scale\": \"x\",\r\n            \"offset\": -1.0,\r\n            \"band\": true\r\n          },\r\n          \"y\": {\r\n            \"field\": \"data.Y\",\r\n            \"scale\": \"y\"\r\n          },\r\n          \"y2\": {\r\n            \"value\": \"0\",\r\n            \"scale\": \"y\"\r\n          }\r\n        },\r\n        \"hover\": {\r\n          \"fill\": {\r\n            \"value\": \"Yellow\"\r\n          }\r\n        }\r\n      }\r\n    }\r\n  ]\r\n}"
        