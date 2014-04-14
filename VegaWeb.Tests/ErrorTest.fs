namespace VegaWeb.Tests

open System
open NUnit.Framework
open FsUnit

open Newtonsoft.Json
open Newtonsoft.Json.FSharp
open VegaWeb.Grammar
open VegaWeb.Error
open VegaWeb.JSON

type Error = { Label : string; Mean: int; Lo : float; Hi : float}

[<TestFixture>]
type ``Testing error chart``() =

    [<Test>]
    member test.``Generate error chart``() =
        let errorData =
            [
                { Label = "Category A"; Mean = 1; Lo = 0.; Hi = 2. }
                { Label = "Category B"; Mean = 2; Lo = 1.5; Hi = 2.5 }
                { Label = "Category C"; Mean = 3; Lo = 1.7; Hi = 4.3 }
                { Label = "Category D"; Mean = 4; Lo = 3.; Hi = 5. }
                { Label = "Category E"; Mean = 5; Lo = 4.1; Hi = 5.9 }
            ]

        let errorBar = error errorData ("Label", "Mean", "LO", "Hi")
        let Json = errorBar |> toJSON
        Json |> should equal "{\r\n  \"name\": \"data\",\r\n  \"width\": 400,\r\n  \"height\": 100,\r\n  \"padding\": {\r\n    \"top\": 10,\r\n    \"left\": 30,\r\n    \"right\": 10,\r\n    \"bottom\": 30\r\n  },\r\n  \"data\": [\r\n    {\r\n      \"name\": \"stats\",\r\n      \"values\": [\r\n        {\r\n          \"Label\": \"Category A\",\r\n          \"Mean\": 1,\r\n          \"Lo\": 0.0,\r\n          \"Hi\": 2.0\r\n        },\r\n        {\r\n          \"Label\": \"Category B\",\r\n          \"Mean\": 2,\r\n          \"Lo\": 1.5,\r\n          \"Hi\": 2.5\r\n        },\r\n        {\r\n          \"Label\": \"Category C\",\r\n          \"Mean\": 3,\r\n          \"Lo\": 1.7,\r\n          \"Hi\": 4.3\r\n        },\r\n        {\r\n          \"Label\": \"Category D\",\r\n          \"Mean\": 4,\r\n          \"Lo\": 3.0,\r\n          \"Hi\": 5.0\r\n        },\r\n        {\r\n          \"Label\": \"Category E\",\r\n          \"Mean\": 5,\r\n          \"Lo\": 4.1,\r\n          \"Hi\": 5.9\r\n        }\r\n      ]\r\n    }\r\n  ],\r\n  \"scales\": [\r\n    {\r\n      \"name\": \"x\",\r\n      \"type\": \"linear\",\r\n      \"domain\": {\r\n        \"data\": \"stats\",\r\n        \"field\": \"data.Hi\"\r\n      },\r\n      \"range\": [\r\n        \"100\",\r\n        \"400\"\r\n      ],\r\n      \"nice\": \"true\",\r\n      \"zero\": true\r\n    },\r\n    {\r\n      \"name\": \"y\",\r\n      \"type\": \"ordinal\",\r\n      \"domain\": {\r\n        \"data\": \"stats\",\r\n        \"field\": \"index\"\r\n      },\r\n      \"range\": \"height\"\r\n    }\r\n  ],\r\n  \"axes\": [\r\n    {\r\n      \"type\": \"x\",\r\n      \"scale\": \"x\",\r\n      \"ticks\": 6\r\n    }\r\n  ],\r\n  \"marks\": [\r\n    {\r\n      \"type\": \"text\",\r\n      \"from\": {\r\n        \"data\": \"stats\"\r\n      },\r\n      \"properties\": {\r\n        \"enter\": {\r\n          \"x\": {\r\n            \"value\": \"0\"\r\n          },\r\n          \"y\": {\r\n            \"field\": \"index\",\r\n            \"scale\": \"y\"\r\n          },\r\n          \"fill\": {\r\n            \"value\": \"#000\"\r\n          },\r\n          \"baseline\": {\r\n            \"value\": \"middle\"\r\n          },\r\n          \"text\": {\r\n            \"field\": \"data.Label\"\r\n          },\r\n          \"font\": {\r\n            \"value\": \"Helvetica Neue\"\r\n          },\r\n          \"fontsize\": {\r\n            \"value\": \"13\"\r\n          }\r\n        }\r\n      }\r\n    },\r\n    {\r\n      \"type\": \"rect\",\r\n      \"from\": {\r\n        \"data\": \"stats\"\r\n      },\r\n      \"properties\": {\r\n        \"enter\": {\r\n          \"x\": {\r\n            \"field\": \"data.LO\",\r\n            \"scale\": \"x\"\r\n          },\r\n          \"x2\": {\r\n            \"field\": \"data.Hi\",\r\n            \"scale\": \"x\"\r\n          },\r\n          \"y\": {\r\n            \"field\": \"index\",\r\n            \"scale\": \"y\",\r\n            \"offset\": -1.0\r\n          },\r\n          \"height\": {\r\n            \"value\": \"1\"\r\n          },\r\n          \"fill\": {\r\n            \"value\": \"#888\"\r\n          }\r\n        }\r\n      }\r\n    },\r\n    {\r\n      \"type\": \"symbol\",\r\n      \"from\": {\r\n        \"data\": \"stats\"\r\n      },\r\n      \"properties\": {\r\n        \"enter\": {\r\n          \"x\": {\r\n            \"field\": \"data.Mean\",\r\n            \"scale\": \"x\"\r\n          },\r\n          \"y\": {\r\n            \"field\": \"index\",\r\n            \"scale\": \"y\"\r\n          },\r\n          \"fill\": {\r\n            \"value\": \"#000\"\r\n          },\r\n          \"size\": {\r\n            \"value\": \"40\"\r\n          }\r\n        }\r\n      }\r\n    }\r\n  ]\r\n}"


