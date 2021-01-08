<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoCompraSuplidores.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.MantenimientoCompraSuplidores" %>
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
                <asp:Label ID="lbIdCompraSuplidoresTitulo" runat="server" Text="CONSULTA DE COMPRA DE SUPLIDORES"></asp:Label>
            </div>

            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:Label ID="lbExportarA" runat="server" Text="Exportar A" CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbExprtarPDF" runat="server" Text="PDF" GroupName="Exportar" CssClass="form-check-input" />
                    <asp:RadioButton ID="rbExprtarWOrd" runat="server" Text="Word" GroupName="Exportar" CssClass="form-check-input" />
                </div>

             
            </div>

               <div class="form-row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="lbFechaDesdeConsulta" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                      <div class="form-group col-md-4">
                        <asp:Label ID="lbFechaHastaConsulta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtFechaHastaConsullta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                      <div class="form-group col-md-4">
                        <asp:Label ID="lbNumeroIdentificacionConsulta" runat="server" Text="Numero de Identificación" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtNumeroIdentificacionConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbSeleccionartipoSupliorCOnsulta" runat="server" Text="Seleccionar Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionarTipoSuplidorConsulta" runat="server" ToolTip="Seleccionar el tipo de suplidor" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbSeleccionarSuplidorConsulta" runat="server" Text="Seleccinar Suplidor" CssClass="Letranegrita"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionarSuplidorConsulta" runat="server" ToolTip="Seleccionar suplidor" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Consultar Registros" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Nuevo Registro" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar Registros" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnEliminarRegistro" runat="server" Text="Eliminar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Eliminar Registros" OnClick="btnEliminarRegistro_Click" />
                <asp:Button ID="btnExportarRegistro" runat="server" Text="Exportar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Exportar Registros" OnClick="btnExportarRegistro_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Restablecer Pantalla" OnClick="btnRestablecerPantalla_Click" />
            </div>
            <br />
            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table-responsive mT20">
                <table class="table table-hover">
                   <thead>
                        <tr>
                        <th style="width:10%" align="left" > <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbTipoSuplidoHEaderRepeater" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:20%" align="left" > <asp:Label ID="lbSuplidorHeaderRepeater" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbNumeroIdentificacionHeaderRepeater" runat="server" Text="RNC / Cedula" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbFechaComprobanteHeaderRepeater" runat="server" Text="Fecha de NCF" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbFechaHeaderRepeater" runat="server" Text="Fecha Pago" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbNCFHEaderRepeater" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbFormaPagoHeaderRepeater" runat="server" Text="Forma de Pago" CssClass="Letranegrita"></asp:Label> </th>
                        <th style="width:10%" align="left" > <asp:Label ID="lbTotalHeaderRepeater" runat="server" Text="Total" CssClass="Letranegrita"></asp:Label> </th>
                    </tr>
                   </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoCompraSuplidores" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField id="hfIdCompraSupldor" runat="server" Value='<%# Eval("IdCompraSuplidor") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionar_Click" /> </td>
                                    <td style="width:20%"> <%# Eval("TipoSuplidor") %> </td>
                                    <td style="width:10%"> <%# Eval("Suplidor") %> </td>
                                    <td style="width:10%"> <%# Eval("RNCCedula") %> </td>
                                    <td style="width:10%"> <%# Eval("FechaComprobante") %> </td>
                                    <td style="width:10%"> <%# Eval("FechaPago") %> </td>
                                    <td style="width:10%"> <%# Eval("NCF") %> </td>
                                    <td style="width:10%"> <%# Eval("FormaPago") %> </td>
                                    <td style="width:10%"> <%# string.Format("{0:n2}", Eval("TotalMontoFacturado")) %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                
            </div>

             <!--PAGINACION-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text="De " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>

             <div id="divPaginacionUnidadMedida" runat="server" align="center" >
                <div style="margin-top=20px;">
                    <table style="width:600px;">
                        <tr>
                            <td> <asp:LinkButton ID="LinkPrimeraPaginaPaginacion" runat="server" Text="Primero" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaPaginacion_Click" CssClass="btn btn-outline-success btn-sm"  ></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkPaginaAnterior" runat="server" Text="Anterior" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkPaginaAnterior_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td>
                                <asp:DataList ID="dlPaginacion" runat="server" OnCancelCommand="dlPaginacion_CancelCommand" OnItemDataBound="dlPaginacion_ItemDataBound" RepeatDirection="Horizontal" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkIndicePaginacion" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="NuevaPagina" Text='<%# Eval("TextoPagina")%>' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>

                            </td>
                            <td> <asp:LinkButton ID="LinkPaginaSiguiente" runat="server" Text="Siguiente" ToolTip="Ir la Siguiente pagina del listado" OnClick="LinkPaginaSiguiente_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkUltipaPagina" runat="server" Text="Ultmo" ToolTip="Ir a la Ultima Pagina del listado" OnClick="LinkUltipaPagina_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                           
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div id="DivBloqueDetalleRegistroSeleccionado" runat="server">
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoSuplidorDetalle" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoSuplidordetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSuplidorDetalle" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtSuplidorDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipIdentificacionDetalle" runat="server" Text="Tipo de Identificación" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoIdentificacionDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroIdentificacionDetalle" runat="server" Text="Numero de Identificación" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroIdentificacion" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoBienesserviciosDetalle" runat="server" Text="Tipo de Bienes y Servicio" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoBienesServicios" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbNCFDetalle" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNCFDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbNCFModificadoDetalle" runat="server" Text="NCF Modificado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNCFModificado" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaComprobanteDetalle" runat="server" Text="Fecha de Comprobante" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaComprobante" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaPagoDetalle" runat="server" Text="Fecha de Pago" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaPagoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoFacturadoServicioDetalle" runat="server" Text="Monto Facturado en servicios" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoFacturadoServicioDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoFacturadoBienesDetalle" runat="server" Text="Monto Facturado en Bienes" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoFacturadoBienesDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbTotalMontoFacturadoDetalle" runat="server" Text="Total Monto Facturado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTotalMontoFacturadoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISFacturado" runat="server" Text="ITBIS Facturado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISFacturadoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISRetenidoDetalle" runat="server" Text="ITBIS Retenido" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISRetenidoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBIsSujetoProporcionalidaddesDetalle" runat="server" Text="ITBIS Sujeto a Proporcionalidades (Art. 349)" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBIsSujetoProporcionalidaddesDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISLlevadoCosto" runat="server" Text="ITBIS LLevado al Costo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISLlevadoCostoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoRetencionISRDetalle" runat="server" Text="Tipo de Retencion ISR" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoRetencionISRDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISPorAdelantarDetalle" runat="server" Text="ITBIS Por Adelantar" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISPorAdelantarDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISRetenidoComprasDetalle" runat="server" Text="ITBIS Retenido en Compras" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISRetenidoComprasDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoRetencionVentasDetalle" runat="server" Text="Monto de Retencion Ventas" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoRetencionVentasDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoetencionComprasDetalle" runat="server" Text="Monto de Retencion de Compras" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoRetencionComprasDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbImpuestoSelectivoConsumoDetalle" runat="server" Text="Impuesto Selectivo al Consumo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtImpuestoSelectivoConsumoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbISRPercibodoComprasDetalle" runat="server" Text="ISRPercibidoCompras" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtISRPercibidoComprasDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbOtrosImpuestosTasaDetalle" runat="server" Text="Otros Impuestos / Tasa" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtOtrosImpuestosTasaDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoPropinaLegalDetalle" runat="server" Text="Monto Propina Legal" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoPropinaLetalDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbFormaPagoDetalle" runat="server" Text="Forma de Pago" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFormaPAgoDetalle" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>


        
    </div>
</asp:Content>
