<%@ Control Language="C#" ClassName="PersonaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPersonaId" runat="server" Text="Persona Id:" AssociatedControlID="dataPersonaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPersonaId" Value='<%# Bind("PersonaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataApellido" runat="server" Text="Apellido:" AssociatedControlID="dataApellido" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataApellido" Text='<%# Bind("Apellido") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNombre" runat="server" Text="Nombre:" AssociatedControlID="dataNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNombre" Text='<%# Bind("Nombre") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipoDocumento" runat="server" Text="Tipo Documento:" AssociatedControlID="dataTipoDocumento" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipoDocumento" Text='<%# Bind("TipoDocumento") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataTipoDocumento" runat="server" Display="Dynamic" ControlToValidate="dataTipoDocumento" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNroDocumento" runat="server" Text="Nro Documento:" AssociatedControlID="dataNroDocumento" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroDocumento" Text='<%# Bind("NroDocumento") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCelular" runat="server" Text="Celular:" AssociatedControlID="dataCelular" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCelular" Text='<%# Bind("Celular") %>' MaxLength="50"></asp:TextBox>
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
        <td class="literal"><asp:Label ID="lbldataFechaNacimiento" runat="server" Text="Fecha Nacimiento:" AssociatedControlID="dataFechaNacimiento" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaNacimiento" Text='<%# Bind("FechaNacimiento", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaNacimiento" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLocalidadId" runat="server" Text="Localidad Id:" AssociatedControlID="dataLocalidadId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataLocalidadId" Text='<%# Bind("LocalidadId") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataLocalidadId" runat="server" Display="Dynamic" ControlToValidate="dataLocalidadId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUserId" runat="server" Text="User Id:" AssociatedControlID="dataUserId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUserId" Text='<%# Bind("UserId") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataUserId" runat="server" Display="Dynamic" ControlToValidate="dataUserId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDomicilio" runat="server" Text="Domicilio:" AssociatedControlID="dataDomicilio" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDomicilio" Text='<%# Bind("Domicilio") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSexo" runat="server" Text="Sexo:" AssociatedControlID="dataSexo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSexo" Text='<%# Bind("Sexo") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataSexo" runat="server" Display="Dynamic" ControlToValidate="dataSexo" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOcupacion" runat="server" Text="Ocupacion:" AssociatedControlID="dataOcupacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOcupacion" Text='<%# Bind("Ocupacion") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNacionalidad" runat="server" Text="Nacionalidad:" AssociatedControlID="dataNacionalidad" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNacionalidad" Text='<%# Bind("Nacionalidad") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaisResidencia" runat="server" Text="Pais Residencia:" AssociatedControlID="dataPaisResidencia" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaisResidencia" Text='<%# Bind("PaisResidencia") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataProvincia" runat="server" Text="Provincia:" AssociatedControlID="dataProvincia" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataProvincia" Text='<%# Bind("Provincia") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataProvincia" runat="server" Display="Dynamic" ControlToValidate="dataProvincia" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


