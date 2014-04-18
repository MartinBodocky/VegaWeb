namespace VegaWeb

module Stocks =

    open VegaWeb.Grammar
    
    let stocks data (symbol : string, date : string, price : string) =
        let iDataName = "stocks"
        let dataSet = { DefaultData with values = Some(data); name = iDataName }

        let scaleX = 
            { 
                DefaultScale with
                    name = "x"
                    range = Some(Field(Width))
                    domain = Some(DataRef(One({data = iDataName; field = "data." + date})))
            }

        let scaleY =
            {
                DefaultScale with
                    name = "y"
                    range = Some(Field(Height))
                    domain = Some(DataRef(One({data = iDataName; field = "data." + price})))
                    nice = Some(True)
            }
        let scaleColor =
            {
                DefaultScale with
                    name = "color"
                    ``type`` = Ordinal
                    range = Some(Field(Category10))
            }

        let axesX = { DefaultAxis with ``type`` = X; scale = "x"; tickSizeEnd = Some(0.) }
        let axesY = { DefaultAxis with ``type`` = Y; scale = "y" }

        let lineProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    enter = Some({
                                    DefaultMarkVisualProperty with
                                        x = Some({ DefaultMarkValueRef with scale = Some("x"); field = Some("data." + date)})
                                        y = Some({ DefaultMarkValueRef with scale = Some("y"); field = Some("data." + price)})
                                        stroke = Some(VisualValue({ DefaultMarkValueRef with scale = Some("y"); field = Some("data." + symbol)}))
                                        strokewidth = Some({ DefaultMarkValueRef with value = Some("2") })
                    })
            }

        let lineMark : Mark =
            {
                DefaultMark with 
                    ``type`` = Text
                    properties = lineProperties
            }

        let textProperties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    enter = Some({
                                    DefaultMarkVisualProperty with
                                        x = Some({ DefaultMarkValueRef with scale = Some("x"); field = Some("data." + date); offset = Some(2.)})
                                        y = Some({ DefaultMarkValueRef with scale = Some("y"); field = Some("data." + price)})
                                        stroke = Some(VisualValue({ DefaultMarkValueRef with scale = Some("color"); field = Some("data." + symbol)}))
                                        text = Some({ DefaultMarkValueRef with field = Some("data." + symbol) })
                                        baseline = Some({ DefaultMarkValueRef with value = Some("middle") })
                    })
            }

        let textMark : Mark =
            {
                DefaultMark with 
                    ``type`` = Text
                    from = Some({ 
                                    DefaultMarkFrom with 
                                        transforms = Some([Filter({
                                                                    DefaultFilterTransform with
                                                                        test = Some("index==data.length-1")
                                                                })])
                                    })
                    properties = textProperties
            }

        let dataFrom : MarkFrom =
            {
                data = Some(iDataName)
                transforms = Some([Facet({
                                            DefaultFacetTransform with
                                                keys = Some([ "data."+symbol ])
                                        })])
            }

        let mark : Mark =
            {
                DefaultMark with
                    ``type`` = Group
                    from = Some(dataFrom)
                    marks = Some([lineMark ; textMark])
            }

        let stocksElement : Element<'a> =
            {
                DefaultElement with
                    height = 200
                    width = 200
                    data = Some([ dataSet ])
                    scales = Some([scaleX;  scaleY; scaleColor])
                    axes = Some([axesX; axesY])
                    marks = Some([mark])
            }
        stocksElement

    

(*

{
    "width": 500,
    "height": 200,
    "data": [
      {
          "name": "stocks",
          "url": "data/stocks.csv",
          "format": { "type": "csv", "parse": { "price": "number", "date": "date" } }
      }
    ],
    "scales": [
      {
          "name": "x",
          "type": "time",
          "range": "width",
          "domain": { "data": "stocks", "field": "data.date" }
      },
      {
          "name": "y",
          "type": "linear",
          "range": "height",
          "nice": true,
          "domain": { "data": "stocks", "field": "data.price" }
      },
      {
          "name": "color", "type": "ordinal", "range": "category10"
      }
    ],
    "axes": [
      { "type": "x", "scale": "x", "tickSizeEnd": 0 },
      { "type": "y", "scale": "y" }
    ],
    "marks": [
      {
          "type": "group",
          "from": {
              "data": "stocks",
              "transform": [{ "type": "facet", "keys": ["data.symbol"] }]
          },
          "marks": [
            {
                "type": "line",
                "properties": {
                    "enter": {
                        "x": { "scale": "x", "field": "data.date" },
                        "y": { "scale": "y", "field": "data.price" },
                        "stroke": { "scale": "color", "field": "data.symbol" },
                        "strokeWidth": { "value": 2 }
                    }
                }
            },
            {
                "type": "text",
                "from": {
                    "transform": [{ "type": "filter", "test": "index==data.length-1" }]
                },
                "properties": {
                    "enter": {
                        "x": { "scale": "x", "field": "data.date", "offset": 2 },
                        "y": { "scale": "y", "field": "data.price" },
                        "fill": { "scale": "color", "field": "data.symbol" },
                        "text": { "field": "data.symbol" },
                        "baseline": { "value": "middle" }
                    }
                }
            }
          ]
      }
    ]
};

*)
