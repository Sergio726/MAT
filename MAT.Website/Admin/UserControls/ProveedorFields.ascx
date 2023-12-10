<%@ Control Language="C#" ClassName="ProveedorFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataProveedorId" runat="server" Text="Proveedor Id:" AssociatedControlID="dataProveedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataProveedorId" DataSourceID="ProveedorIdPersonaDataSource" DataTextField="Apellido" DataValueField="PersonaId" SelectedValue='<%# Bind("ProveedorId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:PersonaDataSource ID="ProveedorIdPersonaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataRazonSocial" runat="server" Text="Razon Social:" AssociatedControlID="dataRazonSocial" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataRazonSocial" Text='<%# Bind("RazonSocial") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTelefono" runat="server" Text="Telefono:" AssociatedControlID="dataTelefono" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTelefono" Text='<%# Bind("Telefono") %>' MaxLength="50"></asp:TextBox>
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
        <td class="literal"><asp:Label ID="lbldataEmail" runat="server" Text="Email:" AssociatedControlID="dataEmail" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEmail" Text='<%# Bind("Email") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIdioma" runat="server" Text="Idioma:" AssociatedControlID="dataIdioma" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIdioma" Text='<%# Bind("Idioma") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCondicionIva" runat="server" Text="Condicion Iva:" AssociatedControlID="dataCondicionIva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCondicionIva" Text='<%# Bind("CondicionIva") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCondicionIva" runat="server" Display="Dynamic" ControlToValidate="dataCondicionIva" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCuit" runat="server" Text="Cuit:" AssociatedControlID="dataCuit" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCuit" Text='<%# Bind("Cuit") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFormaPago" runat="server" Text="Forma Pago:" AssociatedControlID="dataFormaPago" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFormaPago" Text='<%# Bind("FormaPago") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataFormaPago" runat="server" Display="Dynamic" ControlToValidate="dataFormaPago" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLocalidadId" runat="server" Text="Localidad Id:" AssociatedControlID="dataLocalidadId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataLocalidadId" Text='<%# Bind("LocalidadId") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataLocalidadId" runat="server" Display="Dynamic" ControlToValidate="dataLocalidadId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


