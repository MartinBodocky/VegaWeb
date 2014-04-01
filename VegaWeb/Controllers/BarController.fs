namespace VegaWeb.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax

type BarController() =
    inherit Controller()
    member this.Index () = this.View()

