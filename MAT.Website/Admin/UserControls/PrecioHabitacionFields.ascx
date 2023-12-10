<%@ Control Language="C#" ClassName="PrecioHabitacionFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioHabitacionId" runat="server" Text="Precio Habitacion Id:" AssociatedControlID="dataPrecioHabitacionId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPrecioHabitacionId" Value='<%# Bind("PrecioHabitacionId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipoHabitacion" runat="server" Text="Tipo Habitacion:" AssociatedControlID="dataTipoHabitacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipoHabitacion" Text='<%# Bind("TipoHabitacion") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTipoHabitacion" runat="server" Display="Dynamic" ControlToValidate="dataTipoHabitacion" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTipoHabitacion" runat="server" Display="Dynamic" ControlToValidate="dataTipoHabitacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHotelId" runat="server" Text="Hotel Id:" AssociatedControlID="dataHotelId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataHotelId" Value='<%# Bind("HotelId") %>'></asp:HiddenField>
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


