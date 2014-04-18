namespace VegaWeb

module Scatter =

    open VegaWeb.Grammar

    let scatter dataset (x:string, y: string, c: string) =
        let dataSet = { DefaultData with values = Some(dataset); name = "iris" }

        let scaleX = 
            { 
                DefaultScale with
                    name = "x"
                    nice = Some(True)
                    range = Some(Field(Width))
                    domain = Some(DataRef(One({data = "iris"; field = "data." + x})))
            }

        let scaleY =
            {
                DefaultScale with
                    name = "y"
                    range = Some(Field(Height))
                    domain = Some(DataRef(One({data = "iris"; field = "data." + y})))
                    nice = Some(True)
            }
        let scaleC =
            {
                DefaultScale with
                    name = "c"
                    ``type`` = Ordinal
                    range = Some(RangeArray(["#800"; "#080"; "#008"]))
                    domain = Some(DataRef(One({data = "iris"; field = "data." + c})))
            }

        let axesX = { DefaultAxis with ``type`` = X; scale = "x"; offset = Some("5"); ticks=Some(5); title = Some(x) }
        let axesY = { DefaultAxis with ``type`` = Y; scale = "y"; offset = Some("5"); ticks=Some(5); title = Some(y) }

        let legendSymbol =
            {
                DefaultLegenfPropertyValue with
                    fillOpacity = Some({ value = "0.5"})
                    stroke = Some({ value = "transparent"})
            }

        let legendProperties =
            {
                DefaultLegendProperty with
                    symbols = Some(legendSymbol)
            }

        let legend = 
            { 
                DefaultLegend with 
                    fill = Some("c")
                    title= Some(c)
                    offset = Some(0.)
                    properties = Some(legendProperties)
            }

        let markUpdate = Some({
                                DefaultMarkVisualProperty with
                                    size = Some({
                                                    DefaultMarkValueRef with
                                                        value = Some("100")
                                                })
                                    stroke = Some(Value({ value = "transparent"}))
                            })
        let markHover = Some({
                                DefaultMarkVisualProperty with
                                    size = Some({
                                                    DefaultMarkValueRef with
                                                        value = Some("300")
                                                })
                                    stroke = Some(Value({ value = "white"}))
                            })

        let markEnter = Some({
                                DefaultMarkVisualProperty with
                                    x = Some({
                                                DefaultMarkValueRef with
                                                    scale = Some("x"); field = Some("data." + x)
                                        })
                                    y = Some({
                                                DefaultMarkValueRef with
                                                    scale = Some("y"); field = Some("data." + y)
                                        })
                                    fill = Some(VisualValue({
                                                            DefaultMarkValueRef with
                                                                scale = Some("c"); field = Some("data." + c)
                                            }))
                                    fillOpacity = Some({
                                                        DefaultMarkValueRef with
                                                            value = Some("0.5")
                                                    })
                            })

        let properties : MarkPropertySet =
            {
                DefaultMarkPropertySet with
                    update = markUpdate
                    hover = markHover
                    enter = markEnter
            }

        let mark = 
            {
                DefaultMark with 
                    ``type`` = Symbol

                    from = Some({ DefaultMarkFrom with data = Some("iris")})
                    properties = properties
            }                        
                            
        
        let scatterElement : Element<'a> =
            {
                DefaultElement with
                    height = 200
                    width = 200
                    data = Some([ dataSet ])
                    scales = Some([scaleX;  scaleY; scaleC])
                    legends = Some([ legend ])
                    axes = Some([axesX; axesY])
                    marks = Some([mark])
            }
        scatterElement
