<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MantenimientoModelos.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Inventario.MantenimientoModelos" %>
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
            alert("Registro Modificado con exito.");
        }

        function ClaveSeguridadNoValida() {
            alert("La clave de seguridad ingresada no es valida, favor de verificar.");
        }

        $(document).ready(function () {
            $("#<%=btnGuardarMantenimiento.ClientID%>").click(function () {
                var TipoProductoGuardar = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                if (TipoProductoGuardar < 1) {
                    alert("El campo tipo de produto no puede estar vacio para guardar este registro, favor de verificar.");
                    $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;

                }
                else {
                    CategoriaGuardar = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (CategoriaGuardar < 1) {
                        alert("El campo categoria no puede estar vacio para guardar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        MarcaGuardar = $("#<%=ddlSeleccionarMarcasMantenimiento.ClientID%>").val();
                        if (MarcaGuardar < 1) {
                            alert("El campo marca no puede estar vacio para guardar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarMarcasMantenimiento.SelectedValue%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            ModeloGuadar = $("#<%=txtModeloMantenimiento.ClientID%>").val().length;
                            if(ModeloGuadar < 1){
                                alert("El campo modelo no puede estar vacio para guardar este registro, favor de verificar.");
                                $("#<%=txtModeloMantenimiento.ClientID%>").css("border-color", "red");
                                return false;
                            }
                        }
                    }
                }
            });

            $("#<%=btnModificarMantenimiento.ClientID%>").click(function () {
                var TipoProductoGuardar = $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").val();
                 if (TipoProductoGuardar < 1) {
                     alert("El campo tipo de produto no puede estar vacio para guardar este registro, favor de verificar.");
                     $("#<%=ddlSeleccionarTipoProductoMantenimiento.ClientID%>").css("border-color", "red");
                    return false;

                }
                else {
                    CategoriaGuardar = $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").val();
                    if (CategoriaGuardar < 1) {
                        alert("El campo categoria no puede estar vacio para modificar este registro, favor de verificar.");
                        $("#<%=ddlSeleccionarCategoriaMantenimiento.ClientID%>").css("border-color", "red");
                        return false;
                    }
                    else {
                        MarcaGuardar = $("#<%=ddlSeleccionarMarcasMantenimiento.ClientID%>").val();
                        if (MarcaGuardar < 1) {
                            alert("El campo marca no puede estar vacio para modificar este registro, favor de verificar.");
                            $("#<%=ddlSeleccionarMarcasMantenimiento.SelectedValue%>").css("border-color", "red");
                            return false;
                        }
                        else {
                            ModeloGuadar = $("#<%=txtModeloMantenimiento.ClientID%>").val().length;
                            if (ModeloGuadar < 1) {
                                alert("El campo modelo no puede estar vacio para modificar este registro, favor de verificar.");
                                $("#<%=txtModeloMantenimiento.ClientID%>").css("border-color", "red");
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
                 }
             });


        })
    </script>

    <div class="container-fluid">
<div id="idBloqueConsulta" runat="server">
            <div class="jumbotron" align="center">
            <asp:Label ID="lbTituloConsulta" runat="server" Text="CONSULTA DE MODELOS"></asp:Label>
        </div>

        <div class="form-row">

            <div class="form-group col-md-3">
            <asp:Label ID="lbSeleccionarTipoProductoConsulta" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label>
             <asp:DropDownList ID="ddlSeleccionarTipoProductoConsulta" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSeleccionarTipoProductoConsulta_SelectedIndexChanged" ToolTip="Seleccionar el Tipo de Producto"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarCategoriaConsulta" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarCategoriaConsulta" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSeleccionarCategoriaConsulta_SelectedIndexChanged" ToolTip="Seleccionar Categorias"></asp:DropDownList>
            </div>

            <div class="form-group col-md-3">
                <asp:Label ID="lbSeleccionarMarcaConsulta" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label>
                <asp:DropDownList ID="ddlSeleccionarMarcaConsulta" runat="server" ToolTip="Seleccionar Marcas" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group col-md-3">
                <asp:Label ID="lbModeloConsulta" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                <asp:TextBox ID="txtModeloConsulta" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <br />
        <div align="center">
            <asp:Button ID="btnConsultarRegistros" runat="server" Text="Consultar" ToolTip="Consultar Registros" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnConsultarRegistros_Click" />
            <asp:Button ID="btnCrearNuevoRegistro" runat="server" Text="Nuevo" ToolTip="Crear Nuevo Registro" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnCrearNuevoRegistro_Click" />
            <asp:Button ID="btnMoificarRegistro" runat="server" Text="Moificar" ToolTip="Modificar Registro seleccionado" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnMoificarRegistro_Click" />
             <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" ToolTip="Restablecer Pantalla" CssClass="btn btn-outline-secondary btn-sm Custom" OnClick="btnRestablecer_Click" />
             <br />
            <asp:Label ID="lbCantidadRegistrosTitulo" runat="server" Text="Cantidad de registros (" CssClass="Letranegrita"></asp:Label>
             <asp:Label ID="lbCantidadRegistrosVariable" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
             <asp:Label ID="lbCantidadRegistrosCerrar" runat="server" Text=")" CssClass="Letranegrita"></asp:Label>
        </div>
       <br />
    <!--REPEATER-->
           <div class="table-responsive mT20">
               <table class="table table-striped table-hover">
                   <thead>
                       <tr>
                           <th style="width:10%" align="left" > <asp:Label ID="lbSeleccionarHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                           <th style="width:15%" align="left" > <asp:Label ID="lbTipoProductoheaderretaper" runat="server" Text="Tipo de Producto" CssClass="Letranegrita"></asp:Label> </th>
                           <th style="width:15%" align="left" > <asp:Label ID="lbCategoriaHeaderRepeater" runat="server" Text="Categoria" CssClass="Letranegrita"></asp:Label> </th>
                           <th style="width:15%" align="left" > <asp:Label ID="lbMarcaHeaderRepeater" runat="server" Text="Marca" CssClass="Letranegrita"></asp:Label> </th>
                           <th style="width:35%" align="left" > <asp:Label ID="lbModeloHeaderRepeater" runat="server" Text="Modeo" CssClass="Letranegrita"></asp:Label> </th>
                           <th style="width:10%" align="left" > <asp:Label ID="lbEstatusHeaderRepeater" runat="server" Text="Estatus" CssClass="Letranegrita"></asp:Label> </th>
                       </tr>
                   </thead>
                   <tbody>
                       <asp:Repeater ID="rpListadoModelos" runat="server">
                           <ItemTemplate>
                               <tr>
                                   <asp:HiddenField ID="hfIdModeloSeleccionado" runat="server" Value='<%# Eval("IdModelo") %>' />
                                   <td style="width:10%" align="left" > <asp:Button ID="btnSeleccionarregistroRepeater" runat="server" Text="Seleccionar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccionarregistroRepeater_Click" ToolTip="Seleccionar Registro" /> </td>
                                   <td style="width:10%" align="left" > <asp:Label ID="lbTipoProductoBodyRepeater" runat="server" Text='<%# Eval("TipoPrducto") %>'></asp:Label> </td>
                                   <td style="width:10%" align="left" > <asp:Label ID="lbCategoriaBodyRepeater" runat="server" Text='<%# Eval("Categoria") %>'></asp:Label> </td>
                                   <td style="width:10%" align="left" > <asp:Label ID="lbMarcaBodyRepeater" runat="server" Text='<%# Eval("Marca") %>'></asp:Label> </td>
                                   <td style="width:10%" align="left" > <asp:Label ID="lbModeloBodyrepeater" runat="server" Text='<%# Eval("Modelo") %>'></asp:Label> </td>
                                   <td style="width:10%" align="left" > <asp:Label ID="lbEstatusBodyRepeater" runat="server" Text='<%# Eval("Estatus") %>'></asp:Label> </td>
                               </tr>
                           </ItemTemplate>
                       </asp:Repeater>
                   </tbody>
               </table>
           </div>
    <div align="center">
        <asp:Label ID="lbPaginaActualTitulo" runat="server" Text="Pagina: " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbPaginaActualVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadPaginasTitulo" runat="server" Text=" De: " CssClass="Letranegrita"></asp:Label>
        <asp:Label ID="lbCantidadPaginasVariable" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
    </div>
    <!--PAGINACION-->
    <div id="divPaginacionModelos" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPagina" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPagina_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnterior" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnterior_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionModelos" runat="server" OnItemCommand="dtPaginacionModelos_ItemCommand" OnItemDataBound="dtPaginacionModelos_ItemDataBound" RepeatDirection="Horizontal">
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
</div>

  <div id="idBloqueMantenimiento" runat="server">
                    <div class="jumbotron" align="center"> 
              <asp:Label ID="lbTituloMantenimiento" runat="server" Text="MANTENIMIENTO DE MODELOS"></asp:Label>
                        <asp:Label ID="lbIdRegistroSeleccionado" runat="server" Text="Id Seleccionado" Visible="false"></asp:Label>

          </div>
              <div class="form-row">
                  <div class="form-group col-md-4">
                      <asp:Label ID="lbSeleccionarTipoProductoMantenimiento" runat="server" Text="Seleccionar Tipo de Producto" CssClass="Letranegrita"></asp:Label>
                      <asp:DropDownList ID="ddlSeleccionarTipoProductoMantenimiento" runat="server" AutoPostBack="true" OnTextChanged="ddlSeleccionarTipoProductoMantenimiento_TextChanged" ToolTip="Seleccionar el tipo de producto" CssClass="form-control"></asp:DropDownList>
                  </div>

                  <div class="form-group col-md-4">
                      <asp:Label ID="lbSeleccionarCateoriaMantenimiento" runat="server" Text="Seleccionar Categoria" CssClass="Letranegrita"></asp:Label>
                      <asp:DropDownList ID="ddlSeleccionarCategoriaMantenimiento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeleccionarCategoriaMantenimiento_SelectedIndexChanged" ToolTip="Seleccionar Categoria" CssClass="form-control"></asp:DropDownList>
                  </div>

                  <div class="form-group col-md-4">
                      <asp:Label ID="lbMarcaMantenimiento" runat="server" Text="Seleccionar Marca" CssClass="Letranegrita"></asp:Label>
                      <asp:DropDownList ID="ddlSeleccionarMarcasMantenimiento" runat="server" ToolTip="Seleccionar Marcas" CssClass="form-control"></asp:DropDownList>
                  </div>

                  <div class="form-group col-md-4">
                      <asp:Label ID="lbModelosMantenimiento" runat="server" Text="Modelo" CssClass="Letranegrita"></asp:Label>
                      <asp:TextBox ID="txtModeloMantenimiento" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                  </div>
                  <div class="form-group col-md-4">
                      <asp:Label ID="lbClaveSeguridad" runat="server" Text="Clave de Seguridad" CssClass="Letranegrita"></asp:Label>
                      <asp:TextBox ID="txtClaveSeguridad" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                  </div>
              </div>
              <div class="form-check-inline">
                  <div class="form-group form-check">
                      <asp:CheckBox ID="cbEstatusMantenimiento" runat="server" Text="Estatus" CssClass="form-check-input" ToolTip="Estatus de Modelo" />
                  </div>
              </div>
            <br />
             <div align="center">
                  <asp:Button ID="btnGuardarMantenimiento" runat="server" Text="Guardar" ToolTip="Guardar Registro" OnClick="btnGuardarMantenimiento_Click" CssClass="btn btn-outline-secondary btn-sm Custom" />
                  <asp:Button ID="btnModificarMantenimiento" runat="server" Text="Modificar" ToolTip="Modificar Registro" OnClick="btnModificarMantenimiento_Click" CssClass="btn btn-outline-secondary btn-sm Custom" />
                 <asp:Button ID="btnVolverAtras" runat="server" Text="Volver" ToolTip="Volver Atras" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnVolverAtras_Click" />
              </div>
              <br />
        </div>

      
        
    </div>
</asp:Content>
