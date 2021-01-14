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

    <script type="text/javascript">
        function CamposFechaVacios() {
            alert("Los campos fecha son necesarios para realizar esta consulta, favor de verificar.");
        }
        function CampoFechaDesdeVacio() {
            $("#<%=txtFechaDesde.ClientID%>").css("border-color", "red");
        }
        function CampoFechaHastaVacio() {
            $("#<%=txtFechaHastaConsullta.ClientID%>").css("border-color", "red");
        }
    </script>

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
                        <asp:DropDownList ID="ddlSeleccionarTipoSuplidorConsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarTipoSuplidorConsulta_SelectedIndexChanged" ToolTip="Seleccionar el tipo de suplidor" CssClass="form-control"></asp:DropDownList>
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


        <div id="DivBloqueMantenimiento" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE COMPRA A SUPLIDORES"></asp:Label>
            </div>
            <div class="form-row">
                <!--PRIMERA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbRNCCedulaMantenimiento" runat="server" Text="RNC o Cedula" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRNCCedulaMantenimiento" runat="server" CssClass="form-control" MaxLength="100" ToolTip="Digite el RNC o Cedula de la persona o negocio de donde se adquirieron los servicios."></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoIDManteniiento" runat="server" Text="Tipo de ID" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoIDMAntenimiento" CssClass="form-control" ToolTip="seleccione el tipo de RNC del suplidor." runat="server"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoBienesServiciosOmpradosMantenimiento" runat="server" Text="Tipo de Bienes y Servicios Comprados" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoBienesServiciosComprados" runat="server" ToolTip="seleccione la clase de bien oservicio adquirido." CssClass="form-control"></asp:DropDownList>
                </div>
                <!--SEGUNDA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbNCFMantenimiento" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNCFMantenimiento" runat="server" CssClass="form-control" MaxLength="100" ToolTip="coloque el número completo del comprobante fiscal que avala la compra de bienes y/o servicios, incluyendo registro de gastos menores, comprobante de compras y notas de débito o crédito (11 o 13* posiciones alfanuméricas). Cuando informe las retenciones efectuadas y la fecha de pago correspondiente a un comprobante que le hayan emitido antes de mayo 2018, se podrá colocar el NCF con estructura de 19 posiciones. "></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbNCFModificadoMantenimiento" runat="server" Text="NCF o Documento Modificado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNCFModificadoMantenimiento" runat="server" CssClass="form-control" MaxLength="100" ToolTip="En “NCF o Documento Modificado” digite el número completo de comprobante fiscal afectado por una nota de débito o crédito (11 o 13* posiciones alfanuméricas). En caso de afectar un NCF reportado antes de mayo 2018, con una Nota de Crédito o Nota de Débito, se podrá colocar un NCF con estructura de 19 posiciones. "></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaComprobanteMantenimiento" runat="server" Text="Fecha de Comprobante" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaComprobanteMantenimiento" runat="server" CssClass="form-control" TextMode="Date" ToolTip="Fecha de Comprobante"></asp:TextBox>
                </div>

                <!--TERCERA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaPagoMantenimiento" runat="server" Text="Fecha de Pago" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaPagoMantenimiento" runat="server" CssClass="form-control" TextMode="Date" ToolTip="Fecha de Pago"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoFacturadoServiciosMantenimiento" runat="server" Text="Monto Facturado en Servicios" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoFacturadoServiciosMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="coloque la proporción del monto del NCF que corresponde a servicios, sin incluir impuestos."></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoFacturadoBienesMantenimiento" runat="server" Text="Monto Facturado en Bienes" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoFacturadoBienesMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="coloque la proporción del monto en el NCF que corresponde a bienes, sin incluir impuestos."></asp:TextBox>
                </div>
                <!--CUARTA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTotalMontoFActuradoMantenimiento" runat="server" Text="Total Monto Facturado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTotalMntoFacturadoMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" Enabled="false" ToolTip="se completará automáticamente al momento de validar el archivo, con la sumatoria de los campos Monto Facturado en Bienes y Monto Facturado en Servicios."></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISFacturadoMantenimiento" runat="server" Text="ITBIS Facturado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISFacturadoMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="digite el valor del ITBIS generado en el comprobante."></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISRetenidoMantenimiento" runat="server" Text="ITBIS Retenido" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISRetenidoMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="digite el valor del ITBIS que se retuvo producto de la transacción (si aplica). Siempre que se llene este campo, debe haber completado la casilla 7 (fecha pago)."></asp:TextBox>
                </div>

                <!--QUINTA FILA-->
                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISSujetoProporcionalidadMantenimiento" runat="server" Text="ITBIS Sujeto a proporcionalidad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISSujetoProporcionalidadMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="En ITBIS sujeto a Proporcionalidad (Art. 349) registre el valor del ITBIS que estará sujeto al cálculo de la proporcionalidad, según el Art. 349 de la Ley No. 11-92. La sumatoria de esta columna será el valor que deberá distribuir en el Anexo A del Formulario de ITBIS como el ITBIS SUJETO A PROPORCIONALIDAD."></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISLlevadoCostoMantenimiento" runat="server" Text="ITBIS Llevado al Costo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISLlevadoCostoMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="En “ITBIS llevado al Costo” coloque el valor del ITBIS que es llevado directamente al Costo, es decir, que no se va a deducir como adelanto en la Declaración Jurada de ITBIS y que se utilizará como costo en la Declaración Jurada de Impuesto Sobre la Renta. En esta columna no debe colocar el ITBIS no admitido por Proporcionalidad."></asp:TextBox>
                </div>

                  <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISPorAdelantarMantenimiento" runat="server" Text="ITBIS Por Adelantar" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISPorAdelantarMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="El campo “ITBIS por Adelantar” se completará automáticamente al momento de validar el archivo. Resulta al restar el valor del campo “ITBIS Facturado” menos el valor del campo “ITBIS llevado al Costo” del mismo registro."></asp:TextBox>
                </div>

                <!--SEXTA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbITBISPercibidoComprasMantenimiento" runat="server" Text="ITBIS Percibido en Compras" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtITBISPercibidoComprasMantenimiento" runat="server" ToolTip="coloque el monto del ITBIS percibido por terceros al momento de la facturación de las operaciones." TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoRetenecionEnISRMantenimiento" runat="server" Text="Tipo Retencion en ISR" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoRetencionISRMantenimiento" runat="server" ToolTip="seleccione el código según el tipo de retención aplicada, para el Impuesto Sobre la Renta. Siempre que se llene este campo, debe haber completado la casilla 7 (fecha pago)." CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoRetencionRentaMantenimiento" runat="server" Text="Monto Retencion Renta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoRetencionRentaMantenimiento" runat="server" CssClass="form-control" TextMode="Number" step="0.01" ToolTip="digite el monto del Impuesto Sobre la Renta retenido producto de la prestación o locación de servicios. Es el resultado de multiplicar el monto del campo “Servicios” por el porcentaje de la retención según corresponda. Siempre que se llene este campo, debe haber completado la casilla 7 (fecha pago). "></asp:TextBox>
                </div>

                <!--SEPTIMA FILA-->
                <div class="form-group col-md-4">
                    <asp:Label ID="lbISRPercibidoComprasMantenimiento" runat="server" Text="ISR Percibido en Compras" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtISRPercibidoComprasMantenimiento" runat="server" ToolTip="coloque el monto del ISR percibido por terceros al momento de la facturación de las operaciones." TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbImpuestoSelectivoConsumoMantenimiento" runat="server" Text="Impuesto selectivo al consumo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtImpuestoSelectivoConsumoMantenimiento" runat="server" ToolTip="indique el monto correspondiente al Impuesto Selectivo al Consumo producto de una compra gravada con este impuesto (si aplica)." TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbOtrosImpuestosTasaMantenimiento" runat="server" Text="Otros Impuestos / Tasa" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtOtrosImpuestosTasaMantenimiento" runat="server" ToolTip="digite cualquier otro impuesto o tasa no especificado en el Formato de Envío y que formen parte del valor del comprobante fiscal. " TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
                </div>

                <!--OCTAVA FILA-->

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoPropinaLegalMantenimiento" runat="server" Text="Monto Propina Legal" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoPropinaLegalMantenimiento" runat="server" ToolTip="coloque el monto de la propina establecida por la Ley No. 54-32 (10%)." TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbFormaPagoMAntenimiento" runat="server" Text="Forma de Pago" CssClass="Letranegrita"></asp:Label>
                  <asp:DropDownList ID="ddlSeleccionarFormaPagoMantenimiento" runat="server" CssClass="form-control" ToolTip="seleccione el método de pago, según la clasificación establecida."></asp:DropDownList>
                </div>
            </div>
        </div>

        
    </div>
</asp:Content>
