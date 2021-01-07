<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoTipoNomina.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Empresa.MantenimientoTipoNomina" %>
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

            alert("Registro guardado con exito");
        }
        function RegistroModificado() {
            alert("Registro Modificado con exito");
        }
        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardar.ClientID%>").click(function () {
                var TipoNomina = $("#<%=txtTipoNominaMantenimiento.ClientID%>").val().length;
                if (TipoNomina < 1) {
                    alert("El campo tipo de nomina no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=txtTipoNominaMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
            });

            $("#<%=btnModificar.ClientID%>").click(function () {
                var TipoNomina = $("#<%=txtTipoNominaMantenimiento.ClientID%>").val().length;
                if (TipoNomina < 1) {
                    alert("El campo tipo de nomina no puede estar vacio para modificar este registro, favor de verificar.");
                    $("#<%=txtTipoNominaMantenimiento.ClientID%>").css("border-color", "red");
                    return false;
                }
                else {
                    var ClaveSeguridad = $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").val().length;
                    if (ClaveSeguridad < 1) {
                        alert("El campo clave de seguridad no puede estar vacio para modificar este registro, favor de verficar.");
                        $("#<%=txtClaveSeguridadMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                }
             });
        });
    </script>
    <div class="container-fluid">
        <div id="DivBloqueConsulta" runat="server">
            <div class="jumbotron" align="center">
                <asp:Label ID="lbTituloCOnsulta" runat="server" Text="CONSULTA DE TIPO DE NOMINA"></asp:Label>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4">
                    <asp:Label ID="lbTipoNominaConsulta" runat="server" Text="Tipo de Nomina" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtTipoNominaConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div align="center">
                <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnConsultarRegistros_Click" />
                <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear nuevo Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnModificarRegistro" runat="server" Text="Modificar" ToolTip="Modificar Registros" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnModificarRegistro_Click" />
                <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnRestablecer_Click" />
            </div>

            <div align="center">
                <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lblbCantidadRegistrosCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
            </div>

            <div class="table-responsive mT20">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th style="width:10%" align="center" > <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                             <th style="width:80%" align="center" > <asp:Label ID="lbTipoNominaHeaderRepeater" runat="server" Text="Tipo de Nomina" CssClass="Letranegrita"></asp:Label> </th>
                             <th style="width:10%" align="center" > <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpListadoTipoNomina" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hfIdTipoNomina" runat="server" Value='<%# Eval("IdTipoNomina") %>' />
                                    <td style="width:10%"> <asp:Button ID="btnSeleccionarRegistros" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Seleccionar Registr" OnClick="btnSeleccionarRegistros_Click" /> </td>
                                     <td style="width:80%"> <%# Eval("TipoNomina") %> </td>
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

         <div id="DivMantenimiento" runat="server">
             <div class="jumbotron" align="center">
                 <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE TIPO DE NOMINA"></asp:Label>
                 <asp:Label ID="lbIdRegistroSeleciconad" runat="server" Text="IdRegistroSeleccionado" Visible="false"></asp:Label>
             </div>

             <div class="form-row">
                 <div class="form-group col-md-4">
                     <asp:Label ID="lbTipoNominaMantenimiento" runat="server" Text="Tipo de Nomina" CssClass="Letranegrita"></asp:Label>
                     <asp:TextBox ID="txtTipoNominaMantenimiento" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
                 </div>

                 <div class="form-group col-md-4">
                     <asp:Label ID="lbClaveSeguridadMAntenimiento" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                     <asp:TextBox ID="txtClaveSeguridadMantenimiento" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                 </div>
             </div>

             <div class="form-check-inline">
                 <div class="form-group form-check">
                     <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" ToolTip="Estatus de Tipo de Nomina" CssClass="form-check-input" />
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
