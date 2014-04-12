namespace VegaWeb

module Error =

    open VegaWeb.Grammar

    let error (dataset: 'a list) (label:string , mean:string, lo:string, hi:string) =

        let innerPadding  = Orientation({ Top = 10; Left = 30; Bottom = 30; Right = 10})
        let dataElement = { DefaultData with Name = "stats"; Values = Some(dataset) }

        let scaleY = 
            { 
                DefaultScale with
                    Name = "y"
                    Type = Ordinal
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "stats"; Field = "index"})))
            }

        let scaleX =
            {
                DefaultScale with
                    Name = "x"
                    Range = Some(RangeArray(["100" ; "400"]))
                    Domain = Some(DataRef(One({Data = "stats"; Field = "data." + hi})))
                    Nice = Some(True)
                    Zero = Some(true)
            }
        let axes = { DefaultAxis with Type = X; Scale = "x"; Ticks = Some(6) }
            
        let symbolProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    Enter = Some({
                                    DefaultMarkVisualProperty with
                                        X = Some({ DefaultMarkValueRef with Scale = Some("x"); Field = Some("data." + mean) })
                                        Y = Some({ DefaultMarkValueRef with Scale = Some("y"); Field = Some("index")})
                                        Size = Some({ DefaultMarkValueRef with Value = Some("40") })
                                        Fill = Some(Value({Value = "#000"}))
                    })
            }

        let symbolMark : Mark =
            {
                DefaultMark with 
                    Type = Symbol
                    From = Data({ Data = "stats"})
                    Properties = symbolProperties
            }

        let rectProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    Enter = Some({
                                    DefaultMarkVisualProperty with
                                        X = Some({ DefaultMarkValueRef with Scale = Some("x"); Field = Some("data." + lo) })
                                        X2 = Some({ DefaultMarkValueRef with Scale = Some("x"); Field = Some("data." + hi) })
                                        Y = Some({ DefaultMarkValueRef with Scale = Some("y"); Field=Some("index"); Offset = Some(-1.)})
                                        Height = Some({ DefaultMarkValueRef with Value = Some("1") })
                                        Fill = Some(Value({Value = "#888"}))
                    })
            }

        let rectMark : Mark =
            {
                DefaultMark with 
                    Type = Rect
                    From = Data({ Data = "stats"})
                    Properties = rectProperties
            }

        let textProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    Enter = Some({
                                    DefaultMarkVisualProperty with
                                        X = Some({ DefaultMarkValueRef with Value = Some("0") })
                                        Y = Some({ DefaultMarkValueRef with Scale = Some("y"); Field=Some("index")})
                                        Baseline = Some({ DefaultMarkValueRef with Value = Some("middle") })
                                        Fill = Some(Value({Value = "#000"}))
                                        Text = Some({ DefaultMarkValueRef with Field = Some("data."+label) })
                                        Font = Some({ DefaultMarkValueRef with Value = Some("Helvetica Neue") })
                                        FontSize = Some({ DefaultMarkValueRef with Value = Some("13") })
                    })
            }

        let textMark : Mark =
            {
                DefaultMark with 
                    Type = Text
                    From = Data({ Data = "stats"})
                    Properties = textProperties
            }
        
        let errorElement : Element<'a> =
            {
                DefaultElement with
                    Width = 400
                    Height = 100
                    Padding = Some(innerPadding)
                    Data = Some([ dataElement ])
                    Scales = Some([scaleX;  scaleY])
                    Axes = Some([axes])
                    Marks = Some([textMark; rectMark; symbolMark])
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

