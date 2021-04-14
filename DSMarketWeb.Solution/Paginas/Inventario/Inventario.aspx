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
        <asp:Label ID="lbIdRegistro" runat="server" Text="IdRegistro" Visible="false"></asp:Label>
        <asp:Label ID="lbNumeroConector" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
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
                <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" ToolTip="Seleccionar la marca para consultar" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbDescripcionConsulta" runat="server" Text="Descripción" CssClass="Letranegrita"></asp:Label>
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
        <br />
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbGenerarReporteA" runat="server" Text="Generar Reporte A: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbPDF" runat="server" Text="PDF" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en PDF" />
                <asp:RadioButton ID="rbExcel" runat="server" Text="Excel" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en Excel" />
                <asp:RadioButton ID="rbWord" runat="server" Text="Word" CssClass="form-check-input" GroupName="Reporte" ToolTip="Generar Reporte en Word" />
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-primary btn-sm" OnClick="btnConsultarRegistros_Click" />
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" ToolTip="Crear Nuevos Registros" CssClass="btn btn-primary btn-sm" OnClick="btnNuevo_Click" />
            <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Modificar Registro Seleccionado" CssClass="btn btn-primary btn-sm" OnClick="btnEditar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" ToolTip="Eliminar Registro Seleccionado" CssClass="btn btn-danger btn-sm" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnSuplir" runat="server" Text="Suplir" ToolTip="Suplir Inventario" CssClass="btn btn-primary btn-sm" OnClick="btnSuplir_Click" />
            <asp:Button ID="btnReporte" runat="server" Text="Reporte" ToolTip="Reporte de Inventario" CssClass="btn btn-primary btn-sm" OnClick="btnReporte_Click" />
            <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-primary btn-sm" OnClick="btnRestablecer_Click" />
            <br /><br />
        <!--ESTADISTICA DE PANTALLA-->
        <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

        <asp:Label ID="lbCapitalnvertidoTitulo" runat="server" Text="Capital Invertido ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCapitalInvertidovariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCapitalInvertidocerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

        <asp:Label ID="lbGananciaAproximadaTitulo" runat="server" Text="Ganancia Aproximada ( " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbGananciaAproximadaVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbGananciaAproximadaCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
        </div>
        <br /><br />

        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th style="width:10%" align="left"> SELECCIONAR </th>
                        <th style="width:30%" align="left"> DESCRIPCION </th>
                        <th style="width:10%" align="left"> CODIGO </th>
                        <th style="width:10%" align="left"> REFERENCIA </th>
                        <th style="width:10%" align="left"> STOCK </th>
                        <th style="width:10%" align="left"> PRECIO </th>
                        <th style="width:20%" align="left"> ESTATUS </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoProductosServicios" runat="server">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField ID="hfIdRegistro" runat="server" Value='<%# Eval("IdRegistro") %>' />
                                <asp:HiddenField ID="hfNumeroConector" runat="server" Value='<%# Eval("NumeroConector") %>' />

                                <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" CssClass="btn btn-info btn-sm" ToolTip="Seleccionar Registro" OnClick="btnSeleccionarRegistro_Click" /> </td>
                                <td style="width:30%"> <%# Eval("Descripcion") %> </td>
                                <td style="width:10%"> <%# Eval("CodigoProducto") %> </td>
                                <td style="width:10%"> <%# Eval("Referencia") %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n0}", Eval("Stock")) %> </td>
                                <td style="width:10%"> <%#string.Format("{0:n2}", Eval("PrecioVenta")) %> </td>
                                <td style="width:20%"> <%# Eval("Estatus") %> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="LinkBlbPaginaActualVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text=" de " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacionDetalle" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-light btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-light btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacion" runat="server" OnItemCommand="dtPaginacion_ItemCommand" OnItemDataBound="dtPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentral" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkSiguiente_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimo" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkUltimo_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
        <br />
    </div>



    
    <div id="DivBloqueMantenimiento" runat="server">


    </div>

</asp:Content>
