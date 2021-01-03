<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoEmpleado.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.MantenimientoEmpleado" %>
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
                <asp:Label ID="lbTituloConsulta" runat="server" Text="CONSULTA DE EMPLEADOS"></asp:Label>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:Label ID="lbEstatus" runat="server" Text="Estatus de Empleado" CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbTodos" runat="server" Text="Todos" CssClass="form-check-input Letranegrita" ToolTip="Buscar Todos los registros" GroupName="Estatus" />
                    <asp:RadioButton ID="rbActivos" runat="server" Text="Activos" CssClass="form-check-input Letranegrita" ToolTip="Buscar Todos los registros Activos" GroupName="Estatus" />
                    <asp:RadioButton ID="rbInactivos" runat="server" Text="Cancelados" CssClass="form-check-input Letranegrita" ToolTip="Buscar Todos los registros Inactivos" GroupName="Estatus" />
                   
                </div>
                 <br />
                <div class="form-group form-check">
                    <asp:Label ID="lbExportar" runat="server" Text="Exportar A: " CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbExportarPdf" runat="server" Text="PDF" CssClass="form-check-input Letranegrita" ToolTip="Exportar a PDF" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarExcel" runat="server" Text="Excel" CssClass="form-check-input Letranegrita" ToolTip="Exportar a Excel" GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" CssClass="form-check-input Letranegrita" ToolTip="Exportar a Word" GroupName="Exportar" />
                </div>
            </div>
            <div class="form-row">
               <div class="form-group col-md-4">
                    <asp:Label ID="lbCodigoCompleadoConsulta" runat="server" Text="Codigo de Empleado" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCodigoEmpleadoCosulta" runat="server" CssClass="form-control"></asp:TextBox>
               </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreEmpleadoConsulta" runat="server" Text="Nombre de Empleado" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNombreEmpleadoConsulta" runat="server" CssClass="form-control"></asp:TextBox>
               </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroIdentificacionConsulta" runat="server" Text="Numero de identificación" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNumeroIdentificacionConsulta" runat="server" CssClass="form-control"></asp:TextBox>
               </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbNSSConsulta" runat="server" Text="NSS" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNSSConsulta" runat="server" CssClass="form-control"></asp:TextBox>
               </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoNominaConsulta" runat="server" Text="Tipo de Nomina" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoNominaConsulta" runat="server" ToolTip="Seleccionar Tipo de Nomina" CssClass="form-control"></asp:DropDownList>
               </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbipoEmpleadoConsultaConsulta" runat="server" Text="Tipo de Empleado" CssClass="Letranegrita"></asp:Label>
                  <asp:DropDownList ID="ddlSeleccionarTipoEmpleadoCOnsulta" runat="server" ToolTip="Seleccionar Tipo de Empleado" CssClass="form-control"></asp:DropDownList>
               </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbDepartamentoConsulta" runat="server" Text="Seleccionar Departamento" CssClass="Letranegrita"></asp:Label>
                  <asp:DropDownList ID="ddlSeleccionarDepartamentoCOnsulta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarDepartamentoCOnsulta_SelectedIndexChanged" ToolTip="Seleccionar Departamento" CssClass="form-control"></asp:DropDownList>
               </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCargoConsulta" runat="server" Text="Seleccionar Cargo" CssClass="Letranegrita"></asp:Label>
                  <asp:DropDownList ID="ddlSeleccionarCargoConsulta" runat="server" ToolTip="Seleccionar Cargo" CssClass="form-control"></asp:DropDownList>
               </div>
                <div class="form-group col-md-4">
                   <asp:Image ID="IMGProducto" runat="server" onmouseover="this.width=500;this.height=500;" onmouseout="this.width=400;this.height=400;" width="300" height="300" ImageUrl="~/Recursos/ImagenPorDefecto.jpg" />
               </div>
            </div>
            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registro Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnExportarRegistro" runat="server" Text="Exportar" ToolTip="Exportar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnExportarRegistro_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
            </div>
            <br />
            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Total de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadregistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="Label1" runat="server" Text=" " CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbRegistrosActivosTitulo" runat="server" Text="Registros Activos" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbRegistrosActivosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbRegistrosActivosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="Label5" runat="server" Text=" " CssClass="Letranegrita"></asp:Label>

                <asp:Label ID="lbCantidadRegistrosInactivosTitulo" runat="server" Text="Registros Activos" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosInactivosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosInactivosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table embed-responsive mT20">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarheaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbCodigoEmpleadoHeaderRepeater" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:30%" align="left"> <asp:Label ID="lbNombreEmpleadoHeaderRepeater" runat="server" Text="Nombre" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbDepartamentoHeaderRepeater" runat="server" Text="Departamento" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbCargoHeaderRepeater" runat="server" Text="Cargo" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusHeaderreeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoEmpleado" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdEmpleado" runat="server" Value='<%# Eval("IdEmpleado") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistros" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registros" OnClick="btnSeleccionarRegistros_Click" /> </td>
                                    <td style="width:10%"> <%# Eval("IdEmpleado") %> </td>
                                    <td style="width:30%"> <%# Eval("NombreEmpleado") %> </td>
                                    <td style="width:20%"> <%# Eval("Departamento") %> </td>
                                    <td style="width:20%"> <%# Eval("Cargo") %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

             <!--PAGINACION DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavle" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacion" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacion" runat="server" OnItemCommand="dtPaginacion_ItemCommand" OnItemDataBound="dtPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentral" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguiente_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimo" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimo_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
            <br />
              <!--GRAFICO DE TIPO DE PRODUCTOS-->
       <div align="center">
            <div id="divGraficosEstatusEmpleados" runat="server"  >

             <asp:Label ID="lbGraficoEstatusEmpleado" runat="server"  Text="Estatus de Empleados" CssClass="Letranegrita"></asp:Label>
             <br />
            <asp:Chart ID="GraEstatusEmpleados" Width="800px" runat="server" Palette="Excel">
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
        </div>


        <div id="DivBloqueMantenimiento" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE EMPLEADOS"></asp:Label>
                <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbNombreMantenimiento" runat="server" Text="Nombre *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreMantenimiento" runat="server" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbApellidoMAntenimiento" runat="server" Text="Apellido *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtApellidoMAntenimiento" runat="server" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarTipoIdentificacionMantenimiento" runat="server" Text="Seleccionar Tipo de Identificacion *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoIdentificacionMantenimiento" runat="server" ToolTip="Seleccionar el tipo de identificación del empleado" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbNumeroIdentificacionMantenimiento" runat="server" Text="Numero de Identificación *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroidentificacionMantenimiento" runat="server" MaxLength="50" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarNAcionalidadMantenimiento" runat="server" Text="Seleccionar Nacionalidad *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarNacionalidadMantenimiento" runat="server" ToolTip="Seleccionar Nacionalidad" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSexoMantenimiento" runat="server" Text="Seleccionar Sexo *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarSexoMantenimiento" runat="server" ToolTip="Seleccionar Sexo" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbNSSMantenimiento" runat="server" Text="NSS" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtxNSSMantenimiento" runat="server" MaxLength="50" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbEmailMantenimiento" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtEmailMantenimiento" runat="server" MaxLength="100" TextMode="Email" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarEstadoCivilMAntenimiento" runat="server" Text="Estado Civil *" CssClass="Letranegrita">                      
                    </asp:Label>
                     <asp:DropDownList ID="ddlSeleccionarEstadiCivilMantenimiento" runat="server" ToolTip="Selccionar Estado Civil" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaIngresoMantenimiento" runat="server" Text="Fecha de Ingreso *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaIngresoMantenimiento" runat="server" TextMode="Date" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarDepartamentoMantenimiento" runat="server" Text="Seleccionar Departamento *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarDepartamentoMantenimiento" runat="server" ToolTip="Seleccionar el departamento al que pertenece" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarCargoMantenimiento" runat="server" Text="Seleccionar Cargo *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarCargoMantenimiento" runat="server" ToolTip="Seleccionar el Cargo al que pertenece" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarTipoEmpleadoMantenimiento" runat="server" Text="Seleccionar Tipo de Empleado *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoEmpleadoMantenimiento" runat="server" ToolTip="Seleccionar el Tipo de Empleado" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarTipoNominaMantenimiento" runat="server" Text="Seleccionar Tipo de Nomina *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoNominaMantenimiento" runat="server" ToolTip="Seleccionar el Tipo de nomina del empleado" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbTelefonoMantenimiento" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTelefonoMantenimiento" runat="server" TextMode="Phone" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbSegundoTelefonoMantenimiento" runat="server" Text="Segundo Telefono" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtSegundoTelefonoMantenimiento" runat="server" TextMode="Phone" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbFechaNacimientoMantenimiento" runat="server" Text="FEcha de Nacimiento *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtFechaNacimientoMantenimiento" runat="server" TextMode="Date" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                  <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarFormaPagoMantenimiento" runat="server" Text="Seleccionar Tipo de Nomina *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarFormaPagoMantenimiento" runat="server" ToolTip="Seleccionar Forma de Pago" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbSueldoMantenimiento" runat="server" Text="Sueldo *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtSueldoMantenimiento" runat="server" TextMode="Number" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbOtrosIngresosMantenimiento" runat="server" Text="Otros Ingresos" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtOtrosIngresosMantenimiento" runat="server" TextMode="Number" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-12">
                    <asp:Label ID="lbDireccionMAnteimiento" runat="server" Text="Dirección" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDireccionMAntenimiento" runat="server" TextMode="MultiLine" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbPorcientoComisionServicioMantenimiento" runat="server" Text="% de Comision por servicio *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoComisionServicioMantenimiento" runat="server" TextMode="Number" step="0.01" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group col-md-6">
                    <asp:Label ID="lbPorcientoComisionVentasMAntenimiento" runat="server" Text="% de Comision por servicio *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoComisionVentasMantenimiento" runat="server" TextMode="Number" step="0.01" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridadMantenimiento" runat="server" TextMode="Password" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" ToolTip="Estatus de Empleado" CssClass="form-check-input" />
                     <asp:CheckBox ID="cbAplicaParaComision" runat="server" Text="Aplica Para Comision" ToolTip="Aplica Para Comisión" CssClass="form-check-input" />
                    <asp:CheckBox ID="cbLlevaFoto" runat="server" Text="Lleva Foto" ToolTip="Lleva Foto" CssClass="form-check-input" />
                </div>
            </div>

             <div id="DivBloqueImagenEmpleado" runat="server">
                <div class="container" >
                    <div class="row" >
                        <div class="col-md-4 col-md-offset-4" align="center">
                            <asp:Label ID="lbTituloImagen" runat="server" Text="Foto de Empleado" CssClass="Letranegrita"></asp:Label>
                            <br />
                            <asp:Image ID="Image1" runat="server" onmouseover="this.width=500;this.height=500;" onmouseout="this.width=400;this.height=400;" width="300" height="300" ImageUrl="~/Recursos/ImagenPorDefecto.jpg" />
                            <br />
                            <br />
                            <asp:Label ID="lbBuscarImagen" runat="server" Text="Subir Imagen" CssClass="Letranegrita"></asp:Label>
                            <asp:FileUpload ID="UpImagen" runat="server" accept=".jpg" CssClass="form-control" />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>

            </div>

            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
            </div>

        </div>
    </div>
</asp:Content>
