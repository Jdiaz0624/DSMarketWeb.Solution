<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Citas.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.Citas" %>
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
    </style>

    <div class="container-fluid">
        <div id="DivBloqueCOnsulta" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloConsultaCitas" runat="server" Text="CONSULTA DE CITAS" CssClass="Letranegrita"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
