// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System
open VegaWeb
open VegaWeb.Share

#I "../../bin"
#r "Newtonsoft.Json.dll"
open Newtonsoft.Json

#r "Debug/Newtonsoft.Json.FSharp.dll"
open Newtonsoft.Json.FSharp

#load "JSON.fs"
open VegaWeb.Share.JSON
#load "Grammar.fs"
open VegaWeb.Share.Grammar
#load "Bar.fs"
open VegaWeb.Share.Bar

let visual : Visualization = 
    { 
        VegaWeb.Share.Grammar.DefaultVisualization with 
            Name = "Test"; Width=400; Height= 600; Padding = Some(Padding.Number(12))
    }



(*

rewrite classes in this style:

type Record(id : int, name : string, ?flag : bool) = 
  member x.ID = id
  member x.Name = name
  member x.Flag = flag

  ?flag -> optional

*)

// Define your library scripting code here
//
//#I "../../bin"
//#r "Newtonsoft.Json.dll"
//#r "Microsoft.Owin.dll"
//#r "Microsoft.Owin.FileSystems.dll"
//#r "Microsoft.Owin.Hosting.dll"
//
