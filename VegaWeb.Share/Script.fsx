// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System
open System.IO
open System.Data
open System.Windows.Forms

#I "../../bin/"
#I "../../bin/Debug"
#r "Newtonsoft.Json.dll"
#r @"C:\GitHub\VegaWeb\packages\FSharp.Data.2.0.5\lib\net40\FSharp.Data.dll"
open Newtonsoft.Json
open FSharp.Data
open FSharp.Data.CsvExtensions

//#r "Newtonsoft.Json.FSharp.dll"
#r @"C:\GitHub\VegaWeb\Newtonsoft.Json.FSharp\bin\Debug\Newtonsoft.Json.FSharp.dll"
open Newtonsoft.Json.FSharp

#load @"C:\GitHub\VegaWeb\Newtonsoft.Json.FSharp\Library.fs"
open Newtonsoft.Json.FSharp

#load @"C:\GitHub\VegaWeb\Newtonsoft.Json.FSharp\TupleConverter.fs"
open Newtonsoft.Json.FSharp

#load @"C:\GitHub\VegaWeb\Newtonsoft.Json.FSharp\UnionConverter.fs"
open Newtonsoft.Json.FSharp

#load @"C:\GitHub\VegaWeb\Newtonsoft.Json.FSharp\OptionConverter.fs"
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


(*

*)

// Define your library scripting code here
//
//#I "../../bin"
//#r "Newtonsoft.Json.dll"
//#r "Microsoft.Owin.dll"
//#r "Microsoft.Owin.FileSystems.dll"
//#r "Microsoft.Owin.Hosting.dll"
//
