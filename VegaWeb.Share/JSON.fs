namespace VegaWeb

open System
open Newtonsoft.Json
open Newtonsoft.Json.Converters
open Newtonsoft.Json.FSharp
open VegaWeb.Grammar

module JSON =
    //adding custom JSON converters
    let private converters : JsonConverter[] = [| TupleConverter()
                                                  OptionConverter()
                                                  UnionConverter<AutoPadding>()
                                                  UnionConverter<Padding>()
                                                  UnionConverter<Format>()
                                                  UnionConverter<Value>()
                                                  UnionConverter<Values<'a>>()
                                                  UnionConverter<TransfromTypes>()
                                                  UnionConverter<Transform>()
                                                  UnionConverter<ScaleType>()
                                                  UnionConverter<DataRef>()
                                                  UnionConverter<Domain>()
                                                  UnionConverter<DomainValue>()
                                                  UnionConverter<RangeOption>()
                                                  UnionConverter<Range>()
                                                  UnionConverter<NiceScale>()
                                                  UnionConverter<AxisDirection>()
                                                  UnionConverter<AxisOrientation>()
                                                  UnionConverter<AxisOffset>()
                                                  UnionConverter<AxisLayer>()
                                                  UnionConverter<LegendOrient>()
                                                  UnionConverter<MarkType>()
                                                  UnionConverter<MarkFrom>()
                                                  UnionConverter<ColorValueRef>()
                                                |]
    
    //JSON serializer settings where ignore null values
    let private settings = 
        JsonSerializerSettings(
            NullValueHandling = NullValueHandling.Ignore, 
            Converters = converters)

    //public function for execute serialization
    let toJSON v = 
        JsonConvert.SerializeObject(v,Formatting.Indented, settings).ToLower() 