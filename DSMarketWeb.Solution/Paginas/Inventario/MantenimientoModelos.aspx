<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoModelos.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoModelos" %>
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
        
          .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }

        th {
            background-color: dodgerblue;
            color: white;
        }
    </style>

    <br />
    <div class="container-fluid">
        <asp:Label ID="lbIsRegistroSeleccionado" runat="server" Text="0" Visible="false"></asp:Label>
        <asp:Label ID="lbAccion" runat="server" Text="Accion" Visible="false"></asp:Label>
        <div id="DivBloqueConsulta" runat="server">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:Label ID="lbSeleccionarMarca" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionarMarca" runat="server" ToolTip="Seleccionar Marcas" CssClass="form-control"></asp:DropDownList>
                </div>

                 <div class="form-group col-md-6">
                    <asp:Label ID="lbModelo" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                    <asp:TextBox ID="txtModeloConsulta" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
            </div>
             <div align="center">
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-primary btn-sm" OnClick="btnConsultar_Click" />
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" ToolTip="Crear Nuevos Registros" CssClass="btn btn-primary btn-sm" OnClick="btnNuevo_Click" />
            <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Editar Registros" CssClass="btn btn-primary btn-sm" OnClick="btnEditar_Click" />
            <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-primary btn-sm" OnClick="btnRestablecer_Click" />
        </div>
        <br />
        <div class="table-responsive">
            <table class="table table-hover">
                <thead>
                    <tr>
                    <th align="left" style="width:10%">SELECCIONAR</th>
                    <th align="left" style="width:40%">MARCA</th>
                    <th align="left" style="width:40%">MODELO</th>
                    <th align="left" style="width:10%">ESTATUS</th>
                </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpListadoModelos" runat="server">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfIdModelo" runat="server" Value='<%# Eval("IdModelo") %>' />

                            <tr>
                                <td align="left" style="width:10%"><asp:Button ID="btnSeleccionarRegistros" runat="server" Text="Seleccionar" CssClass="btn btn-primary btn-sm" OnClick="btnSeleccionarRegistros_Click" /> </td>
                                <td align="left" style="width:40%"> <%# Eval("Marca") %> </td>
                                <td align="left" style="width:40%"> <%# Eval("Modelo") %> </td>
                                <td align="left" style="width:10%"> <%# Eval("Estatus") %> </td>
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
             <div id="divPaginacionDetalle" runat="server" align="center">
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
       
    </div>

    <div id="DivBloqueMantenimiento" runat="server">
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lbMarcaMantenmiento" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMArcaMantenimiento" runat="server" ToolTip="Seleccionar la Marca para el Modelo" CssClass="form-control"></asp:DropDownList>
            </div>

             <div class="form-group col-md-6">
                <asp:Label ID="lbModeloMantenimiento" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtModeloMantenimiento" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
            </div>
        </div>
        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" CssClass="form-check-input" />
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar Registro" CssClass="btn btn-primary btn-sm" OnClick="btnGuardar_Click"/>
            <asp:Button ID="btnVolverAtras" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-primary btn-sm" OnClick="btnVolverAtras_Click" />
        </div>
        <br />
    </div>
</asp:Content>
