<%@ Control Language="C#" ClassName="HotelFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataHotelId" runat="server" Text="Hotel Id:" AssociatedControlID="dataHotelId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataHotelId" Value='<%# Bind("HotelId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNombre" runat="server" Text="Nombre:" AssociatedControlID="dataNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNombre" Text='<%# Bind("Nombre") %>' MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataNombre" runat="server" Display="Dynamic" ControlToValidate="dataNombre" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDireccion" runat="server" Text="Direccion:" AssociatedControlID="dataDireccion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDireccion" Text='<%# Bind("Direccion") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCp" runat="server" Text="Cp:" AssociatedControlID="dataCp" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCp" Text='<%# Bind("Cp") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTelefono" runat="server" Text="Telefono:" AssociatedControlID="dataTelefono" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTelefono" Text='<%# Bind("Telefono") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEmail" runat="server" Text="Email:" AssociatedControlID="dataEmail" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEmail" Text='<%# Bind("Email") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataContacto" runat="server" Text="Contacto:" AssociatedControlID="dataContacto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataContacto" Text='<%# Bind("Contacto") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCantidadHabitaciones" runat="server" Text="Cantidad Habitaciones:" AssociatedControlID="dataCantidadHabitaciones" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCantidadHabitaciones" Text='<%# Bind("CantidadHabitaciones") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCantidadHabitaciones" runat="server" Display="Dynamic" ControlToValidate="dataCantidadHabitaciones" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCategoria" runat="server" Text="Categoria:" AssociatedControlID="dataCategoria" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCategoria" Text='<%# Bind("Categoria") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCategoria" runat="server" Display="Dynamic" ControlToValidate="dataCategoria" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataChild1" runat="server" Text="Child1:" AssociatedControlID="dataChild1" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataChild1" Text='<%# Bind("Child1") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataChild2" runat="server" Text="Child2:" AssociatedControlID="dataChild2" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataChild2" Text='<%# Bind("Child2") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataChildHabitacion" runat="server" Text="Child Habitacion:" AssociatedControlID="dataChildHabitacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataChildHabitacion" Text='<%# Bind("ChildHabitacion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataChildHabitacion" runat="server" Display="Dynamic" ControlToValidate="dataChildHabitacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCheckIn" runat="server" Text="Check In:" AssociatedControlID="dataCheckIn" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCheckIn" Text='<%# Bind("CheckIn") %>' MaxLength="8"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCheckOut" runat="server" Text="Check Out:" AssociatedControlID="dataCheckOut" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCheckOut" Text='<%# Bind("CheckOut") %>' MaxLength="8"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataGoogleMapHtml" runat="server" Text="Google Map Html:" AssociatedControlID="dataGoogleMapHtml" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataGoogleMapHtml" Text='<%# Bind("GoogleMapHtml") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLocalidadId" runat="server" Text="Localidad Id:" AssociatedControlID="dataLocalidadId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataLocalidadId" DataSourceID="LocalidadIdLocalidadDataSource" DataTextField="IdDepartamento" DataValueField="Id" SelectedValue='<%# Bind("LocalidadId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:LocalidadDataSource ID="LocalidadIdLocalidadDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


