<%@ Control Language="C#" ClassName="PrecioServicioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioServicioId" runat="server" Text="Precio Servicio Id:" AssociatedControlID="dataPrecioServicioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPrecioServicioId" Value='<%# Bind("PrecioServicioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataServicioId" runat="server" Text="Servicio Id:" AssociatedControlID="dataServicioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataServicioId" Value='<%# Bind("ServicioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaRegistro" runat="server" Text="Fecha Registro:" AssociatedControlID="dataFechaRegistro" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaRegistro" Text='<%# Bind("FechaRegistro", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaRegistro" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataFechaRegistro" runat="server" Display="Dynamic" ControlToValidate="dataFechaRegistro" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataActivo" runat="server" Text="Activo:" AssociatedControlID="dataActivo" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataActivo" SelectedValue='<%# Bind("Activo") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecio" runat="server" Text="Precio:" AssociatedControlID="dataPrecio" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecio" Text='<%# Bind("Precio") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPrecio" runat="server" Display="Dynamic" ControlToValidate="dataPrecio" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataPrecio" runat="server" Display="Dynamic" ControlToValidate="dataPrecio" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


