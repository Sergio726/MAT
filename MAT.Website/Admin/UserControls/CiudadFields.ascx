<%@ Control Language="C#" ClassName="CiudadFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataCiudadId" runat="server" Text="Ciudad Id:" AssociatedControlID="dataCiudadId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCiudadId" Text='<%# Bind("CiudadId") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCiudadId" runat="server" Display="Dynamic" ControlToValidate="dataCiudadId" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataCiudadId" runat="server" Display="Dynamic" ControlToValidate="dataCiudadId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCiudadNombre" runat="server" Text="Ciudad Nombre:" AssociatedControlID="dataCiudadNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCiudadNombre" Text='<%# Bind("CiudadNombre") %>' MaxLength="35"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCiudadNombre" runat="server" Display="Dynamic" ControlToValidate="dataCiudadNombre" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaisCodigo" runat="server" Text="Pais Codigo:" AssociatedControlID="dataPaisCodigo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaisCodigo" Text='<%# Bind("PaisCodigo") %>' MaxLength="3"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPaisCodigo" runat="server" Display="Dynamic" ControlToValidate="dataPaisCodigo" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCiudadDistrito" runat="server" Text="Ciudad Distrito:" AssociatedControlID="dataCiudadDistrito" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCiudadDistrito" Text='<%# Bind("CiudadDistrito") %>' MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCiudadDistrito" runat="server" Display="Dynamic" ControlToValidate="dataCiudadDistrito" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCiudadPoblacion" runat="server" Text="Ciudad Poblacion:" AssociatedControlID="dataCiudadPoblacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCiudadPoblacion" Text='<%# Bind("CiudadPoblacion") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCiudadPoblacion" runat="server" Display="Dynamic" ControlToValidate="dataCiudadPoblacion" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataCiudadPoblacion" runat="server" Display="Dynamic" ControlToValidate="dataCiudadPoblacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


