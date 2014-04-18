// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System
open System.IO
open System.Data
open System.Windows.Forms

#I "bin/Debug"
#r "../packages/Newtonsoft.Json.6.0.2/lib/net45/Newtonsoft.Json.dll"
#r "../packages/Owin.1.0/lib/net40/Owin.dll"
#r "../packages/Microsoft.Owin.2.1.0/lib/net45/Microsoft.Owin.dll"
#r "../packages/Microsoft.Owin.FileSystems.2.1.0/lib/net40/Microsoft.Owin.FileSystems.dll"
#r "../packages/Microsoft.Owin.Hosting.2.1.0/lib/net45/Microsoft.Owin.Hosting.dll"
#r "../packages/Microsoft.Owin.Security.2.0.1/lib/net45/Microsoft.Owin.Security.dll"
#r "../packages/Microsoft.Owin.StaticFiles.2.1.0/lib/net45/Microsoft.Owin.StaticFiles.dll"
#r "../packages/Microsoft.Owin.Host.HttpListener.2.1.0/lib/net45/Microsoft.Owin.Host.HttpListener.dll"
#r "../packages/Microsoft.AspNet.SignalR.Core.2.0.3/lib/net45/Microsoft.AspNet.SignalR.Core.dll"
#r "../packages/FSharp.Dynamic.1.4.2.0/lib/net40/FSharp.Dynamic.dll"
#r "../packages/FSharp.Data.2.0.5/lib/net40/FSharp.Data.dll"

open Newtonsoft.Json
open FSharp.Data
open FSharp.Data.CsvExtensions

#r "Newtonsoft.Json.FSharp.dll"
open Newtonsoft.Json.FSharp

#load "../Newtonsoft.Json.FSharp/Library.fs"
open Newtonsoft.Json.FSharp

#load "../Newtonsoft.Json.FSharp/TupleConverter.fs"
open Newtonsoft.Json.FSharp

#load "../Newtonsoft.Json.FSharp/UnionConverter.fs"
open Newtonsoft.Json.FSharp

#load "../Newtonsoft.Json.FSharp/OptionConverter.fs"
open Newtonsoft.Json.FSharp

open Newtonsoft.Json

#load "Grammar.fs"
open VegaWeb.Grammar
#load "Bar.fs"
open VegaWeb.Bar
#load "JSON.fs"
open VegaWeb.JSON
#load "Error.fs"
open VegaWeb.Error
#load "Scatter.fs"
open VegaWeb.Scatter
#load "Stocks.fs"
open VegaWeb.Stocks
#load "VegaHub.fs"
open VegaHub


let datapath = __SOURCE_DIRECTORY__ + @"\stocks.csv"

type Stock = { 
    Symbol: string;
    Date: System.DateTime;
    Price: float; }

let data =
    File.ReadAllLines(datapath)
    |> fun lines -> lines.[1..]
    |> Array.map (fun line -> line.Split(','))
    |> Array.map (fun line -> 
        {   Symbol = line.[0]
            Date = DateTime.Parse(line.[1])
            Price = line.[2] |> float
            })

let stocksChart = stocks (data |> Seq.toList) ("Symbol", "Date", "Price")

stocksChart |> toJSON |> Clipboard.SetText



let requestUrl = "http://localhost:8081"
let disposable = Vega.connect(requestUrl, __SOURCE_DIRECTORY__)
System.Diagnostics.Process.Start(requestUrl + "/index.html")

stocksChart |> Vega.send

disposable.Dispose()




(*

let datapath = __SOURCE_DIRECTORY__ + @"\iris.data"

type Observation = { 
    SepalLength: float;
    XVar: float;
    YVar: float;
    PetalWidth: float;
    Type: string; }

let data =
    File.ReadAllLines(datapath)
    |> fun lines -> lines.[1..]
    |> Array.map (fun line -> line.Split(','))
    |> Array.map (fun line -> 
        {   SepalLength = line.[0] |> float;
            XVar = line.[1] |> float;
            YVar = line.[2] |> float;
            PetalWidth = line.[3] |> float;
            Type = line.[4]; })

let scatter = scatter (data |> Seq.toList) ("XVar", "YVar", "Type")

scatter |> toJSON |> Clipboard.SetText
scatter |> Vega.send

type Error = { Label : string; Mean: int; Lo : float; Hi : float}
let errorData =
    [
        { Label = "Category A"; Mean = 1; Lo = 0.; Hi = 2. }
        { Label = "Category B"; Mean = 2; Lo = 1.5; Hi = 2.5 }
        { Label = "Category C"; Mean = 3; Lo = 1.7; Hi = 4.3 }
        { Label = "Category D"; Mean = 4; Lo = 3.; Hi = 5. }
        { Label = "Category E"; Mean = 5; Lo = 4.1; Hi = 5.9 }
    ]

let errorBar = error errorData ("Label", "Mean", "Lo", "Hi")

errorBar |> toJSON |> Clipboard.SetText

type Item = { X: int; Y:int}

let dataset =
    [
        for i=1 to 10 do yield { X = i; Y = i*i}
    ]

let barElement = bar dataset ("X", "Y")

barElement |> toJSON |> Clipboard.SetText


*)

// Define your library scripting code here
//
//#I "../../bin"
//#r "Newtonsoft.Json.dll"
//#r "Microsoft.Owin.dll"
//#r "Microsoft.Owin.FileSystems.dll"
//#r "Microsoft.Owin.Hosting.dll"
//
