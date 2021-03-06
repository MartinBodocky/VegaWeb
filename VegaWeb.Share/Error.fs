﻿namespace VegaWeb

module Error =

    open VegaWeb.Grammar

    let error (dataset: 'a list) (label:string , mean:string, lo:string, hi:string) =

        let innerPadding  = Orientation({ top = 10; left = 30; bottom = 30; right = 10})
        let dataElement = { DefaultData with name = "stats"; values = Some(dataset) }

        let scaleY = 
            { 
                DefaultOrdinalScale with
                    name = "y"
                    ``type`` = Ordinal
                    range = Some(Field(Height))
                    domain = Some(DataRef(One({data = "stats"; field = "index"})))
            }

        let scaleX =
            {
                DefaultQuantitativeScale with
                    name = "x"
                    range = Some(RangeArray(["100" ; "400"]))
                    domain = Some(DataRef(One({data = "stats"; field = "data." + hi})))
                    nice = Some(True)
                    zero = Some(true)
            }
        let axes = { DefaultAxis with ``type`` = X; scale = "x"; ticks = Some(6) }
            
        let symbolProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    enter = Some(SymbolType({
                                            DefaultSymbolMarkVisualProperty with
                                                x = Some({ DefaultMarkValueRef with scale = Some("x"); field = Some("data." + mean) })
                                                y = Some({ DefaultMarkValueRef with scale = Some("y"); field = Some("index")})
                                                size = Some({ DefaultMarkValueRef with value = Some("40") })
                                                fill = Some(Value({value = "#000"}))
                            }))
            }

        let symbolMark : Mark =
            {
                DefaultMark with 
                    ``type`` = Symbol
                    from = Some({ DefaultMarkFrom with data = Some("stats")})
                    properties = Some(symbolProperties)
            }

        let rectProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    enter = Some(RectType({
                                            DefaultRectMarkVisualProperty with
                                                x = Some({ DefaultMarkValueRef with scale = Some("x"); field = Some("data." + lo) })
                                                x2 = Some({ DefaultMarkValueRef with scale = Some("x"); field = Some("data." + hi) })
                                                y = Some({ DefaultMarkValueRef with scale = Some("y"); field = Some("index"); offset = Some(-1.)})
                                                height = Some({ DefaultMarkValueRef with value = Some("1") })
                                                fill = Some(Value({value = "#888"}))
                    }))
            }

        let rectMark : Mark =
            {
                DefaultMark with 
                    ``type`` = Rect
                    from = Some({ DefaultMarkFrom with data = Some("stats")})
                    properties = Some(rectProperties)
            }

        let textProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    enter = Some(
                                TextType({
                                            DefaultTextMarkVisualProperty with
                                                x = Some({ DefaultMarkValueRef with value = Some("0") })
                                                y = Some({ DefaultMarkValueRef with scale = Some("y"); field = Some("index")})
                                                baseline = Some({ DefaultMarkValueRef with value = Some("middle") })
                                                fill = Some(Value({value = "#000"}))
                                                text = Some({ DefaultMarkValueRef with field = Some("data."+label) })
                                                font = Some({ DefaultMarkValueRef with value = Some("Helvetica Neue") })
                                                fontsize = Some({ DefaultMarkValueRef with value = Some("13") })
                            }))
            }

        let textMark : Mark =
            {
                DefaultMark with 
                    ``type`` = Text
                    from = Some({ DefaultMarkFrom with data = Some("stats")})
                    properties = Some(textProperties)
            }
        
        let errorElement : Element<'a> =
            {
                DefaultElement with
                    width = 400
                    height = 100
                    padding = Some(innerPadding)
                    data = Some([ dataElement ])
                    scales = Some([OrdinalType(scaleY); QuantType(scaleX)])
                    axes = Some([axes])
                    marks = Some([textMark; rectMark; symbolMark])
            }
        errorElement



(*

{
    "width": 400,
    "height": 100,
    "padding": { "top": 30, "left": 30, "bottom": 30, "right": 10 },
    "data": [
      {
          "name": "stats",
          "values": [
            { "label": "Category A", "mean": 1, "lo": 0, "hi": 2 },
            { "label": "Category B", "mean": 2, "lo": 1.5, "hi": 2.5 },
            { "label": "Category C", "mean": 3, "lo": 1.7, "hi": 4.3 },
            { "label": "Category D", "mean": 4, "lo": 3, "hi": 5 },
            { "label": "Category E", "mean": 5, "lo": 4.1, "hi": 5.9 }
          ]
      }
    ],
    "scales": [
      {
          "name": "y",
          "type": "ordinal",
          "range": "height",
          "domain": { "data": "stats", "field": "index" }
      },
      {
          "name": "x",
          "range": [100, 400],
          "nice": true,
          "zero": true,
          "domain": { "data": "stats", "field": "data.hi" }
      }
    ],
    "axes": [
      { "type": "x", "scale": "x", "ticks": 6 }
    ],
    "marks": [
      {
          "type": "text",
          "from": { "data": "stats" },
          "properties": {
              "enter": {
                  "x": { "value": 0 },
                  "y": { "scale": "y", "field": "index" },
                  "baseline": { "value": "middle" },
                  "fill": { "value": "#000" },
                  "text": { "field": "data.label" },
                  "font": { "value": "Helvetica Neue" },
                  "fontSize": { "value": 13 }
              }
          }
      },
      {
          "type": "rect",
          "from": { "data": "stats" },
          "properties": {
              "enter": {
                  "x": { "scale": "x", "field": "data.lo" },
                  "x2": { "scale": "x", "field": "data.hi" },
                  "y": { "scale": "y", "field": "index", "offset": -1 },
                  "height": { "value": 1 },
                  "fill": { "value": "#888" }
              }
          }
      },
      {
          "type": "symbol",
          "from": { "data": "stats" },
          "properties": {
              "enter": {
                  "x": { "scale": "x", "field": "data.mean" },
                  "y": { "scale": "y", "field": "index" },
                  "size": { "value": 40 },
                  "fill": { "value": "#000" }
              }
          }
      }
    ]
};

*)

