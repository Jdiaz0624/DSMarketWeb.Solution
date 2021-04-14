<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <style type="text/css">

        .btn-sm{
            width:90px;
        }

        .Letranegrita {
        font-weight:bold;
        }

         table {
            border-collapse: collapse;
        }
        

        /*th {
            background-color: dodgerblue;
            color: white;
        }*/
    </style>



    <br /><br />

    <div id="IdBloqueConsulta" runat="server">

        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbAgregarRangoFecha" runat="server" Text="Agregar Rango de Fecha" CssClass="form-check-input" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFecha_CheckedChanged" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoProductoConsulta" runat="server" ToolTip="Seleccionar el tipo de producto para consultar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged"></asp:DropDownList>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarCtagoriaConsulta" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" ToolTip="Seleccionar la categoria para consultar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaConsulta_SelectedIndexChanged"></asp:DropDownList>
            </div>

             <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" ToolTip="Seleccionar la marca para consultar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaConsulta_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbDescripcionConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtDescripcionConsulta" runat="server" MaxLength="200" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDescripcionConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoBarraConsulta" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoBarraConsulta" runat="server" MaxLength="50" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCodigoBarraConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbReferenciaConsuklta" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtReferenciaConsulta" runat="server" MaxLength="200" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtReferenciaConsulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbCodigoProductoConsulta" runat="server" Text="Codigo de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoProductoConulta" runat="server" MaxLength="50" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCodigoProductoConulta_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4" id="DivFechaDesde" runat="server">
                <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaDesdeConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4" id="DivFechaHasta" runat="server">
                <asp:Label ID="lbFechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtFechaHastaConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

    </div>



    
    <div id="DivBloqueMantenimiento" runat="server">


    </div>

</asp:Content>
