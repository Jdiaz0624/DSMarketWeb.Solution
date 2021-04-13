<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master"  AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.Facturacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .jumbotron{
            color:#000000; 
            background:#1E90FF;
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
        table {
            border-collapse: collapse;
        }
        

        th {
            background-color: dodgerblue;
            color: white;
        }

        .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }

        .BotonEspecoal2 {
           width:15%;
             font-weight:bold;
          }

         .TamanioImagen {
         width:20%;
         height:20%;
         }
    </style>


</asp:Content>
