<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoCargos.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.MantenimientoCargos" %>
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
                var Departamento = $("#<%=ddlSeleccionarDepartamentoMantenimiento.ClientID%>").val();
                if (Departamento < 1) {
                    alert("El campo departamento no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarDepartamentoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var Cargo = $("#<%=txtCargoManteimiento.ClientID%>").val().length;
                    if (Cargo < 1) {
                        alert("El campo cargo no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=txtCargoManteimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var Departamento = $("#<%=ddlSeleccionarDepartamentoMantenimiento.ClientID%>").val();
                if (Departamento < 1) {
                    alert("El campo departamento no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarDepartamentoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var Cargo = $("#<%=txtCargoManteimiento.ClientID%>").val().length;
                    if (Cargo < 1) {
                        alert("El campo cargo no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=txtCargoManteimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        var ClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                        if (ClaveSeguridad < 1) {
                            alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verificar");
                            $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                            return false;
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
                    <asp:Label ID="lbSeleccionarDepartamentoConsulta" runat="server" Text="Seleccionar Departamento" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarDepartamentoConsulta" runat="server" ToolTip="Seleccionar Departamentos" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group col-md-4">
                    <asp:Label ID="lbCargoConsulta" runat="server" Text="Cargo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtCargoConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

           

            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnRestablecerPantalla" runat="server" Text="Restablecer" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecerPantalla_Click" />
            </div>

             <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistroscerrar" runat="server" Text=" )" CssClass="Letranegrita"></asp:Label>
            </div>

            <div class="table-responsive mT20">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="left" > <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:40%" align="left" > <asp:Label ID="lbDepartamentoHeaderRepeater" runat="server" Text="Departamento" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:40%" align="left" > <asp:Label ID="lbCargoHeaderRepeater" runat="server" Text="Cargo" CssClass="Letranegrita"></asp:Label> </th>
                            <th style="width:10%" align="left" > <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoCargo" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdCargo" runat="server" Value='<%# Eval("IdCargo") %>' />
                                    <td style="width:10%" > <asp:Button ID="btnSeleccionarRegistro" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionarRegistro_Click" /> </td>
                                    <td style="width:40%" > <%# Eval("Departamento") %> </td>
                                    <td style="width:40%" > <%# Eval("Cargo") %> </td>
                                    <td style="width:10%" > <%# Eval("Estatus") %> </td>
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
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text=" de " CssClass="Letranegrita"></asp:Label>
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
                     <br /><br />
                        <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Visible="false" Text="MANTENIMIENTO DE CARGOS"></asp:Label>
                

                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lbSeleccionarDepartamentomantenimiento" runat="server" Text="Seleccionar Departamento" CssClass="Letranegrita"></asp:Label>
                            <asp:DropDownList ID="ddlSeleccionarDepartamentoMantenimiento" runat="server" ToolTip="Seleccionar Departamento" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbCargoMantenimiento" runat="server" Text="Cargo" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtCargoManteimiento" runat="server" MaxLength="100" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4">
                            <asp:Label ID="lbClaveSeguridadMantenimiento" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                            <asp:TextBox ID="txtClaveSeguridadMantenimiento" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-check-inline">
                        <div class="form-group form-check">
                            <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Cargo" />
                        </div>
                    </div>

                    <div align="center">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" ToolTip="Modificar Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnVolver" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolver_Click" />
                    </div>
                    <br />
                </div>
        </div>
</asp:Content>
