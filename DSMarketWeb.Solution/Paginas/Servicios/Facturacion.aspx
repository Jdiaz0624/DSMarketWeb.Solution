<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="DSMarketWeb.Solution.Paginas.Servicios.Facturacion" %>
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

        .BotonEspecoal {
           width:100%;
             font-weight:bold;
          }
    </style>

    <div class="container-fluid">
        <div class="jumbotron" align="center">
            <asp:Label ID="lbTitulo" runat="server" Text="Facturación" CssClass="Letranegrita"></asp:Label>
            <asp:Label ID="lbNumeroConectorFacturacion" runat="server" Text="NumeroConector" Visible="false"></asp:Label>
        </div>

        <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbTipoProcesoFacturacion" runat="server" Text="Tipo de Proceso: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbFacturacion" runat="server" Text="Facturación" CssClass="form-check-input" GroupName="TipoProceso" ToolTip="Facturar" />
                <asp:RadioButton ID="rbCotizar" runat="server" Text="Cotización" CssClass="form-check-inpu" GroupName="TipoProceso" ToolTip="Cotizar" />
            </div>
        </div>

         <div class="form-check-inline">
            <div class="form-group form-check">
                <asp:Label ID="lbTipoVenta" runat="server" Text="Tipo de Venta: " CssClass="Letranegrita"></asp:Label>
                <asp:RadioButton ID="rbContado" runat="server" Text="Contado" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Contado" />
                <asp:RadioButton ID="rbCredito" runat="server" Text="Credito" CssClass="form-check-input" GroupName="TipoVenta" ToolTip="Realizar Venta a Credito" />
            </div>
        </div>


       <%-- <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnInformacionCobertura" data-toggle="collapse" data-target="#InformacionCliente" aria-expanded="false" aria-controls="collapseExample">
                    TITULO AQUI
                     </button><br />
       <div class="collapse" id="InformacionCliente">
                <div class="card card-body">
                 CONTROLES AQUI
                </div>
            </div>--%>
        <asp:ScriptManager ID="ScripManagerFActuracion" runat="server"></asp:ScriptManager>
         <button class="btn btn-outline-primary btn-sm BotonEspecoal" type="button" id="btnInformacionCobertura" data-toggle="collapse" data-target="#InformacionCliente" aria-expanded="false" aria-controls="collapseExample">
                    BUSCAR CLIENTES REGISTRADOS PARA FACTURAR
                     </button><br />
       <div class="collapse" id="InformacionCliente">
                <div class="card card-body">
                 <asp:UpdatePanel ID="UpdatePanelBuscarClientes" runat="server">
                     <ContentTemplate>
                         <div class="form-row">
                             <div class="form-group col-md-4">
                                 <asp:Label ID="lbCodigoClienteConsulta" runat="server" Text="Codigo de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtCodigoClienteConsulta" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-4">
                                  <asp:Label ID="lbRNCClienteConsulta" runat="server" Text="RNC de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtRNCConsultaCliente" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>

                             <div class="form-group col-md-4">
                                  <asp:Label ID="lbNombreClienteConsulta" runat="server" Text="Nombre de Cliente" CssClass="Letranegrita"></asp:Label>
                                 <asp:TextBox ID="txtNombreClienteConsulta" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                             </div>
                         </div>
                         <div align="center">
                             <asp:Button ID="btnConsultarClientes" runat="server" Text="Consultar" CssClass="btn btn-outline-secondary btn-sm" ToolTip="Consultar Registros" OnClick="btnConsultarClientes_Click" />
                             <br /><br />
                             <asp:Label ID="lbCantidadRegistrosEncontradosClienteTitulo" runat="server" Text="Cantidad de Registros ( " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosEncontradosClientesVariables" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                             <asp:Label ID="lbCantidadRegistrosEncontradosClientesCerrar" runat="server" Text=" ) " CssClass="Letranegrita"></asp:Label>
                         </div>
                         <br />

                         <div class="table-responsive">
                             <table class="table table-hover">
                                 <thead>
                                     <tr>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbSeleccionarClienteHeaderRepeater" runat="server" Text="Seleccionar" CssClass="Letranegrita"></asp:Label> </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbCodigoClienteHeaderRepeater" runat="server" Text="Codigo" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:30%" align="center"> <asp:Label ID="lbClienteClienteHeaderRepeater" runat="server" Text="Cliente" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbRNCClienteHeaderRepeater" runat="server" Text="NCF" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbTelefonoClienteHeaderRepeater" runat="server" Text="Telefono" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:20%" align="center"> <asp:Label ID="lbEmailClienteHeaderRepeater" runat="server" Text="Email" CssClass="Letranegrita"></asp:Label>  </th>
                                         <th style="width:10%" align="center"> <asp:Label ID="lbCreditoClienteHeaderRepeater" runat="server" Text="Credito" CssClass="Letranegrita"></asp:Label>  </th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <asp:Repeater ID="rpListadoClientesConsulta" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                         <asp:HiddenField ID="hfIdClienteConsulta" runat="server" Value='<%# Eval("IdCliente") %>' />

                                         <td style="width:10%"> <asp:Button ID="btnSeleccioanrCliente" runat="server" Text="Seleccionar" ToolTip="Seleccionar Cliente" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSeleccioanrCliente_Click" /> </td>
                                         <td style="width:10%"> <%# Eval("IdCliente") %> </td>
                                         <td style="width:30%"> <%# Eval("Nombre") %> </td>
                                         <td style="width:10%"> <%# Eval("Comprobante") %> </td>
                                         <td style="width:10%"> <%# Eval("Telefono") %> </td>
                                         <td style="width:20%"> <%# Eval("Email") %> </td>
                                         <td style="width:10%"> <%#string.Format("{0:n2}", Eval("MontoCredito")) %> </td>
                                     </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </tbody>
                             </table>
                         </div>
                          <!--PAGINACION DEL REPEATER-->
            <div align="center">
                <asp:Label ID="lbPaginaActualTituloClienteConsulta" runat="server" Text="Pagina " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbPaginaActualVariavleClienteConsulta" runat="server" Text=" 0 " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaTituloClienteConsulta" runat="server" Text=" DE " CssClass="Letranegrita"></asp:Label>
                <asp:Label ID="lbCantidadPaginaVAriableClienteConsulta" runat="server" Text="0" CssClass="Letranegrita"></asp:Label>
            </div>
             <div id="divPaginacion" runat="server" align="center">
        <div style="margin-top: 20px;">
            <table style="width: 600px">
                <tr>
                    <td> <asp:LinkButton ID="LinkPrimeraPaginaClienteConsulta" runat="server" Text="Primero" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la primera pagina del listado" OnClick="LinkPrimeraPaginaClienteConsulta_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkAnteriorClienteConsulta" runat="server" Text="Anterior" CssClass="btn btn-outline-success btn-sm" ToolTip="Ir a la pagina anterior del listado" OnClick="LinkAnteriorClienteConsulta_Click"></asp:LinkButton> </td>
                    <td>
                        <asp:DataList ID="dtPaginacionClienteConsulta" runat="server" OnItemCommand="dtPaginacionClienteConsulta_ItemCommand" OnItemDataBound="dtPaginacionClienteConsulta_ItemDataBound" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkPaginacionCentralClienteConsulta" runat="server" CommandArgument='<%# Eval("IndicePagina") %>' CommandName="newPage" Text='<%# Eval("TextoPagina") %>' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                    <td> <asp:LinkButton ID="LinkSiguienteClienteConsulta" runat="server" Text="Siguiente" ToolTip="Ir a la siguiente pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkSiguienteClienteConsulta_Click"></asp:LinkButton> </td>
                    <td> <asp:LinkButton ID="LinkUltimoClienteConsulta" runat="server" Text="Ultimo" ToolTip="Ir a la ultima pagina del listado" CssClass="btn btn-outline-success btn-sm" OnClick="LinkUltimoClienteConsulta_Click"></asp:LinkButton> </td>
                </tr>
            </table>
        </div>
        </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                </div>
            </div>
       <br />
    </div>
</asp:Content>
