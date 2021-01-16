<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoSuplidores.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoSuplidores" %>
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
        function RegistroGuardado() {
            alert("Registro Guardado con exito");
        }

        function RegistroModificado() {
            alert("Registro Modificado Con exito");
        }

        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar");
        }

        $(document).ready(function () {
            $("#<%=btnGaurdar.ClientID%>").click(function () {
                var TipoSplidorGardar = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                if (TipoSplidorGardar < 1) {
                    alert("El campo tipo de suplidor no puede estar vacio para guardar este registro");
                    $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var NombreSuplidorGuardar = $("#<%=txtNombreSuplidorMantenimiento.ClientID%>").val().length;
                    if (NombreSuplidorGuardar < 1) {
                        alert("El campo nombre de suplidor no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=txtNombreSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var RNCSuplidor = $("#<%=txtRNCMantenimiento.ClientID%>").val().length;
                        if (RNCSuplidor < 1) {
                            alert("El campo RNC no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=txtRNCMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ActividadEconomica = $("#<%=txtActividadEconomica.ClientID%>").val().length;
                            if (ActividadEconomica < 1) {
                                alert("El campo Actividad Economica no puede estar vacio para guardar este registro, favor de verificar.");
                                $("#<%=txtActividadEconomica.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var TipoSplidorGardar = $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").val();
                if (TipoSplidorGardar < 1) {
                    alert("El campo tipo de suplidor no puede estar vacio para modificar este registro");
                    $("#<%=ddlSeleccionarTipoSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var NombreSuplidorGuardar = $("#<%=txtNombreSuplidorMantenimiento.ClientID%>").val().length;
                    if (NombreSuplidorGuardar < 1) {
                        alert("El campo nombre de suplidor no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=txtNombreSuplidorMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var RNCSuplidor = $("#<%=txtRNCMantenimiento.ClientID%>").val().length;
                        if (RNCSuplidor < 1) {
                            alert("El campo RNC no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=txtRNCMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ActividadEconomica = $("#<%=txtActividadEconomica.ClientID%>").val().length;
                            if (ActividadEconomica < 1) {
                                alert("El campo Actividad Economica no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=txtActividadEconomica.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var ClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                                if (ClaveSeguridad < 1) {
                                    alert("El campo Clave de seguridad no puede estar vacio para modificar este registro, favor de verificar.");
                                    $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                            }
                        }
                    }
                }
            });
        })
    </script>
<div class="container-fluid">
        <div id="DivBloqueConsulta"  runat="server">
        <div class="jumbotron" align="center">
            <asp:Label ID="lbIdSuplidor" runat="server" Text="CONSULTA DE SUPLIDORES"></asp:Label>
        </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoSulidorConsulta" runat="server" Text="Seleccionar Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoSuplidorConsulta" runat="server" ToolTip="Seleccionar Tipo de Suplidor" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreSuplidorConsulta" runat="server" Text="Nombre de Suplidor" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreSuplidorConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbRNCConsulta" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRNCConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbExportarA" runat="server" Text="Exportar A: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" CssClass="form-check-input" GroupName="Exportar" />
                <asp:RadioButton ID="rbExportarExcel" runat="server" Text="Excel" CssClass="form-check-input" GroupName="Exportar" />
                <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" CssClass="form-check-input" GroupName="Exportar" />
            </div>
        </div>
            <br />
            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear un nuevo registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistros" runat="server" Text="Modificar" ToolTip="Modificar Registro Seleccionado" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistros_Click" />
                <asp:Button ID="btnExportarRegistros" runat="server" Text="Exportar" ToolTip="Exportar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnExportarRegistros_Click"/>
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
            </div><br />
            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros (" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidaRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>

            </div>
            <!--REPEATER-->
            <div class="table-responsive mT20">
                <table class="table table-shipper table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarheaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:15%" align="left"> <asp:Label ID="lbTipoSuplidorHeaderRepeater" runat="server" Text="Tipo de Suplidor" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:25%" align="left"> <asp:Label ID="lbSuplidorHeaderRepeater" runat="server" Text="Suplidor" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbContactoHEaderRepeater" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbEmailHeaderRepeater" runat="server" Text="Actividad Economica" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbTelefonoHeaderRepeater" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoSuplidores" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdSuplidor" runat="server" Value='<%# Eval("IdSuplidor") %>' />
                                    <td style="width:10%" align="left"> <asp:Button ID="btnSeleccionarRegistroBodyRepeater" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registros" OnClick="btnSeleccionarRegistroBodyRepeater_Click" /> </td>
                                    <td style="width:15%" align="left"> <asp:Label ID="lbTipoSuplidorBodyRepeater" runat="server" Text='<%# Eval("TipoSuplidor") %>'></asp:Label> </td>
                                    <td style="width:35%" align="left"> <asp:Label ID="lbSuplidorBodyRepeater" runat="server" Text='<%# Eval("Suplidor") %>'></asp:Label> </td>
                                    <td style="width:10%" align="left"> <asp:Label ID="lbRNCBodyRepeater" runat="server" Text='<%# Eval("RNCSuplidor") %>'></asp:Label> </td>
                                    <td style="width:10%" align="left"> <asp:Label ID="lbActividadEconomicaBodyrepeater" runat="server" Text='<%# Eval("ActividadEconomica") %>'></asp:Label> </td>
                                    <td style="width:10%" align="left"> <asp:Label ID="lbTelefonoBodyRepeater" runat="server" Text='<%# Eval("Telefono") %>'></asp:Label> </td>
                                    <td style="width:10%" align="left"> <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label> </td>
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
    </div>


    <div id="DivBloqueMantenimiento" runat="server">
        <div class="jumbotron" align="center">
            <asp:Label ID="lbMAntenimientoSuplidores" runat="server" Text="MANTENIMIENTO DE SUPLIDORES"></asp:Label>
            <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>
        </div>

        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lbSeleccionarTipoSuplidorMantenimiento" runat="server" Text="Seleccionar Tipo de Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarTipoSuplidorMantenimiento" runat="server" ToolTip="Seleccionar el tipo de suplidor" CssClass="form-control"></asp:DropDownList>

            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbNombreSuplidorMantenimiento" runat="server" Text="Nombre de Suplidor" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtNombreSuplidorMantenimiento" runat="server" CssClass="form-control" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
                    <asp:Label ID="lbRNCMantenimiento" runat="server" Text="RNC" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtRNCMantenimiento" AutoCompleteType="Disabled" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

             <div class="form-group col-md-4">
                    <asp:Label ID="lbActividadEconominaMantenimiento" runat="server" Text="Actividad Economica" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtActividadEconomica" AutoCompleteType="Disabled" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbTelefonoSuplidorMantenimiento" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txttelefonoSuplidorMantenimiento" runat="server" CssClass="form-control" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lbEmailSupldorMantenimiento" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtEmailMantenimiento" runat="server" TextMode="Email" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
            </div>
             <div class="form-group col-md-4">
                <asp:Label ID="lbContactoMantenimiento" runat="server" Text="Contacto" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtCntactoMantenimento" runat="server" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
            </div>

             <div class="form-group col-md-4">
            </div>
             <div class="form-group col-md-12">
                <asp:Label ID="lbDireccionMantenimiento" runat="server" Text="Dirección" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtDirecconMantenimiento" runat="server" MaxLength="8000" TextMode="MultiLine"  AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtClaveSeguridadMantenimiento" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbEstatusSuplidor" runat="server" Text="Estatus" ToolTip="Estatus de Suplidor" CssClass="form-check-input" />
            </div>
        </div>
        <br />
        <div align="center">
            <asp:Button ID="btnGaurdar" runat="server" Text="Gaurdar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Guardar registro" OnClick="btnGaurdar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar registro" OnClick="btnModificar_Click" />
            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Volver Atras" OnClick="btnVolver_Click" />
        </div>
        <br />
    </div>

</div>
</asp:Content>
