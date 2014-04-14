namespace VegaWeb


module Grammar =

    open System.Collections.Generic
    open Newtonsoft.Json
    open Newtonsoft.Json.FSharp

(* START Visualization*)

    type Orientation =
        { 
            top : int
            left : int
            right : int
            bottom : int
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

    type ArrayTransform =
        {
            ``type`` : string 
            fields : (string list) option
        }

    let DefaultArrayTransform = { ``type`` = "array"; fields = None }

    type CopyTransform =
        {
            ``type`` : string
            from : string option
            fields : (string list) option
            ``as`` : (string list) option
        }

    let DefaultCopyTransform = { ``type`` = "copy"; fields = None; from = None; ``as`` = None}

    type CrossTransform =
        {
            ``type`` : string
            ``with`` : string option
            diagonal : bool option
        }
    let DefaultCrossTransform = { ``type`` = "cross"; ``with`` = None; diagonal = None}

    type FacetTransform =
        {
            ``type`` : string
            keys : (string list) option
            sort : (string list) option
        }
    let DefaultFacetTransform = { ``type`` = "facet"; keys = None; sort = None}

    type FilterTransform =
        {
            ``type`` : string
            test : string option
        }
    let DefaultFilterTransform = { ``type`` = "filter"; test = None}

    type FlattenTransform =
        {
            ``type`` : string
        }
    let DefaultFlattenTransform = { ``type`` = "flatten"}

    type FoldTransform = 
        {
            ``type`` : string
            fields : (string list) option
        }
    let DefaultFoldTransform = { ``type`` = "fold"; fields = None}

    type FormulaTransform =
        {
            ``type`` : string
            field : string option
            expr : string option
        }
    let DefaultFormulaTransform = { ``type`` = "formula"; field = None; expr = None}
    
    type SliceTransform =
        {
            ``type`` : string
            by : (int list) option
            field : string option
        }
    let DefaultSliceTransform = { ``type`` = "slice"; by = None; field = None}

    type SortTransform  =
        {
            ``type`` : string
            by : (string list) option
        }
    let DefaultSortTransform = { ``type`` = "sort"; by = None}

    type StatsTransform =
        {
            ``type`` : string
            value : string option
            median : bool option
            assign : bool option
        }
    let DefaultStatsTransform = { ``type`` = "stats"; value = None; median = None; assign = None}

    type TruncateTransform =
        {
            ``type`` : string
            value : string option
            output : string option
            limit : int option
            position : string option
            ellipsis : string option
            wordbreak : bool option
        }
    let DefaultTruncateTransform = 
        {
            ``type`` = "truncate"; value = None; output = None;
            limit = None; position = None; ellipsis = None; wordbreak = None
        }

    type UniqueTransform =
        {
            ``type`` : string
            field : string option
            ``as`` : string option
        }
    let DefaultUniqueTransform = { ``type`` = "unique"; field = None; ``as`` = None}

    type WindowTransform =
        {
            ``type`` : string
            size : int option
            step : int option
        }
    let DefaultWindowTransform = { ``type`` = "window"; size = None; step = None}

    type ZipTransform =
        {
            ``type`` : string
            ``with`` : string option
            ``as`` : string option
            key : string option
            withKey : string option
            ``default`` : string option
        }
    let DefaultZipTransform = { ``type`` = "zip"; ``with`` = None; ``as`` = None; key = None; withKey = None; ``default`` = None }

    type ForceTransform =
        {
            ``type`` : string
            links : string option
            size : (int list) option
            iterations : int option
            charge : string option
            linkDistance : string option
            linkStrength : string option
            friction : int option
            theta : float option
            gravity : int option
            alpha : float option
        }
    let DefaultForceTransform : ForceTransform = 
        { 
            ``type`` = "force"; links = None; size = None;
            iterations = None; charge = None; linkDistance = None
            linkStrength = None; friction = None; theta = None
            gravity = None; alpha = None
        }

    type GeoTransform =
        {
            ``type`` : string
            projection : string option
            lon : string option
            lan : string option
            center : (int list) option
            translate : (int list) option
            scale : int option
            rotate : int option
            precision : int option
            clipAngle : int option
        }

    type GeoPathTransform =
        {
            ``type`` : string
            value : string option
            projection : string option
            lon : string option
            lan : string option
            center : (int list) option
            translate : (int list) option
            scale : int option
            rotate : int option
            precision : int option
            clipAngle : int option
        }

    type LinkTransform =
        {
            ``type`` : string
            source : string option
            target : string option
            shape : string option
            tension : float option
        }

    type PieTransform =
        {
            ``type`` : string
            sort : bool option
            value : string option
        }

    type StackTransform =
        {
            ``type`` : string
            point : string option
            height : string option
            offset : string option
            order : string option
        }

    type TreemapTransform =
        {
            ``type`` : string
            padding : (int list) option
            ratio : int option
            round : bool option
            size : (int list) option
            sticky : bool option
            value : string option
        }

    type WordcloudTransform = 
        {
            ``type`` : string
            font : string option
            fontSize : string option
            fontStyle : string option
            fontWeight : string option
            padding : (int list) option
            rotate : string option
            size : (int list) option
            text : string option
        }

    type Transform =
        | Array of ArrayTransform
        | Copy of CopyTransform
        | Cross of CrossTransform
        | Facet of FacetTransform
        | Filter of FilterTransform
        | Flatten of FlattenTransform
        | Fold of FoldTransform
        | Formula of FormulaTransform
        | Slice of SliceTransform
        | Sort of SortTransform
        | Stats of StatsTransform
        | Truncate of TruncateTransform
        | Unique of UniqueTransform
        | Window of WindowTransform
        | Zip of ZipTransform
        | Force of ForceTransform
        | Geo of GeoTransform
        | GeoPath of GeoPathTransform
        | Link of LinkTransform
        | Pie of PieTransform
        | Stack of StackTransform
        | TreeMap of TreemapTransform
        | WordCloud of WordcloudTransform

    type Data<'a> =
        {
            name : string
            format : Format option
            values : 'a list option
            source : string option
            url : string option
            transforms : (Transform list) option
        }

    let DefaultData<'a> : Data<'a> = {name = "table"; format = None; values = None; source = None; url = None; transforms = None }

(* END Data*)

(* START Scale*)

    type ScaleType =
        | Linear 
        | Ordinal
        | Time 
        | Utc
        | Log 
        | Pow 
        | Sqrt
        | Quantile
        | Quantize
        | Threshold

    type DomainOne = 
        {
            data : string
            field: string
        }
    type DomainOneMore = 
        {
            data : string
            fields : string list
        }
    type DomainMulti = 
        {   
            fields : DomainOne list
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
            name : string
            ``type`` : ScaleType
            domain : Domain option
            domainMin : DomainValue option
            domainMax : DomainValue option
            range : Range option
            rangeMin : float option
            rangeMax : float option
            reverse : bool option
            round : bool option
            //Ordinal Scale
            points : bool option
            padding: float option
            sort : bool option
            // Time Scale
            clamp : bool option
            nice : NiceScale option
            // Quantitative scale
            exponent : float option
            zero : bool option
        }

    let DefaultScale : Scale = 
        { 
            name = "x"; ``type`` = Linear; domain = None; 
            domainMax = None; domainMin = None; range = None;
            rangeMax = None; rangeMin = None; reverse = None;
            round = None; points = None; padding = None;
            sort = None; clamp = None; nice = None;
            exponent = None; zero = None
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
            ticks : (string * string) list
            majorticks : (string * string) list
            labels : (string * string) list
            title : (string * string) list
            axis : (string * string) list
        }

    type Axis =
        {
            ``type`` : AxisDirection
            scale : string
            orient : AxisOrientation option
            title : string option
            titleoffset : float option
            format : string option
            ticks : int option
            values : (System.Object list) option
            subdivide : float option
            tickpadding : int option
            ticksize : float option
            ticksizemajor : float option
            ticksizeminor : float option
            ticksizeend : float option
            layer : AxisLayer option
            offset : string option
            grid : bool option
            properties : AxisProperty option
        }

    let DefaultAxis : Axis = 
        {
            ``type`` = X; scale = "x";
            orient = None; title = None; titleoffset = None;
            format = None; ticks = None; values = None;
            subdivide = None; tickpadding = None; ticksize = None;
            ticksizemajor = None; ticksizeminor = None;
            ticksizeend = None; layer = None; grid = None;
            properties = None; offset = None
        }

(* END Axis*)

(* START Legend*)

    type LegendOrient =
        | Right
        | Left

    type LegengPropertyValue =
        {
            value : string
        }

    type LegendPropertyValue =
        {
            fillOpacity : LegengPropertyValue option
            stroke : LegengPropertyValue option
        }

    let DefaultLegenfPropertyValue : LegendPropertyValue =
        {
            fillOpacity = None; stroke = None
        }

    type LegendProperty = 
        {
            title : LegendPropertyValue option
            labels : LegendPropertyValue option
            symbols : LegendPropertyValue option
            gradient : LegendPropertyValue option
            legend : LegendPropertyValue option
        }

    let DefaultLegendProperty : LegendProperty =
        {
            title = None; labels = None;
            symbols = None; gradient = None;
            legend = None
        }

    type Legend =
        {
            size : string option
            shape : string option
            fill : string option
            stroke : string option
            orient : LegendOrient option
            title : string option
            format : string option
            values : (System.Object list) option
            offset : float option
            properties : LegendProperty option
        }

    let DefaultLegend : Legend =
        {
            size = None; shape = None; fill = None;
            stroke = None; orient = None; title = None;
            format = None; values = None;
            properties = None; offset = None
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
            data : string
        }

    type MarkFrom =
        | Data of DataFrom
        | Transforms of Transform list
        | NoMark

    type MarkValueRef =
        {
            value : string option
            field : string option
            group : string option
            scale : string option
            mult : float option
            offset : float option
            band : bool option
        }

    let DefaultMarkValueRef =
        {
            value = None; field = None;
            group = None; scale = None;
            mult = None; offset = None; band = None
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
            r : MarkValueRef
            g : MarkValueRef
            b : MarkValueRef
        }

    type ColorHSL =
        {
            h : MarkValueRef
            s : MarkValueRef
            l : MarkValueRef 
        }

    type ColorCIELAB =
        {
            l : MarkValueRef
            a : MarkValueRef
            b : MarkValueRef
        }

    type ColorHCL =
        {
            h : MarkValueRef
            c : MarkValueRef
            l : MarkValueRef
        }

    type ColorValue =
        {
            value : string
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
            x : MarkValueRef option
            x2 : MarkValueRef option
            width : MarkValueRef option
            y : MarkValueRef option
            y2 : MarkValueRef option
            height : MarkValueRef option
            opacity : MarkValueRef option
            fill : ColorValueRef option
            fillOpacity : MarkValueRef option
            stroke : ColorValueRef option
            strokewidth : MarkValueRef option
            strokeopacity  : MarkValueRef option
            strokedash : MarkValueRef option
            strokedashoffset : MarkValueRef option
            //Symbol
            size : MarkValueRef option
            shape : MarkValueRef option
            //Path
            path : MarkValueRef option
            //Arc
            innerradius : MarkValueRef option
            outerradius : MarkValueRef option
            startangle : MarkValueRef option
            endangle : MarkValueRef option
            //Area - Line
            interpolate : MarkValueRef option
            tension : MarkValueRef option
            //Image
            url : MarkValueRef option
            align : MarkValueRef option
            baseline : MarkValueRef option
            //Text 
            text : MarkValueRef option
            dx : MarkValueRef option
            dy : MarkValueRef option
            angle : MarkValueRef option
            font : MarkValueRef option
            fontsize : MarkValueRef option
            fontweight : MarkValueRef option
            fontstyle : MarkValueRef option
        }

    let DefaultMarkVisualProperty : MarkVisualProperty =
        {
            x = None; x2 = None; width = None; y = None;
            y2 = None; height = None; opacity = None;
            fill = None; fillOpacity = None; stroke = None;
            strokewidth = None; strokeopacity = None;
            strokedash = None; strokedashoffset = None;
            size = None; shape = None; path = None;
            innerradius = None; outerradius = None;
            startangle = None; endangle = None;
            interpolate = None; tension = None;
            url = None; align = None; baseline = None;
            text = None; dx = None; dy = None;
            angle = None; font = None; fontsize = None;
            fontweight = None; fontstyle = None
        }

    type MarkPropertySet =
        {
            update : MarkVisualProperty option
            exit : MarkVisualProperty option
            enter : MarkVisualProperty option
            hover : MarkVisualProperty option
        }

    let DefaultMarkPropertySet : MarkPropertySet =
        {
            update = None; exit = None;
            enter = None; hover = None;
        }
    
    type Mark =
        {
            ``type`` : MarkType
            name : string option
            description : string option
            from : MarkFrom
            properties : MarkPropertySet
            key : string option
            delay : MarkValueRef option
            ease : EaseFunction option
            marks : (Mark list) option
        }

    let DefaultMark : Mark =
        {
            ``type`` = Rect; name = None; description = None;
            from = NoMark; 
            properties = DefaultMarkPropertySet; 
            key = None; delay = None; ease = None; marks = None
        }

(* END Marks*)

(* START Element *)

    type Element<'a> = 
        {
            name : string
            width : int
            height : int
            viewport : (int * int) option
            padding : Padding option
            data : (Data<'a> list) option
            scales : (Scale list) option
            axes : (Axis list) option
            legends : (Legend list) option
            marks : (Mark list) option
        }

    let DefaultElement<'a> : Element<'a> =
        {
            name = "data"; width = 500; 
            height = 500; viewport = None; 
            padding = None; data = None;
            scales = None; axes = None;
            legends = None; marks = None
        }

(* END Element *)
