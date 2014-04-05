namespace VegaWeb.Share

open System
open Newtonsoft.Json
open Newtonsoft.Json.Converters
open Newtonsoft.Json.FSharp

module JSON =

    let converters : JsonConverter[] = [| TupleConverter()
                                          OptionConverter() |]
                                    
    let toJSON v = 
      JsonConvert.SerializeObject(v,Formatting.Indented,converters)
    let ofJSON (v) : 't = 
      JsonConvert.DeserializeObject<'t>(v,converters)