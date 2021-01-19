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

            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbAgregarRangoFechaConsulta" runat="server" Text="Agregar Rango de Fecha" CssClass="form-check-input Letranegrita" ToolTip="Agregar Rango de fecha a la consulta" /><br />
                    <asp:RadioButton ID="rbTodos" runat="server" Text="Todos" ToolTip="Mostrar todos los registros" CssClass="form-check-input Letranegrita" GroupName="Estatus" />
                    <asp:RadioButton ID="rbRegistrosActivos" runat="server" Text="Activos" ToolTip="Mostrar todos los registros activos" CssClass="form-check-input Letranegrita" GroupName="Estatus" />
                    <asp:RadioButton ID="rbRegistrosInactivos" runat="server" Text="Inactivos" ToolTip="Mostrar todos los registros inactivos" CssClass="form-check-input Letranegrita" GroupName="Estatus" /><br />
                    <asp:Label ID="lbExportarA" runat="server" Text="Exportar A: " CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" ToolTip="Exportar Registros a PDF" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" ToolTip="Exportar Registros a Word" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                    <asp:RadioButton ID="rbEcportarExcel" runat="server" Text="Excel" ToolTip="Exportar Registros a Excel" CssClass="form-check-input Letranegrita" GroupName="Exportar" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroCitaConsulta" runat="server" Text="Numero de Cita" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroCitaConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreClienteConsulta" runat="server" Text="Nombre de Cliente" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreClienteConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroIdentificacionConsulta" runat="server" Text="Numero de Identificación" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroIdentificacionConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarDepartamentoConsulta" runat="server" Text="Seleccionar Departamento" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarDepartamentoConsulta" runat="server" ToolTip="Seleccionar Departamento" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarEmpleadoConsulta" runat="server" Text="Seleccionar Tecnico" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarEmpleadoConsulta" runat="server" ToolTip="Seleccionar Tecnico" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-4">
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaDesde" runat="server" Text="Fecha Desde" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbFechaHasta" runat="server" Text="Fecha Hasta" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                 <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                 <asp:Button ID="btnEditarRegistro" runat="server" Text="Modificar" ToolTip="Editar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEditarRegistro_Click" />
                 <asp:Button ID="btnEliminarRegistro" runat="server" Text="Eliminar" ToolTip="Eliminar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEliminarRegistro_Click" />
                 <asp:Button ID="btnReporte" runat="server" Text="Reporte" ToolTip="Reporte de Citas" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnReporte_Click" />
                 <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
                <br /><br />
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>

            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarregistroHeader" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbNumeroCitaHeader" runat="server" Text="No. Cita" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:25%" align="left"> <asp:Label ID="lbClienteHeader" runat="server" Text="Cliente" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbFechaHeader" runat="server" Text="Fecha" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbHoraHeader" runat="server" Text="Hora" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusHeader" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:25%" align="left"> <asp:Label ID="lbTecnicoHeader" runat="server" Text="Tecnico" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoCitasEncabezado" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdCitaSeleccionada" runat="server" Value=<%# Eval("IdCitas") %> />
                                    <asp:HiddenField ID="hfNumeroConector" runat="server" Value=<%# Eval("NumeroConectorCita") %> />

                                    <td style="width:10%"> <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registros" /> </td>
                                    <td style="width:10%"> <%# Eval("IdCitas") %> </td>
                                    <td style="width:25%"> <%# Eval("NombreCliente") %> </td>
                                    <td style="width:10%"> <%# Eval("FechaCita") %> </td>
                                    <td style="width:10%"> <%# Eval("Hora") %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                    <td style="width:25%"> <%# Eval("Empleado") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

             <div align="center">
                <asp:Label ID="lbPaginaActualTituloCitaEncabezado" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariableCitaEncabezado" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloCitaEncabezado" runat="server" Text="De " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariableCitaEncabezado" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>

             <div id="divPaginacionCitasEncabezado" runat="server" align="center" >
                <div style="margin-top=20px;">
                    <table style="width:600px;">
                        <tr>
                            <td> <asp:LinkButton ID="LinkPrimeraPaginaCitasEncabezado" runat="server" Text="Primero" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaCitasEncabezado_Click" CssClass="btn btn-outline-success btn-sm"  ></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkPaginaAnteriorCitasEncabezado" runat="server" Text="Anterior" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkPaginaAnteriorCitasEncabezado_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td>
                                <asp:DataList ID="dlPaginacionCitasEncabezado" runat="server" OnCancelCommand="dlPaginacionCitasEncabezado_CancelCommand" OnItemDataBound="dlPaginacionCitasEncabezado_ItemDataBound" RepeatDirection="Horizontal" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkIndicePaginacionCitasEncabezado" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="NuevaPagina" Text='<%# Eval("TextoPagina")%>' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>

                            </td>
                            <td> <asp:LinkButton ID="LinkPaginaSiguienteCitasEncabezado" runat="server" Text="Siguiente" ToolTip="Ir la Siguiente pagina del listado" OnClick="LinkPaginaSiguienteCitasEncabezado_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkUltipaPaginaCitasEncabezado" runat="server" Text="Ultmo" ToolTip="Ir a la Ultima Pagina del listado" OnClick="LinkUltipaPaginaCitasEncabezado_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                           
                        </tr>
                    </table>
                </div>
            </div>
            <br />

            <div id="DivBloqueDetalleCita" runat="server">
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <asp:Label ID="lbNumeroCitaDetalle" runat="server" Text="Numero de Cita" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtNumeroCitaDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbEmpleadoDetalle" runat="server" Text="Tecnico" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtEmpleadoDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbClienteDetalle" runat="server" Text="Cliente" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtClienteDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbNumeroIdentificacionDetalle" runat="server" Text="Numero de Identificación" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtNumeroIdentificacionDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbFechaCitaDetalle" runat="server" Text="Fecha de Cita" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtFechaCitaDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbHoraCitaDetalle" runat="server" Text="Hora de Cita" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtHoraCitaDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbTelefonoDetalle" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtTelefonoDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <asp:Label ID="lbEstatusDetalle" runat="server" Text="Estatus de Cita" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtEstatusDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">

                    </div>

                    <div class="form-group col-md-12">
                        <asp:Label ID="lbDireccionClienteDetalle" runat="server" Text="Dirección de Cliente" CssClass="Letranegrita"></asp:Label>
                        <asp:TextBox ID="txtDireccionClienteDetalle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <hr />
                <div align="center">
                    <asp:label ID="lbCantidadServiciosTitulo" runat="server" Text="Cantidad de Servicios ( " CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbCantidadServiciosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbCantidadServiciosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbEspacio" runat="server" Text="   " CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbTotalMontoTitulo" runat="server" Text="Total ( " CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbTotalMontoVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:label>
                    <asp:label ID="lbTotalMontoCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:label>
                </div>
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th style="width:80%" align="left"> <asp:Label ID="lbDescripcionHeaderDetalle" runat="server" CssClass="Letranegrita" Text="Servicio"></asp:Label> </th>
                                 <th style="width:20%" align="left"> <asp:Label ID="lbPrecioHeaderDetalle" runat="server" CssClass="Letranegrita" Text="Precio"></asp:Label> </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rpListadoCitaDetalle" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width:80%"> <%# Eval("DescripcionProducto") %> </td>
                                        <td style="width:20%"> <%#string.Format("{0:n2}", Eval("Precio")) %> </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                  <div align="center">
                <asp:Label ID="lbPaginaActualTituloCitaDetalle" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariableCitaDetalle" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloCitaDetalle" runat="server" Text="De " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariableCitaDetalle" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>

             <div id="DivpaginacionCitaDetalle" runat="server" align="center" >
                <div style="margin-top=20px;">
                    <table style="width:600px;">
                        <tr>
                            <td> <asp:LinkButton ID="LinkPrimeroCitaDetalle" runat="server" Text="Primero" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeroCitaDetalle_Click" CssClass="btn btn-outline-success btn-sm"  ></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkAnteriorCitaDetalle" runat="server" Text="Anterior" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorCitaDetalle_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td>
                                <asp:DataList ID="dlPaginacionCitaDetalle" runat="server" OnCancelCommand="dlPaginacionCitaDetalle_CancelCommand" OnItemDataBound="dlPaginacionCitaDetalle_ItemDataBound" RepeatDirection="Horizontal" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkIndicePaginacionCitasDetalle" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="NuevaPagina" Text='<%# Eval("TextoPagina")%>' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>

                            </td>
                            <td> <asp:LinkButton ID="LinkSiguienteCitaDetalle" runat="server" Text="Siguiente" ToolTip="Ir la Siguiente pagina del listado" OnClick="LinkSiguienteCitaDetalle_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                            <td> <asp:LinkButton ID="LinkUltimoSiguienteDetalle" runat="server" Text="Ultmo" ToolTip="Ir a la Ultima Pagina del listado" OnClick="LinkUltimoSiguienteDetalle_Click" CssClass="btn btn-outline-success btn-sm"></asp:LinkButton> </td>
                           
                        </tr>
                    </table>
                </div>
            </div>
            <br />

            </div>




             <div align="center">
            <div id="divGraficosEstatusCitas" runat="server"  >

             <asp:Label ID="lbGraficoEstatusCitas" runat="server"  Text="Estatus de Citas" CssClass="Letranegrita"></asp:Label>
             <br />
            <asp:Chart ID="GraEstatusCitas" Width="800px" runat="server" Palette="Excel">
           <Series>
               <asp:Series Name="Serie" XValueMember="1" YValueMembers="2" IsValueShownAsLabel="true" ChartType="Pie"></asp:Series>
              
           </Series>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Default" LegendStyle="Row"></asp:Legend>
                </Legends>
           <ChartAreas>
               <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
              
           </ChartAreas>
                <BorderSkin SkinStyle="Emboss" />
       </asp:Chart>
        </div>
       </div>
            <br />
        </div>

    </div>
</asp:Content>
