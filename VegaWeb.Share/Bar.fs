namespace VegaWeb

module Bar =

    open VegaWeb.Grammar

    let bar (dataset: 'a list) (fx, fy) =

        let innerPadding  = Orientation({ Top = 10; Left = 30; Bottom = 30; Right = 10})
        let customVisualization =
            {
                DefaultVisualization with
                    Padding = Some(innerPadding)   
            }
        let dataElement = { DefaultData with Values = Some(dataset) }
        let scaleX = 
            { 
                DefaultScale with
                    Name = fx
                    Type = Ordinal
                    Range = Some(Field(Width))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + fx})))
            }

        let scaleY =
            {
                DefaultScale with
                    Name = fy
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + fy})))
                    Nice = Some(True)
            }
        let axesX = { DefaultAxis with Type = X; Scale = fx }
        let axesY = { DefaultAxis with Type = Y; Scale = fy }

        let properties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    Update = Some({
                                    DefaultMarkVisualProperty with
                                        Fill = Some(Value("steelblue"))
                            })
                    Hover = Some({
                                    DefaultMarkVisualProperty with
                                        Fill = Some(Value("red"))
                            })
                    Enter = Some({
                                    DefaultMarkVisualProperty with
                                        X = Some({
                                                    DefaultMarkValueRef with
                                                        Scale = Some(fx); Field = Some("data." + fx)
                                            })
                                        Width = Some({
                                                        DefaultMarkValueRef with
                                                            Scale = Some(fx); Band = Some(true)
                                                            Offset = Some(-1.)
                                                })
                                        Y = Some({
                                                    DefaultMarkValueRef with
                                                        Scale = Some(fy); Field = Some("data." + fy)
                                            })
                                        Y2 = Some({
                                                    DefaultMarkValueRef with
                                                        Scale = Some(fy); Value = Some( 0 |> string)
                                             })
                            })

            }

        let mark = 
            {
                DefaultMark with 
                    Type = Rect
                    From = Data("table")
                    Properties = properties
            }

        let barElement : Element<'a> =
            {
                DefaultElement with
                    Visualization = Some(customVisualization)
                    Data = Some(dataElement)
                    Scales = Some([scaleX;  scaleY])
                    Axes = Some([axesX; axesY])
                    Marks = Some([mark])
            }
        barElement


(*

Here I'm going to define generic model for create Bar Json representation for Bar chart
 
 {
    "width": 400,
    "height": 200,
    "padding": { "top": 10, "left": 30, "bottom": 30, "right": 10 },
    "data": [
      {
          "name": "table",
          "values": [
            { "x": 1, "y": 28 }, { "x": 2, "y": 55 },
            { "x": 3, "y": 43 }, { "x": 4, "y": 91 },
            { "x": 5, "y": 81 }, { "x": 6, "y": 53 },
            { "x": 7, "y": 19 }, { "x": 8, "y": 87 },
            { "x": 9, "y": 52 }, { "x": 10, "y": 48 },
            { "x": 11, "y": 24 }, { "x": 12, "y": 49 },
            { "x": 13, "y": 87 }, { "x": 14, "y": 66 },
            { "x": 15, "y": 17 }, { "x": 16, "y": 27 },
            { "x": 17, "y": 68 }, { "x": 18, "y": 16 },
            { "x": 19, "y": 49 }, { "x": 20, "y": 15 }
          ]
      }
    ],
    "scales": [
      {
          "name": "x",
          "type": "ordinal",
          "range": "width",
          "domain": { "data": "table", "field": "data.x" }
      },
      {
          "name": "y",
          "range": "height",
          "nice": true,
          "domain": { "data": "table", "field": "data.y" }
      }
    ],
    "axes": [
      { "type": "x", "scale": "x" },
      { "type": "y", "scale": "y" }
    ],
    "marks": [
      {
          "type": "rect",
          "from": { "data": "table" },
          "properties": {
              "enter": {
                  "x": { "scale": "x", "field": "data.x" },
                  "width": { "scale": "x", "band": true, "offset": -1 },
                  "y": { "scale": "y", "field": "data.y" },
                  "y2": { "scale": "y", "value": 0 }
              },
              "update": {
                  "fill": { "value": "steelblue" }
              },
              "hover": {
                  "fill": { "value": "red" }
              }
          }
      }
    ]
};

*)

(*

  "{
  "Visualization": {
    "Value": {
      "Name": "data",
      "Width": 500,
      "Height": 500,
      "ViewPort": null,
      "Padding": {
        "Value": {}
      }
    }
  },
  "Data": {
    "Value": {
      "Name": "table",
      "Format": null,
      "Values": {
        "Value": [
          {
            "X": 1,
            "Y": 1
          },
          {
            "X": 2,
            "Y": 4
          },
          {
            "X": 3,
            "Y": 9
          },
          {
            "X": 4,
            "Y": 16
          },
          {
            "X": 5,
            "Y": 25
          },
          {
            "X": 6,
            "Y": 36
          },
          {
            "X": 7,
            "Y": 49
          },
          {
            "X": 8,
            "Y": 64
          },
          {
            "X": 9,
            "Y": 81
          },
          {
            "X": 10,
            "Y": 100
          }
        ]
      },
      "Source": null,
      "Url": null,
      "Transforms": null
    }
  },
  "Scales": {
    "Value": [
      {
        "Name": "X",
        "Type": {},
        "Domain": {
          "Value": {}
        },
        "DomainMin": null,
        "DomainMax": null,
        "Range": {
          "Value": {}
        },
        "RangeMin": null,
        "RangeMax": null,
        "Reverse": null,
        "Round": null,
        "Points": null,
        "Padding": null,
        "Sort": null,
        "Clamp": null,
        "Nice": null,
        "Exponent": null,
        "Zero": null
      },
      {
        "Name": "Y",
        "Type": {},
        "Domain": {
          "Value": {}
        },
        "DomainMin": null,
        "DomainMax": null,
        "Range": {
          "Value": {}
        },
        "RangeMin": null,
        "RangeMax": null,
        "Reverse": null,
        "Round": null,
        "Points": null,
        "Padding": null,
        "Sort": null,
        "Clamp": null,
        "Nice": {
          "Value": {}
        },
        "Exponent": null,
        "Zero": null
      }
    ]
  },
  "Axes": {
    "Value": [
      {
        "Type": {
          "Tag": 0,
          "IsX": true,
          "IsY": false
        },
        "Scale": "X",
        "Orient": null,
        "Title": null,
        "TitleOffset": null,
        "Format": null,
        "Ticks": null,
        "Values": null,
        "SubDivide": null,
        "TickPadding": null,
        "TickSize": null,
        "TickSizeMajor": null,
        "TickSizeMinor": null,
        "TickSizeEnd": null,
        "Layer": null,
        "Grid": null,
        "Properties": null
      },
      {
        "Type": {
          "Tag": 1,
          "IsX": false,
          "IsY": true
        },
        "Scale": "Y",
        "Orient": null,
        "Title": null,
        "TitleOffset": null,
        "Format": null,
        "Ticks": null,
        "Values": null,
        "SubDivide": null,
        "TickPadding": null,
        "TickSize": null,
        "TickSizeMajor": null,
        "TickSizeMinor": null,
        "TickSizeEnd": null,
        "Layer": null,
        "Grid": null,
        "Properties": null
      }
    ]
  },
  "Legend": null,
  "Marks": {
    "Value": [
      {
        "Type": {},
        "Name": null,
        "Description": null,
        "From": {},
        "Properties": {
          "Update": {
            "Value": {
              "X": null,
              "X2": null,
              "Width": null,
              "Y": null,
              "Y2": null,
              "Height": null,
              "Opacity": null,
              "Fill": {
                "Value": {
                  "Tag": 4,
                  "Item": "steelblue",
                  "IsHCL": false,
                  "IsCIELAB": false,
                  "IsHSL": false,
                  "IsRGB": false,
                  "IsValue": true
                }
              },
              "FillOpacity": null,
              "Stroke": null,
              "StrokeWidth": null,
              "StrokeOpacity": null,
              "StrokeDash": null,
              "StrokeDashOffset": null,
              "Size": null,
              "Shape": null,
              "Path": null,
              "InnerRadius": null,
              "OuterRadius": null,
              "StartAngle": null,
              "EndAngle": null,
              "InterPolate": null,
              "Tension": null,
              "Url": null,
              "Align": null,
              "Baseline": null,
              "Text": null,
              "Dx": null,
              "Dy": null,
              "Angle": null,
              "Font": null,
              "FontSize": null,
              "FontWeight": null,
              "FontStyle": null
            }
          },
          "Exit": null,
          "Enter": {
            "Value": {
              "X": {
                "Value": {
                  "Value": null,
                  "Field": {
                    "Value": "data.X"
                  },
                  "Group": null,
                  "Scale": {
                    "Value": "X"
                  },
                  "Mult": null,
                  "Offset": null,
                  "Band": null
                }
              },
              "X2": null,
              "Width": {
                "Value": {
                  "Value": null,
                  "Field": null,
                  "Group": null,
                  "Scale": {
                    "Value": "X"
                  },
                  "Mult": null,
                  "Offset": {
                    "Value": -1.0
                  },
                  "Band": {
                    "Value": true
                  }
                }
              },
              "Y": {
                "Value": {
                  "Value": null,
                  "Field": {
                    "Value": "data.Y"
                  },
                  "Group": null,
                  "Scale": {
                    "Value": "Y"
                  },
                  "Mult": null,
                  "Offset": null,
                  "Band": null
                }
              },
              "Y2": {
                "Value": {
                  "Value": {
                    "Value": "0"
                  },
                  "Field": null,
                  "Group": null,
                  "Scale": {
                    "Value": "Y"
                  },
                  "Mult": null,
                  "Offset": null,
                  "Band": null
                }
              },
              "Height": null,
              "Opacity": null,
              "Fill": null,
              "FillOpacity": null,
              "Stroke": null,
              "StrokeWidth": null,
              "StrokeOpacity": null,
              "StrokeDash": null,
              "StrokeDashOffset": null,
              "Size": null,
              "Shape": null,
              "Path": null,
              "InnerRadius": null,
              "OuterRadius": null,
              "StartAngle": null,
              "EndAngle": null,
              "InterPolate": null,
              "Tension": null,
              "Url": null,
              "Align": null,
              "Baseline": null,
              "Text": null,
              "Dx": null,
              "Dy": null,
              "Angle": null,
              "Font": null,
              "FontSize": null,
              "FontWeight": null,
              "FontStyle": null
            }
          },
          "Hover": {
            "Value": {
              "X": null,
              "X2": null,
              "Width": null,
              "Y": null,
              "Y2": null,
              "Height": null,
              "Opacity": null,
              "Fill": {
                "Value": {
                  "Tag": 4,
                  "Item": "red",
                  "IsHCL": false,
                  "IsCIELAB": false,
                  "IsHSL": false,
                  "IsRGB": false,
                  "IsValue": true
                }
              },
              "FillOpacity": null,
              "Stroke": null,
              "StrokeWidth": null,
              "StrokeOpacity": null,
              "StrokeDash": null,
              "StrokeDashOffset": null,
              "Size": null,
              "Shape": null,
              "Path": null,
              "InnerRadius": null,
              "OuterRadius": null,
              "StartAngle": null,
              "EndAngle": null,
              "InterPolate": null,
              "Tension": null,
              "Url": null,
              "Align": null,
              "Baseline": null,
              "Text": null,
              "Dx": null,
              "Dy": null,
              "Angle": null,
              "Font": null,
              "FontSize": null,
              "FontWeight": null,
              "FontStyle": null
            }
          }
        },
        "Key": null,
        "Delay": null,
        "Ease": null,
        "Marks": null
      }
    ]
  }
}

*)