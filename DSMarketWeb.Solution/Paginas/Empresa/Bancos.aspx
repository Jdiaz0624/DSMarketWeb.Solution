<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Bancos.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.Bancos" %>
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
            alert("Registro modificado con exito.");
        }
        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                var NombreBanco = $("#<%=txtNombreBancoMantenimiento.ClientID%>").val().length;
                if (NombreBanco < 1) {
                    alert("El campo Nombre de Banco no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=txtNombreBancoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var CuentaBanco = $("#<%=txtCuentaContableMantenimiento.ClientID%>").val().length;
                    if (CuentaBanco < 1) {
                        alert("El campo cuenta de banco no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=txtCuentaContableMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var Auxiliar = $("#<%=txtAuxiliarMantenimiento.ClientID%>").val().length;
                        if (Auxiliar < 1) {
                            alert("El campo auxiliar no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=txtAuxiliarMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                    }
                }
            });


            $("#<%=btnModificar.ClientID%>").click(function () {
                var NombreBanco = $("#<%=txtNombreBancoMantenimiento.ClientID%>").val().length;
                if (NombreBanco < 1) {
                    alert("El campo Nombre de Banco no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtNombreBancoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var CuentaBanco = $("#<%=txtCuentaContableMantenimiento.ClientID%>").val().length;
                    if (CuentaBanco < 1) {
                        alert("El campo cuenta de banco no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=txtCuentaContableMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var Auxiliar = $("#<%=txtAuxiliarMantenimiento.ClientID%>").val().length;
                        if (Auxiliar < 1) {
                            alert("El campo auxiliar no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=txtAuxiliarMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            var ClaveSeguridad = $("#<%=txtClaveSeguridad.ClientID%>").val().length;
                            if (ClaveSeguridad < 1) {
                                alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=txtClaveSeguridad.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }
                }
            });
        })
    </script>

    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloConsulta" runat="server" Text="CONSULTA DE BANCOS"></asp:Label>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreBancoConsulta" runat="server" Text="Nombre de Banco" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreBancoConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                
            </div>
            <br />
                <div align="center">
                    <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                    <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                    <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                    <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
                </div>
                <br /><br />
                <div align="center">
                    <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
                    <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
                </div>
            <br />
            <div class="table-responsive mT20">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left"> <asp:Label ID="lbSeleccionarRegistrosHEaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:60%" align="left"> <asp:Label ID="lbBAncoHeaderRepeatr" runat="server" Text="Banco" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbCuentaContableHEaderRepeatr" runat="server" Text="Cuenta" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbAuxiliarRepeater" runat="server" Text="Auxiliar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left"> <asp:Label ID="lbEstatusRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoBancos" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdBanco" runat="server" Value='<%# Eval("IdBanco") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" ToolTip="Seleccionar Registros" OnClick="btnSeleccionar_Click" CssClass="btn btn-outline-secondary btn-sm" /> </td>
                                    <td style="width:60%"> <%# Eval("Banco") %> </td>
                                    <td style="width:10%"> <%# Eval("CuentaContable") %> </td>
                                    <td style="width:10%"> <%# Eval("Auxiliar") %> </td>
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

             <div id="divPaginacionBancos" runat="server" align="center" >
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

        <div id="DivBloqueMatenimiento" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE BANCOS" CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbNombreBancoMantenimiento" runat="server" Text="Nombre de Banco" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtNombreBancoMantenimiento" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCuentaContableMantenimiento" runat="server" Text="Cuenta de Contable" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCuentaContableMantenimiento" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbAuxiliarMantenimiento" runat="server" Text="Auxiliar" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtAuxiliarMantenimiento" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtClaveSeguridad" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-check-inline">
                <div class="form-group form-check">
                    <asp:CheckBox ID="cbEstatus" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Banco" />
                </div>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
