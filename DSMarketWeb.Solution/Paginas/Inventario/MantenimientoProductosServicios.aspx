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

    <div id="divBloqueConsulta" runat="server">
        <div class="container-fluid">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloConsulta" runat="server" Text="Consulta de Productos"></asp:Label>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbAgregarRangoFecha" runat="server" Text="Agregar Rango de Fecha" CssClass="form-check-input Letranegrita" ToolTip="Agregar Rango de Fecha a la Consulta" AutoPostBack="true" OnCheckedChanged="cbAgregarRangoFecha_CheckedChanged" />
                    <asp:CheckBox ID="cbMostrarTodoHistorialVenta" runat="server" Text="Mostrar todo el Inventario (Reporte)" ToolTip="Mostrar Todo el Inventario en el reprte" CssClass="form-check-input Letranegrita" />
                    <asp:CheckBox ID="cbProductosVendisodDescartados" runat="server" Text="Productos Vendididos / Descartados" ToolTip="Mostrar el Historial de Productos Vendidos y Descartados" CssClass="form-check-input Letranegrita" />
                </div>
            </div>
            <br />
            <asp:Label ID="lbExportarA" runat="server" Text="Exportar A:" CssClass="Letranegrita"></asp:Label>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" ToolTip="Exportar Información a PDF" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarExcel" runat="server" Text="Excel" ToolTip="Exportar Información a Excel" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" ToolTip="Exportar Información a Word" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarTXT" runat="server" Text="TXT" ToolTip="Exportar Información a TXT" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarCSV" runat="server" Text="CSV" ToolTip="Exportar Información a CSV" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaDesdeConsulta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaHAstaConsulta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbDescripcionConsulta" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDescripcionConsulta" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbCodigoBarraCOnsulta" runat="server" Text="Codigo de Barra" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCodigoBarra" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtCodigoBarra_TextChanged" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbRefeenciaConsulta" runat="server" Text="Referencia" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtReferenciaConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbNumeroSeguimientoConsulta" runat="server" Text="Numero de Seguimiento" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroSeguimientoConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoCOnsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoCOnsulta_SelectedIndexChanged" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarCategoriaConsulta" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoria_SelectedIndexChanged" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaConsulta_SelectedIndexChanged" ToolTip="Seleccionar Marca" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarModeloConsulta" runat="server" Text="Seleccionar Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarModelosConsulta" runat="server" ToolTip="Seleccionar Modelos" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-3">
                    <asp:Label ID="lbSeleccionarUnidadMedida" runat="server" Text="Seleccionar Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarUnidadMedida" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <!--BOTONES DE LA PANTALLA DE CONSULTA-->
            <div align="center">
                <asp:Button ID="btnConsultarRegistrosConsulta" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoConsulta" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnNuevoConsulta_Click" />
                <asp:Button ID="btnModificarConsulta" runat="server" Text="Modificar" ToolTip="Modificar Registros Seleccionados" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnModificarConsulta_Click" />
                <asp:Button ID="btnEliminarConsulta" runat="server" Text="Eliminar" ToolTip="Eliminar Registros Seleccionados" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnEliminarConsulta_Click" />
                <button type="button" id="btnSuplirConsulta" runat="server" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoMarcas">Suplir</button>
                <asp:Button ID="btnExportarConsulta" runat="server" Text="Exportar" ToolTip="Exportar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnExportarConsulta_Click" />
                <button type="button" id="btnDescartarConsulta" class="btn btn-outline-secondary btn-sm Custom" data-toggle="modal" data-target=".MantenimientoMarcas">Descartar</button>
                <hr />
                <asp:Label ID="lbCantidadRegistrosConsultaTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosConsultaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosConsultaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbCantidadInventidoTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadInventidoVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadInventidoCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbGananciaAproximadaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
                <asp:GridView ID="gvListado" runat="server" AllowPaging="true" OnPageIndexChanging="gvListado_PageIndexChanging" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <%-- <%$ Resources:Traducciones,OrdenNivel %>--%>
                    <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-outline-secondary btn-sm" SelectText="Seleccionar" ShowSelectButton="True" />
                    <asp:BoundField DataField="IdProducto" HeaderText="ID" HeaderStyle-CssClass="hiddenGrid" ItemStyle-CssClass="hiddenGrid" />
                    <asp:BoundField DataField="NumeroConector" HeaderText="Numero de Conector" HeaderStyle-CssClass="hiddenGrid" ItemStyle-CssClass="hiddenGrid" />
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" DataFormatString="{0:N0}" HtmlEncode="false" />
                    <asp:BoundField DataField="PrecioVenta" HeaderText="Precio" DataFormatString="{0:N2}" HtmlEncode="false" />
              
                </Columns  >
                 <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#7BC5FF" HorizontalAlign="Center" Font-Bold="True" ForeColor="Black" />
                <PagerStyle BackColor="#7BC5FF" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" HorizontalAlign="Center" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </div>

    <div id="divBloqueMantenimiento" runat="server">
        <div class="container-fluid">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbMantenimientoProducto" runat="server" Text="Mantenimiento de Producto" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbAccionMAntenimiento" runat="server" Text="Accion" Visible="false"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" ToolTip="Seleccionar Tipo de Producto" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoProductoMantenimiento_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarCategoriaMantenimiento" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" ToolTip="Seleccionar Categoria" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarUnidadMedidaMantenimiento" runat="server" Text="Seleccionar Unidad de Medida" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarUnidadMedidaMantenimiento" runat="server" ToolTip="Seleccionar Unidad de Medida" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarMarcaMantenimiento" runat="server" Text="Seleccionar Mantenimiento" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarMarcaMantenimiento" runat="server" ToolTip="Seleccionar Marca" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarMarcaMantenimiento_SelectedIndexChanged1" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarModeloMantenimiento" runat="server" Text="Seleccionar Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarModeloMantenimiento" runat="server" ToolTip="Seleccionar Modelo" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarModeloMantenimiento_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarColorMantenimiento" runat="server" Text="Seleccionar Color" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarColorMantenimiento" runat="server" ToolTip="Seleccionar Color" CssClass="form-control"></asp:DropDownList>
                </div>

            </div>
        </div>
    </div>

    <div id="divBloqueDetalle" runat="server"></div>

    <div id="divBloqueDescartar" runat="server"></div>


</asp:Content>
