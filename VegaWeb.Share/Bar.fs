namespace VegaWeb

module Bar =

    open VegaWeb.Grammar

    let bar (dataset: 'a list) (x:string , y:string) =

        let innerPadding  = Orientation({ top = 10; left = 30; bottom = 30; right = 10})
        let dataElement = { DefaultData with values = Some(dataset) }
        let scaleX = 
            { 
                DefaultScale with
                    name = "x"
                    ``type`` = Ordinal
                    range = Some(Field(Width))
                    domain = Some(DataRef(One({data = "table"; field = "data." + x})))
            }

        let scaleY =
            {
                DefaultScale with
                    name = "y"
                    range = Some(Field(Height))
                    domain = Some(DataRef(One({data = "table"; field = "data." + y})))
                    nice = Some(True)
            }
        let axesX = { DefaultAxis with ``type`` = X; scale = "x" }
        let axesY = { DefaultAxis with ``type`` = Y; scale = "y" }

        let markUpdate = Some({
                                DefaultMarkVisualProperty with
                                    fill = Some(Value({ value = "steelblue"}))
                            })
        let markHover = Some({
                                DefaultMarkVisualProperty with
                                    fill = Some(Value({ value = "Yellow"}))
                            })

        let markEnter = Some({
                                DefaultMarkVisualProperty with
                                    x = Some({
                                                DefaultMarkValueRef with
                                                    scale = Some("x"); field = Some("data." + x)
                                        })
                                    width = Some({
                                                    DefaultMarkValueRef with
                                                        scale = Some("x"); band = Some(true)
                                                        offset = Some(-1.)
                                            })
                                    y = Some({
                                                DefaultMarkValueRef with
                                                    scale = Some("y"); field = Some("data." + y)
                                        })
                                    y2 = Some({
                                                DefaultMarkValueRef with
                                                    scale = Some("y"); value = Some( 0 |> string)
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
                    ``type`` = Rect
                    from = Some({ DefaultMarkFrom with data = Some("table") })
                    properties = properties
            }

        let barElement : Element<'a> =
            {
                DefaultElement with
                    padding = Some(innerPadding)
                    data = Some([ dataElement ])
                    scales = Some([scaleX;  scaleY])
                    axes = Some([axesX; axesY])
                    marks = Some([mark])
            }
        barElement
