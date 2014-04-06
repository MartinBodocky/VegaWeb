namespace VegaWeb

open System
open Newtonsoft.Json
open Newtonsoft.Json.Converters
open Newtonsoft.Json.FSharp

//type JSON =
//
//    let converters = [| TupleConverter() ; OptionConverter() |]
//                                    
//    let toJSON v = 
//      JsonConvert.SerializeObject(v,Formatting.Indented,converters)
//    let ofJSON (v) : 't = 
//      JsonConvert.DeserializeObject<'t>(v,converters)