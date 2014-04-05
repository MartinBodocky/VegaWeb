namespace VegaWeb.Share

module Grammar =

    open System.Collections.Generic
    open Newtonsoft.Json
    open Newtonsoft.Json.FSharp
    open VegaWeb.Share.JSON

(* START Visualization*)

    type Orientation =
        { 
            Top : int
            Left : int
            Right : int
            Bottom : int
        }                 

    type AutoPadding =
        | Auto
        | Strict

    type Padding =
        | Number of int
        | Object of Orientation
        | String of AutoPadding
        member x.ToJson =
            match x with
            | Number(v) -> v.ToString()
            | Object(orient) -> (toJSON orient).ToLower()
            | String(pad) -> pad.ToString().ToLower()

    type Visualization =
        {
            Name : string
            Width : int
            Height : int
            ViewPort : (int * int) option
            Padding : Padding option
        }
        member x.ToJson =
            (toJSON x).ToLower()

    let DefaultVisualization : Visualization = { Name = "data"; Width = 500; Height=500; ViewPort = None; Padding = None}

(* END Visualization*)

(* START Data*)

    type Format = 
        | Json
        | Csv
        | Tsv
        | TopoJson
        | TreeJson

    type Value =
        | Val of int
        | Point of string * int
        | Object of System.Object

    type Values<'a> =
        | Data of List<'a>

    type TransfromTypes =
        | Array | Copy | Cross | Facet
        | Filter | Flatten | Fold
        | Formula | Slice | Sort
        | Stats | Truncate | Unique
        | Window | Zip | Force
        | Geo | GeoPath | Link | Pie
        | Stack | TreeMap | WordCloud

    type Transform =
        | NotYet //TODO: Define Data Transform

    type Data =
        {
            Name : string
            Format : Format option
            Values : (System.Object list) option
            Source : string option
            Url : string option
            Transforms : (Transform list) option
        }

    let DefaultData : Data = {Name = "table"; Format = None; Values = None; Source = None; Url = None; Transforms = None }

(* END Data*)

(* START Scale*)

    type ScaleType =
        | Linear | Ordinal
        | Time | Utc
        | Log | Pow | Sqrt
        | Quantile
        | Quantize
        | Threshold

    type DomainOne = { Data : string; Field: string}
    type DomainOneMore = {Data : string; Fields : string list}
    type DomainMulti = {Fields : DomainOne list}

    type DataRef =
        | One of DomainOne
        | OneMore of DomainOneMore
        | Multi of DomainMulti

    type Domain = 
        | Values of float list
        | DataRef of DataRef

    type DomainValue =
        | Value of float
        | Ref of DataRef

    type RangeOption =
        | Width
        | Height
        | Shapes
        | Category10
        | Category20

    type Range =
        | Field of RangeOption
        | RangeArray of float list

    type NiceScale =
        | Second | Minute | Hour
        | Day | Week | Month
        | Year | True | False

    type Scale =
        {
            Name : string
            Type : ScaleType
            Domain : Domain option
            DomainMin : DomainValue option
            DomainMax : DomainValue option
            Range : Range option
            RangeMin : float option
            RangeMax : float option
            Reverse : bool option
            Round : bool option
            //Ordinal Scale
            Points : bool option
            Padding: float option
            Sort : bool option
            // Time Scale
            Clamp : bool option
            Nice : NiceScale option
            // Quantitative scale
            Exponent : float option
            Zero : bool option
        }

    let DefaultScale : Scale = 
        { 
            Name = "x"; Type = Linear; Domain = None; 
            DomainMax = None; DomainMin = None; Range = None;
            RangeMax = None; RangeMin = None; Reverse = None;
            Round = None; Points = None; Padding = None;
            Sort = None; Clamp = None; Nice = None;
            Exponent = None; Zero = None
        }

(* END Scale*)

(* START Axis*)

    type AxisDirection =
        | X
        | Y

    type AxisOrientation =
        | Top
        | Bottom
        | Left
        | Right

    type AxisOffset =
        | Number of int
        | Object of string * string

    type AxisLayer =
        | Front
        | Back

    type AxisProperty =
        {
            Ticks : (string * string) list
            MajorTicks : (string * string) list
            Labels : (string * string) list
            Title : (string * string) list
            Axis : (string * string) list
        }

    type Axis =
        {
            Type : AxisDirection
            Scale : string
            Orient : AxisOrientation option
            Title : string option
            TitleOffset : float option
            Format : string option
            Ticks : bigint option
            Values : (System.Object list) option
            SubDivide : float option
            TickPadding : int option
            TickSize : float option
            TickSizeMajor : float option
            TickSizeMinor : float option
            TickSizeEnd : float option
            Layer : AxisLayer option
            Grid : bool option
            Properties : AxisProperty option
        }

    let DefaultAxis : Axis = 
        {
            Type = X; Scale = "x";
            Orient = None; Title = None; TitleOffset = None;
            Format = None; Ticks = None; Values = None;
            SubDivide = None; TickPadding = None; TickSize = None;
            TickSizeMajor = None; TickSizeMinor = None;
            TickSizeEnd = None; Layer = None; Grid = None;
            Properties = None
        }

(* END Axis*)

(* START Legend*)

    type LegendOrient =
        | Right
        | Left

    type LegendProperty = 
        {
            Title : (string * string) list 
            Labels : (string * string) list
            Symbols : (string * string) list
            Gradient : (string * string) list
            Legend : (string * string) list
        }

    type Legend =
        {
            Size : string option
            Shape : string option
            Fill : string option
            Stroke : string option
            Orient : LegendOrient option
            Title : string option
            Format : string option
            Values : (System.Object list) option
            Properties : LegendProperty option
        }

    let DefaultLegend : Legend =
        {
            Size = None; Shape = None; Fill = None;
            Stroke = None; Orient = None; Title = None;
            Format = None; Values = None;
            Properties = None
        }

(* END Legend*)

(* START Marks*)

    type MarkType =
        | Rect | Symbol
        | Path | Arc
        | Area | Line
        | Image | Text
        | Group
        
    type MarkFrom =
        | Data of string
        | Transforms of string list
        | NoMark

    type MarkValueRef =
        {
            Value : System.Object option
            Field : string option
            Group : string option
            Scale : string option
            Mult : float option
            Offset : float option
            Band : bool option
        }

    let DefaultMarkValueRef =
        {
            Value = None; Field = None;
            Group = None; Scale = None;
            Mult = None; Offset = None; Band = None
        }

    type EaseFunction =
        | Linear | Quad
        | Cibin | Sin
        | Exp | Circle
        | Bounce | In
        | Out | InOut
        | OutIn | CubicInOut

    type ColorRGB =
        {
            R : MarkValueRef
            G : MarkValueRef
            B : MarkValueRef
        }

    type ColorHSL =
        {
            H : MarkValueRef
            S : MarkValueRef
            L : MarkValueRef 
        }

    type ColorCIELAB =
        {
            L : MarkValueRef
            A : MarkValueRef
            B : MarkValueRef
        }

    type ColorHCL =
        {
            H : MarkValueRef
            C : MarkValueRef
            L : MarkValueRef
        }

    type ColorValueRef =
        | RGB of ColorRGB
        | HSL of ColorHSL
        | CIELAB of ColorCIELAB
        | HCL of ColorHCL
        | Color of string

    type MarkVisualProperty =
        {
            X : MarkValueRef
            X2 : MarkValueRef
            Width : MarkValueRef
            Y : MarkValueRef
            Y2 : MarkValueRef
            Height : MarkValueRef
            Opacity : MarkValueRef
            Fill : ColorValueRef option
            FillOpacity : MarkValueRef
            Stroke : ColorValueRef option
            StrokeWidth : MarkValueRef
            StrokeOpacity  : MarkValueRef
            StrokeDash : MarkValueRef
            StrokeDashOffset : MarkValueRef
            //Symbol
            Size : MarkValueRef
            Shape : MarkValueRef
            //Path
            Path : MarkValueRef
            //Arc
            InnerRadius : MarkValueRef
            OuterRadius : MarkValueRef
            StartAngle : MarkValueRef
            EndAngle : MarkValueRef
            //Area - Line
            InterPolate : MarkValueRef
            Tension : MarkValueRef
            //Image
            Url : MarkValueRef
            Align : MarkValueRef
            Baseline : MarkValueRef
            //Text 
            Text : MarkValueRef
            Dx : MarkValueRef
            Dy : MarkValueRef
            Angle : MarkValueRef
            Font : MarkValueRef
            FontSize : MarkValueRef
            FontWeight : MarkValueRef
            FontStyle : MarkValueRef
        }

    let DefaultMarkVisualProperty : MarkVisualProperty =
        {
            X = DefaultMarkValueRef; X2 = DefaultMarkValueRef; Width = DefaultMarkValueRef; Y = DefaultMarkValueRef;
            Y2 = DefaultMarkValueRef; Height = DefaultMarkValueRef; Opacity = DefaultMarkValueRef;
            Fill = None; FillOpacity = DefaultMarkValueRef; Stroke = None;
            StrokeWidth = DefaultMarkValueRef; StrokeOpacity = DefaultMarkValueRef;
            StrokeDash = DefaultMarkValueRef; StrokeDashOffset = DefaultMarkValueRef;
            Size = DefaultMarkValueRef; Shape = DefaultMarkValueRef; Path = DefaultMarkValueRef;
            InnerRadius = DefaultMarkValueRef; OuterRadius = DefaultMarkValueRef;
            StartAngle = DefaultMarkValueRef; EndAngle = DefaultMarkValueRef;
            InterPolate = DefaultMarkValueRef; Tension = DefaultMarkValueRef;
            Url = DefaultMarkValueRef; Align = DefaultMarkValueRef; Baseline = DefaultMarkValueRef;
            Text = DefaultMarkValueRef; Dx = DefaultMarkValueRef; Dy = DefaultMarkValueRef;
            Angle = DefaultMarkValueRef; Font = DefaultMarkValueRef; FontSize = DefaultMarkValueRef;
            FontWeight = DefaultMarkValueRef; FontStyle = DefaultMarkValueRef
        }

    type MarkPropertySet =
        {
            Update : MarkVisualProperty option
            Exit : MarkVisualProperty option
            Enter : MarkVisualProperty option
            Hover : MarkVisualProperty option
        }

    let DefaultMarkPropertySet : MarkPropertySet =
        {
            Update = None; Exit = None;
            Enter = None; Hover = None;
        }
    
    type Mark =
        {
            Type : MarkType
            Name : string option
            Description : string option
            From : MarkFrom
            Properties : MarkPropertySet
            Key : string option
            Delay : MarkValueRef option
            Ease : EaseFunction option
            Marks : (Mark list) option
        }

    let DefaultMark : Mark =
        {
            Type = Rect; Name = None; Description = None;
            From = NoMark; 
            Properties = DefaultMarkPropertySet; 
            Key = None; Delay = None; Ease = None; Marks = None
        }

(* END Marks*)

(* START Element *)

    type Element = 
        {
            Visualization : Visualization option
            Data : Data option
            Scales : (Scale list) option
            Axes : (Axis list) option
            Legend : Legend option
            Marks : (Mark list) option
        }

    let DefaultElement : Element =
        {
            Visualization = None; Data = None;
            Scales = None; Axes = None;
            Legend = None; Marks = None
        }

(* END Element *)
