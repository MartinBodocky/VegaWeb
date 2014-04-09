namespace VegaWeb

module Bar =

    open VegaWeb.Grammar

    let bar (dataset: 'a list) (fx:string , fy:string) =

        let x = fx.ToLower()
        let y = fy.ToLower()

        let innerPadding  = Orientation({ Top = 10; Left = 30; Bottom = 30; Right = 10})
        let dataElement = { DefaultData with Values = Some(dataset) }
        let scaleX = 
            { 
                DefaultScale with
                    Name = fx
                    Type = Ordinal
                    Range = Some(Field(Width))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + x})))
            }

        let scaleY =
            {
                DefaultScale with
                    Name = fy
                    Range = Some(Field(Height))
                    Domain = Some(DataRef(One({Data = "table"; Field = "data." + y})))
                    Nice = Some(True)
            }
        let axesX = { DefaultAxis with Type = X; Scale = fx }
        let axesY = { DefaultAxis with Type = Y; Scale = fy }

        let markUpdate = Some({
                                DefaultMarkVisualProperty with
                                    Fill = Some(Value({ Value = "steelblue"}))
                            })
        let markHover = Some({
                                DefaultMarkVisualProperty with
                                    Fill = Some(Value({ Value = "Yellow"}))
                            })

        let markEnter = Some({
                                DefaultMarkVisualProperty with
                                    X = Some({
                                                DefaultMarkValueRef with
                                                    Scale = Some(fx); Field = Some("data." + x)
                                        })
                                    Width = Some({
                                                    DefaultMarkValueRef with
                                                        Scale = Some(fx); Band = Some(true)
                                                        Offset = Some(-1.)
                                            })
                                    Y = Some({
                                                DefaultMarkValueRef with
                                                    Scale = Some(fy); Field = Some("data." + y)
                                        })
                                    Y2 = Some({
                                                DefaultMarkValueRef with
                                                    Scale = Some(fy); Value = Some( 0 |> string)
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
                    Type = Rect
                    From = Data({ Data = "table"})
                    Properties = properties
            }

        let barElement : Element<'a> =
            {
                DefaultElement with
                    Padding = Some(innerPadding)
                    Data = Some([ dataElement ])
                    Scales = Some([scaleX;  scaleY])
                    Axes = Some([axesX; axesY])
                    Marks = Some([mark])
            }
        barElement
