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

    <script type="text/javascript">
        function RegistroGuardado() {
            alert("Registro guardado con exito.");
        }
        function RegistroModificado() {
            alert("Registro modificado con exito");
        }
        function RegistroEliminado() {
            alert("Registro eliminado con exito");
        }
        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                var ValidarNombreCliente = $("#<%=txtNombreClienteMantenimiento.ClientID%>").val().length;
                if (ValidarNombreCliente < 1) {
                    alert("El campo nombre de cliente no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=txtNombreClienteMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ValidarTipoComrobante = $("#<%=ddlSeleccionarComprobanteMantenimiento.ClientID%>").val();
                    if (ValidarTipoComrobante < 1) {
                        alert("El campo Tipo de comprobante no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarComprobanteMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var ValidarTipoRNC = $("#<%=ddlSeleccionarTipoRNC.ClientID%>").val();
                        if (ValidarTipoRNC < 1) {
                            alert("El campo tipo de rnc no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarTipoRNC.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var NumeroIdentificacion = $("#<%=txtNumeroIdentificacionMantenimiento.ClientID%>").val().length;
                            if (NumeroIdentificacion < 1) {
                                alert("El campo numero de identificación no puede estar vacio para guardar este registro, favor de verificar.");
                                $("#<%=txtNumeroIdentificacionMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var ValidarLimiteCredito = $("#<%=txtLimiteCredito.ClientID%>").val().length;
                                if (ValidarLimiteCredito < 1) {
                                    alert("El campo limite de credito no puede estar vacio para guardar este registro, favor de verificar.");
                                    $("#<%=txtLimiteCredito.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                            }
                        }
                    }
                   
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var ValidarNombreEditar = $("#<%=txtNombreClienteMantenimiento.ClientID%>").val().length;
                if (ValidarNombreEditar < 1) {
                    alert("El campo nombre de cliente no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtNombreClienteMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ValidarTipoComprobanteEditar = $("#<%=ddlSeleccionarComprobanteMantenimiento.ClientID%>").val();
                    if (ValidarTipoComprobanteEditar < 1) {
                        alert("El campo tipo de comprobante no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarComprobanteMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var ValidarTipoRNCEditar = $("#<%=ddlSeleccionarTipoRNC.ClientIDMode%>").val();
                        if (ValidarTipoRNCEditar < 1) {
                            alert("El campo tipo de rnc no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarTipoRNC.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var NumeroIdentificacionEditar = $("#<%=txtNumeroIdentificacionMantenimiento.ClientID%>").val().length;
                            if (NumeroIdentificacionEditar < 1) {
                                alert("El campo numero de identificación no puede estar vacia para modificar este registro, favor de verificar.");
                                $("#<%=txtNumeroIdentificacionMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var LimiteCredito = $("#<%=txtLimiteCredito.ClientID%>").val().length;
                                if (LimiteCredito < 1) {
                                    alert("El campo limite de credito no puede esta vacio para modificar este registro, favor de verificar.");
                                    $("#<%=txtLimiteCredito.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var ValidarClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                                    if (ValidarClaveSeguridad < 1) {
                                        alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de seguridad.");
                                        $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            });

            $("#<%=btnEliminar.ClientID%>").click(function () {
                var ClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                if (ClaveSeguridad < 1) {
                    alert("El campo clave de seguridad no puede estar vacio para eliminar este registro, favor de verificar.");
                    $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

       
        })
    </script>

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
                    <asp:Label ID="lbExportarRegistroA" runat="server" Text="Exportar A: " CssClass="Letranegrita"></asp:Label>
                    <asp:RadioButton ID="rbExportarPDF" runat="server" Text="PDF" ToolTip="Exportar Reporte a Exel" CssClass="form-check-input Letranegrita " GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarExel" runat="server" Text="Excel" ToolTip="Exportar Reporte a Excel" CssClass="form-check-input Letranegrita " GroupName="Exportar" />
                    <asp:RadioButton ID="rbExportarWord" runat="server" Text="Word" ToolTip="Exportar Reporte a Word" CssClass="form-check-input Letranegrita " GroupName="Exportar" />
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
            <br />
            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div><br />

            <div class="table-responsive mT20">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarHeaderrepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left">  <asp:Label ID="lbCodigoClienteHeaderRepeater" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label></th>
                            <th style="width:30%" align="left"> <asp:Label ID="lbNombreClienteHeaderRepeater" runat="server" Text="Nombre" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbNCFHeaderRepeater" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbTelefonoHeaderRepeater" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:20%" align="left"> <asp:Label ID="lbContactoHeaderRepeater" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEmailHeaderRepeater" runat="server" Text="Credito" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoClientes" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdclientes" runat="server" Value='<%# Eval("IdCliente") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistros" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionarRegistros_Click" ToolTip="Seleccionar Registro" /> </td>
                                    <td style="width:10%"> <%# Eval("IdCliente") %> </td>
                                    <td style="width:30%"> <%# Eval("Nombre") %> </td>
                                    <td style="width:10%"> <%# Eval("Comprobante") %> </td>
                                    <td style="width:10%"> <%# Eval("Telefono") %> </td>
                                    <td style="width:20%"> <%# Eval("Email") %> </td>
                                    <td style="width:10%"> <%# string.Format("{0:n2}", Eval("MontoCredito")) %> </td>
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
                <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE CLIENTE"></asp:Label>
                <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="Id Registro Seleccionado" Visible="false"></asp:Label>
                <asp:Label ID="lbRegistroUnico" runat="server" Text="0" Visible="false"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreClienteMantenimiento" runat="server" Text="Nombre de Cliente *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreClienteMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="500" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarComprobanteMAntenimiento" runat="server" Text="Seleccionar Comprobante *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarComprobanteMantenimiento" runat="server" ToolTip="Seleccionar Comprobante" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarTipoRNCMantenimiento" runat="server" Text="Seleccionar Tipo de RNC *" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarTipoRNC" runat="server" ToolTip="Seleccionar Tipo de RNC" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbNumeroIdentificacionMAntenimiento" runat="server" Text="Numero de Identificación *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNumeroIdentificacionMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbTelefonoMantenimiento" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTelefonoMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbEmailMAntenimiento" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtEmailMAntenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbLimiteCreditoMantenimiento" runat="server" Text="Limite de Credito *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtLimiteCredito" runat="server" AutoCompleteType="Disabled" TextMode="Number" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-8">
                    <asp:Label ID="lbComentarioMantenimiento" runat="server" Text="Comentario" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtComentarioMantenimiento" runat="server" AutoCompleteType="Disabled" MaxLength="8000" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-12">
                    <asp:Label ID="lbDireccionMantenimiento" runat="server" Text="Dirección" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtDireccionMAntenimiento" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" MaxLength="8000" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridadMAntenimiento" runat="server" Text="Clave de Seguridad *" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridadMantenimiento" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" ToolTip="Estatus de Cliente" CssClass="form-check-input" />
                     <asp:CheckBox ID="cbEnvioEmail" runat="server" Text="Envio de Mail" ToolTip="Enviar Correo a este cliente" CssClass="form-check-input" />
                </div>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" ToolTip="Eliminar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnRestablecer" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecer_Click" />
            </div>
            <br />
        </div>
        </div>
</asp:Content>
