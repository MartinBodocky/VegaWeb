module EditorController

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax

type EditorController() =
    inherit Controller()
    member this.Index () = this.View()
