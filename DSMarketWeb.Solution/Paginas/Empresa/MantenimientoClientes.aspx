<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoClientes.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.MantenimientoClientes" %>
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
        <div id="DivBloqueConsulta" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbIdTitulo" runat="server" Text="CONSULTA DE CLIENTES"></asp:Label>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbCodigoCliente" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoClienteConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                     <asp:Label ID="lbComprobanteConsulta" runat="server" Text="Seleccionar Comprobante" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarComprobanteConsulta" runat="server" CssClass="form-control" ToolTip="Seleccionar el tipo de comprobante para realizar la consulta"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                     <asp:Label ID="lbNombreConsulta" runat="server" Text="Nombre de Cliente" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreClienteConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                     <asp:Label ID="lbrncconsulta" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRNCConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
            </div>

            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" ToolTip="Exportar Reporte a Exel" CssClass="form-check-input" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarExel" runat="server" Text="Excel" ToolTip="Exportar Reporte a Excel" CssClass="form-check-input" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" ToolTip="Exportar Reporte a Word" CssClass="form-check-input" GroupName="Exportar" />
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnEliminarRegistro" runat="server" Text="Eliminar" ToolTip="Eliminar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEliminarRegistro_Click" />
                <asp:Button ID="btnExportarRegistros" runat="server" Text="Exportar" ToolTip="Exportar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnExportarRegistros_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
            </div>
        </div>
    </div>
</asp:Content>
