<%@ Control Language="C#" ClassName="ClienteFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataClienteId" runat="server" Text="Cliente Id:" AssociatedControlID="dataClienteId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataClienteId" Value='<%# Bind("ClienteId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataRazonSocial" runat="server" Text="Razon Social:" AssociatedControlID="dataRazonSocial" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataRazonSocial" Text='<%# Bind("RazonSocial") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCuit" runat="server" Text="Cuit:" AssociatedControlID="dataCuit" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCuit" Text='<%# Bind("Cuit") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMoneda" runat="server" Text="Moneda:" AssociatedControlID="dataMoneda" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMoneda" Text='<%# Bind("Moneda") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEmpresa" runat="server" Text="Empresa:" AssociatedControlID="dataEmpresa" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEmpresa" Text='<%# Bind("Empresa") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOcupacion" runat="server" Text="Ocupacion:" AssociatedControlID="dataOcupacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOcupacion" Text='<%# Bind("Ocupacion") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFormaPago" runat="server" Text="Forma Pago:" AssociatedControlID="dataFormaPago" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFormaPago" Text='<%# Bind("FormaPago") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataFormaPago" runat="server" Display="Dynamic" ControlToValidate="dataFormaPago" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCondicionIva" runat="server" Text="Condicion Iva:" AssociatedControlID="dataCondicionIva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCondicionIva" Text='<%# Bind("CondicionIva") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCondicionIva" runat="server" Display="Dynamic" ControlToValidate="dataCondicionIva" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataVendedorId" Value='<%# Bind("VendedorId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFax" runat="server" Text="Fax:" AssociatedControlID="dataFax" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFax" Text='<%# Bind("Fax") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataWeb" runat="server" Text="Web:" AssociatedControlID="dataWeb" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataWeb" Text='<%# Bind("Web") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIdioma" runat="server" Text="Idioma:" AssociatedControlID="dataIdioma" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIdioma" Text='<%# Bind("Idioma") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPromotor" runat="server" Text="Promotor:" AssociatedControlID="dataPromotor" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPromotor" Text='<%# Bind("Promotor") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataObservacion" runat="server" Text="Observacion:" AssociatedControlID="dataObservacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataObservacion" Text='<%# Bind("Observacion") %>' MaxLength="250"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipoId" runat="server" Text="Tipo Id:" AssociatedControlID="dataTipoId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipoId" Text='<%# Bind("TipoId") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTipoId" runat="server" Display="Dynamic" ControlToValidate="dataTipoId" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTipoId" runat="server" Display="Dynamic" ControlToValidate="dataTipoId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


