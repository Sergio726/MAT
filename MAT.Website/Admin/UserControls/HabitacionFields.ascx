<%@ Control Language="C#" ClassName="HabitacionFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataHabitacionId" runat="server" Text="Habitacion Id:" AssociatedControlID="dataHabitacionId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataHabitacionId" Value='<%# Bind("HabitacionId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNroHabitacion" runat="server" Text="Nro Habitacion:" AssociatedControlID="dataNroHabitacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroHabitacion" Text='<%# Bind("NroHabitacion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataNroHabitacion" runat="server" Display="Dynamic" ControlToValidate="dataNroHabitacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipo" runat="server" Text="Tipo:" AssociatedControlID="dataTipo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipo" Text='<%# Bind("Tipo") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTipo" runat="server" Display="Dynamic" ControlToValidate="dataTipo" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTipo" runat="server" Display="Dynamic" ControlToValidate="dataTipo" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHotelId" runat="server" Text="Hotel Id:" AssociatedControlID="dataHotelId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataHotelId" DataSourceID="HotelIdHotelDataSource" DataTextField="Nombre" DataValueField="HotelId" SelectedValue='<%# Bind("HotelId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:HotelDataSource ID="HotelIdHotelDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEstado" runat="server" Text="Estado:" AssociatedControlID="dataEstado" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEstado" Text='<%# Bind("Estado") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataEstado" runat="server" Display="Dynamic" ControlToValidate="dataEstado" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataEstado" runat="server" Display="Dynamic" ControlToValidate="dataEstado" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCapacidad" runat="server" Text="Capacidad:" AssociatedControlID="dataCapacidad" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCapacidad" Text='<%# Bind("Capacidad") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCapacidad" runat="server" Display="Dynamic" ControlToValidate="dataCapacidad" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataCapacidad" runat="server" Display="Dynamic" ControlToValidate="dataCapacidad" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOcupacion" runat="server" Text="Ocupacion:" AssociatedControlID="dataOcupacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOcupacion" Text='<%# Bind("Ocupacion") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataOcupacion" runat="server" Display="Dynamic" ControlToValidate="dataOcupacion" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataOcupacion" runat="server" Display="Dynamic" ControlToValidate="dataOcupacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNombre" runat="server" Text="Nombre:" AssociatedControlID="dataNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNombre" Text='<%# Bind("Nombre") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


