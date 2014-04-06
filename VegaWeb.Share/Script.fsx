// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System
//open VegaWeb
//open VegaWeb.Share

#I "../../bin/"
#I "../../bin/Debug"
#r "Newtonsoft.Json.dll"
open Newtonsoft.Json

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

//let convertersA : Newtonsoft.Json.JsonConverter[]  = [| new Newtonsoft.Json.FSharp.TupleConverter(); new Newtonsoft.Json.FSharp.OptionConverter() |]


open Newtonsoft.Json
#load "JSON.fs"
open VegaWeb
#load "Grammar.fs"
open VegaWeb
#load "Bar.fs"
open VegaWeb

//let toJSON v = 
//    JsonConvert.SerializeObject(v,Formatting.Indented)

type Item = { X: int; Y:int}

let dataset =
    [
        for i=1 to 10 do yield { X = i; Y = i*i}
    ]

let barElement = Bar.bar dataset ("X", "Y")



//Newtonsoft.Json.FSharp.TupleConverter
//
//let convertersA : Newtonsoft.Json.JsonConverter[]  = [| new Newtonsoft.Json.FSharp.TupleConverter(); new Newtonsoft.Json.FSharp.OptionConverter() |]
//
//let convertersB : JsonConverter[]  = [| new Newtonsoft.Json.FSharp.TupleConverter()|]


let converters : JsonConverter[] = [| TupleConverter()
                                      OptionConverter()
                                      UnionConverter<Grammar.AutoPadding>()
                                      UnionConverter<Grammar.AxisDirection>()
                                      UnionConverter<Grammar.ColorValueRef>() |]
let toJSON v = 
    JsonConvert.SerializeObject(v,Formatting.Indented, converters)
let ofJSON (v) : 't = 
    JsonConvert.DeserializeObject<'t>(v,converters)

barElement |> toJSON



(*

rewrite classes in this style:

type Record(id : int, name : string, ?flag : bool) = 
  member x.ID = id
  member x.Name = name
  member x.Flag = flag

  ?flag -> optional

  let visual : Visualization = 
    { 
        VegaWeb.Grammar.DefaultVisualization with 
            Name = "Test"; Width=400; Height= 600; Padding = Some(Padding.Number(12))
    }

*)

// Define your library scripting code here
//
//#I "../../bin"
//#r "Newtonsoft.Json.dll"
//#r "Microsoft.Owin.dll"
//#r "Microsoft.Owin.FileSystems.dll"
//#r "Microsoft.Owin.Hosting.dll"
//
