<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="ConfiguracionesGenerales.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Configuracion.ConfiguracionesGenerales" %>
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
        function ClaveSeguridadInvalida() {
            alert("La clave de seguriad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnValidarClaveSeguridad.ClientID%>").click(function () {
                var ClaveSeguridad = $("#<%=txtClaveSeguridad.ClientID%>").val().length;
                if (ClaveSeguridad < 1) {
                    alert("El campo clave de seguridad no puede estar vacio para validar.");
                    $("#<%=txtClaveSeguridad.ClientID%>").css("border-color", "red");
                    return false;
                }

            });
            })
        
    </script>
    <div id="DivBloqueClaveSeguridad" runat="server">
        <br />


        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbFiltrarPorModulo" runat="server" Text="Filtrar por Modulo" CssClass="form-check-input Letranegrita" AutoPostBack="true" OnCheckedChanged="cbFiltrarPorModulo_CheckedChanged" />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtClaveSeguridad" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
             <div class="form-group col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Modulo" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarModulo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarModulo_SelectedIndexChanged" ToolTip="Seleccionar Modulo" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnValidarClaveSeguridad" runat="server" Text="Validar" CssClass="btn btn-primary btn-sm" OnClick="btnValidarClaveSeguridad_Click" />
        </div>
        <br />

        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th style="width:10%" align="left"> SELECCIONAR </th>
                        <th style="width:20%" align="left"> MODULO </th>
                        <th style="width:60%" align="left"> CONCEPTO </th>
                        <th style="width:10%" align="left"> ESTATUS </th>
                    </tr>
                </thead>
                <tbody>
                   <asp:Repeater ID="rpListadoConfiguraciones" runat="server">
                       <ItemTemplate>
                           <tr>
                               <asp:HiddenField ID="hfIdConfiguracionGeneral" runat="server" Value='<%# Eval("IdConfiguracion") %>' />
                               <asp:HiddenField ID="hfIdModulo" runat="server" Value='<%# Eval("IdModulo") %>' />

                               <td style="width:10%"> <asp:Button ID="btnSeleccionar" Text="Cambiar" CssClass="btn btn-primary btn-sm" OnClick="btnSeleccionar_Click" ToolTip="Seleccionar Registro" runat="server" /> </td>
                               <td style="width:20%"> <%# Eval("Modulo") %> </td>
                               <td style="width:60%"> <%# Eval("Descripcion") %> </td>
                               <td style="width:10%"> <%# Eval("Estatus") %> </td>
                           </tr>
                       </ItemTemplate>
                   </asp:Repeater>
                </tbody>
            </table>
        </div>

           <div align="center">
                <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="LinkBlbPaginaActualVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTitulo" runat="server" Text=" de " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacion" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-light btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-light btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacion" runat="server" OnItemCommand="dtPaginacion_ItemCommand" OnItemDataBound="dtPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentral" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguiente" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkSiguiente_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimo" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-light btn-sm" OnClick="LinkUltimo_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
        <br />
    </div>
</asp:Content>
