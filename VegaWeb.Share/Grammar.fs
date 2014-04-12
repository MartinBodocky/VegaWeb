namespace VegaWeb


module Grammar =

    open System.Collections.Generic
    open Newtonsoft.Json
    open Newtonsoft.Json.FSharp

(* START Visualization*)

    type Orientation =
        { 
            [<JsonProperty("top")>]
            Top : int
            [<JsonProperty("left")>]
            Left : int
            [<JsonProperty("right")>]
            Right : int
            [<JsonProperty("bottom")>]
            Bottom : int
        }                 

    type AutoPadding =
        | Auto
        | Strict

    type Padding =
        | Number of int
        | Orientation of Orientation
        | String of AutoPadding

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

    type Data<'a> =
        {
            [<JsonProperty("name")>]
            Name : string
            [<JsonProperty("format")>]
            Format : Format option
            [<JsonProperty("values")>]
            Values : 'a list option
            [<JsonProperty("source")>]
            Source : string option
            [<JsonProperty("url")>]
            Url : string option
            [<JsonProperty("transforms")>]
            Transforms : (Transform list) option
        }

    let DefaultData<'a> : Data<'a> = {Name = "table"; Format = None; Values = None; Source = None; Url = None; Transforms = None }

(* END Data*)

(* START Scale*)

    type ScaleType =
        | Linear | Ordinal
        | Time | Utc
        | Log | Pow | Sqrt
        | Quantile
        | Quantize
        | Threshold

    type DomainOne = 
        {
            [<JsonProperty("data")>] 
            Data : string
            [<JsonProperty("field")>]
            Field: string
        }
    type DomainOneMore = 
        {
            [<JsonProperty("data")>]
            Data : string
            [<JsonProperty("fields")>]
            Fields : string list
        }
    type DomainMulti = 
        {   
            [<JsonProperty("fields")>]
            Fields : DomainOne list
        }

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
        | RangeArray of string list

    type NiceScale =
        | Second | Minute | Hour
        | Day | Week | Month
        | Year | True | False

    type Scale =
        {
            [<JsonProperty("name")>]
            Name : string
            [<JsonProperty("type")>]
            Type : ScaleType
            [<JsonProperty("domain")>]
            Domain : Domain option
            [<JsonProperty("domainmix")>]
            DomainMin : DomainValue option
            [<JsonProperty("domainmax")>]
            DomainMax : DomainValue option
            [<JsonProperty("range")>]
            Range : Range option
            [<JsonProperty("rangemix")>]
            RangeMin : float option
            [<JsonProperty("rangemax")>]
            RangeMax : float option
            [<JsonProperty("reverse")>]
            Reverse : bool option
            [<JsonProperty("round")>]
            Round : bool option
            //Ordinal Scale
            [<JsonProperty("points")>]
            Points : bool option
            [<JsonProperty("padding")>]
            Padding: float option
            [<JsonProperty("sort")>]
            Sort : bool option
            // Time Scale
            [<JsonProperty("clamp")>]
            Clamp : bool option
            [<JsonProperty("nice")>]
            Nice : NiceScale option
            // Quantitative scale
            [<JsonProperty("exponent")>]
            Exponent : float option
            [<JsonProperty("zero")>]
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
            [<JsonProperty("ticks")>]
            Ticks : (string * string) list
            [<JsonProperty("majorticks")>]
            MajorTicks : (string * string) list
            [<JsonProperty("labels")>]
            Labels : (string * string) list
            [<JsonProperty("title")>]
            Title : (string * string) list
            [<JsonProperty("axis")>]
            Axis : (string * string) list
        }

    type Axis =
        {
            [<JsonProperty("type")>]
            Type : AxisDirection
            [<JsonProperty("scale")>]
            Scale : string
            [<JsonProperty("orient")>]
            Orient : AxisOrientation option
            [<JsonProperty("title")>]
            Title : string option
            [<JsonProperty("titleoffset")>]
            TitleOffset : float option
            [<JsonProperty("format")>]
            Format : string option
            [<JsonProperty("ticks")>]
            Ticks : int option
            [<JsonProperty("values")>]
            Values : (System.Object list) option
            [<JsonProperty("subdivide")>]
            SubDivide : float option
            [<JsonProperty("tickpadding")>]
            TickPadding : int option
            [<JsonProperty("ticksize")>]
            TickSize : float option
            [<JsonProperty("ticksizemajor")>]
            TickSizeMajor : float option
            [<JsonProperty("ticksizeminor")>]
            TickSizeMinor : float option
            [<JsonProperty("ticksizeend")>]
            TickSizeEnd : float option
            [<JsonProperty("layer")>]
            Layer : AxisLayer option
            [<JsonProperty("offset")>]
            OffSet : string option
            [<JsonProperty("grid")>]
            Grid : bool option
            [<JsonProperty("properties")>]
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
            Properties = None; OffSet = None
        }

(* END Axis*)

(* START Legend*)

    type LegendOrient =
        | Right
        | Left

    type LegengPropertyValue =
        {
            [<JsonProperty("value")>]
            Value : string
        }

    type LegendPropertyValue =
        {
            [<JsonProperty("fillOpacity")>]
            FillOpacity : LegengPropertyValue option
            [<JsonProperty("stroke")>]
            Stroke : LegengPropertyValue option
        }

    let DefaultLegenfPropertyValue : LegendPropertyValue =
        {
            FillOpacity = None; Stroke = None
        }

    type LegendProperty = 
        {
            [<JsonProperty("title")>]
            Title : LegendPropertyValue option
            [<JsonProperty("labels")>]
            Labels : LegendPropertyValue option
            [<JsonProperty("symbols")>]
            Symbols : LegendPropertyValue option
            [<JsonProperty("gradient")>]
            Gradient : LegendPropertyValue option
            [<JsonProperty("legend")>]
            Legend : LegendPropertyValue option
        }

    let DefaultLegendProperty : LegendProperty =
        {
            Title = None; Labels = None;
            Symbols = None; Gradient = None;
            Legend = None
        }

    type Legend =
        {
            [<JsonProperty("size")>]
            Size : string option
            [<JsonProperty("shape")>]
            Shape : string option
            [<JsonProperty("fill")>]
            Fill : string option
            [<JsonProperty("stroke")>]
            Stroke : string option
            [<JsonProperty("orient")>]
            Orient : LegendOrient option
            [<JsonProperty("title")>]
            Title : string option
            [<JsonProperty("format")>]
            Format : string option
            [<JsonProperty("values")>]
            Values : (System.Object list) option
            [<JsonProperty("offset")>]
            Offset : float option
            [<JsonProperty("properties")>]
            Properties : LegendProperty option
        }

    let DefaultLegend : Legend =
        {
            Size = None; Shape = None; Fill = None;
            Stroke = None; Orient = None; Title = None;
            Format = None; Values = None;
            Properties = None; Offset = None
        }

(* END Legend*)

(* START Marks*)

    type MarkType =
        | Rect | Symbol
        | Path | Arc
        | Area | Line
        | Image | Text
        | Group
        
    type DataFrom =
        {
            [<JsonProperty("data")>]
            Data : string
        }

    type MarkFrom =
        | Data of DataFrom
        | Transforms of string list
        | NoMark

    type MarkValueRef =
        {
            [<JsonProperty("value")>]
            Value : string option
            [<JsonProperty("field")>]
            Field : string option
            [<JsonProperty("group")>]
            Group : string option
            [<JsonProperty("scale")>]
            Scale : string option
            [<JsonProperty("mult")>]
            Mult : float option
            [<JsonProperty("offset")>]
            Offset : float option
            [<JsonProperty("band")>]
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
            [<JsonProperty("r")>]
            R : MarkValueRef
            [<JsonProperty("g")>]
            G : MarkValueRef
            [<JsonProperty("b")>]
            B : MarkValueRef
        }

    type ColorHSL =
        {
            [<JsonProperty("h")>]
            H : MarkValueRef
            [<JsonProperty("s")>]
            S : MarkValueRef
            [<JsonProperty("l")>]
            L : MarkValueRef 
        }

    type ColorCIELAB =
        {
            [<JsonProperty("l")>]
            L : MarkValueRef
            [<JsonProperty("a")>]
            A : MarkValueRef
            [<JsonProperty("b")>]
            B : MarkValueRef
        }

    type ColorHCL =
        {
            [<JsonProperty("h")>]
            H : MarkValueRef
            [<JsonProperty("c")>]
            C : MarkValueRef
            [<JsonProperty("l")>]
            L : MarkValueRef
        }

    type ColorValue =
        {
            [<JsonProperty("value")>]
            Value : string
        }

    type ColorValueRef =
        | RGB of ColorRGB
        | HSL of ColorHSL
        | CIELAB of ColorCIELAB
        | HCL of ColorHCL
        | Value of ColorValue
        | VisualValue of MarkValueRef

    type MarkVisualProperty =
        {
            [<JsonProperty("x")>]
            X : MarkValueRef option
            [<JsonProperty("x2")>]
            X2 : MarkValueRef option
            [<JsonProperty("width")>]
            Width : MarkValueRef option
            [<JsonProperty("y")>]
            Y : MarkValueRef option
            [<JsonProperty("y2")>]
            Y2 : MarkValueRef option
            [<JsonProperty("height")>]
            Height : MarkValueRef option
            [<JsonProperty("opacity")>]
            Opacity : MarkValueRef option
            [<JsonProperty("fill")>]
            Fill : ColorValueRef option
            [<JsonProperty("fillOpacity")>]
            FillOpacity : MarkValueRef option
            [<JsonProperty("stroke")>]
            Stroke : ColorValueRef option
            [<JsonProperty("strokewidth")>]
            StrokeWidth : MarkValueRef option
            [<JsonProperty("strokeopacity")>]
            StrokeOpacity  : MarkValueRef option
            [<JsonProperty("strokedash")>]
            StrokeDash : MarkValueRef option
            [<JsonProperty("strokedashoffset")>]
            StrokeDashOffset : MarkValueRef option
            //Symbol
            [<JsonProperty("size")>]
            Size : MarkValueRef option
            [<JsonProperty("shape")>]
            Shape : MarkValueRef option
            //Path
            [<JsonProperty("path")>]
            Path : MarkValueRef option
            //Arc
            [<JsonProperty("innerradius")>]
            InnerRadius : MarkValueRef option
            [<JsonProperty("outerradius")>]
            OuterRadius : MarkValueRef option
            [<JsonProperty("startangle")>]
            StartAngle : MarkValueRef option
            [<JsonProperty("endangle")>]
            EndAngle : MarkValueRef option
            //Area - Line
            [<JsonProperty("interpolate")>]
            InterPolate : MarkValueRef option
            [<JsonProperty("tension")>]
            Tension : MarkValueRef option
            //Image
            [<JsonProperty("url")>]
            Url : MarkValueRef option
            [<JsonProperty("align")>]
            Align : MarkValueRef option
            [<JsonProperty("baseline")>]
            Baseline : MarkValueRef option
            //Text 
            [<JsonProperty("text")>]
            Text : MarkValueRef option
            [<JsonProperty("dx")>]
            Dx : MarkValueRef option
            [<JsonProperty("dy")>]
            Dy : MarkValueRef option
            [<JsonProperty("angle")>]
            Angle : MarkValueRef option
            [<JsonProperty("font")>]
            Font : MarkValueRef option
            [<JsonProperty("fontsize")>]
            FontSize : MarkValueRef option
            [<JsonProperty("fontweight")>]
            FontWeight : MarkValueRef option
            [<JsonProperty("fontstyle")>]
            FontStyle : MarkValueRef option
        }

    let DefaultMarkVisualProperty : MarkVisualProperty =
        {
            X = None; X2 = None; Width = None; Y = None;
            Y2 = None; Height = None; Opacity = None;
            Fill = None; FillOpacity = None; Stroke = None;
            StrokeWidth = None; StrokeOpacity = None;
            StrokeDash = None; StrokeDashOffset = None;
            Size = None; Shape = None; Path = None;
            InnerRadius = None; OuterRadius = None;
            StartAngle = None; EndAngle = None;
            InterPolate = None; Tension = None;
            Url = None; Align = None; Baseline = None;
            Text = None; Dx = None; Dy = None;
            Angle = None; Font = None; FontSize = None;
            FontWeight = None; FontStyle = None
        }

    type MarkPropertySet =
        {
            [<JsonProperty("update")>]
            Update : MarkVisualProperty option
            [<JsonProperty("exit")>]
            Exit : MarkVisualProperty option
            [<JsonProperty("enter")>]
            Enter : MarkVisualProperty option
            [<JsonProperty("hover")>]
            Hover : MarkVisualProperty option
        }

    let DefaultMarkPropertySet : MarkPropertySet =
        {
            Update = None; Exit = None;
            Enter = None; Hover = None;
        }
    
    type Mark =
        {
            [<JsonProperty("type")>]
            Type : MarkType
            [<JsonProperty("name")>]
            Name : string option
            [<JsonProperty("description")>]
            Description : string option
            [<JsonProperty("from")>]
            From : MarkFrom
            [<JsonProperty("properties")>]
            Properties : MarkPropertySet
            [<JsonProperty("key")>]
            Key : string option
            [<JsonProperty("delay")>]
            Delay : MarkValueRef option
            [<JsonProperty("ease")>]
            Ease : EaseFunction option
            [<JsonProperty("marks")>]
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

    type Element<'a> = 
        {
            [<JsonProperty("name")>]
            Name : string
            [<JsonProperty("width")>]
            Width : int
            [<JsonProperty("height")>]
            Height : int
            [<JsonProperty("viewport")>]
            ViewPort : (int * int) option
            [<JsonProperty("padding")>]
            Padding : Padding option
            [<JsonProperty("data")>]
            Data : (Data<'a> list) option
            [<JsonProperty("scales")>]
            Scales : (Scale list) option
            [<JsonProperty("axes")>]
            Axes : (Axis list) option
            [<JsonProperty("legends")>]
            Legends : (Legend list) option
            [<JsonProperty("marks")>]
            Marks : (Mark list) option
        }

    let DefaultElement<'a> : Element<'a> =
        {
            Name = "data"; Width = 500; 
            Height = 500; ViewPort = None; 
            Padding = None; Data = None;
            Scales = None; Axes = None;
            Legends = None; Marks = None
        }

(* END Element *)
