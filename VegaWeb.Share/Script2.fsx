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
#r "Newtonsoft.Json.FSharp.dll"
#r "VegaWeb.Share.dll"

open Newtonsoft.Json
open FSharp.Data
open Newtonsoft.Json.FSharp
open VegaWeb.Grammar
open VegaWeb.Bar
open VegaWeb.JSON
open VegaWeb.Error
open VegaWeb.Scatter
open VegaWeb.Stocks
open VegaHub

let Y = [12;13;43;24;56]
let X = [1;2;3;4;5]
let Name = "Custom"

type Stock = { 
    symbol: string;
    time: int;
    price: int; }

let data  = [ for i in 0..4 -> {symbol= Name; time = X.[i]; price = Y.[i]}]

let stocksChart = stocks (data |> Seq.toList) ("symbol", "time", "price")

stocksChart |> toJSON |> Clipboard.SetText

let requestUrl = "http://localhost:8081"
let disposable = Vega.connect(requestUrl, __SOURCE_DIRECTORY__)
System.Diagnostics.Process.Start(requestUrl + "/index.html")

stocksChart |> Vega.send

disposable.Dispose()

(*
let datapath = __SOURCE_DIRECTORY__ + @"\stocks.csv"

type Stock = { 
    symbol: string;
    date: string;
    price: float; }

let data =
    File.ReadAllLines(datapath)
    |> fun lines -> lines.[1..]
    |> Array.map (fun line -> line.Split(','))
    |> Array.map (fun line -> 
        {   symbol = line.[0]
            date = DateTime.Parse(line.[1]).ToString("yyyy-MM-ddTHH\:mm\:ss.fffffffzzz")
            price = line.[2] |> float
            })
*)