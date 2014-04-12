namespace VegaWeb

module Scatter =

    open VegaWeb.Grammar

    

    let scatter dataset (x:string, y: string, c: string) =
        let dataSet = { DefaultData with Values = Some(dataset); Name = "iris" }

        let scaleX = 
            { 
                DefaultScale with
                    Name = "x"
                    Nice = Some(True)
                    Range = Some(Field(Width))
                    Domain = Some(DataRef(One({Data = "iris"; Field = "data." + x})))
            }

        let scaleY =
            {
                DefaultScale with
                    Name = "y"
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "iris"; Field = "data." + y})))
                    Nice = Some(True)
            }
        let scaleC =
            {
                DefaultScale with
                    Name = "c"
                    Type = Ordinal
                    Range = Some(RangeArray(["#800"; "#080"; "#008"]))
                    Domain = Some(DataRef(One({Data = "iris"; Field = "data." + c})))
            }

        let axesX = { DefaultAxis with Type = X; Scale = "x"; OffSet = Some("5"); Ticks=Some(5); Title = Some(x) }
        let axesY = { DefaultAxis with Type = Y; Scale = "y"; OffSet = Some("5"); Ticks=Some(5); Title = Some(y) }

        let legendSymbol =
            {
                DefaultLegenfPropertyValue with
                    FillOpacity = Some({ Value = "0.5"})
                    Stroke = Some({ Value = "transparent"})
            }

        let legendProperties =
            {
                DefaultLegendProperty with
                    Symbols = Some(legendSymbol)
            }

        let legend = 
            { 
                DefaultLegend with 
                    Fill = Some("c")
                    Title= Some(c)
                    Offset = Some(0.)
                    Properties = Some(legendProperties)
            }

        let markUpdate = Some({
                                DefaultMarkVisualProperty with
                                    Size = Some({
                                                    DefaultMarkValueRef with
                                                        Value = Some("100")
                                                })
                                    Stroke = Some(Value({ Value = "transparent"}))
                            })
        let markHover = Some({
                                DefaultMarkVisualProperty with
                                    Size = Some({
                                                    DefaultMarkValueRef with
                                                        Value = Some("300")
                                                })
                                    Stroke = Some(Value({ Value = "white"}))
                            })

        let markEnter = Some({
                                DefaultMarkVisualProperty with
                                    X = Some({
                                                DefaultMarkValueRef with
                                                    Scale = Some("x"); Field = Some("data." + x)
                                        })
                                    Y = Some({
                                                DefaultMarkValueRef with
                                                    Scale = Some("y"); Field = Some("data." + y)
                                        })
                                    Fill = Some(VisualValue({
                                                            DefaultMarkValueRef with
                                                                Scale = Some("c"); Field = Some("data." + c)
                                            }))
                                    FillOpacity = Some({
                                                        DefaultMarkValueRef with
                                                            Value = Some("0.5")
                                                    })
                            })

        let properties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    Update = markUpdate
                    Hover = markHover
                    Enter = markEnter
            }

        let mark = 
            {
                DefaultMark with 
                    Type = Symbol
                    From = Data({ Data = "iris"})
                    Properties = properties
            }                        
                            
        
        let scatterElement : Element<'a> =
            {
                DefaultElement with
                    Height = 200
                    Width = 200
                    Data = Some([ dataSet ])
                    Scales = Some([scaleX;  scaleY; scaleC])
                    Legends = Some([ legend ])
                    Axes = Some([axesX; axesY])
                    Marks = Some([mark])
            }
        scatterElement
