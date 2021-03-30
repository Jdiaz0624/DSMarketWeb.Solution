<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="PorcientoRetenciones.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.PorcientoRetenciones" %>
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
            alert("Registro modificado con exito.");
        }
        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                var Retencion = $("#<%=ddlSeleccionarRetencionMantenimiento.ClientID%>").val();
                if (Retencion < 1) {
                    alert("El campo Retención no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarRetencionMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var MontoInicial = $("#<%=txtMontoInicialMantenimiento.ClientID%>").val().length;
                    if (MontoInicial < 1) {
                        alert("El campo monto inicial no puede estar vacio para guardar este registro, en caso de no usar este campo colocar un 0 como parametro.");
                        $("#<%=txtMontoInicialMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var MontoFinal = $("#<%=txtMontoFinalMantenimiento.ClientID%>").val().length;
                        if (MontoFinal < 1) {
                            alert("El campo Monto Final no puede estar vacio para guardar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                            $("#<%=txtMontoFinalMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var MontoSuma = $("#<%=txtMontoSumarMantenimiento.ClientID%>").val().length;
                            if (MontoSuma < 1) {
                                alert("El campo Monto Suma no pued estar vacio para guardar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                $("#<%=txtMontoSumarMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var PorcientoCia = $("#<%=txtPorcientoCia.ClientID%>").val().length;
                                if (PorcientoCia < 1) {
                                    alert("El campo % Cia no puede estar vacio para guardar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                    $("#<%=txtPorcientoCia.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var PorcientoEmpleado = $("#<%=txtPorcientoEmpleado.ClientID%>").val().length;
                                    if (PorcientoEmpleado < 1) {
                                        alert("El campo % Empleado no puede estar vacio para guardar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                        $("#<%=txtPorcientoEmpleado.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var Retencion = $("#<%=ddlSeleccionarRetencionMantenimiento.ClientID%>").val();
                if (Retencion < 1) {
                    alert("El campo Retención no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarRetencionMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var MontoInicial = $("#<%=txtMontoInicialMantenimiento.ClientID%>").val().length;
                    if (MontoInicial < 1) {
                        alert("El campo monto inicial no puede estar vacio para modificar este registro, en caso de no usar este campo colocar un 0 como parametro.");
                        $("#<%=txtMontoInicialMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var MontoFinal = $("#<%=txtMontoFinalMantenimiento.ClientID%>").val().length;
                        if (MontoFinal < 1) {
                            alert("El campo Monto Final no puede estar vacio para modificar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                            $("#<%=txtMontoFinalMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var MontoSuma = $("#<%=txtMontoSumarMantenimiento.ClientID%>").val().length;
                            if (MontoSuma < 1) {
                                alert("El campo Monto Suma no pued estar vacio para modificar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                $("#<%=txtMontoSumarMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                            else {
                                var PorcientoCia = $("#<%=txtPorcientoCia.ClientID%>").val().length;
                                if (PorcientoCia < 1) {
                                    alert("El campo % Cia no puede estar vacio para modificar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                    $("#<%=txtPorcientoCia.ClientID%>").css("border-color", "red");
                                    return false;
                                }
                                else {
                                    var PorcientoEmpleado = $("#<%=txtPorcientoEmpleado.ClientID%>").val().length;
                                    if (PorcientoEmpleado < 1) {
                                        alert("El campo % Empleado no puede estar vacio para modificar este registro, en caso de no usarlo favor de colocar un 0 como parametro.");
                                        $("#<%=txtPorcientoEmpleado.ClientID%>").css("border-color", "red");
                                        return false;
                                    }
                                    else {
                                        var ClaveSeguridad = $("#<%=txtClaveseguridadMantenimiento.ClientID%>").val().length;
                                        if (ClaveSeguridad < 1) {
                                            alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar.");
                                            $("#<%=txtClaveseguridadMantenimiento.ClientID%>").css("border-color", "red");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        })
    </script>

    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <br /><br />
           

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarRetencionConsulta" runat="server" Text="Seleccionar Retención" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarRetencionConsulta" runat="server" ToolTip="Seleccionar Retención para el filtro" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <br />
            <div align="center">
                <asp:Button ID="btnConsularRegistros" runat="server" Text="Consultar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Consultar Registros" OnClick="btnConsularRegistros_Click" />
                <asp:Button ID="btnNuevoregistro" runat="server" Text="Nuevo" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Nuevo Registro" OnClick="btnNuevoregistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar Registro" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Restablecer Pantalla" OnClick="btnRestablecer_Click" />
                <br /><br />
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>
            <br />
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="center"> <asp:Label ID="lbSeleccionarHeader" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:50%" align="center"> <asp:Label ID="lbRetencionHeader" runat="server" Text="Retención" CssClass="Letranegrita"></asp:Label>  </th>
                            <th style="width:10%" align="center"> <asp:Label ID="lbMontoinicialHEader" runat="server" Text="Monto Inicial" CssClass="Letranegrita"></asp:Label>  </th>
                            <th style="width:10%" align="center"> <asp:Label ID="lbMontoFinalHEader" runat="server" Text="Monto Final" CssClass="Letranegrita"></asp:Label>  </th>
                            
                            <th style="width:10%" align="center"> <asp:Label ID="lbPorcientoEmpleadoHeader" runat="server" Text="% Empleado" CssClass="Letranegrita"></asp:Label>  </th>
                            <th style="width:10%" align="center"> <asp:Label ID="lbEstatusHeader" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label>  </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoPorcientoRetencion" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdPorcientoRetencion" runat="server" Value='<%# Eval("IdPorcientoRetencion") %>' />
                                    <asp:HiddenField ID="hfIdRetencion" runat="server" Value='<%# Eval("IdRetencion") %>' />
                                    <asp:HiddenField ID="hfSecuencia" runat="server" Value='<%# Eval("Secuencia") %>' />

                                    <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar registro" OnClick="btnSeleccionarRegistro_Click" /> </td>
                                    <td style="width:50%"> <%# Eval("Retencion") %> </td>
                                    <td style="width:10%"> <%#string.Format("{0:n2}", Eval("MontoInicial")) %> </td>
                                    <td style="width:10%"> <%#string.Format("{0:n2}", Eval("MontoFinal")) %> </td>
                                    <td style="width:10%"> <%#string.Format("{0:n2}", Eval("PorcientoEmpleado")) %> </td>
                                    <td style="width:10%"> <%# Eval("Estatus") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

              <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text="De " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>

             <div id="divPaginacionPorcientoRetencion" runat="server" align="center" >
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
            <br />
        </div>

        <div id="DivBloqueMantenimiento" runat="server">
             <br /><br />
             <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>
                <asp:Label ID="lbSecuenciaSeleccionada" runat="server" Text="SecuenciaSeleccionada" Visible="false"></asp:Label>
           
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbSeleccionarretencionMantenimiento" runat="server" Text="Seleccionar Retencion" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarRetencionMantenimiento" runat="server" ToolTip="Seleccionar Retencion" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoinicialMantenimiento" runat="server" Text="Monto Inicial" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoInicialMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoFinalMantenimiento" runat="server" Text="Monto Final" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoFinalMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbMontoSumarMantenimientoMantenimiento" runat="server" Text="Monto Sumar" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtMontoSumarMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbPorcientoComicionCia" runat="server" Text="% Cia" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoCia" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbPorcientoEmpleado" runat="server" Text="% Empleado" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtPorcientoEmpleado" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveseguridadMantenimiento" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" ToolTip="Estatus de Porciento de Retención" CssClass="form-check-input"></asp:CheckBox>
                </div>
            </div>
            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Guardar Registro" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Modificar Registro" OnClick="btnModificar_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Volver Atras" OnClick="btnVolver_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
