<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" Debug="true" AutoEventWireup="true" CodeBehind="MantenimientoProductosServicios.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoProductosServicios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <style type="text/css">
        .jumbotron{
            color:#000000; 
            background:#96CEF7;
            font-size:30px;
            font-weight:bold;
            font-family:'Gill Sans';
            padding:25px;
        }

        .btn-sm{
            width:90px;
        }

        .Letranegrita {
        font-weight:bold;
        }

        .hiddenGrid
     {
         display:none;
     }
    </style>

    <div id="divBloqueConsulta" runat="server"></div>

    <div id="divBloqueMantenimiento" runat="server"></div>

    <div id="divBloqueDetalle" runat="server"></div>

    <div id="divBloqueDescartar" runat="server"></div>


</asp:Content>
